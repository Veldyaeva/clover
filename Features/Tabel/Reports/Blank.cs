using System.Drawing;
using DevExpress.Drawing.Printing;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace SewingProduction.Features.Tabel.Reports
{
    public partial class Blank : XtraReport
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;

        private XRPanel xrPanelForm1;
        private XRPanel xrPanelForm2;
        private XRPanel panel1;
        private XRPanel panel2;
        private XRPanel panel3;
        private XRLabel title1;
        private XRLabel fio1;
        private XRLabel date1;
        private XRTable table1;
        private XRTableRow hRow1;
        private XRLabel sign1;
        private XRTableRow headerRow1;
        private XRTableRow dataRow11;
        private XRTableRow dataRow12;
        private XRTableRow dataRow13;
        private XRTableRow dataRow14;
        private XRTableRow dataRow15;
        private XRPanel xrPanel1;
        private XRLabel xrLabel1;
        private XRLabel xrLabel2;
        private XRLabel xrLabel3;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableRow xrTableRow2;
        private XRTableRow xrTableRow3;
        private XRTableRow xrTableRow4;
        private XRTableRow xrTableRow5;
        private XRTableRow xrTableRow6;
        private XRLabel xrLabel4;
        private XRLabel title2;
        private XRLabel fio2;
        private XRLabel date2;
        private XRTable table2;
        private XRTableRow headerRow2;
        private XRTableRow dataRow21;
        private XRTableRow dataRow22;
        private XRTableRow dataRow23;
        private XRTableRow dataRow24;
        private XRTableRow dataRow25;
        private XRLabel sign2;
        private XRLabel title3;
        private XRLabel fio3;
        private XRLabel date3;
        private XRTable table3;
        private XRTableRow headerRow3;
        private XRTableRow dataRow31;
        private XRTableRow dataRow32;
        private XRTableRow dataRow33;
        private XRTableRow dataRow34;
        private XRTableRow dataRow35;
        private XRLabel sign3;
        private XRPanel xrPanelForm3;

        public Blank()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();

            this.panel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.panel2 = new DevExpress.XtraReports.UI.XRPanel();
            this.panel3 = new DevExpress.XtraReports.UI.XRPanel();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 20F;
            this.topMarginBand1.Name = "topMarginBand1";

            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 20F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";

            // 
            // detailBand1
            // 
            this.detailBand1.HeightF = 1020F;
            this.detailBand1.Name = "detailBand1";
            this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
        this.panel1,
        this.panel2,
        this.panel3
    });

            // =========================================================
            // PANEL 1
            // =========================================================
            this.panel1.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.panel1.BorderWidth = 1F;
            this.panel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.panel1.Name = "panel1";
            this.panel1.SizeF = new System.Drawing.SizeF(787F, 320F);

            DevExpress.XtraReports.UI.XRLabel title1 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel fio1 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel date1 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRTable table1 = new DevExpress.XtraReports.UI.XRTable();
            DevExpress.XtraReports.UI.XRTableRow headerRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRLabel sign1 = new DevExpress.XtraReports.UI.XRLabel();

            ((System.ComponentModel.ISupportInitialize)(table1)).BeginInit();

            // title1
            title1.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            title1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            title1.Multiline = true;
            title1.Name = "title1";
            title1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            title1.SizeF = new System.Drawing.SizeF(787F, 25F);
            title1.Text = "Почасовая занятость работника склада";
            title1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            // fio1
            fio1.LocationFloat = new DevExpress.Utils.PointFloat(470F, 25F);
            fio1.Multiline = true;
            fio1.Name = "fio1";
            fio1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            fio1.SizeF = new System.Drawing.SizeF(310F, 20F);
            fio1.Text = "ФИО ________________________________";

            // date1
            date1.LocationFloat = new DevExpress.Utils.PointFloat(470F, 45F);
            date1.Multiline = true;
            date1.Name = "date1";
            date1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            date1.SizeF = new System.Drawing.SizeF(310F, 20F);
            date1.Text = "от \"___\" ____________";

            // table1
            table1.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table1.BorderWidth = 1F;
            table1.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F);
            table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            table1.Name = "table1";
            table1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            table1.SizeF = new System.Drawing.SizeF(787F, 200F);
            table1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            // headerRow1
            headerRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Прием товара", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Выкладка", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Работа на другом складе", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Отвлеченные часы", Weight = 1.2D }
    });
            headerRow1.Name = "headerRow1";
            headerRow1.Weight = 1D;

            // dataRow11
            dataRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow11.Name = "dataRow11";
            dataRow11.Weight = 1D;

            // dataRow12
            dataRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow12.Name = "dataRow12";
            dataRow12.Weight = 1D;

            // dataRow13
            dataRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow13.Name = "dataRow13";
            dataRow13.Weight = 1D;

            // dataRow14
            dataRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow14.Name = "dataRow14";
            dataRow14.Weight = 1D;

            // dataRow15
            dataRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow15.Name = "dataRow15";
            dataRow15.Weight = 1D;

            table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
        headerRow1,
        dataRow11,
        dataRow12,
        dataRow13,
        dataRow14,
        dataRow15
    });

            // sign1
            sign1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 285F);
            sign1.Multiline = true;
            sign1.Name = "sign1";
            sign1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            sign1.SizeF = new System.Drawing.SizeF(500F, 20F);
            sign1.Text = "Зав. Складом ________________________________";

            this.panel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
        title1, fio1, date1, table1, sign1
    });

            ((System.ComponentModel.ISupportInitialize)(table1)).EndInit();

            // =========================================================
            // PANEL 2
            // =========================================================
            this.panel2.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.panel2.BorderWidth = 1F;
            this.panel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 340F);
            this.panel2.Name = "panel2";
            this.panel2.SizeF = new System.Drawing.SizeF(787F, 320F);

            DevExpress.XtraReports.UI.XRLabel title2 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel fio2 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel date2 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRTable table2 = new DevExpress.XtraReports.UI.XRTable();
            DevExpress.XtraReports.UI.XRTableRow headerRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRLabel sign2 = new DevExpress.XtraReports.UI.XRLabel();

            ((System.ComponentModel.ISupportInitialize)(table2)).BeginInit();

            title2.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            title2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            title2.Multiline = true;
            title2.Name = "title2";
            title2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            title2.SizeF = new System.Drawing.SizeF(787F, 25F);
            title2.Text = "Почасовая занятость работника склада";
            title2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            fio2.LocationFloat = new DevExpress.Utils.PointFloat(470F, 25F);
            fio2.Multiline = true;
            fio2.Name = "fio2";
            fio2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            fio2.SizeF = new System.Drawing.SizeF(310F, 20F);
            fio2.Text = "ФИО ________________________________";

            date2.LocationFloat = new DevExpress.Utils.PointFloat(470F, 45F);
            date2.Multiline = true;
            date2.Name = "date2";
            date2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            date2.SizeF = new System.Drawing.SizeF(310F, 20F);
            date2.Text = "от \"___\" ____________";

            table2.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table2.BorderWidth = 1F;
            table2.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F);
            table2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            table2.Name = "table2";
            table2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            table2.SizeF = new System.Drawing.SizeF(787F, 200F);
            table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            headerRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Прием товара", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Выкладка", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Работа на другом складе", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Отвлеченные часы", Weight = 1.2D }
    });
            headerRow2.Name = "headerRow2";
            headerRow2.Weight = 1D;

            dataRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow21.Name = "dataRow21";
            dataRow21.Weight = 1D;

            dataRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow22.Name = "dataRow22";
            dataRow22.Weight = 1D;

            dataRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow23.Name = "dataRow23";
            dataRow23.Weight = 1D;

            dataRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow24.Name = "dataRow24";
            dataRow24.Weight = 1D;

            dataRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow25.Name = "dataRow25";
            dataRow25.Weight = 1D;

            table2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
        headerRow2,
        dataRow21,
        dataRow22,
        dataRow23,
        dataRow24,
        dataRow25
    });

            sign2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 285F);
            sign2.Multiline = true;
            sign2.Name = "sign2";
            sign2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            sign2.SizeF = new System.Drawing.SizeF(500F, 20F);
            sign2.Text = "Зав. Складом ________________________________";

            this.panel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
        title2, fio2, date2, table2, sign2
    });

            ((System.ComponentModel.ISupportInitialize)(table2)).EndInit();

            // =========================================================
            // PANEL 3
            // =========================================================
            this.panel3.Borders = DevExpress.XtraPrinting.BorderSide.All;
            this.panel3.BorderWidth = 1F;
            this.panel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 680F);
            this.panel3.Name = "panel3";
            this.panel3.SizeF = new System.Drawing.SizeF(787F, 320F);

            DevExpress.XtraReports.UI.XRLabel title3 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel fio3 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRLabel date3 = new DevExpress.XtraReports.UI.XRLabel();
            DevExpress.XtraReports.UI.XRTable table3 = new DevExpress.XtraReports.UI.XRTable();
            DevExpress.XtraReports.UI.XRTableRow headerRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRTableRow dataRow35 = new DevExpress.XtraReports.UI.XRTableRow();
            DevExpress.XtraReports.UI.XRLabel sign3 = new DevExpress.XtraReports.UI.XRLabel();

            ((System.ComponentModel.ISupportInitialize)(table3)).BeginInit();

            title3.Font = new DevExpress.Drawing.DXFont("Times New Roman", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            title3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            title3.Multiline = true;
            title3.Name = "title3";
            title3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            title3.SizeF = new System.Drawing.SizeF(787F, 25F);
            title3.Text = "Почасовая занятость работника склада";
            title3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            fio3.LocationFloat = new DevExpress.Utils.PointFloat(470F, 25F);
            fio3.Multiline = true;
            fio3.Name = "fio3";
            fio3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            fio3.SizeF = new System.Drawing.SizeF(310F, 20F);
            fio3.Text = "ФИО ________________________________";

            date3.LocationFloat = new DevExpress.Utils.PointFloat(470F, 45F);
            date3.Multiline = true;
            date3.Name = "date3";
            date3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            date3.SizeF = new System.Drawing.SizeF(310F, 20F);
            date3.Text = "от \"___\" ____________";

            table3.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table3.BorderWidth = 1F;
            table3.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F);
            table3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            table3.Name = "table3";
            table3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            table3.SizeF = new System.Drawing.SizeF(787F, 200F);
            table3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

            headerRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Прием товара", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Выкладка", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Работа на другом складе", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "Отвлеченные часы", Weight = 1.2D }
    });
            headerRow3.Name = "headerRow3";
            headerRow3.Weight = 1D;

            dataRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow31.Name = "dataRow31";
            dataRow31.Weight = 1D;

            dataRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow32.Name = "dataRow32";
            dataRow32.Weight = 1D;

            dataRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow33.Name = "dataRow33";
            dataRow33.Weight = 1D;

            dataRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow34.Name = "dataRow34";
            dataRow34.Weight = 1D;

            dataRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D },
        new DevExpress.XtraReports.UI.XRTableCell() { Text = "", Weight = 1.2D }
    });
            dataRow35.Name = "dataRow35";
            dataRow35.Weight = 1D;

            table3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
        headerRow3,
        dataRow31,
        dataRow32,
        dataRow33,
        dataRow34,
        dataRow35
    });

            sign3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 285F);
            sign3.Multiline = true;
            sign3.Name = "sign3";
            sign3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);
            sign3.SizeF = new System.Drawing.SizeF(500F, 20F);
            sign3.Text = "Зав. Складом ________________________________";

            this.panel3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
        title3, fio3, date3, table3, sign3
    });

            ((System.ComponentModel.ISupportInitialize)(table3)).EndInit();

            // 
            // Blank
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
        this.topMarginBand1,
        this.detailBand1,
        this.bottomMarginBand1
    });
            this.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9F);
            this.Margins = new DevExpress.Drawing.DXMargins(20F, 20F, 20F, 20F);
            this.PageHeightF = 1169.291F;
            this.PageWidthF = 826.7717F;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.Version = "25.2";

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }
}