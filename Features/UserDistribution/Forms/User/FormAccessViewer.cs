using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Forms
{
	public partial class FormAccessViewer : CustomForm
	{
		private readonly FormAccessViewerDataService _dataService;

		private bool _suppressFocusedEvents;
		private int _loadUsersVersion;
		private int _loadObjectsVersion;

		public FormAccessViewer(UserClass user) : base(user)
		{
			InitializeComponent();

			_dataService = new FormAccessViewerDataService();

			PrepareGrid(gridViewForms);
			PrepareGrid(gridViewUsers);
			PrepareGrid(gridViewObjects);
		}

		private async void FormAccessViewer_Load(object sender, EventArgs e)
		{
			await LoadFormsAsync();
		}

		private static void PrepareGrid(GridView view)
		{
			if (view == null) return;

			view.OptionsBehavior.Editable = false;
			view.OptionsBehavior.ReadOnly = true;
			view.OptionsSelection.EnableAppearanceFocusedCell = false;
			view.FocusRectStyle = DrawFocusRectStyle.RowFocus;
			view.OptionsView.ShowGroupPanel = false;
			view.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
			view.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
		}

		private async Task LoadFormsAsync()
		{
			try
			{
				SetStatus("Загрузка списка форм...");

				_suppressFocusedEvents = true;

				bindingSourceForms.DataSource = await _dataService.GetFormsAsync();
				customGridControlForms.RefreshDataSource();

				_suppressFocusedEvents = false;

				BestFitSafe(gridViewForms);

				if (gridViewForms.RowCount > 0)
				{
					gridViewForms.FocusedRowHandle = 0;
					await LoadUsersForFocusedFormAsync();
				}
				else
				{
					ClearUsersAndObjects();
					SetStatus("Формы не найдены.");
				}
			}
			catch (Exception ex)
			{
				_suppressFocusedEvents = false;
				ClearUsersAndObjects();
				ShowLoadError("Ошибка загрузки форм", ex);
			}
		}

		private async void gridViewForms_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (_suppressFocusedEvents) return;
			await LoadUsersForFocusedFormAsync();
		}

		private async void gridViewUsers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (_suppressFocusedEvents) return;
			await LoadObjectsForFocusedUserAsync();
		}

		private async Task LoadUsersForFocusedFormAsync()
		{
			int currentVersion = ++_loadUsersVersion;
			++_loadObjectsVersion;

			int? formId = GetFocusedInt(gridViewForms, "ProjectFormsID");
			string formName = GetFocusedString(gridViewForms, "NameForm");
			string formRus = GetFocusedString(gridViewForms, "NameFormRus");

			bindingSourceUsers.DataSource = null;
			bindingSourceObjects.DataSource = null;

			if (!formId.HasValue || formId.Value <= 0)
			{
				SetStatus("Форма не выбрана.");
				return;
			}

			try
			{
				SetStatus($"Загрузка пользователей для формы: {GetFormCaption(formName, formRus)}...");

				DataTable users = await _dataService.GetUsersForFormAsync(formId.Value);

				if (currentVersion != _loadUsersVersion) return;

				_suppressFocusedEvents = true;

				bindingSourceUsers.DataSource = users;
				customGridControlUsers.RefreshDataSource();

				_suppressFocusedEvents = false;

				BestFitSafe(gridViewUsers);

				if (gridViewUsers.RowCount > 0)
				{
					gridViewUsers.FocusedRowHandle = 0;
					await LoadObjectsForFocusedUserAsync();
				}
				else
				{
					bindingSourceObjects.DataSource = null;
					customGridControlObjects.RefreshDataSource();
					SetStatus($"Для формы {GetFormCaption(formName, formRus)} пользователей с доступом не найдено.");
				}
			}
			catch (Exception ex)
			{
				_suppressFocusedEvents = false;
				bindingSourceUsers.DataSource = null;
				bindingSourceObjects.DataSource = null;
				ShowLoadError("Ошибка загрузки пользователей", ex);
			}
		}

		private async Task LoadObjectsForFocusedUserAsync()
		{
			int currentVersion = ++_loadObjectsVersion;

			int? formId = GetFocusedInt(gridViewForms, "ProjectFormsID");
			int? userId = GetFocusedInt(gridViewUsers, "UserID");

			string formName = GetFocusedString(gridViewForms, "NameForm");
			string formRus = GetFocusedString(gridViewForms, "NameFormRus");
			string userName = GetFocusedString(gridViewUsers, "UserName");

			bindingSourceObjects.DataSource = null;

			if (!formId.HasValue || formId.Value <= 0)
			{
				SetStatus("Форма не выбрана.");
				return;
			}

			if (!userId.HasValue || userId.Value <= 0)
			{
				SetStatus("Пользователь не выбран.");
				return;
			}

			try
			{
				SetStatus($"Загрузка объектов формы {GetFormCaption(formName, formRus)} для пользователя {userName}...");

				DataTable objects = await _dataService.GetObjectsForUserAsync(formId.Value, userId.Value);

				if (currentVersion != _loadObjectsVersion) return;

				bindingSourceObjects.DataSource = objects;
				customGridControlObjects.RefreshDataSource();

				BestFitSafe(gridViewObjects);

				SetStatus(
					$"Форма: {GetFormCaption(formName, formRus)}. " +
					$"Пользователь: {userName}. " +
					$"Объектов: {gridViewObjects.RowCount}."
				);
			}
			catch (Exception ex)
			{
				bindingSourceObjects.DataSource = null;
				ShowLoadError("Ошибка загрузки объектов", ex);
			}
		}

		private void ClearUsersAndObjects()
		{
			bindingSourceUsers.DataSource = null;
			bindingSourceObjects.DataSource = null;

			customGridControlUsers.RefreshDataSource();
			customGridControlObjects.RefreshDataSource();
		}

		private static int? GetFocusedInt(GridView view, string fieldName)
		{
			if (view == null || view.FocusedRowHandle < 0) return null;

			object value = view.GetFocusedRowCellValue(fieldName);

			if (value == null || value == DBNull.Value) return null;

			try
			{
				return Convert.ToInt32(value);
			}
			catch
			{
				return null;
			}
		}

		private static string GetFocusedString(GridView view, string fieldName)
		{
			if (view == null || view.FocusedRowHandle < 0) return string.Empty;

			object value = view.GetFocusedRowCellValue(fieldName);

			return value == null || value == DBNull.Value
				? string.Empty
				: value.ToString();
		}

		private static string GetFormCaption(string nameForm, string nameFormRus)
		{
			if (!string.IsNullOrWhiteSpace(nameFormRus))
				return nameFormRus;

			return string.IsNullOrWhiteSpace(nameForm) ? "<без имени>" : nameForm;
		}

		private void SetStatus(string text)
		{
			labelControlStatus.Text = text ?? string.Empty;
		}

		private static void BestFitSafe(GridView view)
		{
			try
			{
				view?.BestFitColumns();
			}
			catch
			{
				// Не критично для работы формы.
			}
		}

		private static void ShowLoadError(string title, Exception ex)
		{
			MessageBox.Show(
				$"{title}:{Environment.NewLine}{ex.Message}",
				"Ошибка",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
			);
		}
	}

	
}