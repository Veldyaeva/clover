USE ACE
GO

CREATE OR ALTER PROCEDURE dbo.PZV_Split_2 
  @pzvId int, 
  @mode int, --1 = уточнение факт, 2 = отриц. строка без таб, 3 = предв. деление 
  @qtyFact int, -- mode 1: факт; mode 3: первая часть 
  @userName sysname = NULL, 
  @gradacia bit = 0 
AS 
BEGIN 
  SET NOCOUNT ON; 
--  защита от повторного выполнения/гонок.
  -- Правило: любой "смысловой" UPDATE делаем атомарно с проверкой ожидаемого состояния,
  -- затем проверяем @@ROWCOUNT. Если 0 — значит строка уже изменилась (повторный вызов/параллельный апдейт).
  DECLARE @rc int;
  
  BEGIN TRY 
    BEGIN TRAN;
    -------------------------------------------------------------------- 
    -- 1. Чтение исходной строки и базовых параметров 
    -------------------------------------------------------------------- 
    DECLARE 
      @plannedNazn  int, --kolNazn 
      @qty          int, --Kol 
      @remaining    int, --Nazn-Fact 
      @plannedHours DECIMAL(18, 2), -- 
      @sek          int, 
      @sekNazn      int, 
      @remainingHours DECIMAL(18, 2), -- 
      @now          datetime = GETDATE(), 
      @dateStart    datetime, 
      @dateEnd      datetime, 
      @newDateStart datetime, 
      @newDateEnd   datetime, 
      @dateNaznTab  datetime, 
      @tab int; 
      
    DECLARE @NewRows TABLE 
    (
      Kind sysname NULL, -- метка, что именно вставили 
      Id int NOT NULL 
     ); 
     


