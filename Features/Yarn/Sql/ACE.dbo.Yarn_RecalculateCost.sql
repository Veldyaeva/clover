CREATE OR ALTER PROCEDURE dbo.Yarn_RecalculateCost
    @Nakl nvarchar(50),
    @KodArt nvarchar(7)
AS
BEGIN
    SET NOCOUNT ON;

    IF ISNULL(@Nakl, '') = '' OR @Nakl = '0'
    BEGIN
        SELECT 1 AS Error, N'Номер карты не указан.' AS MessageError, CAST(NULL AS decimal(18,3)) AS SebUpr;
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.prihod_pryz pp
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

        UPDATE pp SET pp.seb_t_m = pv.np_summa / ISNULL(NULLIF(pv.nps_kol, 0), 1)
        FROM dbo.prihod_pryz pp
        INNER JOIN dbo.prihod_v pv ON pp.kod_pr = pv.kod_pr
        WHERE pp.nakl = @Nakl;

        SELECT TOP 1 @NewSeb = pv.np_summa / ISNULL(NULLIF(pv.nps_kol, 0), 1)
        FROM dbo.prihod_pryz pp
        INNER JOIN dbo.prihod_v pv ON pp.kod_pr = pv.kod_pr
        WHERE pp.nakl = @Nakl;

        DECLARE @ZadanyUpdate TABLE (Articul nvarchar(50), zad_pl nvarchar(50));
        INSERT INTO @ZadanyUpdate (Articul, zad_pl)
        SELECT rzv.Articul, rzv.zad_pl
        FROM dbo.v_spis_pryz vsp
        INNER JOIN dbo.raskr_zeh_vyaz rzv ON vsp.nom_zadany = rzv.zad_pl
        WHERE LEFT(rzv.kod, 7) = @KodArt AND rzv.data_got IS NULL AND vsp.nakl = @Nakl
        GROUP BY rzv.Articul, rzv.zad_pl;

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
            SELECT @rash_kol = SUM(kol), @sebest_sum = SUM(kol * seb)
            FROM (
                SELECT pp.seb_t_m AS seb, SUM(vs.kol + vs.rash_proizv + vs.pogr) AS kol
                FROM dbo.v_spis_pryz vs
                INNER JOIN dbo.prihod_pryz pp ON vs.nakl = pp.nakl
                WHERE vs.nom_zadany = @CurZadPl AND vs.type_pryz = 0
                GROUP BY pp.seb_t_m, vs.nakl
            ) seb_all;

            SELECT @kol_zadany = SUM(kol) FROM dbo.raskr_zeh_vyaz WHERE zad_pl = @CurZadPl;

            IF ISNULL(@kol_zadany, 0) = 0
            BEGIN
                FETCH NEXT FROM cur_zadany INTO @CurArticul, @CurZadPl;
                CONTINUE;
            END;

            SET @kg_all = ROUND(ISNULL(@rash_kol, 0) / @kol_zadany, 3);
            SET @seb_all_val = ROUND(ISNULL(@sebest_sum, 0) / @kol_zadany, 2);

            DECLARE @CalcSebNorm TABLE (kod nvarchar(50), kod_k nvarchar(50), norma_pr decimal(18,6),
                norma_fact decimal(18,3), seb decimal(18,2), percant decimal(18,7), rn int);
            DELETE FROM @CalcSebNorm;

            INSERT INTO @CalcSebNorm (kod, kod_k, norma_pr, norma_fact, seb, percant, rn)
            SELECT DISTINCT kod, kod_k, norma_pr, 0, 0, 0, ROW_NUMBER() OVER (ORDER BY kod)
            FROM dbo.raskr_zeh_vyaz WHERE zad_pl = @CurZadPl;

            IF EXISTS (SELECT 1 FROM @CalcSebNorm WHERE ISNULL(kod_k, '') <> '')
            BEGIN
                SELECT @norm_all = SUM(norma_pr) FROM @CalcSebNorm;
                IF ISNULL(@norm_all, 0) <> 0
                BEGIN
                    UPDATE @CalcSebNorm SET percant = norma_pr / @norm_all;
                    UPDATE @CalcSebNorm SET norma_fact = @kg_all * percant, seb = @seb_all_val * percant;

                    DECLARE @sum_nf decimal(18,3), @sum_s decimal(18,2), @max_rn int;
                    SELECT @sum_nf = SUM(norma_fact), @sum_s = SUM(seb), @max_rn = MAX(rn) FROM @CalcSebNorm;
                    IF @sum_nf <> @kg_all
                        UPDATE @CalcSebNorm SET norma_fact = @kg_all - (@sum_nf - norma_fact) WHERE rn = @max_rn;
                    IF @sum_s <> @seb_all_val
                        UPDATE @CalcSebNorm SET seb = @seb_all_val - (@sum_s - seb) WHERE rn = @max_rn;
                END;

                UPDATE rzv SET rzv.norma_fed = csn.norma_fact, rzv.seb_isd = csn.seb
                FROM dbo.raskr_zeh_vyaz rzv
                INNER JOIN @CalcSebNorm csn ON rzv.kod = csn.kod
                WHERE rzv.zad_pl = @CurZadPl;
            END
            ELSE
            BEGIN
                UPDATE dbo.raskr_zeh_vyaz SET norma_fed = @kg_all, seb_isd = @seb_all_val
                WHERE zad_pl = @CurZadPl;
            END;

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
        BEGIN CLOSE cur_zadany; DEALLOCATE cur_zadany; END;
        SELECT 1 AS Error, ERROR_MESSAGE() AS MessageError, CAST(NULL AS decimal(18,3)) AS SebUpr;
    END CATCH;
END
GO
