CREATE VIEW dbo.knitWorkingShiftNewCurrentSmen_view 
AS SELECT TOP 100 PERCENT ss.kmaID
          , ss.kwsID
          ,ss.kmaNumber
          ,ss.dateShiftStart
          ,ss.dateShiftEnd
          ,ss.tabShiftStart
          ,dbo.getSokrFio(f.fio) AS fioShiftStart
          ,f.fio AS fioShiftStartFull
          ,ss.diffHours
          ,ss.diffSeconds
          , IIF(ss.dateShiftStart IS NULL OR ss.dateShiftEnd IS NOT NULL, 'не открыта смена', IIF(diffHours > 12, 'не закрыта предыдущая смена', '')) AS shiftStatus
          , IIF(ss.dateShiftStart IS NULL OR ss.dateShiftEnd IS NOT NULL, 1, IIF(diffHours > 12, 2, 0)) AS shiftStatusID
          ,CAST(dbo.getNumbersOnly(ss.kmaNumber) AS INT) AS kmaNumberInt
    --INTO #shiftInfoList
    FROM (
        SELECT s.kwsID, s.kmaID, s.kmaNumber
            , dateShiftStart
            , dateShiftEnd
            , tabShiftStart
            , DATEDIFF(HOUR, dateShiftStart, ISNULL(dateShiftEnd, GETDATE())) AS diffHours
            , DATEDIFF(SECOND, dateShiftStart, ISNULL(dateShiftEnd, GETDATE())) AS diffSeconds
            , s.kolMach
        FROM 
          (
          SELECT mav.kmaID, mav.kmaNumber
              , (SELECT TOP 1 kwsID FROM knitWorkingShiftNew_view WHERE kwsKmaID = mav.kmaID AND kwsDateDel IS NULL ORDER BY kwsDateStart DESC)  AS kwsID
              , (SELECT TOP 1 kwsDateStart FROM knitWorkingShiftNew_view WHERE kwsKmaID = mav.kmaID AND kwsDateDel IS NULL ORDER BY kwsDateStart DESC)  AS dateShiftStart
              , (SELECT TOP 1 kwsDateEnd FROM knitWorkingShiftNew_view WHERE kwsKmaID = mav.kmaID AND kwsDateDel IS NULL ORDER BY kwsDateStart DESC)  AS dateShiftEnd
              , (SELECT TOP 1 kwsTabStart FROM knitWorkingShiftNew_view WHERE kwsKmaID = mav.kmaID AND kwsDateDel IS NULL ORDER BY kwsDateStart DESC)  AS tabShiftStart
              , (SELECT count(*) AS kolMach FROM knitMachineList_view mlv WHERE mlv.kmlKmaID = mav.kmaID) AS kolMach
            FROM knitMachineArea_view mav
            WHERE ISNULL(mav.kmaIDNazn,0) = 6 -- and mav.kmaNumber < '90'
              AND mav.kmaIDNazn = 6
          ) s
          WHERE ISNULL(s.kolMach, 0) > 0
        ) ss
        LEFT JOIN fio f ON ss.tabShiftStart = f.tab
GO