DECLARE @Base TABLE
(
    pzvID        int NOT NULL,
    pzvMod       nvarchar(25) NULL,
    pzvArticul   nvarchar(25) NULL,
    pzvKmlID     int NULL,
    pzvNomN      int NULL,
    pzvVidPr     char(2) NULL,
    pzvIDMlOp    int NULL,
    pzvNrID      int NULL,
    pzvIdBrig    int NULL,
    pzvDateML    datetime NULL,
    pzvDateMLUt  datetime NULL,
    pzvGsID      int NULL,
    pzvNomZad    nvarchar(10) NULL,
    pzvAnnID     int NULL,
    pzvNom       int NULL,
    pzvRKol      int NULL,
    pzvSek       int NULL,
    pzvTab       int NULL,
    pzvKwsID     int NULL,
    pzvKol       int Null,
    pzvKolNazn   INT Null,
    pzvNChasi    DECIMAL(6,2) NULL,
    pzvDateStart DATETIME NULL,
    pzvDateEnd   DATETIME null,
    pzvDateNaznTab datetime NULL,
    pzvDateNaznKm  datetime NULL
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
      @plannedNazn =    CASE WHEN ISNULL(pzvTab,0) = 0 THEN ISNULL(pzvKol,0) 
      ELSE ISNULL(pzvKolNazn,0) END, -- ISNULL(pzvKol, 0),--ISNULL(pzvKolNazn, 0), 
      @qty =            CASE WHEN ISNULL(pzvTab,0) = 0 THEN ISNULL(pzvKol,0)
      ELSE ISNULL(pzvKolNazn,0) END, --ISNULL(pzvKol, 0),
      @plannedHours=    ISNULL(pzvNChasi, 0),
      @remainingHours=  Isnull(pzvNChasi, 0), 
      @sek =            ISNULL(pzvSek, 0), 
      @dateStart =      pzvDateStart, 
      @dateEnd =        pzvDateEnd, 
      @dateNaznTab =    pzvDateNaznTab, 
      @tab =            pzvTab 
    FROM @Base
    --WHERE pzvID = @pzvId; 




      
    IF (@qty IS NULL OR @qty < 0) 
      THROW 50001, 'Некорректный план: pzvKolNazn/pzvKol.', 1; 
      
    SET @remaining= @plannedNazn - @qtyFact; 
    SET @plannedHours = CAST(ROUND((ISNULL(@qtyFact, 0) * ISNULL(@sek, 0)) / 3600.0, 2) AS decimal(18,2)); 
    set @remainingHours = CAST(ROUND((ISNULL(@remaining, 0) * ISNULL(@sek, 0)) / 3600.0, 2) AS decimal(18,2)); 
    SET @sekNazn = @sek*@qty 
    

    -------------------------------------------------------------------- 
    -- 2. Режим 1: Завершение с фактом 
    -------------------------------------------------------------------- 
    IF (@mode = 1) 
    BEGIN -- Завершение с фактом (@qtyFact - факт) 
IF (ISNULL(@tab,0) = 0)
BEGIN
    -- фиксируем корректировку отдельно
    UPDATE dbo.planZagrVyaz
    SET pzvDateStart = NULL,
        pzvUpdDate = @now
    WHERE pzvID = @pzvId;

    COMMIT;  -- завершили транзакцию, чтобы изменение сохранилось

    THROW 50002, N'Операция не назначена на табельный. Обратитесь к мастеру', 1;
END;


--       -- ВАЖНО: не делаем COMMIT перед THROW. Любые "починки" при ошибке — отдельной процедурой.
--       IF (ISNULL(@tab,0) = 0)
--         THROW 50002, N'Операция не назначена на табельный. Обратитесь к мастеру', 1;
      -- Проверка даты начала 
      IF (@dateStart IS NULL) 
      BEGIN 
        THROW 50002, N'Некорректные даты: дата начала операции не заполнена', 1; 
      END; 
      IF (@qtyFact IS NULL OR @qtyFact < 0 OR @qtyFact > @qty) 
        THROW 50003, 'Факт должен быть в диапазоне [0..план]', 1; 

      -- Фиксируем факт в исходной строке 
--      UPDATE dbo.planZagrVyaz 
--      SET 
--        pzvKol = @qtyFact,
--        pzvKolNazn = @qtyFact,--!!изменили, чтоб цифры совпадали и статусы правильно считались 
--        pzvDateEnd = ISNULL(pzvDateEnd, @now), 
--        pzvUpdDate = @now, 
--        -- pzvDivision= 1, Надя сказала, в исходной строке не надо 
--        pzvNChasi = @plannedHours,-- ROUND(@qtyFact * / NULLIF(@kol0,0), 2); 
--        pzvChasNazn = @plannedHours --!!изменили, чтоб цифры совпадали и статусы правильно считались 
--      WHERE pzvID = @pzvId; 
      
       UPDATE p
       SET
         p.pzvKol      = @qtyFact,
         p.pzvKolNazn  = @qtyFact,  -- чтобы статусы/суммы не ехали
         p.pzvDateEnd  = ISNULL(p.pzvDateEnd, @now),
         p.pzvUpdDate  = @now,
         p.pzvNChasi   = @plannedHours,
         p.pzvChasNazn = @plannedHours
       FROM dbo.planZagrVyaz p
       WHERE p.pzvID = @pzvId
         AND ISNULL(p.pzvTab,0) > 0
         AND p.pzvDateStart IS NOT NULL
         AND p.pzvDateEnd IS NULL; -- защита от повторного завершения
 
       SET @rc = @@ROWCOUNT;
       IF (@rc = 0)
         THROW 50010, N'Повторное завершение запрещено: операция уже закрыта/не начата/не назначена.', 1;
      
      
    IF (@remaining<0) 
    BEGIN 
      THROW 50004, 
        N'остаток меньше нуля', 1; 
    END; 
      
      -- Остаток в новую строку, если есть что делить 
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
END;      
      -- Отдельная сводная строка по завершённой операции 
      INSERT INTO @NewRows (Kind, Id) 
      VALUES ('FinishedFact', @pzvId); 
    END 
    
    -------------------------------------------------------------------- 
    -- 3. Режим 2: назначено на таб, но не выполнено 
    -- - отрицательная строка назначения 
    -- - остаток без таб 
    -------------------------------------------------------------------- 
    ELSE IF (@mode = 2) 
    BEGIN 
IF (ISNULL(@tab,0) = 0)
BEGIN
    -- фиксируем корректировку отдельно
    UPDATE dbo.planZagrVyaz
    SET pzvDateStart = NULL,
        pzvUpdDate = @now
    WHERE pzvID = @pzvId;

    COMMIT;  -- завершили транзакцию, чтобы изменение сохранилось

    THROW 50002, N'Операция не назначена на табельный. Обратитесь к мастеру', 1;
END;
--
--       IF (ISNULL(@tab,0) = 0)
--        THROW 50002, N'Операция не назначена на табельный. Обратитесь к мастеру', 1;
-- 
      -- Проверка и расчёт дат для новых строк (Negative + Remainder) 
      IF (@dateStart IS NULL AND @dateEnd IS NULL) 
      BEGIN 
        -- обе пустые: в новые строки ставим @now 
        SET @newDateStart = @now; 
        SET @newDateEnd = @now; 
      END 
      ELSE IF (@dateStart IS NOT NULL AND @dateEnd IS NOT NULL) 
      BEGIN 
        -- обе заполнены: копируем в новые строки 
        SET @newDateStart = @dateStart; 
        SET @newDateEnd = @dateEnd; 
      END 
      ELSE 
      BEGIN
        -- одна дата есть, другой нет - ошибка 
        THROW 50006, 
        N'Некорректные даты: одна из pzvDateStart/pzvDateEnd заполнена, другая нет.', 1; 
        END; 
        -- Фиксируем факт деления в исходной строке 
 --       UPDATE dbo.planZagrVyaz 
 --       SET 
 --         pzvDateStart = @newDateStart, 
 --         pzvDateEnd = @newDateEnd 
 --         -- pzvDivision= 1, Надя сказала, в исходной строке не надо 
 --       WHERE pzvID = @pzvId; 
         UPDATE p
         SET
           p.pzvDateStart = @newDateStart,
           p.pzvDateEnd   = @newDateEnd,
           p.pzvUpdDate   = @now
         FROM dbo.planZagrVyaz p
         WHERE p.pzvID = @pzvId
           AND ISNULL(p.pzvTab,0) > 0
           AND p.pzvDateStart IS NULL
           AND p.pzvDateEnd IS NULL; -- защита: нельзя сторнировать начатое/закрытое
 
         SET @rc = @@ROWCOUNT;
         IF (@rc = 0)
           THROW 50011, N'Нельзя разназначить/сторно: операция уже начата/закрыта/не назначена.', 1;
        
        -- 1) отрицательная строка на том же таб 
        INSERT dbo.planZagrVyaz 
        ( 
          pzvDivision, 
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig, pzvDateNaznKm, pzvDateNaznTab, pzvDateML, pzvDateMLUt, pzvGsID, 
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol, 
          pzvKol, pzvSek, pzvNChasi, 
          pzvKolNazn, pzvChasNazn, pzvSekNazn, 
          pzvTab, pzvKwsID, 
          pzvDateStart, pzvDateEnd, 
          pzvDateAdd, pzvUpdDate, pzvIDParent 
        ) 
        OUTPUT 'Negative', inserted.pzvID INTO @NewRows 
        SELECT 
         1, 
         b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig, b.pzvDateNaznKm, b.pzvDateNaznTab, b.pzvDateML, b.pzvDateMLUt, b.pzvGsID, 
         b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol, 
         0-@remaining, b.pzvSek, -@remainingHours, 
         @remaining, @remainingHours, b.pzvSek * @remaining, 
         b.pzvTab, b.pzvKwsID, 
         @newDateStart, @newDateEnd, 
         @now, @now, 
         @pzvId 
      FROM @Base b;
       
       -- 2) копия исходной (остаток) — без таб 
       INSERT dbo.planZagrVyaz 
       (
          pzvDivision, 
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig, pzvDateML, pzvDateMLUt, pzvGsID, 
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol, 
          pzvKol, pzvSek, pzvNChasi, 
          pzvKolNazn, pzvSekNazn, pzvChasNazn, 
          pzvTab, pzvKwsID, 
          pzvDateStart, pzvDateEnd, pzvDateNaznTab, pzvDateNaznKm, 
          pzvDateAdd, pzvUpdDate, pzvIDParent 
        ) 
        OUTPUT 'Remainder', inserted.pzvID INTO @NewRows 
        SELECT 
          1,
          b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig, b.pzvDateML, b.pzvDateMLUt, b.pzvGsID, 
          b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol, 
          @remaining, b.pzvSek, @remainingHours, 
          @remaining, @sekNazn , @remainingHours, 
          0, 0, 
          null, null, null, b.pzvDateNaznKm, 
          @now, @now, 
          @pzvId 
        FROM @Base b;
        
        SELECT 
          Result = 'UnassignWithNegative', 
          OriginalPzvId = @pzvId, 
          NegativeQty = -@remaining 
        END 
        -------------------------------------------------------------------- 
        -- 4. Режим 3: предварительное деление мастером 
        -------------------------------------------------------------------- 
        ELSE IF (@mode = 3) 
        BEGIN 
          IF (@qtyFact IS NULL OR @qtyFact <= 0) 
            THROW 50007, 'Не задана первая часть', 1; 
            
          IF (@remaining < 0) 
            THROW 50008, 'Первая часть превышает плановое количество', 1;
          IF        (@dateEnd IS NOT NULL
    OR @dateStart IS NOT NULL
    OR @dateNaznTab IS NOT NULL
    OR ISNULL(@tab,0) > 0)
            THROW 50009, 'Операция не должна быть назначена на табельный', 1; 
            -- Вставляем первую часть qtyFact 
