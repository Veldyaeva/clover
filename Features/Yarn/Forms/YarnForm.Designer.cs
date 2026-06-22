using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Core.Class;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.Features.Yarn.Forms
{
    internal partial class YarnForm
    {
        private IContainer components = null;

        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraLayout.LayoutControlGroup grpInputs;
        private DevExpress.XtraLayout.LayoutControlGroup grpGrid;

        private CustomTextBoxEx txtNakl;
        private CustomTextBoxEx txtArticul;
        private CustomTextBoxEx txtSebUpr;
        private CustomTextBoxEx txtSebDok;
        private CustomSimpleButton btnObnovit;
        private CustomSimpleButton btnCalc;
        private CustomGridControl gridControl;
        private GridView gridView;
        private BindingSource bindingSource;

        private GridColumn colNakl;
        private GridColumn colArticul;
        private GridColumn colZvet;
        private GridColumn colSebTM;

        private DevExpress.XtraLayout.LayoutControlItem lciNakl;
        private DevExpress.XtraLayout.LayoutControlItem lciArticul;
        private DevExpress.XtraLayout.LayoutControlItem lciSebUpr;
        private DevExpress.XtraLayout.LayoutControlItem lciSebDok;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnCalc;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnObnovit;
        private DevExpress.XtraLayout.LayoutControlItem lciGrid;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpace1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            txtNakl = new CustomTextBoxEx();
            txtArticul = new CustomTextBoxEx();
            txtSebUpr = new CustomTextBoxEx();
            txtSebDok = new CustomTextBoxEx();
            btnCalc = new CustomSimpleButton();
            btnObnovit = new CustomSimpleButton();
            gridControl = new CustomGridControl();
            bindingSource = new BindingSource(components);
            gridView = new GridView();
            colNakl = new GridColumn();
            colArticul = new GridColumn();
            colZvet = new GridColumn();
            colSebTM = new GridColumn();
            layoutControl = new DevExpress.XtraLayout.LayoutControl();
            rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            grpInputs = new DevExpress.XtraLayout.LayoutControlGroup();
            grpGrid = new DevExpress.XtraLayout.LayoutControlGroup();
            lciNakl = new DevExpress.XtraLayout.LayoutControlItem();
            lciArticul = new DevExpress.XtraLayout.LayoutControlItem();
            lciSebUpr = new DevExpress.XtraLayout.LayoutControlItem();
            lciSebDok = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnCalc = new DevExpress.XtraLayout.LayoutControlItem();
            lciBtnObnovit = new DevExpress.XtraLayout.LayoutControlItem();
            lciGrid = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpace1 = new DevExpress.XtraLayout.EmptySpaceItem();

            ((ISupportInitialize)txtNakl.Properties).BeginInit();
            ((ISupportInitialize)txtArticul.Properties).BeginInit();
            ((ISupportInitialize)txtSebUpr.Properties).BeginInit();
            ((ISupportInitialize)txtSebDok.Properties).BeginInit();
            ((ISupportInitialize)gridControl).BeginInit();
            ((ISupportInitialize)bindingSource).BeginInit();
            ((ISupportInitialize)gridView).BeginInit();
            ((ISupportInitialize)layoutControl).BeginInit();
            layoutControl.SuspendLayout();
            ((ISupportInitialize)rootGroup).BeginInit();
            ((ISupportInitialize)grpInputs).BeginInit();
            ((ISupportInitialize)grpGrid).BeginInit();
            ((ISupportInitialize)lciNakl).BeginInit();
            ((ISupportInitialize)lciArticul).BeginInit();
            ((ISupportInitialize)lciSebUpr).BeginInit();
            ((ISupportInitialize)lciSebDok).BeginInit();
            ((ISupportInitialize)lciBtnCalc).BeginInit();
            ((ISupportInitialize)lciBtnObnovit).BeginInit();
            ((ISupportInitialize)lciGrid).BeginInit();
            ((ISupportInitialize)emptySpace1).BeginInit();
            SuspendLayout();
            //
            // txtNakl
            //
            txtNakl.Location = new Point(179, 45);
            txtNakl.Name = "txtNakl";
            txtNakl.ObjectName = null;
            txtNakl.Properties.Appearance.Font = new Font("Arial", 10F);
            txtNakl.Properties.Appearance.Options.UseFont = true;
            txtNakl.Size = new Size(200, 22);
            txtNakl.TabIndex = 0;
            txtNakl.KeyDown += txtNakl_KeyDown;
            //
            // txtArticul
            //
            txtArticul.Location = new Point(179, 71);
            txtArticul.Name = "txtArticul";
            txtArticul.ObjectName = null;
            txtArticul.Properties.Appearance.Font = new Font("Arial", 10F);
            txtArticul.Properties.Appearance.Options.UseFont = true;
            txtArticul.Properties.ReadOnly = true;
            txtArticul.Size = new Size(200, 22);
            txtArticul.TabIndex = 1;
            //
            // txtSebUpr
            //
            txtSebUpr.Location = new Point(179, 97);
            txtSebUpr.Name = "txtSebUpr";
            txtSebUpr.ObjectName = null;
            txtSebUpr.Properties.Appearance.Font = new Font("Arial", 10F);
            txtSebUpr.Properties.Appearance.Options.UseFont = true;
            txtSebUpr.Properties.ReadOnly = true;
            txtSebUpr.Size = new Size(200, 22);
            txtSebUpr.TabIndex = 2;
            //
            // txtSebDok
            //
            txtSebDok.Location = new Point(179, 123);
            txtSebDok.Name = "txtSebDok";
            txtSebDok.ObjectName = null;
            txtSebDok.Properties.Appearance.Font = new Font("Arial", 10F);
            txtSebDok.Properties.Appearance.Options.UseFont = true;
            txtSebDok.Properties.ReadOnly = true;
            txtSebDok.Size = new Size(200, 22);
            txtSebDok.TabIndex = 3;
            //
            // btnCalc
            //
            btnCalc.Location = new Point(390, 97);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(120, 22);
            btnCalc.TabIndex = 4;
            btnCalc.Text = "рассчитать";
            btnCalc.Click += btnCalc_Click;
            //
            // btnObnovit
            //
            btnObnovit.Location = new Point(5, 149);
            btnObnovit.Name = "btnObnovit";
            btnObnovit.Size = new Size(150, 30);
            btnObnovit.TabIndex = 4;
            btnObnovit.Text = "Обновить";
            btnObnovit.Click += btnObnovit_Click;
            //
            // gridControl
            //
            gridControl.DataSource = bindingSource;
            gridControl.Font = new Font("Arial", 10F);
            gridControl.Location = new Point(5, 210);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl";
            gridControl.Size = new Size(770, 400);
            gridControl.TabIndex = 5;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            //
            // gridView
            //
            gridView.Columns.AddRange(new GridColumn[] { colNakl, colArticul, colZvet, colSebTM });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ColumnAutoWidth = true;
            //
            // colNakl
            //
            colNakl.Caption = "Карта";
            colNakl.FieldName = "nakl";
            colNakl.Name = "colNakl";
            colNakl.Visible = true;
            colNakl.VisibleIndex = 0;
            colNakl.Width = 120;
            //
            // colArticul
            //
            colArticul.Caption = "Артикул";
            colArticul.FieldName = "t_articul";
            colArticul.Name = "colArticul";
            colArticul.Visible = true;
            colArticul.VisibleIndex = 1;
            colArticul.Width = 200;
            //
            // colZvet
            //
            colZvet.Caption = "Цвет";
            colZvet.FieldName = "zvet";
            colZvet.Name = "colZvet";
            colZvet.Visible = true;
            colZvet.VisibleIndex = 2;
            colZvet.Width = 150;
            //
            // colSebTM
            //
            colSebTM.Caption = "Себестоимость";
            colSebTM.FieldName = "seb_t_m";
            colSebTM.Name = "colSebTM";
            colSebTM.Visible = true;
            colSebTM.VisibleIndex = 3;
            colSebTM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSebTM.DisplayFormat.FormatString = "N3";
            colSebTM.Width = 150;
            //
            // layoutControl
            //
            layoutControl.Controls.Add(txtNakl);
            layoutControl.Controls.Add(txtArticul);
            layoutControl.Controls.Add(txtSebUpr);
            layoutControl.Controls.Add(txtSebDok);
            layoutControl.Controls.Add(btnCalc);
            layoutControl.Controls.Add(btnObnovit);
            layoutControl.Controls.Add(gridControl);
            layoutControl.Dock = DockStyle.Fill;
            layoutControl.Location = new Point(0, 0);
            layoutControl.Name = "layoutControl";
            layoutControl.Root = rootGroup;
            layoutControl.Size = new Size(800, 650);
            layoutControl.TabIndex = 0;
            //
            // rootGroup
            //
            rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            rootGroup.GroupBordersVisible = false;
            rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { grpInputs, grpGrid });
            rootGroup.Name = "rootGroup";
            rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            rootGroup.Size = new Size(800, 650);
            //
            // grpInputs
            //
            grpInputs.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                lciNakl, lciArticul, lciSebUpr, lciBtnCalc, lciSebDok, lciBtnObnovit, emptySpace1 });
            grpInputs.Location = new Point(0, 0);
            grpInputs.Name = "grpInputs";
            grpInputs.Size = new Size(796, 200);
            grpInputs.Text = "Пряжа";
            //
            // grpGrid
            //
            grpGrid.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { lciGrid });
            grpGrid.Location = new Point(0, 200);
            grpGrid.Name = "grpGrid";
            grpGrid.Size = new Size(796, 446);
            grpGrid.Text = "Данные карты";
            //
            // lciNakl
            //
            lciNakl.Control = txtNakl;
            lciNakl.Location = new Point(0, 0);
            lciNakl.Name = "lciNakl";
            lciNakl.Size = new Size(380, 26);
            lciNakl.Text = "Введите № карты пряжи";
            lciNakl.TextSize = new Size(170, 13);
            //
            // lciArticul
            //
            lciArticul.Control = txtArticul;
            lciArticul.Location = new Point(0, 26);
            lciArticul.Name = "lciArticul";
            lciArticul.Size = new Size(380, 26);
            lciArticul.Text = "Артикул";
            lciArticul.TextSize = new Size(170, 13);
            //
            // lciSebUpr
            //
            lciSebUpr.Control = txtSebUpr;
            lciSebUpr.Location = new Point(0, 52);
            lciSebUpr.Name = "lciSebUpr";
            lciSebUpr.Size = new Size(380, 26);
            lciSebUpr.Text = "Себ-ть упр.";
            lciSebUpr.TextSize = new Size(170, 13);
            //
            // lciSebDok
            //
            lciSebDok.Control = txtSebDok;
            lciSebDok.Location = new Point(0, 78);
            lciSebDok.Name = "lciSebDok";
            lciSebDok.Size = new Size(380, 26);
            lciSebDok.Text = "Себ-ть по документам";
            lciSebDok.TextSize = new Size(170, 13);
            //
            // lciBtnCalc
            //
            lciBtnCalc.Control = btnCalc;
            lciBtnCalc.Location = new Point(380, 52);
            lciBtnCalc.Name = "lciBtnCalc";
            lciBtnCalc.Size = new Size(130, 26);
            lciBtnCalc.TextVisible = false;
            //
            // lciBtnObnovit
            //
            lciBtnObnovit.Control = btnObnovit;
            lciBtnObnovit.Location = new Point(0, 104);
            lciBtnObnovit.Name = "lciBtnObnovit";
            lciBtnObnovit.Size = new Size(160, 40);
            lciBtnObnovit.TextVisible = false;
            //
            // emptySpace1
            //
            emptySpace1.AllowHotTrack = false;
            emptySpace1.Location = new Point(160, 104);
            emptySpace1.Name = "emptySpace1";
            emptySpace1.Size = new Size(620, 40);
            //
            // lciGrid
            //
            lciGrid.Control = gridControl;
            lciGrid.Location = new Point(0, 0);
            lciGrid.Name = "lciGrid";
            lciGrid.Size = new Size(772, 400);
            lciGrid.TextVisible = false;
            //
            // YarnForm
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 650);
            Controls.Add(layoutControl);
            Name = "YarnForm";
            Text = "Пряжа";
            Load += YarnForm_Load;
            ((ISupportInitialize)txtNakl.Properties).EndInit();
            ((ISupportInitialize)txtArticul.Properties).EndInit();
            ((ISupportInitialize)txtSebUpr.Properties).EndInit();
            ((ISupportInitialize)txtSebDok.Properties).EndInit();
            ((ISupportInitialize)gridControl).EndInit();
            ((ISupportInitialize)bindingSource).EndInit();
            ((ISupportInitialize)gridView).EndInit();
            ((ISupportInitialize)layoutControl).EndInit();
            layoutControl.ResumeLayout(false);
            ((ISupportInitialize)rootGroup).EndInit();
            ((ISupportInitialize)grpInputs).EndInit();
            ((ISupportInitialize)grpGrid).EndInit();
            ((ISupportInitialize)lciNakl).EndInit();
            ((ISupportInitialize)lciArticul).EndInit();
            ((ISupportInitialize)lciSebUpr).EndInit();
            ((ISupportInitialize)lciSebDok).EndInit();
            ((ISupportInitialize)lciBtnCalc).EndInit();
            ((ISupportInitialize)lciBtnObnovit).EndInit();
            ((ISupportInitialize)lciGrid).EndInit();
            ((ISupportInitialize)emptySpace1).EndInit();
            ResumeLayout(false);
        }
    }
}
