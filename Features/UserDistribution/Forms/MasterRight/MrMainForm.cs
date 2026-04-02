using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Charts.Native;
using DevExpress.XtraEditors;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class MrMainForm : CustomForm
    {
        private readonly UserClass _user;

        private Form _currentInnerForm;
        private List<MrStepItem> _steps = new List<MrStepItem>();
        private string _help = null;
        private int _currentStepIndex = -1;
        private MrWizardContext _context = new MrWizardContext();
        public FormManager _formManager;

        public MrMainForm(UserClass user) : base(user)
        {
            _user = user;
            _formManager = new FormManager(this, barManager1, _user);
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            BuildInitialSteps();
            ShowCurrentStep();
        }

        #region Steps

        private void BuildInitialSteps()
        {
            _steps = new List<MrStepItem>
            {
                new MrStepItem
                {
                    StepType = MrStepType.Choice,
                    Caption = "Выбор шагов",
                    IsDone = false
                }
            };

            _currentStepIndex = 0;
            RefreshStepList();
            UpdateButtons();
        }

        private void BuildStepsFromContext()
        {
            _steps = new List<MrStepItem>
            {
                new MrStepItem
                {
                    StepType = MrStepType.Choice,
                    Caption = "Выбор шагов",
                    IsDone = true
                }
            };

            if (_context.NeedCreateUser)
            {
                _steps.Add(new MrStepItem
                {
                    StepType = MrStepType.CreateUser,
                    Caption = _context.UserMode == MrUserMode.Copy
                        ? "Создание пользователя (копирование)"
                        : "Создание пользователя"
                });
            }

            if (_context.NeedCreateRole)
            {
                _steps.Add(new MrStepItem
                {
                    StepType = MrStepType.CreateRole,
                    Caption = _context.RoleMode == MrRoleMode.Copy
                        ? "Создание роли (копирование)"
                        : "Создание роли"
                });
            }

            if (_context.NeedAdminForm)
            {
                _steps.Add(new MrStepItem
                {
                    StepType = MrStepType.AdminForm,
                    Caption = "Добавить формы / элементы / меню"
                });
            }

            if (_context.NeedAssignRoleObject)
            {
                _steps.Add(new MrStepItem
                {
                    StepType = MrStepType.AssignRoleObject,
                    Caption = "Добавить элементы в роль"
                });
            }

            if (_context.NeedAssignUserRole)
            {
                _steps.Add(new MrStepItem
                {
                    StepType = MrStepType.AssignUserRole,
                    Caption = "Назначить роль пользователю"
                });
            }

            RefreshStepList();
            UpdateButtons();
        }

        private void BuildHelpFromContext()
        {
            if (_currentStepIndex < 0 || _currentStepIndex >= _steps.Count)
            {
                customTextBoxHelp.Text = string.Empty;
                return;
            }

            MrStepItem currentStep = _steps[_currentStepIndex];
            List<string> lines = new List<string>();

            switch (currentStep.StepType)
            {
                case MrStepType.Choice:
                    lines.Add("Отметьте галочками нужные действия мастера.");
                    break;

                case MrStepType.CreateUser:
                    if (_context.UserMode == MrUserMode.New)
                    {
                        lines.Add("Шаг создания пользователя.");
                        lines.Add("Заполните данные пользователя и сохраните запись.");
                    }
                    else if (_context.UserMode == MrUserMode.Copy)
                    {
                        lines.Add("Шаг копирования пользователя.");
                        lines.Add("Выберите пользователя, нажмите 'Копировать пользователя', затем при необходимости исправьте данные и сохраните запись.");
                    }
                    else
                    {
                        lines.Add("Шаг работы с пользователями.");
                    }
                    break;

                case MrStepType.CreateRole:
                    if (_context.RoleMode == MrRoleMode.New)
                    {
                        lines.Add("Шаг создания роли.");
                        lines.Add("Заполните данные роли и сохраните запись.");
                    }
                    else if (_context.RoleMode == MrRoleMode.Copy)
                    {
                        lines.Add("Шаг копирования роли.");
                        lines.Add("Выберите роль, нажмите 'Копировать роль', затем при необходимости исправьте данные и сохраните запись.");
                    }
                    else
                    {
                        lines.Add("Шаг работы с ролями.");
                    }
                    break;

                case MrStepType.AdminForm:
                    string skan = "";
                    if (_context.AdminNeedAddMenu)
                    {
                        skan += "Меню отсканировано, ";
                    }
                    if (_context.AdminNeedAddForms)
                    {
                        skan += "Формы отсканированы";
                    }
                    lines.Add(skan);
                    if (_context.AdminNeedAddElements)
                    {
                        lines.Add("Добавьте в базу формы проекта, элементы управления и пункты меню.");
                    }
                    break;

                case MrStepType.AssignRoleObject:
                    lines.Add("Шаг добавления элементов в роль.");
                    lines.Add("Выберите роль и свяжите с ней нужные формы, элементы или пункты меню.");
                    break;

                case MrStepType.AssignUserRole:
                    lines.Add("Шаг назначения роли пользователю.");
                    lines.Add("Выберите пользователя и назначьте ему нужную роль.");
                    break;

                default:
                    lines.Add("Текущий шаг мастера.");
                    break;
            }

            if (_currentStepIndex < _steps.Count - 1)
                lines.Add("Для перехода дальше нажмите 'Далее'.");

            if (_steps.Count > 1 && _currentStepIndex == _steps.Count - 1)
                lines.Add("Для завершения мастера нажмите 'Завершить'.");

            customTextBoxHelp.Text = string.Join(Environment.NewLine, lines);
        }
        private void RefreshStepList()
        {
            listBoxControlSteps.Items.Clear();

            for (int i = 0; i < _steps.Count; i++)
            {
                MrStepItem step = _steps[i];

                string markerCurrent = i == _currentStepIndex ? "►" : " ";
                string markerDone = step.IsDone ? "[V]" : "[ ]";

                listBoxControlSteps.Items.Add($"{markerCurrent} {markerDone} {step.Caption}");
            }
        }

        private void ShowCurrentStep()
        {
            if (_currentStepIndex < 0 || _currentStepIndex >= _steps.Count)
                return;

            MrStepItem step = _steps[_currentStepIndex];
            Form form = CreateStepForm(step.StepType);

            ShowInnerForm(form);
            RefreshStepList();
            UpdateButtons();
            BuildHelpFromContext();
        }

        private Form CreateStepForm(MrStepType stepType)
        {
            switch (stepType)
            {
                case MrStepType.Choice:
                    MrChoiceForm choiceForm = new MrChoiceForm();
                    choiceForm.OnApplySteps = ApplyChoiceSteps;
                    choiceForm.SetContext(_context);
                    return choiceForm;

                case MrStepType.CreateUser:
                    return CreateUserStepForm();

                case MrStepType.CreateRole:
                    return CreateRoleStepForm();

                case MrStepType.AdminForm:
                    return CreateAdminStepForm();

                case MrStepType.AssignRoleObject:
                    return new RoleFormObject(_user);

                case MrStepType.AssignUserRole:
                    return new UserRole(_user);

                default:
                    XtraMessageBox.Show("Неизвестный шаг мастера.");
                    return new XtraForm();
            }
        }
        private Form CreateUserStepForm()
        {
            AllUser form = new AllUser(_user);
            form.StartMode = _context.UserMode;
            return form;
        }

        private Form CreateRoleStepForm()
        {
            AllRole form = new AllRole(_user);
            form.StartMode = _context.RoleMode;
            return form;
        }

        private Form CreateAdminStepForm()
        {
            AdminForm form = new AdminForm(_user);
            form.StartMode = _context;
            return form;
        }


        private void ShowInnerForm(Form form)
        {
            if (_currentInnerForm != null)
            {
                panelContent.Controls.Remove(_currentInnerForm);
                _currentInnerForm.Close();
                _currentInnerForm.Dispose();
                _currentInnerForm = null;
            }

            _currentInnerForm = form;
            _currentInnerForm.TopLevel = false;
            _currentInnerForm.FormBorderStyle = FormBorderStyle.None;
            _currentInnerForm.Dock = DockStyle.Fill;

            panelContent.Controls.Clear();
            panelContent.Controls.Add(_currentInnerForm);

            _currentInnerForm.Show();
        }

        private void ApplyChoiceSteps(MrWizardContext context)
        {
            _context = context;

            BuildStepsFromContext();

            if (_steps.Count > 1)
            {
                _currentStepIndex = 1;
                ShowCurrentStep();
            }
            else
            {
                XtraMessageBox.Show("Не выбран ни один шаг.");
            }
        }

        #endregion

        #region Buttons

        private void UpdateButtons()
        {
            btnBack.Enabled = _currentStepIndex > 0;

            btnNext.Enabled = _currentStepIndex >= 0 && _currentStepIndex < _steps.Count;
            btnFinish.Enabled = _steps.Count > 1 && _currentStepIndex == _steps.Count - 1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_currentStepIndex <= 0)
                return;

            _currentStepIndex--;
            ShowCurrentStep();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentStepIndex < 0)
                return;

            if (_steps[_currentStepIndex].StepType == MrStepType.Choice)
            {
                MrChoiceForm choiceForm = _currentInnerForm as MrChoiceForm;
                if (choiceForm == null)
                    return;

                MrWizardContext context = choiceForm.GetContext();

                if (!context.NeedCreateUser
                    && !context.NeedCreateRole
                    && !context.NeedAdminForm
                    && !context.NeedAssignRoleObject
                    && !context.NeedAssignUserRole)
                {
                    XtraMessageBox.Show("Выберите хотя бы один шаг.");
                    return;
                }

                ApplyChoiceSteps(context);
                return;
            }

            if (_currentStepIndex >= _steps.Count - 1)
                return;

            _steps[_currentStepIndex].IsDone = true;
            _currentStepIndex++;
            ShowCurrentStep();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


        #endregion

        private void MrMainForm_Load(object sender, EventArgs e)
        {

        }

        private void barBtnQuestion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showHelpForm();
        }
        private void showHelpForm(string filePath = null)
        {
            string helpPath = filePath;

            if (string.IsNullOrWhiteSpace(helpPath))
            {
                // Для мастера приоритет — текущая вложенная форма
                if (_currentInnerForm != null)
                    helpPath = _formManager.GetHelpFilePath(_currentInnerForm);
                else
                    helpPath = _formManager.GetHelpFilePath(this);
            }

            var helpForm = new HelpForm(this._formManager, helpPath);
            helpForm.ShowDialog();
        }

        private void MrMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                showHelpForm();
            }
        }
    }

    public enum MrStepType
    {
        Choice = 0,
        CreateUser = 1,
        CreateRole = 2,
        AdminForm = 3,
        AssignRoleObject = 4,
        AssignUserRole = 5
    }

    public class MrStepItem
    {
        public MrStepType StepType { get; set; }
        public string Caption { get; set; }
        public bool IsDone { get; set; }
    }
}