--          UPDATE dbo.planZagrVyaz 
--          set 
-- 
--          pzvKol = @qtyFact, 
--          pzvKolNazn = 0 
--        FROM dbo.planZagrVyaz 
--        WHERE pzvID = @pzvId; 
       UPDATE p
       SET
         p.pzvKol     = @qtyFact,
         p.pzvKolNazn = 0,
         p.pzvUpdDate = @now
       FROM dbo.planZagrVyaz p
       WHERE p.pzvID = @pzvId
         AND ISNULL(p.pzvTab,0) = 0
         AND p.pzvDateNaznTab IS NULL
         AND p.pzvDateStart IS NULL
         AND p.pzvDateEnd IS NULL; -- защита от "предделить уже назначенное/начатое"
 
       SET @rc = @@ROWCOUNT;
       IF (@rc = 0)
         THROW 50012, N'Нельзя предварительно делить: операция уже назначена/начата/закрыта.', 1;

        -- Вставляем вторую часть qty2 
        INSERT dbo.planZagrVyaz 
        (
          pzvDivision, 
          pzvMod, pzvArticul, pzvKmlID, pzvNomN, pzvVidPr, pzvIDMlOp, pzvNrID, pzvIdBrig, pzvDateML, pzvDateMLUt, pzvGsID, 
          pzvNomZad, pzvAnnID, pzvNom, pzvRKol, 
          pzvKol, pzvSek, pzvKolNazn, pzvGradacia, 
          pzvTab, pzvKwsID, 
          pzvDateStart, pzvDateEnd, 
          pzvDateNaznTab, pzvDateNaznKm, 
          pzvDateAdd, pzvUpdDate, pzvIDParent 
        ) 
