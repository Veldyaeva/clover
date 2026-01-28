using SewingProduction.Core.Class;
namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class TestForm1
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
            components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            data_type11 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            data_type1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            id_atn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            id_acn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            name1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            readonly1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            name_rus1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            customGridControl1 = new CustomGridControlColumn();
            bindingSource1 = new System.Windows.Forms.BindingSource(components);
            gridViewTable1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            id_atn = new DevExpress.XtraGrid.Columns.GridColumn();
            name = new DevExpress.XtraGrid.Columns.GridColumn();
            name_rus = new DevExpress.XtraGrid.Columns.GridColumn();
            customGroupBox1 = new CustomGroupBox();
            customHeaderLabel1 = new CustomHeaderLabel();
            customLabel4 = new CustomLabel();
            customLabel1 = new CustomLabel();
            customTextBox1 = new CustomTextBox();
            customButton1 = new CustomButton();
            customTextBox2 = new CustomTextBox();
            customButton2 = new CustomButton();
            customLabel2 = new CustomLabel();
            customButton3 = new CustomButton();
            customLayoutControl1 = new CustomLayoutControl();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable1).BeginInit();
            customGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
            customLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checkEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            SuspendLayout();
            // 
            // bandedGridView1
            // 
            bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { data_type11 });
            bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { id_acn1, id_atn1, name1, name_rus1, data_type1, readonly1 });
            bandedGridView1.GridControl = customGridControl1;
            bandedGridView1.Name = "bandedGridView1";
            bandedGridView1.OptionsDetail.SmartDetailHeight = true;
            bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // data_type11
            // 
            data_type11.Caption = "data_type";
            data_type11.Columns.Add(data_type1);
            data_type11.Columns.Add(id_atn1);
            data_type11.Columns.Add(id_acn1);
            data_type11.Columns.Add(name1);
            data_type11.Columns.Add(readonly1);
            data_type11.Columns.Add(name_rus1);
            data_type11.Name = "data_type11";
            data_type11.VisibleIndex = 0;
            data_type11.Width = 479;
            // 
            // data_type1
            // 
            data_type1.Caption = "data_type";
            data_type1.Name = "data_type1";
            // 
            // id_atn1
            // 
            id_atn1.Caption = "id_atn";
            id_atn1.Name = "id_atn1";
            id_atn1.Visible = true;
            id_atn1.Width = 74;
            // 
            // id_acn1
            // 
            id_acn1.Caption = "id_acn";
            id_acn1.Name = "id_acn1";
            id_acn1.Visible = true;
            // 
            // name1
            // 
            name1.Caption = "name";
            name1.Name = "name1";
            name1.Visible = true;
            name1.Width = 164;
            // 
            // readonly1
            // 
            readonly1.Caption = "readonly";
            readonly1.Name = "readonly1";
            // 
            // name_rus1
            // 
            name_rus1.Caption = "name_rus";
            name_rus1.Name = "name_rus1";
            name_rus1.Visible = true;
            name_rus1.Width = 166;
            // 
            // customGridControl1
            // 
            customGridControl1.DataSource = bindingSource1;
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            gridLevelNode1.LevelTemplate = bandedGridView1;
            gridLevelNode1.RelationName = "Level1";
            customGridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1 });
            customGridControl1.Location = new System.Drawing.Point(12, 12);
            customGridControl1.MainView = gridViewTable1;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.Size = new System.Drawing.Size(639, 474);
            customGridControl1.TabIndex = 0;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTable1, bandedGridView1 });
            // 
            // gridViewTable1
            // 
            gridViewTable1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            gridViewTable1.Appearance.EvenRow.Options.UseBackColor = true;
            gridViewTable1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { id_atn, name, name_rus });
            gridViewTable1.DetailHeight = 4038;
            gridViewTable1.GridControl = customGridControl1;
            gridViewTable1.Name = "gridViewTable1";
            gridViewTable1.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            gridViewTable1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridViewTable1.OptionsDetail.AllowExpandEmptyDetails = true;
            gridViewTable1.OptionsPrint.EnableAppearanceEvenRow = true;
            gridViewTable1.OptionsView.EnableAppearanceEvenRow = true;
            gridViewTable1.OptionsView.EnableAppearanceOddRow = true;
            gridViewTable1.OptionsView.ShowGroupPanel = false;
            gridViewTable1.FocusedRowChanged += gridViewTable1_FocusedRowChanged;
            // 
            // id_atn
            // 
            id_atn.Caption = "id_atn";
            id_atn.FieldName = "id_atn";
            id_atn.Name = "id_atn";
            id_atn.Visible = true;
            id_atn.VisibleIndex = 0;
            // 
            // name
            // 
            name.Caption = "name";
            name.FieldName = "name";
            name.Name = "name";
            name.Visible = true;
            name.VisibleIndex = 1;
            // 
            // name_rus
            // 
            name_rus.Caption = "name_rus";
            name_rus.FieldName = "name_rus";
            name_rus.Name = "name_rus";
            name_rus.Visible = true;
            name_rus.VisibleIndex = 2;
            // 
            // customGroupBox1
            // 
            customGroupBox1.BackColor = System.Drawing.Color.Transparent;
            customGroupBox1.Controls.Add(customHeaderLabel1);
            customGroupBox1.Controls.Add(customLabel4);
            customGroupBox1.Controls.Add(customLabel1);
            customGroupBox1.Controls.Add(customTextBox1);
            customGroupBox1.Location = new System.Drawing.Point(657, 12);
            customGroupBox1.Name = "customGroupBox1";
            customGroupBox1.Size = new System.Drawing.Size(259, 250);
            customGroupBox1.TabIndex = 1;
            customGroupBox1.TabStop = false;
            customGroupBox1.Text = "customGroupBox1";
            // 
            // customHeaderLabel1
            // 
            customHeaderLabel1.AutoSize = true;
            customHeaderLabel1.ForeColor = System.Drawing.Color.FromArgb(72, 61, 139);
            customHeaderLabel1.Location = new System.Drawing.Point(15, 32);
            customHeaderLabel1.Name = "customHeaderLabel1";
            customHeaderLabel1.Size = new System.Drawing.Size(131, 27);
            customHeaderLabel1.TabIndex = 6;
            customHeaderLabel1.Text = "Заголовок";
            // 
            // customLabel4
            // 
            customLabel4.AutoSize = true;
            customLabel4.Font = new System.Drawing.Font("Arial", 10F);
            customLabel4.ForeColor = System.Drawing.Color.FromArgb(72, 61, 139);
            customLabel4.Location = new System.Drawing.Point(15, 135);
            customLabel4.Name = "customLabel4";
            customLabel4.Size = new System.Drawing.Size(126, 16);
            customLabel4.TabIndex = 5;
            customLabel4.Text = "размер побольше";
            // 
            // customLabel1
            // 
            customLabel1.AutoSize = true;
            customLabel1.Font = new System.Drawing.Font("Arial", 10F);
            customLabel1.ForeColor = System.Drawing.Color.Black;
            customLabel1.Location = new System.Drawing.Point(15, 99);
            customLabel1.Name = "customLabel1";
            customLabel1.Size = new System.Drawing.Size(118, 16);
            customLabel1.TabIndex = 0;
            customLabel1.Text = "обычный размер";
            // 
            // customTextBox1
            // 
            customTextBox1.BackColor = System.Drawing.Color.White;
            customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            customTextBox1.ErrorColor = System.Drawing.Color.Red;
            customTextBox1.ErrorMessage = null;
            customTextBox1.Font = new System.Drawing.Font("Arial", 10F);
            customTextBox1.ForeColor = System.Drawing.Color.Black;
            customTextBox1.Location = new System.Drawing.Point(15, 204);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.Size = new System.Drawing.Size(187, 23);
            customTextBox1.TabIndex = 3;
            customTextBox1.Text = "customTextBox1";
            // 
            // customButton1
            // 
            customButton1.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButton1.Font = new System.Drawing.Font("Arial", 10F);
            customButton1.ForeColor = System.Drawing.Color.Black;
            customButton1.Location = new System.Drawing.Point(946, 76);
            customButton1.Name = "customButton1";
            customButton1.Size = new System.Drawing.Size(187, 51);
            customButton1.TabIndex = 2;
            customButton1.Text = "изменить визибл у 2 кнопки";
            customButton1.UseVisualStyleBackColor = false;
            customButton1.Click += customButton1_Click;
            // 
            // customTextBox2
            // 
            customTextBox2.BackColor = System.Drawing.Color.White;
            customTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            customTextBox2.ErrorColor = System.Drawing.Color.Red;
            customTextBox2.ErrorMessage = null;
            customTextBox2.Font = new System.Drawing.Font("Arial", 10F);
            customTextBox2.ForeColor = System.Drawing.Color.Black;
            customTextBox2.Location = new System.Drawing.Point(946, 185);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.Size = new System.Drawing.Size(191, 23);
            customTextBox2.TabIndex = 1;
            customTextBox2.Text = "customTextBox2";
            // 
            // customButton2
            // 
            customButton2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButton2.Font = new System.Drawing.Font("Arial", 10F);
            customButton2.ForeColor = System.Drawing.Color.Black;
            customButton2.Location = new System.Drawing.Point(946, 133);
            customButton2.Name = "customButton2";
            customButton2.Size = new System.Drawing.Size(187, 30);
            customButton2.TabIndex = 2;
            customButton2.Text = "вторая кнопка";
            customButton2.UseVisualStyleBackColor = false;
            // 
            // customLabel2
            // 
            customLabel2.AutoSize = true;
            customLabel2.Font = new System.Drawing.Font("Arial", 10F);
            customLabel2.ForeColor = System.Drawing.Color.Black;
            customLabel2.Location = new System.Drawing.Point(946, 57);
            customLabel2.Name = "customLabel2";
            customLabel2.Size = new System.Drawing.Size(95, 16);
            customLabel2.TabIndex = 3;
            customLabel2.Text = "customLabel2";
            // 
            // customButton3
            // 
            customButton3.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            customButton3.Font = new System.Drawing.Font("Arial", 10F);
            customButton3.ForeColor = System.Drawing.Color.Black;
            customButton3.Location = new System.Drawing.Point(686, 289);
            customButton3.Name = "customButton3";
            customButton3.Size = new System.Drawing.Size(187, 30);
            customButton3.TabIndex = 2;
            customButton3.Text = "Пользователь";
            customButton3.UseVisualStyleBackColor = false;
            customButton3.Click += customButton3_Click;
            // 
            // customLayoutControl1
            // 
            customLayoutControl1.BackColor = System.Drawing.Color.FromArgb(240, 220, 245);
            customLayoutControl1.Controls.Add(simpleButton1);
            customLayoutControl1.Controls.Add(checkEdit1);
            customLayoutControl1.Controls.Add(progressBarControl1);
            customLayoutControl1.Font = new System.Drawing.Font("Arial", 10F);
            customLayoutControl1.ForeColor = System.Drawing.Color.FromArgb(90, 50, 120);
            customLayoutControl1.Location = new System.Drawing.Point(933, 234);
            customLayoutControl1.Name = "customLayoutControl1";
            customLayoutControl1.Root = Root;
            customLayoutControl1.Size = new System.Drawing.Size(220, 256);
            customLayoutControl1.TabIndex = 4;
            customLayoutControl1.Text = "customLayoutControl1";
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new System.Drawing.Point(12, 30);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(196, 22);
            simpleButton1.StyleController = customLayoutControl1;
            simpleButton1.TabIndex = 4;
            simpleButton1.Text = "simpleButton1";
            // 
            // checkEdit1
            // 
            checkEdit1.Location = new System.Drawing.Point(12, 56);
            checkEdit1.Name = "checkEdit1";
            checkEdit1.Properties.Caption = "checkEdit1";
            checkEdit1.Size = new System.Drawing.Size(196, 20);
            checkEdit1.StyleController = customLayoutControl1;
            checkEdit1.TabIndex = 5;
            // 
            // progressBarControl1
            // 
            progressBarControl1.Location = new System.Drawing.Point(12, 80);
            progressBarControl1.Name = "progressBarControl1";
            progressBarControl1.Size = new System.Drawing.Size(196, 18);
            progressBarControl1.StyleController = customLayoutControl1;
            progressBarControl1.TabIndex = 6;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { simpleLabelItem1, simpleSeparator1, simpleLabelItem2, layoutControlItem1, layoutControlItem2, layoutControlItem3 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(220, 256);
            Root.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.Location = new System.Drawing.Point(0, 90);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(200, 146);
            simpleLabelItem1.TextSize = new System.Drawing.Size(107, 13);
            // 
            // simpleSeparator1
            // 
            simpleSeparator1.Location = new System.Drawing.Point(0, 0);
            simpleSeparator1.Name = "simpleSeparator1";
            simpleSeparator1.Size = new System.Drawing.Size(200, 1);
            // 
            // simpleLabelItem2
            // 
            simpleLabelItem2.Location = new System.Drawing.Point(0, 1);
            simpleLabelItem2.Name = "simpleLabelItem2";
            simpleLabelItem2.Size = new System.Drawing.Size(200, 17);
            simpleLabelItem2.TextSize = new System.Drawing.Size(107, 13);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = simpleButton1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 18);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(200, 26);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = checkEdit1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(200, 24);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = progressBarControl1;
            layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(200, 22);
            layoutControlItem3.TextVisible = false;
            // 
            // TestForm1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1171, 508);
            Controls.Add(customLayoutControl1);
            Controls.Add(customLabel2);
            Controls.Add(customButton3);
            Controls.Add(customButton2);
            Controls.Add(customButton1);
            Controls.Add(customTextBox2);
            Controls.Add(customGroupBox1);
            Controls.Add(customGridControl1);
            Name = "TestForm1";
            Text = "TestForm1";
            FormClosing += ProductForm_FormClosing;
            Load += TestForm1_Load;
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTable1).EndInit();
            customGroupBox1.ResumeLayout(false);
            customGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
            customLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checkEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)progressBarControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleSeparator1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomGridControlColumn customGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTable1;
        private CustomGroupBox customGroupBox1;
        private CustomLabel customLabel1;
        private CustomTextBox customTextBox1;
        private CustomButton customButton1;
        private CustomTextBox customTextBox2;
        private CustomButton customButton2;
        private CustomLabel customLabel2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn id_atn;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn name_rus;
        private CustomLabel customLabel4;
        private CustomHeaderLabel customHeaderLabel3;
        private CustomHeaderLabel customHeaderLabel1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn id_acn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn id_atn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn name1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn name_rus1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn data_type1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn readonly1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand data_type11;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn id_acn1;
        private CustomButton customButton3;
        private CustomLayoutControl customLayoutControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}