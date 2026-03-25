using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.UserDistribution.Forms.MasterRight
{
    public partial class MrMainForm : CustomForm
    {
        private readonly UserClass _user;

        private Form _currentInnerForm;
        private List<MrStepItem> _steps = new List<MrStepItem>();
        private int _currentStepIndex = -1;
        private MrWizardContext _context = new MrWizardContext();

        public MrMainForm(UserClass user) : base(user)
        {
            _user = user;

            InitializeComponent();

            btnBack.Click += btnBack_Click;
            btnNext.Click += btnNext_Click;
            btnFinish.Click += btnFinish_Click;
            customButtonCancel.Click += btnCancel_Click;
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

        private void RefreshStepList()
        {
            listBoxControlSteps.Items.Clear();

            for (int i = 0; i < _steps.Count; i++)
            {
                MrStepItem step = _steps[i];

                string markerCurrent = i == _currentStepIndex ? "►" : " ";
                string markerDone = step.IsDone ? "[x]" : "[ ]";

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
        }

        private Form CreateStepForm(MrStepType stepType)
        {
            switch (stepType)
            {
                case MrStepType.Choice:
                    MrChoiceForm choiceForm = new MrChoiceForm();
                    choiceForm.OnApplySteps = ApplyChoiceSteps;
                    return choiceForm;

                case MrStepType.CreateUser:
                    return CreateUserStepForm();

                case MrStepType.CreateRole:
                    return CreateRoleStepForm();

                case MrStepType.AdminForm:
                    return new AdminForm(_user);

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

            switch (_context.RoleMode)
            {
                case MrRoleMode.New:
                    // form.StartCreateNewRole();
                    break;

                case MrRoleMode.Copy:
                    // form.StartCopyRole();
                    break;

                case MrRoleMode.None:
                default:
                    break;
            }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
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