OUTPUT 'Part2', inserted.pzvID INTO @NewRows 
SELECT 
  1,
  b.pzvMod, b.pzvArticul, b.pzvKmlID, b.pzvNomN, b.pzvVidPr, b.pzvIDMlOp, b.pzvNrID, b.pzvIdBrig, b.pzvDateML, b.pzvDateMLUt, b.pzvGsID,
  b.pzvNomZad, b.pzvAnnID, b.pzvNom, b.pzvRKol,
  @qty-@qtyFact, b.pzvSek, 0, @gradacia,
  NULL, 0,
  NULL, NULL,
  NULL, NULL,
  @now, @now, @pzvId
FROM @Base b;
        
        SELECT 
        Result = 'PreSplit', 
        OriginalPzvId = @pzvId, 
        PartA = @qtyFact, 
        PartB = @remaining; 
    END 
        -- 5. Неизвестный режим 
        -------------------------------------------------------------------- 
        ELSE 
        BEGIN 
          THROW 50006, 'Неизвестный режим разделения.', 1; 
        END; 
        
        -------------------------------------------------------------------- 
        -- 6. Завершение транзакции и вывод новых Id 
        -------------------------------------------------------------------- 
        COMMIT; 
        
        -- Пусто, если ничего не вставили 
        SELECT 
          Kind, 
          Id AS NewPzvId 
        FROM @NewRows; 
      END TRY 
      BEGIN CATCH 
         IF @@TRANCOUNT > 0 
          ROLLBACK TRAN; 
          
        DECLARE @msg nvarchar(4000) = ERROR_MESSAGE(); 
        THROW 51000, @msg, 1; 
      END CATCH 
    END; 
    
    
    

GO