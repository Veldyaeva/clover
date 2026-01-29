namespace SewingProduction.Features.Tabel.Forms
{
    partial class SpisokConnect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            customGridSpisokForLinking = new SewingProduction.Core.Class.CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridSpisokInn = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokMiddleName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokDateP = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokDateU = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokUin = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokOrgName = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokGrafik = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            gridSpisokPodr1c = new DevExpress.XtraGrid.Columns.GridColumn();
            gridSpisokPodr = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customGridSpisokForLinking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            layoutControl1.Controls.Add(customGridSpisokForLinking);
            layoutControl1.Location = new System.Drawing.Point(-9, -4);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(720, 292, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(1425, 602);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // customGridSpisokForLinking
            // 
            customGridSpisokForLinking.Font = new System.Drawing.Font("Arial", 10F);
            customGridSpisokForLinking.Location = new System.Drawing.Point(24, 24);
            customGridSpisokForLinking.MainView = gridView1;
            customGridSpisokForLinking.Name = "customGridSpisokForLinking";
            customGridSpisokForLinking.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemLookUpEdit1, repositoryItemLookUpEdit2 });
            customGridSpisokForLinking.Size = new System.Drawing.Size(1377, 554);
            customGridSpisokForLinking.TabIndex = 4;
            customGridSpisokForLinking.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            customGridSpisokForLinking.Click += customGridSpisokForLinking_Click;
            // 
            // gridView1
            // 
            gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.Options.UseFont = true;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridSpisokInn, gridSpisokLastName, gridSpisokFirstName, gridSpisokMiddleName, gridSpisokDateP, gridSpisokDateU, gridSpisokUin, gridSpisokOrgName, gridSpisokGrafik, gridSpisokPodr1c, gridSpisokPodr });
            gridView1.GridControl = customGridSpisokForLinking;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.CellValueChanged += gridView1_CellValueChanged;
            // 
            // gridSpisokInn
            // 
            gridSpisokInn.Caption = "Инн";
            gridSpisokInn.Name = "gridSpisokInn";
            gridSpisokInn.Visible = true;
            gridSpisokInn.VisibleIndex = 0;
            gridSpisokInn.Width = 104;
            // 
            // gridSpisokLastName
            // 
            gridSpisokLastName.Caption = "Фамилия";
            gridSpisokLastName.Name = "gridSpisokLastName";
            gridSpisokLastName.Visible = true;
            gridSpisokLastName.VisibleIndex = 1;
            gridSpisokLastName.Width = 127;
            // 
            // gridSpisokFirstName
            // 
            gridSpisokFirstName.Caption = "Имя";
            gridSpisokFirstName.Name = "gridSpisokFirstName";
            gridSpisokFirstName.Visible = true;
            gridSpisokFirstName.VisibleIndex = 2;
            gridSpisokFirstName.Width = 82;
            // 
            // gridSpisokMiddleName
            // 
            gridSpisokMiddleName.Caption = "Отчество";
            gridSpisokMiddleName.Name = "gridSpisokMiddleName";
            gridSpisokMiddleName.Visible = true;
            gridSpisokMiddleName.VisibleIndex = 3;
            gridSpisokMiddleName.Width = 101;
            // 
            // gridSpisokDateP
            // 
            gridSpisokDateP.Caption = "Дата принятия";
            gridSpisokDateP.Name = "gridSpisokDateP";
            gridSpisokDateP.Visible = true;
            gridSpisokDateP.VisibleIndex = 4;
            gridSpisokDateP.Width = 79;
            // 
            // gridSpisokDateU
            // 
            gridSpisokDateU.Caption = "Дата увольнения";
            gridSpisokDateU.Name = "gridSpisokDateU";
            gridSpisokDateU.Visible = true;
            gridSpisokDateU.VisibleIndex = 5;
            gridSpisokDateU.Width = 91;
            // 
            // gridSpisokUin
            // 
            gridSpisokUin.Caption = "УИН";
            gridSpisokUin.Name = "gridSpisokUin";
            gridSpisokUin.Visible = true;
            gridSpisokUin.VisibleIndex = 6;
            gridSpisokUin.Width = 117;
            // 
            // gridSpisokOrgName
            // 
            gridSpisokOrgName.Caption = "Организация";
            gridSpisokOrgName.Name = "gridSpisokOrgName";
            gridSpisokOrgName.Visible = true;
            gridSpisokOrgName.VisibleIndex = 7;
            gridSpisokOrgName.Width = 154;
            // 
            // gridSpisokGrafik
            // 
            gridSpisokGrafik.Caption = "График";
            gridSpisokGrafik.ColumnEdit = repositoryItemLookUpEdit1;
            gridSpisokGrafik.Name = "gridSpisokGrafik";
            gridSpisokGrafik.Visible = true;
            gridSpisokGrafik.VisibleIndex = 9;
            gridSpisokGrafik.Width = 142;
            // 
            // repositoryItemLookUpEdit1
            // 
            repositoryItemLookUpEdit1.AutoHeight = false;
            repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            repositoryItemLookUpEdit1.NullText = "Не выбран график!";
            // 
            // gridSpisokPodr1c
            // 
            gridSpisokPodr1c.Caption = "Подразделение 1c";
            gridSpisokPodr1c.Name = "gridSpisokPodr1c";
            gridSpisokPodr1c.Visible = true;
            gridSpisokPodr1c.VisibleIndex = 8;
            gridSpisokPodr1c.Width = 189;
            // 
            // gridSpisokPodr
            // 
            gridSpisokPodr.Caption = "Подразделение";
            gridSpisokPodr.ColumnEdit = repositoryItemLookUpEdit2;
            gridSpisokPodr.Name = "gridSpisokPodr";
            gridSpisokPodr.Visible = true;
            gridSpisokPodr.VisibleIndex = 10;
            gridSpisokPodr.Width = 166;
            // 
            // repositoryItemLookUpEdit2
            // 
            repositoryItemLookUpEdit2.AutoHeight = false;
            repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            repositoryItemLookUpEdit2.NullText = "Не выбрано подразделение!";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(1425, 602);
            Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Size = new System.Drawing.Size(1405, 582);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = customGridSpisokForLinking;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(1381, 558);
            layoutControlItem1.TextVisible = false;
            // 
            // SpisokConnect
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1413, 591);
            Controls.Add(layoutControl1);
            Name = "SpisokConnect";
            Text = "Список новых сотрудников";
            Load += SpisokConnect_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)customGridSpisokForLinking).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Core.Class.CustomGridControl customGridSpisokForLinking;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokInn;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokLastName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokFirstName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokMiddleName;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokPodr1c;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokGrafik;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokDateP;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokDateU;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokUin;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokOrgName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn gridSpisokPodr;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
    }
}