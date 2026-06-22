CREATE OR ALTER PROCEDURE dbo.Yarn_RecalculateCost
    @Nakl nvarchar(50),
    @KodArt nvarchar(7)
AS
BEGIN
    SET NOCOUNT ON;

    -- Валидация
    IF ISNULL(@Nakl, '') = '' OR @Nakl = '0'
    BEGIN
        SELECT 1 AS Error, N'Номер карты не указан.' AS MessageError, CAST(NULL AS decimal(18,3)) AS SebUpr;
        RETURN;
    END;

    -- Шаг 1: Проверить существование карты
    IF NOT EXISTS (
        SELECT 1
        FROM dbo.prihod_pryz pp
        INNER JOIN dbo.prihod_v pv ON pp.kod_pr = pv.kod_pr
        WHERE pp.nakl = @Nakl
    )
    BEGIN
        SELECT 1 AS Error, N'Карта не найдена!' AS MessageError, CAST(NULL AS decimal(18,3)) AS SebUpr;
        RETURN;
    END;

    DECLARE @NewSeb decimal(18,3);

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Шаг 2: Обновить prihod_pryz.seb_t_m = np_summa / nps_kol
        UPDATE pp
        SET pp.seb_t_m = pv.np_summa / ISNULL(NULLIF(pv.nps_kol, 0), 1)
        FROM dbo.prihod_pryz pp
        INNER JOIN dbo.prihod_v pv ON pp.kod_pr = pv.kod_pr
        WHERE pp.nakl = @Nakl;

        -- Получить новую себестоимость для отображения
        SELECT TOP 1
            @NewSeb = pv.np_summa / ISNULL(NULLIF(pv.nps_kol, 0), 1)
        FROM dbo.prihod_pryz pp
        INNER JOIN dbo.prihod_v pv ON pp.kod_pr = pv.kod_pr
        WHERE pp.nakl = @Nakl;

        -- Шаг 3: Найти связанные задания
        DECLARE @ZadanyUpdate TABLE (
            Articul nvarchar(50),
            zad_pl nvarchar(50)
        );

        INSERT INTO @ZadanyUpdate (Articul, zad_pl)
        SELECT rzv.Articul, rzv.zad_pl
        FROM dbo.v_spis_pryz vsp
        INNER JOIN dbo.raskr_zeh_vyaz rzv ON vsp.nom_zadany = rzv.zad_pl
        WHERE LEFT(rzv.kod, 7) = @KodArt
          AND rzv.data_got IS NULL
          AND vsp.nakl = @Nakl
        GROUP BY rzv.Articul, rzv.zad_pl;

        -- Шаг 4-6: Для каждого задания пересчитать себестоимость
        DECLARE @CurArticul nvarchar(50), @CurZadPl nvarchar(50);
        DECLARE @kg_all decimal(18,3), @seb_all_val decimal(18,2);
        DECLARE @kol_zadany decimal(18,3);
        DECLARE @rash_kol decimal(18,3), @sebest_sum decimal(18,2);
        DECLARE @norm_all decimal(18,6);

        DECLARE cur_zadany CURSOR LOCAL FAST_FORWARD FOR
            SELECT Articul, zad_pl FROM @ZadanyUpdate;
        OPEN cur_zadany;
        FETCH NEXT FROM cur_zadany INTO @CurArticul, @CurZadPl;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            -- Расход пряжи и себестоимость по заданию (type_pryz=0 — основная пряжа)
            SELECT
                @rash_kol = SUM(kol),
                @sebest_sum = SUM(kol * seb)
            FROM (
                SELECT
                    pp.seb_t_m AS seb,
                    SUM(vs.kol + vs.rash_proizv + vs.pogr) AS kol
                FROM dbo.v_spis_pryz vs
                INNER JOIN dbo.prihod_pryz pp ON vs.nakl = pp.nakl
                WHERE vs.nom_zadany = @CurZadPl
                  AND vs.type_pryz = 0
                GROUP BY pp.seb_t_m, vs.nakl
            ) seb_all;

            -- Количество изделий в задании
            SELECT @kol_zadany = SUM(kol)
            FROM dbo.raskr_zeh_vyaz
            WHERE zad_pl = @CurZadPl;

            IF ISNULL(@kol_zadany, 0) = 0
            BEGIN
                FETCH NEXT FROM cur_zadany INTO @CurArticul, @CurZadPl;
                CONTINUE;
            END;

            -- кг на 1 изделие и себестоимость на 1 изделие
            SET @kg_all = ROUND(ISNULL(@rash_kol, 0) / @kol_zadany, 3);
            SET @seb_all_val = ROUND(ISNULL(@sebest_sum, 0) / @kol_zadany, 2);

            -- Получить компоненты задания
            DECLARE @CalcSebNorm TABLE (
                kod nvarchar(50),
                kod_k nvarchar(50),
                norma_pr decimal(18,6),
                norma_fact decimal(18,3),
                seb decimal(18,2),
                percant decimal(18,7),
                rn int
            );
            DELETE FROM @CalcSebNorm;

            INSERT INTO @CalcSebNorm (kod, kod_k, norma_pr, norma_fact, seb, percant, rn)
            SELECT DISTINCT
                kod, kod_k, norma_pr,
                0, 0, 0,
                ROW_NUMBER() OVER (ORDER BY kod)
            FROM dbo.raskr_zeh_vyaz
            WHERE zad_pl = @CurZadPl;

            -- Проверить есть ли несколько компонентов (kod_k заполнен)
            IF EXISTS (SELECT 1 FROM @CalcSebNorm WHERE ISNULL(kod_k, '') <> '')
            BEGIN
                -- Сумма норм
                SELECT @norm_all = SUM(norma_pr) FROM @CalcSebNorm;

                IF ISNULL(@norm_all, 0) <> 0
                BEGIN
                    -- Распределить пропорционально
                    UPDATE @CalcSebNorm
                    SET percant = norma_pr / @norm_all;

                    UPDATE @CalcSebNorm
                    SET norma_fact = @kg_all * percant,
                        seb = @seb_all_val * percant;

                    -- Корректировка остатка на последнюю запись (norma_fact)
                    DECLARE @sum_norma_fact decimal(18,3), @sum_seb decimal(18,2);
                    DECLARE @max_rn int;

                    SELECT @sum_norma_fact = SUM(norma_fact), @sum_seb = SUM(seb), @max_rn = MAX(rn)
                    FROM @CalcSebNorm;

                    IF @sum_norma_fact <> @kg_all
                    BEGIN
                        UPDATE @CalcSebNorm
                        SET norma_fact = @kg_all - (@sum_norma_fact - norma_fact)
                        WHERE rn = @max_rn;
                    END;

                    IF @sum_seb <> @seb_all_val
                    BEGIN
                        UPDATE @CalcSebNorm
                        SET seb = @seb_all_val - (@sum_seb - seb)
                        WHERE rn = @max_rn;
                    END;
                END;

                -- Обновить raskr_zeh_vyaz по каждому компоненту
                UPDATE rzv
                SET rzv.norma_fed = csn.norma_fact,
                    rzv.seb_isd = csn.seb
                FROM dbo.raskr_zeh_vyaz rzv
                INNER JOIN @CalcSebNorm csn ON rzv.kod = csn.kod
                WHERE rzv.zad_pl = @CurZadPl;
            END
            ELSE
            BEGIN
                -- Один компонент — ставим напрямую
                UPDATE dbo.raskr_zeh_vyaz
                SET norma_fed = @kg_all,
                    seb_isd = @seb_all_val
                WHERE zad_pl = @CurZadPl;
            END;

            -- Шаг 7: Обновить nakl_ras (расходные накладные)
            -- Перечитать обновлённые данные raskr_zeh_vyaz
            -- Для каждой записи обновить nakl_ras с коэффициентами из sp_articul
            UPDATE nr
            SET nr.t_seb_t = CASE WHEN rzv.norma_fed <> 0
                                  THEN rzv.seb_isd / rzv.norma_fed
                                  ELSE 0 END,
                nr.t_seb1 = (rzv.seb_isd + ISNULL(a.seb_z, 0) + ISNULL(a.seb_dop, 0))
                            * ISNULL(NULLIF(a.koef_d, 0), 1)
                            * ISNULL(NULLIF(a.koef_pr, 0), 1)
                            * ISNULL(NULLIF(a.koef, 0), 1),
                nr.t_seb  = (rzv.seb_isd + ISNULL(a.seb_z, 0) + ISNULL(a.seb_dop, 0))
                            * ISNULL(NULLIF(a.koef_d, 0), 1)
                            * ISNULL(NULLIF(a.koef_pr, 0), 1)
            FROM dbo.nakl_ras nr
            INNER JOIN (
                SELECT n.dost_n, n.iz, nr2.pach_kod
                FROM dbo.nakl_ras nr2
                INNER JOIN dbo.nakl n ON nr2.iz = n.iz
                WHERE nr2.pach_kod = rzv.pach_kod
                  AND ISNULL(n.dost_n, 0) = 0
            ) vib ON nr.iz = vib.iz AND nr.pach_kod = vib.pach_kod
            INNER JOIN dbo.raskr_zeh_vyaz rzv ON nr.pach_kod = rzv.pach_kod
            INNER JOIN dbo.v_spis_pryz vsp ON rzv.zad_pl = vsp.nom_zadany
            INNER JOIN dbo.sp_articul a ON LEFT(rzv.kod, 7) = LEFT(a.kod, 7)
            WHERE vsp.nakl = @Nakl
              AND rzv.zad_pl = @CurZadPl
              AND LEFT(rzv.kod, 7) = @KodArt
              AND rzv.data_got IS NULL
              AND ISNULL(vib.dost_n, 0) = 0;

            FETCH NEXT FROM cur_zadany INTO @CurArticul, @CurZadPl;
        END;

        CLOSE cur_zadany;
        DEALLOCATE cur_zadany;

        COMMIT TRANSACTION;

        SELECT 0 AS Error, CAST(NULL AS nvarchar(max)) AS MessageError, @NewSeb AS SebUpr;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

        IF CURSOR_STATUS('local', 'cur_zadany') >= 0
        BEGIN
            CLOSE cur_zadany;
            DEALLOCATE cur_zadany;
        END;

        SELECT 1 AS Error, ERROR_MESSAGE() AS MessageError, CAST(NULL AS decimal(18,3)) AS SebUpr;
    END CATCH;
END
GO
