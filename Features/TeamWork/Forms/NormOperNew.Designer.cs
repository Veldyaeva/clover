using SewingProduction.form.TeamWork;
using SewingProduction.Core.Class;
namespace SewingProduction.Features.TeamWork.Forms
{
    partial class NormOperNew
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            customGridControl1 = new CustomGridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colkod_o = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            colspec = new DevExpress.XtraGrid.Columns.GridColumn();
            colrazryd = new DevExpress.XtraGrid.Columns.GridColumn();
            colobor = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            colkod_ob = new DevExpress.XtraGrid.Columns.GridColumn();
            colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            colkod_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext_proizv = new DevExpress.XtraGrid.Columns.GridColumn();
            coltext_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemResourcesComboBox1 = new DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox();
            repositoryItemLookUpEdit_kodProizv = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            normoperBindingSource = new System.Windows.Forms.BindingSource(components);
            radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            customOkButton1 = new CustomOkButton();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            customAddButton = new CustomActionButton();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)customGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemResourcesComboBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit_kodProizv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)normoperBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tablePanel2).BeginInit();
            tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            SuspendLayout();
            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "gridColumn1";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "gridColumn2";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            gridColumn3.Caption = "gridColumn3";
            gridColumn3.Name = "gridColumn3";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            gridColumn4.Caption = "gridColumn4";
            gridColumn4.Name = "gridColumn4";
            gridColumn4.Visible = true;
            gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            gridColumn5.Caption = "gridColumn5";
            gridColumn5.Name = "gridColumn5";
            gridColumn5.Visible = true;
            gridColumn5.VisibleIndex = 4;
            // 
            // customGridControl1
            // 
            tablePanel1.SetColumn(customGridControl1, 0);
            customGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            customGridControl1.Font = new System.Drawing.Font("Arial", 10F);
            customGridControl1.Location = new System.Drawing.Point(13, 74);
            customGridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            customGridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            customGridControl1.MainView = gridView1;
            customGridControl1.Name = "customGridControl1";
            customGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemResourcesComboBox1, repositoryItemComboBox1, repositoryItemLookUpEdit_kodProizv });
            tablePanel1.SetRow(customGridControl1, 2);
            customGridControl1.Size = new System.Drawing.Size(922, 354);
            customGridControl1.TabIndex = 0;
            customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colkod_o, coltext, coltext_ob, colspec, colrazryd, colobor, gridColumn8, gridColumn7, colkod_ob, colsek, proizv, colkod_proizv, coltext_proizv, coltext_vyaz, gridColumn6 });
            gridView1.GridControl = customGridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(coltext, DevExpress.Data.ColumnSortOrder.Ascending) });
            // 
            // colkod_o
            // 
            colkod_o.Caption = "Код оп.";
            colkod_o.FieldName = "kod_o";
            colkod_o.Name = "colkod_o";
            colkod_o.Visible = true;
            colkod_o.VisibleIndex = 0;
            colkod_o.Width = 94;
            // 
            // coltext
            // 
            coltext.Caption = "Наименование";
            coltext.FieldName = "text";
            coltext.Name = "coltext";
            coltext.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            coltext.Visible = true;
            coltext.VisibleIndex = 1;
            coltext.Width = 351;
            // 
            // coltext_ob
            // 
            coltext_ob.Caption = "Оборудование";
            coltext_ob.FieldName = "text_ob";
            coltext_ob.Name = "coltext_ob";
            coltext_ob.Visible = true;
            coltext_ob.VisibleIndex = 2;
            coltext_ob.Width = 200;
            // 
            // colspec
            // 
            colspec.Caption = "Специальность";
            colspec.FieldName = "spec";
            colspec.Name = "colspec";
            colspec.Visible = true;
            colspec.VisibleIndex = 3;
            colspec.Width = 50;
            // 
            // colrazryd
            // 
            colrazryd.Caption = "Разряд";
            colrazryd.FieldName = "razryd";
            colrazryd.Name = "colrazryd";
            colrazryd.Visible = true;
            colrazryd.VisibleIndex = 4;
            colrazryd.Width = 49;
            // 
            // colobor
            // 
            colobor.Caption = "Оборуд";
            colobor.FieldName = "obor";
            colobor.Name = "colobor";
            colobor.Width = 48;
            // 
            // gridColumn8
            // 
            gridColumn8.Caption = "gridColumn8";
            gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn7
            // 
            gridColumn7.Caption = "gridColumn7";
            gridColumn7.Name = "gridColumn7";
            // 
            // colkod_ob
            // 
            colkod_ob.Caption = "Код оборудования";
            colkod_ob.FieldName = "kod_ob";
            colkod_ob.Name = "colkod_ob";
            colkod_ob.Width = 48;
            // 
            // colsek
            // 
            colsek.FieldName = "sek";
            colsek.Name = "colsek";
            // 
            // proizv
            // 
            proizv.Caption = "proizv";
            proizv.ColumnEdit = repositoryItemComboBox1;
            proizv.FieldName = "kod_proizv";
            proizv.Name = "proizv";
            proizv.Width = 48;
            // 
            // repositoryItemComboBox1
            // 
            repositoryItemComboBox1.AutoHeight = false;
            repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // colkod_proizv
            // 
            colkod_proizv.ColumnEdit = repositoryItemComboBox1;
            colkod_proizv.FieldName = "kod_proizv";
            colkod_proizv.Name = "colkod_proizv";
            colkod_proizv.Width = 48;
            // 
            // coltext_proizv
            // 
            coltext_proizv.Caption = "Произв.";
            coltext_proizv.FieldName = "text_proizv";
            coltext_proizv.Name = "coltext_proizv";
            coltext_proizv.Visible = true;
            coltext_proizv.VisibleIndex = 6;
            coltext_proizv.Width = 84;
            // 
            // coltext_vyaz
            // 
            coltext_vyaz.Caption = "Вязание";
            coltext_vyaz.FieldName = "text_vyaz";
            coltext_vyaz.Name = "coltext_vyaz";
            coltext_vyaz.Visible = true;
            coltext_vyaz.VisibleIndex = 5;
            coltext_vyaz.Width = 77;
            // 
            // gridColumn6
            // 
            gridColumn6.Caption = "Код";
            gridColumn6.FieldName = "kod";
            gridColumn6.Name = "gridColumn6";
            // 
            // repositoryItemResourcesComboBox1
            // 
            repositoryItemResourcesComboBox1.AutoHeight = false;
            repositoryItemResourcesComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemResourcesComboBox1.Name = "repositoryItemResourcesComboBox1";
            // 
            // repositoryItemLookUpEdit_kodProizv
            // 
            repositoryItemLookUpEdit_kodProizv.AutoHeight = false;
            repositoryItemLookUpEdit_kodProizv.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemLookUpEdit_kodProizv.DisplayMember = "text_proizv";
            repositoryItemLookUpEdit_kodProizv.Name = "repositoryItemLookUpEdit_kodProizv";
            repositoryItemLookUpEdit_kodProizv.ValueMember = "kod_proizv";
            // 
            // radioGroup1
            // 
            tablePanel2.SetColumn(radioGroup1, 2);
            radioGroup1.Location = new System.Drawing.Point(271, 12);
            radioGroup1.Name = "radioGroup1";
            radioGroup1.Properties.Columns = 4;
            radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Все"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Вязальное"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Швейное"), new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Носки") });
            tablePanel2.SetRow(radioGroup1, 0);
            radioGroup1.Size = new System.Drawing.Size(540, 21);
            radioGroup1.TabIndex = 1;
            radioGroup1.SelectedIndexChanged += radioGroup1_SelectedIndexChanged;
            // 
            // customOkButton1
            // 
   //         customOkButton1.BackColor = System.Drawing.Color.FromArgb(255, 223, 196);
            tablePanel2.SetColumn(customOkButton1, 3);
            customOkButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            customOkButton1.Font = new System.Drawing.Font("Arial", 10F);
   //         customOkButton1.ForeColor = System.Drawing.Color.FromArgb(139, 69, 19);
            customOkButton1.Location = new System.Drawing.Point(815, 12);
            customOkButton1.Name = "customOkButton1";
            tablePanel2.SetRow(customOkButton1, 0);
            customOkButton1.Size = new System.Drawing.Size(94, 21);
            customOkButton1.TabIndex = 3;
            customOkButton1.Text = "Ок";
            customOkButton1.UseVisualStyleBackColor = false;
            customOkButton1.Click += customOkButton1_Click;
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26.35F) });
            tablePanel1.Controls.Add(tablePanel2);
            tablePanel1.Controls.Add(customGridControl1);
            tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tablePanel1.Location = new System.Drawing.Point(0, 0);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Separator, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F) });
            tablePanel1.Size = new System.Drawing.Size(948, 441);
            tablePanel1.TabIndex = 4;
            tablePanel1.UseSkinIndents = true;
            // 
            // tablePanel2
            // 
            tablePanel1.SetColumn(tablePanel2, 0);
            tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 50F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26.96F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 70.36F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 12.68F) });
            tablePanel2.Controls.Add(customAddButton);
            tablePanel2.Controls.Add(searchControl1);
            tablePanel2.Controls.Add(customOkButton1);
            tablePanel2.Controls.Add(radioGroup1);
            tablePanel2.Location = new System.Drawing.Point(13, 12);
            tablePanel2.Name = "tablePanel2";
            tablePanel1.SetRow(tablePanel2, 0);
            tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F) });
            tablePanel2.Size = new System.Drawing.Size(922, 46);
            tablePanel2.TabIndex = 4;
            tablePanel2.UseSkinIndents = true;
            // 
            // customAddButton
            // 
   //         customAddButton.BackColor = System.Drawing.Color.FromArgb(173, 216, 230);
            tablePanel2.SetColumn(customAddButton, 0);
            customAddButton.Font = new System.Drawing.Font("Arial", 10F);
   //         customAddButton.ForeColor = System.Drawing.Color.FromArgb(0, 105, 148);
            customAddButton.Location = new System.Drawing.Point(13, 12);
            customAddButton.Name = "customAddButton";
            tablePanel2.SetRow(customAddButton, 0);
            customAddButton.Size = new System.Drawing.Size(46, 21);
            customAddButton.TabIndex = 5;
            customAddButton.Text = "+";
            customAddButton.UseVisualStyleBackColor = false;
            customAddButton.Visible = false;
            customAddButton.Click += customAddButton_Click;
            // 
            // searchControl1
            // 
            searchControl1.Client = customGridControl1;
            tablePanel2.SetColumn(searchControl1, 1);
            searchControl1.Location = new System.Drawing.Point(63, 12);
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = customGridControl1;
            tablePanel2.SetRow(searchControl1, 0);
            searchControl1.Size = new System.Drawing.Size(204, 20);
            searchControl1.TabIndex = 4;
            // 
            // NormOperNew
            // 
            ClientSize = new System.Drawing.Size(948, 441);
            Controls.Add(tablePanel1);
            Name = "NormOperNew";
            FormClosing += NormOperNew_FormClosing;
            Load += NormOperNew_Load;
            ((System.ComponentModel.ISupportInitialize)customGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemComboBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemResourcesComboBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemLookUpEdit_kodProizv).EndInit();
            ((System.ComponentModel.ISupportInitialize)normoperBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tablePanel2).EndInit();
            tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private CustomOkButton customOkButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_o;
        private DevExpress.XtraGrid.Columns.GridColumn coltext;
        private DevExpress.XtraGrid.Columns.GridColumn colsek;
        private DevExpress.XtraGrid.Columns.GridColumn colrazryd;
        private DevExpress.XtraGrid.Columns.GridColumn colspec;
        private DevExpress.XtraGrid.Columns.GridColumn colobor;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_ob;
        private DevExpress.XtraGrid.Columns.GridColumn colkod_proizv;
        private System.Windows.Forms.BindingSource normoperBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_ob;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_proizv;
        private DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox repositoryItemResourcesComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn proizv;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn coltext_vyaz;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private CustomActionButton customAddButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit_kodProizv;
    }
}
