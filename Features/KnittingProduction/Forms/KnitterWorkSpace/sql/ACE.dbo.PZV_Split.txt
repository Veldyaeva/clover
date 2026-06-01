CREATE PROCEDURE dbo.PZV_Split
  @pzvId int,
  @mode int,         -- 1 = уточнение факт, 2 = сторно/деление, 3 = предв. деление
  @qtyFact int,      -- mode 1: факт; mode 3: первая часть; mode 2: обычно 0
  @userName sysname = NULL,
  @gradacia bit = 0
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @entryTranCount int = @@TRANCOUNT;
  DECLARE @startedTran bit = 0;
  DECLARE @hadXactAbortOn bit = CASE WHEN (16384 & @@OPTIONS) = 16384 THEN 1 ELSE 0 END;
  DECLARE @rc int;
  DECLARE @now datetime = GETDATE();

  BEGIN TRY
    IF (@entryTranCount = 0)
    BEGIN
      IF (@hadXactAbortOn = 0)
        SET XACT_ABORT ON;

      BEGIN TRAN;
      SET @startedTran = 1;
    END
    ELSE
    BEGIN
      IF (@hadXactAbortOn = 1)
        SET XACT_ABORT OFF;

      SAVE TRAN PZV_Split_Save;
    END

    --------------------------------------------------------------------
    -- 1) Читаем базовую строку под блокировкой
    --------------------------------------------------------------------
    DECLARE
      @plannedNazn int,
      @qty int,
      @remaining int,
      @plannedHours decimal(18,2),
      @sek int,
      @sekNazn int,
      @remainingHours decimal(18,2),
      @dateStart datetime,
      @dateEnd datetime,
      @dateNaznTab datetime,
      @tab int;

    DECLARE @NewRows TABLE (Kind sysname NULL, Id int NOT NULL);

    DECLARE @Base TABLE
    (
      pzvID int NOT NULL,
      pzvMod nvarchar(50) NULL,
      pzvArticul nvarchar(25) NULL,
      pzvKmlID int NULL,
      pzvNomN int NULL,
      pzvVidPr char(2) NULL,
      pzvIDMlOp int NULL,
      pzvNrID int NULL,
      pzvIdBrig int NULL,
      pzvDateML datetime NULL,
      pzvDateMLUt datetime NULL,
      pzvGsID int NULL,
      pzvNomZad nvarchar(10) NULL,
      pzvAnnID int NULL,
      pzvNom int NULL,
      pzvRKol int NULL,
      pzvSek int NULL,
      pzvTab int NULL,
      pzvKwsID int NULL,
      pzvKol int NULL,
      pzvKolNazn int NULL,
      pzvNChasi decimal(6,2) NULL,
      pzvDateStart datetime NULL,
      pzvDateEnd datetime NULL,
      pzvDateNaznTab datetime NULL,
      pzvDateNaznKm datetime NULL
    );

    INSERT @Base
    (
      pzvID,pzvMod,pzvArticul,pzvKmlID,pzvNomN,pzvVidPr,pzvIDMlOp,pzvNrID,pzvIdBrig,pzvDateML,pzvDateMLUt,pzvGsID,
      pzvNomZad,pzvAnnID,pzvNom,pzvRKol,pzvKol,pzvKolNazn,pzvNChasi,pzvDateStart,pzvDateEnd,
      pzvSek,pzvTab,pzvKwsID,pzvDateNaznTab,pzvDateNaznKm
    )
    SELECT
      pzvID,pzvMod,pzvArticul,pzvKmlID,pzvNomN,pzvVidPr,pzvIDMlOp,pzvNrID,pzvIdBrig,pzvDateML,pzvDateMLUt,pzvGsID,
      pzvNomZad,pzvAnnID,pzvNom,pzvRKol,pzvKol,pzvKolNazn,pzvNChasi,pzvDateStart,pzvDateEnd,
      pzvSek,pzvTab,pzvKwsID,pzvDateNaznTab,pzvDateNaznKm
    FROM dbo.planZagrVyaz WITH (UPDLOCK, HOLDLOCK)
    WHERE pzvID = @pzvId;

    IF NOT EXISTS (SELECT 1 FROM @Base)
      THROW 50000, N'Строка pzvId не найдена', 1;

    SELECT
      @tab = pzvTab,
      @dateStart = pzvDateStart,
      @dateEnd = pzvDateEnd,
      @dateNaznTab = pzvDateNaznTab,
      @sek = ISNULL(pzvSek,0),
      @plannedHours = ISNULL(pzvNChasi,0),
      @plannedNazn = CASE WHEN ISNULL(pzvTab,0)=0 THEN ISNULL(pzvKol,0) ELSE ISNULL(pzvKolNazn,0) END,
      @qty        = CASE WHEN ISNULL(pzvTab,0)=0 THEN ISNULL(pzvKol,0) ELSE ISNULL(pzvKolNazn,0) END
    FROM @Base;

    IF (@qty IS NULL OR @qty < 0)
      THROW 50001, N'Некорректный план: pzvKolNazn/pzvKol.', 1;

    SET @remaining = @plannedNazn - ISNULL(@qtyFact,0);
    SET @plannedHours   = CAST(ROUND((ISNULL(@qtyFact,0) * ISNULL(@sek,0)) / 3600.0, 2) AS decimal(18,2));
    SET @remainingHours = CAST(ROUND((ISNULL(@remaining,0) * ISNULL(@sek,0)) / 3600.0, 2) AS decimal(18,2));
    SET @sekNazn = @sek * @qty;

    --------------------------------------------------------------------
    -- MODE 1: завершение с фактом 
    --------------------------------------------------------------------
    IF (@mode = 1)
    BEGIN
      IF (ISNULL(@tab,0) = 0)
        THROW 50002, N'Операция не назначена на табельный. Обратитесь к мастеру', 1;

      IF (@dateStart IS NULL)
        THROW 50002, N'Некорректные даты: дата начала операции не заполнена', 1;

      IF (@qtyFact IS NULL OR @qtyFact < 0 OR @qtyFact > @qty)
        THROW 50003, N'Факт должен быть в диапазоне [0..план]', 1;

      UPDATE p
      SET
        p.pzvKol      = @qtyFact,
        p.pzvKolNazn  = @qtyFact,
        p.pzvDateEnd  = ISNULL(p.pzvDateEnd, @now),
        p.pzvUpdDate  = @now,
        p.pzvNChasi   = @plannedHours,
        p.pzvChasNazn = @plannedHours
      FROM dbo.planZagrVyaz p
      WHERE p.pzvID = @pzvId
        AND ISNULL(p.pzvTab,0) > 0
        AND p.pzvDateStart IS NOT NULL
        AND p.pzvDateEnd IS NULL;

      SET @rc = @@ROWCOUNT;
      IF (@rc = 0)
        THROW 50010, N'Повторное завершение запрещено: операция уже закрыта/не начата/не назначена.', 1;

      IF (@remaining < 0)
        THROW 50004, N'Остаток меньше нуля', 1;

      IF (@remaining > 0)
      BEGIN
        INSERT dbo.planZagrVyaz
        (
          pzvDivision,
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig, pzvDateML, pzvDateMLUt, pzvGsID,
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol,
          pzvKolNazn, pzvSekNazn, pzvChasNazn,
          pzvKol, pzvSek, pzvNChasi,
          pzvTab, pzvKwsID,
          pzvDateStart, pzvDateEnd, pzvDateNaznTab, pzvDateNaznKm,
          pzvDateAdd, pzvUpdDate, pzvIDParent
        )
        OUTPUT 'Remainder', inserted.pzvID INTO @NewRows
        SELECT
          1,
          b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig, b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
          b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
          @remaining, b.pzvSek * @remaining, @remainingHours,
          @remaining, b.pzvSek, @remainingHours,
          b.pzvTab, b.pzvKwsID,
          NULL, NULL, b.pzvDateNaznTab, b.pzvDateNaznKm,
          @now, @now, @pzvId
        FROM @Base b;
      END

      INSERT INTO @NewRows(Kind, Id) VALUES (N'FinishedFact', @pzvId);

      GOTO __FINISH;
    END

    --------------------------------------------------------------------
    --  Режим 2: назначено на таб, но не выполнено 
    -- - отрицательная строка назначения 
    -- - остаток без таб 
    --------------------------------------------------------------------
    IF (@mode = 2)
    BEGIN
      -- 1) начато, но не завершено => нельзя
      IF (@dateStart IS NOT NULL AND @dateEnd IS NULL)
        THROW 50006, N'Операция начата, но не завершена. Деление/сторно запрещено.', 1;

      -- 2) конец без начала => битые даты
      IF (@dateStart IS NULL AND @dateEnd IS NOT NULL)
        THROW 50006, N'Некорректные даты: pzvDateEnd заполнена при пустой pzvDateStart.', 1;

      ----------------------------------------------------------------
      -- A) СТОРНО при tab>0:
      --    - если даты есть => берём их
      --    - если дат нет  => ставим now/now (это нужно для завершения смены)
      ----------------------------------------------------------------
      IF (ISNULL(@tab,0) > 0)
      BEGIN
        DECLARE @s datetime = @dateStart;
        DECLARE @e datetime = @dateEnd;

        IF (@s IS NULL AND @e IS NULL)
        BEGIN
          SET @s = @now;
          SET @e = @now;

          -- атомарно фиксируем даты, чтобы вторым потоком не повторить сторно
          UPDATE p
          SET p.pzvDateStart = @s,
              p.pzvDateEnd   = @e,
              p.pzvUpdDate   = @now
          FROM dbo.planZagrVyaz p
          WHERE p.pzvID = @pzvId
            AND ISNULL(p.pzvTab,0) > 0
            AND p.pzvDateStart IS NULL
            AND p.pzvDateEnd IS NULL;

          SET @rc = @@ROWCOUNT;
          IF (@rc = 0)
            THROW 50011, N'Нельзя сторнировать: операция уже изменилась (начата/закрыта/разназначена).', 1;
        END
        ELSE
        BEGIN
          -- даты уже были: просто гейт на конкурентность
          UPDATE p
          SET p.pzvUpdDate = @now
          FROM dbo.planZagrVyaz p
          WHERE p.pzvID = @pzvId
            AND ISNULL(p.pzvTab,0) > 0
            AND p.pzvDateStart IS NOT NULL
            AND p.pzvDateEnd IS NOT NULL;

          SET @rc = @@ROWCOUNT;
          IF (@rc = 0)
            THROW 50011, N'Состояние операции изменилось. Повторите действие.', 1;
        END

        -- Negative (сторно) на том же таб
        INSERT dbo.planZagrVyaz
        (
          pzvDivision,
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig,
          pzvDateNaznKm, pzvDateNaznTab, pzvDateML, pzvDateMLUt, pzvGsID,
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol,
          pzvKol, pzvSek, pzvNChasi,
          pzvKolNazn, pzvChasNazn, pzvSekNazn,
          pzvTab, pzvKwsID,
          pzvDateStart, pzvDateEnd,
          pzvDateAdd, pzvUpdDate, pzvIDParent
        )
        OUTPUT N'Negative', inserted.pzvID INTO @NewRows
        SELECT
          1,
          b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig,
          b.pzvDateNaznKm, b.pzvDateNaznTab, b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
          b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
          0-@remaining, b.pzvSek, -@remainingHours,
          @remaining, @remainingHours, b.pzvSek * @remaining,
          b.pzvTab, b.pzvKwsID,
          @s, @e,
          @now, @now, @pzvId
        FROM @Base b;

        -- Remainder без таб (остаток)
        INSERT dbo.planZagrVyaz
        (
          pzvDivision,
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig,
          pzvDateML, pzvDateMLUt, pzvGsID,
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol,
          pzvKol, pzvSek, pzvNChasi,
          pzvKolNazn, pzvSekNazn, pzvChasNazn,
          pzvTab, pzvKwsID,
          pzvDateStart, pzvDateEnd, pzvDateNaznTab, pzvDateNaznKm,
          pzvDateAdd, pzvUpdDate, pzvIDParent
        )
        OUTPUT N'Remainder', inserted.pzvID INTO @NewRows
        SELECT
          1,
          b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig,
          b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
          b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
          @remaining, b.pzvSek, @remainingHours,
          @remaining, b.pzvSek * @remaining, @remainingHours,
          0, 0,
          NULL, NULL, NULL, b.pzvDateNaznKm,
          @now, @now, @pzvId
        FROM @Base b;

        INSERT INTO @NewRows(Kind, Id) VALUES (N'UnassignWithNegative', @pzvId);

        GOTO __FINISH;
      END

      ----------------------------------------------------------------
      -- B) ПРОСТОЕ ДЕЛЕНИЕ: tab=0 и дат нет
      ----------------------------------------------------------------
      IF (@dateStart IS NULL AND @dateEnd IS NULL AND ISNULL(@tab,0) = 0)
      BEGIN
        IF (@qtyFact IS NULL OR @qtyFact <= 0)
          THROW 50007, N'Не задана первая часть', 1;

        IF (@remaining < 0)
          THROW 50008, N'Первая часть превышает плановое количество', 1;

        UPDATE p
        SET p.pzvKol = @qtyFact,
            p.pzvKolNazn = 0,
            p.pzvSekNazn = 0,
            p.pzvChasNazn = 0,
            p.pzvNChasi   = CAST(ROUND(@qtyFact * @sek / 3600.0, 2) AS decimal(18,2)),
            p.pzvUpdDate = @now
        FROM dbo.planZagrVyaz p
        WHERE p.pzvID = @pzvId
          AND p.pzvDateStart IS NULL
          AND p.pzvDateEnd IS NULL
          AND ISNULL(p.pzvTab,0) = 0;

        SET @rc = @@ROWCOUNT;
        IF (@rc = 0)
          THROW 50011, N'Состояние операции изменилось. Повторите действие.', 1;

        INSERT dbo.planZagrVyaz
        (
          pzvDivision,
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig,
          pzvDateML, pzvDateMLUt, pzvGsID,
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol,
          pzvKol, pzvSek, pzvKolNazn, pzvNChasi, pzvChasNazn,
          pzvGradacia,
          pzvTab, pzvKwsID,
          pzvDateStart, pzvDateEnd,
          pzvDateNaznTab, pzvDateNaznKm,
          pzvDateAdd, pzvUpdDate, pzvIDParent
        )
        OUTPUT N'Part2', inserted.pzvID INTO @NewRows
        SELECT
          1,
          b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig,
          b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
          b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
          @remaining, b.pzvSek, 0, CAST(ROUND(@qtyFact * @sek / 3600.0, 2) AS decimal(18,2)),0,
          @gradacia,
          NULL, 0,
          NULL, NULL,
          NULL, NULL,
          @now, @now, @pzvId
        FROM @Base b;

        GOTO __FINISH;
      END;

      THROW 50006, N'Неконсистентное состояние операции для mode=2.', 1;
    END

    --------------------------------------------------------------------
    -- Режим 3: предварительное деление мастером 
    --------------------------------------------------------------------
    IF (@mode = 3)
    BEGIN
      IF (@qtyFact IS NULL OR @qtyFact <= 0)
        THROW 50007, N'Не задано отделяемое количество', 1;

      IF (@remaining < 0)
        THROW 50008, N'Первая часть превышает плановое количество', 1;

      IF (@dateEnd IS NOT NULL OR @dateStart IS NOT NULL OR @dateNaznTab IS NOT NULL OR ISNULL(@tab,0) > 0)
        THROW 50009, N'Операция не должна быть назначена на табельный', 1;

      UPDATE p
      SET p.pzvKol = @qtyFact,
          p.pzvKolNazn = 0,
          p.pzvSekNazn = 0,
          p.pzvNChasi = CAST(ROUND(@qtyFact * @sek / 3600.0, 2) AS decimal(18,2)),
          p.pzvUpdDate = @now
      FROM dbo.planZagrVyaz p
      WHERE p.pzvID = @pzvId
        AND ISNULL(p.pzvTab,0) = 0
        AND p.pzvDateNaznTab IS NULL
        AND p.pzvDateStart IS NULL
        AND p.pzvDateEnd IS NULL;

      SET @rc = @@ROWCOUNT;
      IF (@rc = 0)
        THROW 50012, N'Нельзя предварительно делить: операция уже назначена/начата/закрыта.', 1;

      INSERT dbo.planZagrVyaz
      (
        pzvDivision,
        pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig,
        pzvDateML, pzvDateMLUt, pzvGsID,
        pzvNomZad, pzvAnnID, pzvNom, pzvRKol,
        pzvKol, pzvSek, pzvChasNazn, pzvNChasi, pzvKolNazn, pzvGradacia,
        pzvTab, pzvKwsID,
        pzvDateStart, pzvDateEnd,
        pzvDateNaznTab, pzvDateNaznKm,
        pzvDateAdd, pzvUpdDate, pzvIDParent
      )
      OUTPUT N'Part2', inserted.pzvID INTO @NewRows
      SELECT
        1,
        b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig,
        b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
        b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
        @qty-@qtyFact, b.pzvSek, 0, CAST(ROUND((@qty-@qtyFact) * @sek / 3600.0, 2) AS decimal(18,2)), 0, @gradacia,
        NULL, 0,
        NULL, NULL,
        NULL, NULL,
        @now, @now, @pzvId
      FROM @Base b;

      GOTO __FINISH;
    END;

    THROW 50006, N'Неизвестный режим разделения.', 1;

__FINISH:
    IF (@startedTran = 1)
      COMMIT;

    IF (@hadXactAbortOn = 1)
      SET XACT_ABORT ON;
    ELSE
      SET XACT_ABORT OFF;

    SELECT Kind, Id AS NewPzvId
    FROM @NewRows;

  END TRY
  BEGIN CATCH
    IF (XACT_STATE() = 1)
    BEGIN
      IF (@startedTran = 1 AND @@TRANCOUNT > 0)
        ROLLBACK TRAN;
      ELSE IF (@startedTran = 0 AND @@TRANCOUNT > 0)
        ROLLBACK TRAN PZV_Split_Save;
    END
    ELSE IF (XACT_STATE() = -1 AND @startedTran = 1 AND @@TRANCOUNT > 0)
    BEGIN
      ROLLBACK TRAN;
    END

    IF (@hadXactAbortOn = 1)
      SET XACT_ABORT ON;
    ELSE
      SET XACT_ABORT OFF;

    THROW;
  END CATCH
END
GO
