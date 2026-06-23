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
        private SplitContainer splitMain;

        // == LEFT: Полотно ==
        private DevExpress.XtraLayout.LayoutControl lcFabric;
        private DevExpress.XtraLayout.LayoutControlGroup lcgFabric;
        private DevExpress.XtraLayout.LayoutControlGroup lcgSpreading;
        private CustomTextBoxEx txtFabricNakl, txtFabricPrice, txtFabricQty;
        private CustomTextBoxEx txtFabricConsumption, txtFabricCost, txtFabricNorm;
        private CustomSimpleButton btnFabricReplace;
        private CustomGridControl gridSpreading;
        private GridView gvSpreading;
        private BindingSource bsSpreading;

        // == RIGHT: Пряжа ==
        private DevExpress.XtraLayout.LayoutControl lcYarn;
        private DevExpress.XtraLayout.LayoutControlGroup lcgYarn;
        private DevExpress.XtraLayout.LayoutControlGroup lcgColorSearch;
        private CustomTextBoxEx txtNakl, txtArticul, txtSebUpr, txtSebDok, txtColorSearch;
        private CustomSimpleButton btnCalc, btnObnovit, btnHistory;
        private CustomGridControl gridYarnData;
        private GridView gvYarnData;
        private BindingSource bsYarnData;
        private CustomGridControl gridColorResult;
        private GridView gvColorResult;
        private BindingSource bsColorResult;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            splitMain = new SplitContainer();
            ((ISupportInitialize)splitMain).BeginInit();
            splitMain.SuspendLayout();

            BuildFabricPanel();
            BuildYarnPanel();

            // splitMain
            splitMain.Dock = DockStyle.Fill;
            splitMain.Name = "splitMain";
            splitMain.SplitterWidth = 6;
            splitMain.Panel1.Controls.Add(lcFabric);
            splitMain.Panel2.Controls.Add(lcYarn);
            splitMain.Panel1MinSize = 300;
            splitMain.Panel2MinSize = 400;

            // Form
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 750);
            MinimumSize = new Size(1000, 550);
            Controls.Add(splitMain);
            Name = "YarnForm";
            Text = "Просчет стоимости полотна и пряжи";
            Load += YarnForm_Load;

            ((ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void BuildFabricPanel()
        {
            txtFabricNakl = Tb("txtFabricNakl");
            txtFabricNakl.KeyDown += txtFabricNakl_KeyDown;
            txtFabricPrice = Tb("txtFabricPrice");
            txtFabricQty = Tb("txtFabricQty", ro: true);
            txtFabricConsumption = Tb("txtFabricConsumption", ro: true);
            txtFabricCost = Tb("txtFabricCost", ro: true, bold: true);
            txtFabricNorm = Tb("txtFabricNorm", ro: true);
            btnFabricReplace = Btn("btnFabricReplace", "заменить");
            btnFabricReplace.Click += btnFabricReplace_Click;

            bsSpreading = new BindingSource(components);
            gridSpreading = new CustomGridControl();
            gvSpreading = new GridView();
            ((ISupportInitialize)gridSpreading).BeginInit();
            ((ISupportInitialize)bsSpreading).BeginInit();
            ((ISupportInitialize)gvSpreading).BeginInit();

            gridSpreading.DataSource = bsSpreading;
            gridSpreading.Font = new Font("Arial", 9F);
            gridSpreading.MainView = gvSpreading;
            gridSpreading.Name = "gridSpreading";
            gridSpreading.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvSpreading });

            var spCols = new[]
            {
                Col("v", "v", 30), Col("карта", "karta", 65), Col("дата", "data", 75, "dd.MM.yy", DevExpress.Utils.FormatType.DateTime),
                Col("ткань", "tkan", 70), Col("себ-ть", "seb", 60, "N2"), Col("вид тк", "vid_tk", 50),
                Col("кол(м)", "kol_m", 60, "N3"), Col("расх ост", "rash_ost", 60, "N3"),
                Col("нер ост", "nerash_ost", 60, "N3"), Col("к/л", "kl", 35, "N0"),
                Col("погр", "pogr", 45, "N3"), Col("конц", "konc", 45, "N3"),
                Col("расх(М)", "rash_m", 60, "N3"), Col("%", "percent", 40, "N2")
            };
            gvSpreading.Columns.AddRange(spCols);
            gvSpreading.GridControl = gridSpreading;
            gvSpreading.Name = "gvSpreading";
            ApplyGridStyle(gvSpreading, autoWidth: false);

            // Layout
            lcFabric = new DevExpress.XtraLayout.LayoutControl();
            lcgFabric = new DevExpress.XtraLayout.LayoutControlGroup();
            lcgSpreading = new DevExpress.XtraLayout.LayoutControlGroup();
            ((ISupportInitialize)lcFabric).BeginInit();
            lcFabric.SuspendLayout();
            ((ISupportInitialize)lcgFabric).BeginInit();
            ((ISupportInitialize)lcgSpreading).BeginInit();

            var liFabricNakl = Lci(txtFabricNakl, "Введите № карты полотна");
            var liFabricPrice = Lci(txtFabricPrice, "Ввод цены корр.");
            var liFabricQty = Lci(txtFabricQty, "кол-во / шт");
            var liFabricConsumption = Lci(txtFabricConsumption, "расход ткани / м");
            var liFabricCost = Lci(txtFabricCost, "СЕБ-ТЬ СРСЦ");
            var liFabricNorm = Lci(txtFabricNorm, "Норма расхода, м");
            var liBtnReplace = LciBtn(btnFabricReplace);
            var emFabric = new DevExpress.XtraLayout.EmptySpaceItem();
            ((ISupportInitialize)emFabric).BeginInit();
            emFabric.AllowHotTrack = false;
            emFabric.Name = "emFabric";
            emFabric.Size = new Size(200, 38);

            var liGridSpreading = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)liGridSpreading).BeginInit();
            liGridSpreading.Control = gridSpreading;
            liGridSpreading.Name = "liGridSpreading";
            liGridSpreading.TextVisible = false;

            lcFabric.Controls.Add(txtFabricNakl);
            lcFabric.Controls.Add(txtFabricPrice);
            lcFabric.Controls.Add(txtFabricQty);
            lcFabric.Controls.Add(txtFabricConsumption);
            lcFabric.Controls.Add(txtFabricCost);
            lcFabric.Controls.Add(txtFabricNorm);
            lcFabric.Controls.Add(btnFabricReplace);
            lcFabric.Controls.Add(gridSpreading);
            lcFabric.Dock = DockStyle.Fill;
            lcFabric.Name = "lcFabric";
            lcFabric.Root = lcgFabric;

            lcgSpreading.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { liGridSpreading });
            lcgSpreading.Name = "lcgSpreading";
            lcgSpreading.Text = "Настилание";
            lcgSpreading.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);

            lcgFabric.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            lcgFabric.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                liFabricNakl, liFabricPrice, liFabricQty, liFabricConsumption,
                liFabricCost, liFabricNorm, liBtnReplace, emFabric, lcgSpreading });
            lcgFabric.Name = "lcgFabric";
            lcgFabric.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            lcgFabric.Text = "Полотно";
            lcgFabric.AppearanceGroup.Font = new Font("Arial", 10F, FontStyle.Bold);
            lcgFabric.AppearanceGroup.Options.UseFont = true;

            ((ISupportInitialize)lcgSpreading).EndInit();
            ((ISupportInitialize)lcgFabric).EndInit();
            ((ISupportInitialize)liGridSpreading).EndInit();
            ((ISupportInitialize)emFabric).EndInit();
            ((ISupportInitialize)lcFabric).EndInit();
            lcFabric.ResumeLayout(false);
            EndLci(liFabricNakl, liFabricPrice, liFabricQty, liFabricConsumption, liFabricCost, liFabricNorm, liBtnReplace);
            ((ISupportInitialize)gridSpreading).EndInit();
            ((ISupportInitialize)bsSpreading).EndInit();
            ((ISupportInitialize)gvSpreading).EndInit();
        }

        private void BuildYarnPanel()
        {
            txtNakl = Tb("txtNakl");
            txtNakl.KeyDown += txtNakl_KeyDown;
            txtArticul = Tb("txtArticul", ro: true);
            txtSebUpr = Tb("txtSebUpr", ro: true, bold: true);
            txtSebDok = Tb("txtSebDok", ro: true);
            txtColorSearch = Tb("txtColorSearch");
            txtColorSearch.Properties.NullValuePrompt = "введите цвет и Enter";
            txtColorSearch.Properties.NullValuePromptShowForEmptyValue = true;
            txtColorSearch.KeyDown += txtColorSearch_KeyDown;

            btnCalc = Btn("btnCalc", "рассчитать", small: true);
            btnCalc.Click += btnCalc_Click;
            btnObnovit = Btn("btnObnovit", "Обновить");
            btnObnovit.Click += btnObnovit_Click;
            btnHistory = Btn("btnHistory", "История изменений");
            btnHistory.Click += btnHistory_Click;

            bsYarnData = new BindingSource(components);
            gridYarnData = new CustomGridControl();
            gvYarnData = new GridView();
            ((ISupportInitialize)gridYarnData).BeginInit();
            ((ISupportInitialize)bsYarnData).BeginInit();
            ((ISupportInitialize)gvYarnData).BeginInit();

            gridYarnData.DataSource = bsYarnData;
            gridYarnData.Font = new Font("Arial", 10F);
            gridYarnData.MainView = gvYarnData;
            gridYarnData.Name = "gridYarnData";
            gridYarnData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvYarnData });

            var yCols = new[] { Col("Карта", "nakl", 100), Col("Артикул", "t_articul", 180), Col("Цвет", "zvet", 140), Col("Себестоимость", "seb_t_m", 110, "N3") };
            gvYarnData.Columns.AddRange(yCols);
            gvYarnData.GridControl = gridYarnData;
            gvYarnData.Name = "gvYarnData";
            ApplyGridStyle(gvYarnData);

            bsColorResult = new BindingSource(components);
            gridColorResult = new CustomGridControl();
            gvColorResult = new GridView();
            ((ISupportInitialize)gridColorResult).BeginInit();
            ((ISupportInitialize)bsColorResult).BeginInit();
            ((ISupportInitialize)gvColorResult).BeginInit();

            gridColorResult.DataSource = bsColorResult;
            gridColorResult.Font = new Font("Arial", 10F);
            gridColorResult.MainView = gvColorResult;
            gridColorResult.Name = "gridColorResult";
            gridColorResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvColorResult });

            var crCols = new[] { Col("Карта", "nakl", 90), Col("Артикул", "t_articul", 140) };
            gvColorResult.Columns.AddRange(crCols);
            gvColorResult.GridControl = gridColorResult;
            gvColorResult.Name = "gvColorResult";
            ApplyGridStyle(gvColorResult);
            gvColorResult.DoubleClick += gvColorResult_DoubleClick;

            // Layout
            lcYarn = new DevExpress.XtraLayout.LayoutControl();
            lcgYarn = new DevExpress.XtraLayout.LayoutControlGroup();
            lcgColorSearch = new DevExpress.XtraLayout.LayoutControlGroup();
            ((ISupportInitialize)lcYarn).BeginInit();
            lcYarn.SuspendLayout();
            ((ISupportInitialize)lcgYarn).BeginInit();
            ((ISupportInitialize)lcgColorSearch).BeginInit();

            var liNakl = Lci(txtNakl, "Введите № карты пряжи");
            var liArticul = Lci(txtArticul, "Артикул");
            var liSebUpr = Lci(txtSebUpr, "Себ-ть упр.");
            var liBtnCalc = LciBtn(btnCalc);
            var liSebDok = Lci(txtSebDok, "Себ-ть по документам");
            var liBtnObnovit = LciBtn(btnObnovit);
            var liBtnHistory = LciBtn(btnHistory);
            var emYarn = new DevExpress.XtraLayout.EmptySpaceItem();
            ((ISupportInitialize)emYarn).BeginInit();
            emYarn.AllowHotTrack = false;
            emYarn.Name = "emYarn";
            emYarn.Size = new Size(200, 38);

            var liGrid = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)liGrid).BeginInit();
            liGrid.Control = gridYarnData;
            liGrid.Name = "liGrid";
            liGrid.TextVisible = false;

            var liColorSearch = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)liColorSearch).BeginInit();
            liColorSearch.Control = txtColorSearch;
            liColorSearch.Name = "liColorSearch";
            liColorSearch.TextVisible = false;

            var liGridColor = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)liGridColor).BeginInit();
            liGridColor.Control = gridColorResult;
            liGridColor.Name = "liGridColor";
            liGridColor.TextVisible = false;

            lcYarn.Controls.Add(txtNakl);
            lcYarn.Controls.Add(txtArticul);
            lcYarn.Controls.Add(txtSebUpr);
            lcYarn.Controls.Add(txtSebDok);
            lcYarn.Controls.Add(txtColorSearch);
            lcYarn.Controls.Add(btnCalc);
            lcYarn.Controls.Add(btnObnovit);
            lcYarn.Controls.Add(btnHistory);
            lcYarn.Controls.Add(gridYarnData);
            lcYarn.Controls.Add(gridColorResult);
            lcYarn.Dock = DockStyle.Fill;
            lcYarn.Name = "lcYarn";
            lcYarn.Root = lcgYarn;

            lcgColorSearch.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { liColorSearch, liGridColor });
            lcgColorSearch.Name = "lcgColorSearch";
            lcgColorSearch.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            lcgColorSearch.Text = "Поиск по цвету";

            lcgYarn.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            lcgYarn.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
                liNakl, liArticul, liSebUpr, liBtnCalc, liSebDok,
                liBtnObnovit, liBtnHistory, emYarn, liGrid, lcgColorSearch });
            lcgYarn.Name = "lcgYarn";
            lcgYarn.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            lcgYarn.Text = "Пряжа";
            lcgYarn.AppearanceGroup.Font = new Font("Arial", 10F, FontStyle.Bold);
            lcgYarn.AppearanceGroup.Options.UseFont = true;

            ((ISupportInitialize)lcgColorSearch).EndInit();
            ((ISupportInitialize)lcgYarn).EndInit();
            ((ISupportInitialize)liGrid).EndInit();
            ((ISupportInitialize)liColorSearch).EndInit();
            ((ISupportInitialize)liGridColor).EndInit();
            ((ISupportInitialize)emYarn).EndInit();
            ((ISupportInitialize)lcYarn).EndInit();
            lcYarn.ResumeLayout(false);
            EndLci(liNakl, liArticul, liSebUpr, liBtnCalc, liSebDok, liBtnObnovit, liBtnHistory);
            ((ISupportInitialize)gridYarnData).EndInit();
            ((ISupportInitialize)bsYarnData).EndInit();
            ((ISupportInitialize)gvYarnData).EndInit();
            ((ISupportInitialize)gridColorResult).EndInit();
            ((ISupportInitialize)bsColorResult).EndInit();
            ((ISupportInitialize)gvColorResult).EndInit();
        }

        // === Helpers ===

        private static CustomTextBoxEx Tb(string name, bool ro = false, bool bold = false)
        {
            var t = new CustomTextBoxEx();
            t.Name = name;
            t.ObjectName = null;
            t.Properties.Appearance.Font = new Font("Arial", 10F, bold ? FontStyle.Bold : FontStyle.Regular);
            t.Properties.Appearance.Options.UseFont = true;
            if (ro)
            {
                t.Properties.ReadOnly = true;
                t.Properties.Appearance.BackColor = Color.FromArgb(245, 245, 245);
                t.Properties.Appearance.Options.UseBackColor = true;
            }
            return t;
        }

        private static CustomSimpleButton Btn(string name, string text, bool small = false)
        {
            var b = new CustomSimpleButton();
            b.Appearance.Font = new Font("Arial", small ? 9F : 10F);
            b.Appearance.Options.UseFont = true;
            b.Name = name;
            b.Text = text;
            b.Size = new Size(small ? 110 : 150, small ? 26 : 34);
            return b;
        }

        private static DevExpress.XtraLayout.LayoutControlItem Lci(Control ctrl, string caption)
        {
            var li = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)li).BeginInit();
            li.Control = ctrl;
            li.Name = "li_" + ctrl.Name;
            li.Text = caption;
            li.TextSize = new Size(150, 16);
            li.AppearanceItemCaption.Font = new Font("Arial", 10F);
            li.AppearanceItemCaption.Options.UseFont = true;
            return li;
        }

        private static DevExpress.XtraLayout.LayoutControlItem LciBtn(Control ctrl)
        {
            var li = new DevExpress.XtraLayout.LayoutControlItem();
            ((ISupportInitialize)li).BeginInit();
            li.Control = ctrl;
            li.Name = "li_" + ctrl.Name;
            li.TextVisible = false;
            li.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 6, 4, 2);
            return li;
        }

        private static void EndLci(params DevExpress.XtraLayout.LayoutControlItem[] items)
        {
            foreach (var li in items)
                ((ISupportInitialize)li).EndInit();
        }

        private static GridColumn Col(string caption, string field, int width, string fmt = null, DevExpress.Utils.FormatType ftype = DevExpress.Utils.FormatType.Numeric)
        {
            var c = new GridColumn();
            c.Caption = caption;
            c.FieldName = field;
            c.Name = "col_" + field;
            c.Visible = true;
            c.VisibleIndex = -1;
            c.Width = width;
            if (fmt != null)
            {
                c.DisplayFormat.FormatType = ftype;
                c.DisplayFormat.FormatString = fmt;
            }
            return c;
        }

        private static void ApplyGridStyle(GridView gv, bool autoWidth = true)
        {
            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsView.ShowAutoFilterRow = true;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ColumnAutoWidth = autoWidth;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.EnableAppearanceEvenRow = true;
            gv.Appearance.EvenRow.BackColor = Color.FromArgb(245, 248, 252);
            gv.Appearance.EvenRow.Options.UseBackColor = true;
            gv.Appearance.FocusedRow.BackColor = Color.FromArgb(210, 228, 248);
            gv.Appearance.FocusedRow.Options.UseBackColor = true;
            gv.RowHeight = 22;
        }
    }
}
