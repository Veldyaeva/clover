SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazKnitEconomAssort_LoadRows
    @PodrMode varchar(16),
    @ShowAll bit = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        nn,
        nom_zadany,
        razm_ryad,
        pach_min,
        pach_max,
        nom,
        articul,
        mod,
        date_econom,
        last_otm_izm,
        grup,
        data_cd_min,
        koef_zatrat,
        id_podr,
        seb_all
    FROM dbo.view_seb_vyaz_econom_assort WITH (NOLOCK)
    WHERE
        (
            ISNULL(@PodrMode, '') NOT IN ('Sock', 'Knit', 'Cord')
            OR (@PodrMode = 'Sock' AND id_podr IN (10, 16))
            OR (@PodrMode = 'Knit' AND id_podr = 6)
            OR (@PodrMode = 'Cord' AND id_podr = 19)
        )
        AND
        (
            @ShowAll = 1
            OR date_econom IS NULL
            OR (
                date_econom >= CONVERT(date, GETDATE())
                AND date_econom < DATEADD(day, 1, CONVERT(date, GETDATE()))
            )
        )
    ORDER BY data_cd_min;
END
GO
