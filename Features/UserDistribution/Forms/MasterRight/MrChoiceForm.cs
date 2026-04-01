using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace SewingProduction.Features.UserDistribution.Forms
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

                TreeListNode addMenu = treeListSteps.AppendNode(
                    new object[] { "Добавить пункты меню" }, adminForm);
                addMenu.Tag = "AddMenu";

                TreeListNode addForms = treeListSteps.AppendNode(
                    new object[] { "Добавить формы" }, adminForm);
                addForms.Tag = "AddForms";

                TreeListNode addElements = treeListSteps.AppendNode(
                    new object[] { "Добавить элементы" }, adminForm);
                addElements.Tag = "AddElements";

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

                    case "AddMenu":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddMenu = true;
                        break;

                    case "AddForms":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddForms = true;
                        break;

                    case "AddElements":
                        context.NeedAdminForm = true;
                        context.AdminNeedAddElements = true;
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
        public void SetContext(MrWizardContext context)
        {
            if (context == null)
                return;

            treeListSteps.BeginUpdate();
            try
            {
                foreach (TreeListNode node in treeListSteps.Nodes)
                {
                    SetCheckedRecursive(node, false);
                }

                TreeListNode createUserNode = FindNodeByTag("CreateUser");
                TreeListNode newUserNode = FindNodeByTag("NewUser");
                TreeListNode copyUserNode = FindNodeByTag("CopyUser");

                TreeListNode createRoleNode = FindNodeByTag("CreateRole");
                TreeListNode newRoleNode = FindNodeByTag("NewRole");
                TreeListNode copyRoleNode = FindNodeByTag("CopyRole");

                TreeListNode adminFormNode = FindNodeByTag("AdminForm");
                TreeListNode addFormsNode = FindNodeByTag("AddForms");
                TreeListNode addElementsNode = FindNodeByTag("AddElements");
                TreeListNode addMenuNode = FindNodeByTag("AddMenu");

                TreeListNode assignRoleObjectNode = FindNodeByTag("AssignRoleObject");
                TreeListNode assignUserRoleNode = FindNodeByTag("AssignUserRole");

                if (context.NeedCreateUser && createUserNode != null)
                    createUserNode.Checked = true;

                if (context.UserMode == MrUserMode.New && newUserNode != null)
                    newUserNode.Checked = true;

                if (context.UserMode == MrUserMode.Copy && copyUserNode != null)
                    copyUserNode.Checked = true;

                if (context.NeedCreateRole && createRoleNode != null)
                    createRoleNode.Checked = true;

                if (context.RoleMode == MrRoleMode.New && newRoleNode != null)
                    newRoleNode.Checked = true;

                if (context.RoleMode == MrRoleMode.Copy && copyRoleNode != null)
                    copyRoleNode.Checked = true;

                if (context.NeedAdminForm && adminFormNode != null)
                    adminFormNode.Checked = true;

                if (context.AdminNeedAddMenu && addMenuNode != null)
                    addMenuNode.Checked = true;

                if (context.AdminNeedAddForms && addFormsNode != null)
                    addFormsNode.Checked = true;

                if (context.AdminNeedAddElements && addElementsNode != null)
                    addElementsNode.Checked = true;

                if (context.NeedAssignRoleObject && assignRoleObjectNode != null)
                    assignRoleObjectNode.Checked = true;

                if (context.NeedAssignUserRole && assignUserRoleNode != null)
                    assignUserRoleNode.Checked = true;
            }
            finally
            {
                treeListSteps.EndUpdate();
            }
        }
        private TreeListNode FindNodeByTag(string tag)
        {
            foreach (TreeListNode node in treeListSteps.Nodes)
            {
                TreeListNode found = FindNodeByTagRecursive(node, tag);
                if (found != null)
                    return found;
            }

            return null;
        }

        private TreeListNode FindNodeByTagRecursive(TreeListNode node, string tag)
        {
            if (node == null)
                return null;

            if (Convert.ToString(node.Tag) == tag)
                return node;

            foreach (TreeListNode child in node.Nodes)
            {
                TreeListNode found = FindNodeByTagRecursive(child, tag);
                if (found != null)
                    return found;
            }

            return null;
        }

        private void SetCheckedRecursive(TreeListNode node, bool isChecked)
        {
            if (node == null)
                return;

            node.Checked = isChecked;

            foreach (TreeListNode child in node.Nodes)
            {
                SetCheckedRecursive(child, isChecked);
            }
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
        //public List<MrAdminMode> AdminMode { get; set; } = new List<MrAdminMode>();

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

    //public enum MrAdminMode
    //{
    //    None = 0,
    //    Menu = 1,
    //    Form = 2,
    //    Object = 3
    //}
}