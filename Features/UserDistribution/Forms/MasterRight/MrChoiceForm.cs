using System;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace SewingProduction.Features.UserDistribution.Forms.MasterRight
{
    public partial class MrChoiceForm : CustomForm
    {
        public Action<MrWizardContext> OnApplySteps { get; set; }

        private bool _isNodeChecking;

        public MrChoiceForm()
        {
            InitializeComponent();
            ConfigureTree();
            InitTree();

            treeListSteps.AfterCheckNode += treeListSteps_AfterCheckNode;
        }

        private void ConfigureTree()
        {
            treeListSteps.BeginUpdate();
            try
            {
                treeListSteps.OptionsBehavior.Editable = true;
                treeListSteps.OptionsBehavior.AllowRecursiveNodeChecking = false;
                treeListSteps.OptionsView.ShowCheckBoxes = true;
                treeListSteps.OptionsView.ShowIndicator = false;
                treeListSteps.OptionsView.ShowColumns = true;
                treeListSteps.OptionsSelection.EnableAppearanceFocusedCell = false;
                treeListSteps.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;

                if (treeListSteps.Columns.Count == 0)
                {
                    var column = treeListSteps.Columns.Add();
                    column.Caption = "Шаг";
                    column.FieldName = "Name";
                    column.Visible = true;
                    column.VisibleIndex = 0;
                }
                else
                {
                    treeListSteps.Columns[0].Caption = "Шаг";
                    treeListSteps.Columns[0].FieldName = "Name";
                    treeListSteps.Columns[0].Visible = true;
                    treeListSteps.Columns[0].VisibleIndex = 0;
                }
            }
            finally
            {
                treeListSteps.EndUpdate();
            }
        }

        private void InitTree()
        {
            treeListSteps.BeginUnboundLoad();
            try
            {
                treeListSteps.Nodes.Clear();

                TreeListNode createUser = treeListSteps.AppendNode(new object[] { "Создание пользователя" }, null);
                createUser.Tag = "CreateUser";

                TreeListNode newUser = treeListSteps.AppendNode(
                    new object[] { "Новый пользователь" }, createUser);
                newUser.Tag = "NewUser";

                TreeListNode copyUser = treeListSteps.AppendNode(
                    new object[] { "Копировать пользователя" }, createUser);
                copyUser.Tag = "CopyUser";

                TreeListNode createRole = treeListSteps.AppendNode(new object[] { "Создание роли" }, null);
                createRole.Tag = "CreateRole";

                TreeListNode newRole = treeListSteps.AppendNode(
                    new object[] { "Новая роль" }, createRole);
                newRole.Tag = "NewRole";

                TreeListNode copyRole = treeListSteps.AppendNode(
                    new object[] { "Копировать роль" }, createRole);
                copyRole.Tag = "CopyRole";

                TreeListNode adminForm = treeListSteps.AppendNode(new object[] { "Добавить элементы / формы" }, null);
                adminForm.Tag = "AdminForm";

                TreeListNode addForms = treeListSteps.AppendNode(
                    new object[] { "Добавить формы" }, adminForm);
                addForms.Tag = "AddForms";

                TreeListNode addElements = treeListSteps.AppendNode(
                    new object[] { "Добавить элементы" }, adminForm);
                addElements.Tag = "AddElements";

                TreeListNode addMenu = treeListSteps.AppendNode(
                    new object[] { "Добавить пункты меню" }, adminForm);
                addMenu.Tag = "AddMenu";

                TreeListNode assignRoleObject = treeListSteps.AppendNode(
                    new object[] { "Добавить объекты в роль" }, null);
                assignRoleObject.Tag = "AssignRoleObject";

                TreeListNode assignUserRole = treeListSteps.AppendNode(
                    new object[] { "Назначить роли пользователю" }, null);
                assignUserRole.Tag = "AssignUserRole";

                treeListSteps.ExpandAll();
            }
            finally
            {
                treeListSteps.EndUnboundLoad();
            }
        }

        private void treeListSteps_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (_isNodeChecking)
                return;

            _isNodeChecking = true;
            try
            {
                if (e.Node == null)
                    return;

                var currentNode = e.Node;
                var parentNode = currentNode.ParentNode;

                // Родительский узел
                if (parentNode == null)
                {
                    // Если сняли родителя - снять всех детей
                    if (!currentNode.Checked)
                    {
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode child in currentNode.Nodes)
                            child.Checked = false;
                    }

                    // Если поставили родителя - детей НЕ трогаем
                    return;
                }

                // Дочерний узел
                if (currentNode.Checked)
                {
                    // Родитель включается автоматически
                    parentNode.Checked = true;

                    string parentTag = Convert.ToString(parentNode.Tag);

                    // Только для некоторых родителей выбор одного дочернего пункта
                    if (IsSingleChoiceParent(parentTag))
                    {
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode sibling in parentNode.Nodes)
                        {
                            if (sibling != currentNode)
                                sibling.Checked = false;
                        }
                    }
                }
                else
                {
                    // Если сняли дочерний узел - родителя не трогаем
                    // Он может оставаться включенным сам по себе
                }
            }
            finally
            {
                _isNodeChecking = false;
            }
        }

        public MrWizardContext GetContext()
        {
            MrWizardContext context = new MrWizardContext();

            foreach (TreeListNode node in treeListSteps.GetAllCheckedNodes())
            {
                if (node.Tag == null)
                    continue;

                string tag = Convert.ToString(node.Tag);

                switch (tag)
                {
                    case "CreateUser":
                        context.NeedCreateUser = true;
                        break;

                    case "NewUser":
                        context.NeedCreateUser = true;
                        context.UserMode = MrUserMode.New;
                        break;

                    case "CopyUser":
                        context.NeedCreateUser = true;
                        context.UserMode = MrUserMode.Copy;
                        break;

                    case "CreateRole":
                        context.NeedCreateRole = true;
                        break;

                    case "NewRole":
                        context.NeedCreateRole = true;
                        context.RoleMode = MrRoleMode.New;
                        break;

                    case "CopyRole":
                        context.NeedCreateRole = true;
                        context.RoleMode = MrRoleMode.Copy;
                        break;

                    case "AdminForm":
                        context.NeedAdminForm = true;
                        break;

                    case "AddForms":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddForms = true;
                        break;

                    case "AddElements":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddElements = true;
                        break;

                    case "AddMenu":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddMenu = true;
                        break;

                    case "AssignRoleObject":
                        context.NeedAssignRoleObject = true;
                        break;

                    case "AssignUserRole":
                        context.NeedAssignUserRole = true;
                        break;
                }
            }

            return context;
        }

        private bool HasSelectedAnyStep(MrWizardContext context)
        {
            return context.NeedCreateUser
                || context.NeedCreateRole
                || context.NeedAdminForm
                || context.NeedAssignRoleObject
                || context.NeedAssignUserRole;
        }

        private void customButtonApply_Click(object sender, EventArgs e)
        {
            MrWizardContext context = GetContext();

            if (!HasSelectedAnyStep(context))
            {
                MessageBox.Show(
                    "Выберите хотя бы один шаг.",
                    "Мастер распределения прав",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            OnApplySteps?.Invoke(context);
        }

        private bool IsSingleChoiceParent(string parentTag)
        {
            switch (parentTag)
            {
                case "CreateUser":
                case "CreateRole":
                    return true;

                default:
                    return false;
            }
        }
    }

    public class MrWizardContext
    {
        public bool NeedCreateUser { get; set; }
        public bool NeedCreateRole { get; set; }
        public bool NeedAdminForm { get; set; }
        public bool NeedAssignRoleObject { get; set; }
        public bool NeedAssignUserRole { get; set; }

        public MrUserMode UserMode { get; set; } = MrUserMode.None;
        public MrRoleMode RoleMode { get; set; } = MrRoleMode.None;

        public bool AdminNeedAddForms { get; set; }
        public bool AdminNeedAddElements { get; set; }
        public bool AdminNeedAddMenu { get; set; }
    }

    public enum MrUserMode
    {
        None = 0,
        New = 1,
        Copy = 2
    }

    public enum MrRoleMode
    {
        None = 0,
        New = 1,
        Copy = 2
    }
}