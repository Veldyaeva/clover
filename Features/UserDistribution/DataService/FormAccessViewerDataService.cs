using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.DataService
{
	internal class FormAccessViewerDataService
	{
		private readonly DbService _dbService;
		private readonly DatabaseHelperSQL _dbHelper;

		public FormAccessViewerDataService()
		{
			_dbHelper = new DatabaseHelperSQL();
			_dbService = new DbService(_dbHelper);
		}

		public Task<DataTable> GetFormsAsync()
		{
			string query = @"
SELECT
    pf.ProjectFormsID,
    pf.NameForm,
    pf.NameFormRus,
    u.UserName AS CreatorName,
    COUNT(DISTINCT ur.UserID) AS UsersWithAccess
FROM ProjectForms pf
LEFT JOIN Users u ON u.UserID = pf.CreatorID
OUTER APPLY
(
    SELECT TOP (1) ofm.ObjectID
    FROM ObjectForm ofm
    WHERE ofm.FormID = pf.ProjectFormsID
      AND
      (
          ofm.ObjectName = pf.NameForm
          OR ofm.ObjectType = N'CustomForm'
      )
    ORDER BY
        CASE
            WHEN ofm.ObjectName = pf.NameForm THEN 0
            WHEN ofm.ObjectType = N'CustomForm' THEN 1
            ELSE 2
        END,
        ofm.ObjectID
) formObject
LEFT JOIN RoleObject ro
    ON ro.ObjectID = formObject.ObjectID
   AND ISNULL(ro.ModeID, 0) > 0
LEFT JOIN UserRoles ur ON ur.RoleID = ro.RoleID
GROUP BY
    pf.ProjectFormsID,
    pf.NameForm,
    pf.NameFormRus,
    u.UserName
ORDER BY
    COALESCE(NULLIF(pf.NameFormRus, N''), pf.NameForm);";

			return _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>());
		}

		public Task<DataTable> GetUsersForFormAsync(int formId)
		{
			string query = @"
;WITH FormAccessObject AS
(
    SELECT TOP (1) ofm.ObjectID
    FROM ProjectForms pf
    INNER JOIN ObjectForm ofm ON ofm.FormID = pf.ProjectFormsID
    WHERE pf.ProjectFormsID = @FormID
      AND
      (
          ofm.ObjectName = pf.NameForm
          OR ofm.ObjectType = N'CustomForm'
      )
    ORDER BY
        CASE
            WHEN ofm.ObjectName = pf.NameForm THEN 0
            WHEN ofm.ObjectType = N'CustomForm' THEN 1
            ELSE 2
        END,
        ofm.ObjectID
),
UserAccess AS
(
    SELECT
        u.UserID,
        u.UserName,
        MAX(ISNULL(ro.ModeID, 0)) AS ModeID
    FROM Users u
    INNER JOIN UserRoles ur ON ur.UserID = u.UserID
    INNER JOIN RoleObject ro ON ro.RoleID = ur.RoleID
    INNER JOIN FormAccessObject fao ON fao.ObjectID = ro.ObjectID
    GROUP BY
        u.UserID,
        u.UserName
)
SELECT
    ua.UserID,
    ua.UserName,
    ua.ModeID,
    CASE ua.ModeID
        WHEN 2 THEN N'Редактор'
        WHEN 1 THEN N'Просмотр'
        ELSE N'Нет доступа'
    END AS HasAccessForm,
    STUFF
    (
        (
            SELECT DISTINCT
                N', ' + r.RoleName + N' (' +
                CASE ISNULL(ro2.ModeID, 0)
                    WHEN 2 THEN N'Редактор'
                    WHEN 1 THEN N'Просмотр'
                    ELSE N'Нет доступа'
                END + N')'
            FROM UserRoles ur2
            INNER JOIN Roles r ON r.RoleID = ur2.RoleID
            INNER JOIN RoleObject ro2 ON ro2.RoleID = r.RoleID
            INNER JOIN FormAccessObject fao2 ON fao2.ObjectID = ro2.ObjectID
            WHERE ur2.UserID = ua.UserID
              AND ISNULL(ro2.ModeID, 0) > 0
            FOR XML PATH(''), TYPE
        ).value('.', 'nvarchar(max)'),
        1,
        2,
        N''
    ) AS RoleNames
FROM UserAccess ua
WHERE ua.ModeID > 0
ORDER BY ua.UserName;";

			return _dbHelper.ExecuteQueryAsync(
				query,
				new Dictionary<string, object>
				{
					{ "@FormID", formId }
				});
		}

		public Task<DataTable> GetObjectsForUserAsync(int formId, int userId)
		{
			string query = @"
;WITH ObjectAccess AS
(
    SELECT
        ofm.ObjectID,
        ofm.ObjectName,
        ofm.ObjectNameRus,
        ofm.ObjectType,
        ofm.FormID,
        MAX
        (
            CASE
                WHEN ur.UserID IS NULL THEN 0
                ELSE ISNULL(ro.ModeID, 0)
            END
        ) AS ModeID
    FROM ObjectForm ofm
    LEFT JOIN RoleObject ro ON ro.ObjectID = ofm.ObjectID
    LEFT JOIN UserRoles ur
        ON ur.RoleID = ro.RoleID
       AND ur.UserID = @UserID
    WHERE ofm.FormID = @FormID
    GROUP BY
        ofm.ObjectID,
        ofm.ObjectName,
        ofm.ObjectNameRus,
        ofm.ObjectType,
        ofm.FormID
)
SELECT
    oa.ObjectID,
    oa.ObjectNameRus,
    oa.ObjectName,
    oa.ObjectType,
    oa.ModeID,
    CASE oa.ModeID
        WHEN 2 THEN N'Редактор'
        WHEN 1 THEN N'Просмотр'
        ELSE N'Нет доступа'
    END AS HasAccessObject,
    STUFF
    (
        (
            SELECT DISTINCT
                N', ' + r.RoleName + N' (' +
                CASE ISNULL(ro2.ModeID, 0)
                    WHEN 2 THEN N'Редактор'
                    WHEN 1 THEN N'Просмотр'
                    ELSE N'Нет доступа'
                END + N')'
            FROM UserRoles ur2
            INNER JOIN Roles r ON r.RoleID = ur2.RoleID
            INNER JOIN RoleObject ro2 ON ro2.RoleID = r.RoleID
            WHERE ur2.UserID = @UserID
              AND ro2.ObjectID = oa.ObjectID
              AND ISNULL(ro2.ModeID, 0) > 0
            FOR XML PATH(''), TYPE
        ).value('.', 'nvarchar(max)'),
        1,
        2,
        N''
    ) AS RoleNames
FROM ObjectAccess oa
ORDER BY
    CASE
        WHEN oa.ObjectName =
        (
            SELECT TOP (1) pf.NameForm
            FROM ProjectForms pf
            WHERE pf.ProjectFormsID = @FormID
        )
        THEN 0
        ELSE 1
    END,
    COALESCE(NULLIF(oa.ObjectNameRus, N''), oa.ObjectName);";

			return _dbHelper.ExecuteQueryAsync(
				query,
				new Dictionary<string, object>
				{
					{ "@FormID", formId },
					{ "@UserID", userId }
				});
		}
	}
}
