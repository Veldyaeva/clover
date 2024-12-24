namespace SewingProduction
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.customGridControl1 = new SewingProduction.CustomGridControl();
            this.artnormnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aCEDataSet = new SewingProduction.ACE_backupDataSet();
            this.art_norm_nTableAdapter = new SewingProduction.ACE_backupDataSetTableAdapters.art_norm_nTableAdapter();
            this.customGridControl2 = new SewingProduction.CustomGridControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colkod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgrup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarticul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_shv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz62 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz71 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz72 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyazo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colseb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colst = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colpo1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkomment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldata_sozd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldiz = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colconstr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldata_obn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz70 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_kr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colslogn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsek_vyaz14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colarh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsql_pr_add = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coldate_add = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colkomp_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannDateDel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannCompDel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannDateAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colannCompAdd = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCEDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // customGridControl1
            // 
            this.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl1.DataSource = this.artnormnBindingSource;
            this.customGridControl1.Location = new System.Drawing.Point(12, 12);
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(400, 200);
            this.customGridControl1.TabIndex = 0;
            // 
            // artnormnBindingSource
            // 
            this.artnormnBindingSource.DataMember = "art_norm_n";
            this.artnormnBindingSource.DataSource = this.aCEDataSet;
            // 
            // aCEDataSet
            // 
            this.aCEDataSet.DataSetName = "ACEDataSet";
            this.aCEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // art_norm_nTableAdapter
            // 
            this.art_norm_nTableAdapter.ClearBeforeFill = true;
            // 
            // customGridControl2
            // 
            this.customGridControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl2.DataSource = this.artnormnBindingSource;
            this.customGridControl2.Location = new System.Drawing.Point(172, 273);
            this.customGridControl2.Name = "customGridControl2";
            this.customGridControl2.Size = new System.Drawing.Size(400, 200);
            this.customGridControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.artnormnBindingSource;
            this.gridControl1.Location = new System.Drawing.Point(455, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(400, 200);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Aquamarine;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colkod,
            this.colgrup,
            this.colarticul,
            this.colmod,
            this.colpo,
            this.colsek_shv,
            this.colsek_vyaz3,
            this.colsek_vyaz5,
            this.colsek_vyaz6,
            this.colsek_vyaz7,
            this.colsek_vyaz10,
            this.colsek_vyaz12,
            this.colsek_vyaz62,
            this.colsek_vyaz71,
            this.colsek_vyaz72,
            this.colsek_vyazo,
            this.colsek_vyaz,
            this.colsek,
            this.colseb,
            this.colst,
            this.colpo1,
            this.colkomment,
            this.coldata_sozd,
            this.coldiz,
            this.colconstr,
            this.coldata_obn,
            this.colsek_vyaz70,
            this.colsek_kr,
            this.colslogn,
            this.colsek_vyaz14,
            this.colarh,
            this.colsql_pr_add,
            this.coldate_add,
            this.colkomp_name,
            this.colannDateDel,
            this.colannCompDel,
            this.colannID,
            this.colannDateAdd,
            this.colannCompAdd});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // colkod
            // 
            this.colkod.FieldName = "kod";
            this.colkod.Name = "colkod";
            this.colkod.Visible = true;
            this.colkod.VisibleIndex = 0;
            // 
            // colgrup
            // 
            this.colgrup.FieldName = "grup";
            this.colgrup.Name = "colgrup";
            this.colgrup.Visible = true;
            this.colgrup.VisibleIndex = 1;
            // 
            // colarticul
            // 
            this.colarticul.FieldName = "articul";
            this.colarticul.Name = "colarticul";
            this.colarticul.Visible = true;
            this.colarticul.VisibleIndex = 2;
            // 
            // colmod
            // 
            this.colmod.FieldName = "mod";
            this.colmod.Name = "colmod";
            this.colmod.Visible = true;
            this.colmod.VisibleIndex = 3;
            // 
            // colpo
            // 
            this.colpo.FieldName = "po";
            this.colpo.Name = "colpo";
            this.colpo.Visible = true;
            this.colpo.VisibleIndex = 4;
            // 
            // colsek_shv
            // 
            this.colsek_shv.FieldName = "sek_shv";
            this.colsek_shv.Name = "colsek_shv";
            this.colsek_shv.Visible = true;
            this.colsek_shv.VisibleIndex = 5;
            // 
            // colsek_vyaz3
            // 
            this.colsek_vyaz3.FieldName = "sek_vyaz3";
            this.colsek_vyaz3.Name = "colsek_vyaz3";
            this.colsek_vyaz3.Visible = true;
            this.colsek_vyaz3.VisibleIndex = 6;
            // 
            // colsek_vyaz5
            // 
            this.colsek_vyaz5.FieldName = "sek_vyaz5";
            this.colsek_vyaz5.Name = "colsek_vyaz5";
            this.colsek_vyaz5.Visible = true;
            this.colsek_vyaz5.VisibleIndex = 7;
            // 
            // colsek_vyaz6
            // 
            this.colsek_vyaz6.FieldName = "sek_vyaz6";
            this.colsek_vyaz6.Name = "colsek_vyaz6";
            this.colsek_vyaz6.Visible = true;
            this.colsek_vyaz6.VisibleIndex = 8;
            // 
            // colsek_vyaz7
            // 
            this.colsek_vyaz7.FieldName = "sek_vyaz7";
            this.colsek_vyaz7.Name = "colsek_vyaz7";
            this.colsek_vyaz7.Visible = true;
            this.colsek_vyaz7.VisibleIndex = 9;
            // 
            // colsek_vyaz10
            // 
            this.colsek_vyaz10.FieldName = "sek_vyaz10";
            this.colsek_vyaz10.Name = "colsek_vyaz10";
            this.colsek_vyaz10.Visible = true;
            this.colsek_vyaz10.VisibleIndex = 10;
            // 
            // colsek_vyaz12
            // 
            this.colsek_vyaz12.FieldName = "sek_vyaz12";
            this.colsek_vyaz12.Name = "colsek_vyaz12";
            this.colsek_vyaz12.Visible = true;
            this.colsek_vyaz12.VisibleIndex = 11;
            // 
            // colsek_vyaz62
            // 
            this.colsek_vyaz62.FieldName = "sek_vyaz62";
            this.colsek_vyaz62.Name = "colsek_vyaz62";
            this.colsek_vyaz62.Visible = true;
            this.colsek_vyaz62.VisibleIndex = 12;
            // 
            // colsek_vyaz71
            // 
            this.colsek_vyaz71.FieldName = "sek_vyaz71";
            this.colsek_vyaz71.Name = "colsek_vyaz71";
            this.colsek_vyaz71.Visible = true;
            this.colsek_vyaz71.VisibleIndex = 13;
            // 
            // colsek_vyaz72
            // 
            this.colsek_vyaz72.FieldName = "sek_vyaz72";
            this.colsek_vyaz72.Name = "colsek_vyaz72";
            this.colsek_vyaz72.Visible = true;
            this.colsek_vyaz72.VisibleIndex = 14;
            // 
            // colsek_vyazo
            // 
            this.colsek_vyazo.FieldName = "sek_vyazo";
            this.colsek_vyazo.Name = "colsek_vyazo";
            this.colsek_vyazo.Visible = true;
            this.colsek_vyazo.VisibleIndex = 15;
            // 
            // colsek_vyaz
            // 
            this.colsek_vyaz.FieldName = "sek_vyaz";
            this.colsek_vyaz.Name = "colsek_vyaz";
            this.colsek_vyaz.Visible = true;
            this.colsek_vyaz.VisibleIndex = 16;
            // 
            // colsek
            // 
            this.colsek.FieldName = "sek";
            this.colsek.Name = "colsek";
            this.colsek.Visible = true;
            this.colsek.VisibleIndex = 17;
            // 
            // colseb
            // 
            this.colseb.FieldName = "seb";
            this.colseb.Name = "colseb";
            this.colseb.Visible = true;
            this.colseb.VisibleIndex = 18;
            // 
            // colst
            // 
            this.colst.FieldName = "st";
            this.colst.Name = "colst";
            this.colst.Visible = true;
            this.colst.VisibleIndex = 19;
            // 
            // colpo1
            // 
            this.colpo1.FieldName = "po1";
            this.colpo1.Name = "colpo1";
            this.colpo1.Visible = true;
            this.colpo1.VisibleIndex = 20;
            // 
            // colkomment
            // 
            this.colkomment.FieldName = "komment";
            this.colkomment.Name = "colkomment";
            this.colkomment.Visible = true;
            this.colkomment.VisibleIndex = 21;
            // 
            // coldata_sozd
            // 
            this.coldata_sozd.FieldName = "data_sozd";
            this.coldata_sozd.Name = "coldata_sozd";
            this.coldata_sozd.Visible = true;
            this.coldata_sozd.VisibleIndex = 22;
            // 
            // coldiz
            // 
            this.coldiz.FieldName = "diz";
            this.coldiz.Name = "coldiz";
            this.coldiz.Visible = true;
            this.coldiz.VisibleIndex = 23;
            // 
            // colconstr
            // 
            this.colconstr.FieldName = "constr";
            this.colconstr.Name = "colconstr";
            this.colconstr.Visible = true;
            this.colconstr.VisibleIndex = 24;
            // 
            // coldata_obn
            // 
            this.coldata_obn.FieldName = "data_obn";
            this.coldata_obn.Name = "coldata_obn";
            this.coldata_obn.Visible = true;
            this.coldata_obn.VisibleIndex = 25;
            // 
            // colsek_vyaz70
            // 
            this.colsek_vyaz70.FieldName = "sek_vyaz70";
            this.colsek_vyaz70.Name = "colsek_vyaz70";
            this.colsek_vyaz70.Visible = true;
            this.colsek_vyaz70.VisibleIndex = 26;
            // 
            // colsek_kr
            // 
            this.colsek_kr.FieldName = "sek_kr";
            this.colsek_kr.Name = "colsek_kr";
            this.colsek_kr.Visible = true;
            this.colsek_kr.VisibleIndex = 27;
            // 
            // colslogn
            // 
            this.colslogn.FieldName = "slogn";
            this.colslogn.Name = "colslogn";
            this.colslogn.Visible = true;
            this.colslogn.VisibleIndex = 28;
            // 
            // colsek_vyaz14
            // 
            this.colsek_vyaz14.FieldName = "sek_vyaz14";
            this.colsek_vyaz14.Name = "colsek_vyaz14";
            this.colsek_vyaz14.Visible = true;
            this.colsek_vyaz14.VisibleIndex = 29;
            // 
            // colarh
            // 
            this.colarh.FieldName = "arh";
            this.colarh.Name = "colarh";
            this.colarh.Visible = true;
            this.colarh.VisibleIndex = 30;
            // 
            // colsql_pr_add
            // 
            this.colsql_pr_add.FieldName = "sql_pr_add";
            this.colsql_pr_add.Name = "colsql_pr_add";
            this.colsql_pr_add.Visible = true;
            this.colsql_pr_add.VisibleIndex = 31;
            // 
            // coldate_add
            // 
            this.coldate_add.FieldName = "date_add";
            this.coldate_add.Name = "coldate_add";
            this.coldate_add.Visible = true;
            this.coldate_add.VisibleIndex = 32;
            // 
            // colkomp_name
            // 
            this.colkomp_name.FieldName = "komp_name";
            this.colkomp_name.Name = "colkomp_name";
            this.colkomp_name.Visible = true;
            this.colkomp_name.VisibleIndex = 33;
            // 
            // colannDateDel
            // 
            this.colannDateDel.FieldName = "annDateDel";
            this.colannDateDel.Name = "colannDateDel";
            this.colannDateDel.Visible = true;
            this.colannDateDel.VisibleIndex = 34;
            // 
            // colannCompDel
            // 
            this.colannCompDel.FieldName = "annCompDel";
            this.colannCompDel.Name = "colannCompDel";
            this.colannCompDel.Visible = true;
            this.colannCompDel.VisibleIndex = 35;
            // 
            // colannID
            // 
            this.colannID.FieldName = "annID";
            this.colannID.Name = "colannID";
            this.colannID.Visible = true;
            this.colannID.VisibleIndex = 36;
            // 
            // colannDateAdd
            // 
            this.colannDateAdd.FieldName = "annDateAdd";
            this.colannDateAdd.Name = "colannDateAdd";
            this.colannDateAdd.Visible = true;
            this.colannDateAdd.VisibleIndex = 37;
            // 
            // colannCompAdd
            // 
            this.colannCompAdd.FieldName = "annCompAdd";
            this.colannCompAdd.Name = "colannCompAdd";
            this.colannCompAdd.Visible = true;
            this.colannCompAdd.VisibleIndex = 38;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.customGridControl2);
            this.Controls.Add(this.customGridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.artnormnBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCEDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomGridControl customGridControl1;
        private ACE_backupDataSet aCEDataSet;
        private System.Windows.Forms.BindingSource artnormnBindingSource;
        private ACE_backupDataSetTableAdapters.art_norm_nTableAdapter art_norm_nTableAdapter;
        private CustomGridControl customGridControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colkod;
        private DevExpress.XtraGrid.Columns.GridColumn colgrup;
        private DevExpress.XtraGrid.Columns.GridColumn colarticul;
        private DevExpress.XtraGrid.Columns.GridColumn colmod;
        private DevExpress.XtraGrid.Columns.GridColumn colpo;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_shv;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz3;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz5;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz6;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz7;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz10;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz12;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz62;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz71;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz72;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyazo;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz;
        private DevExpress.XtraGrid.Columns.GridColumn colsek;
        private DevExpress.XtraGrid.Columns.GridColumn colseb;
        private DevExpress.XtraGrid.Columns.GridColumn colst;
        private DevExpress.XtraGrid.Columns.GridColumn colpo1;
        private DevExpress.XtraGrid.Columns.GridColumn colkomment;
        private DevExpress.XtraGrid.Columns.GridColumn coldata_sozd;
        private DevExpress.XtraGrid.Columns.GridColumn coldiz;
        private DevExpress.XtraGrid.Columns.GridColumn colconstr;
        private DevExpress.XtraGrid.Columns.GridColumn coldata_obn;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz70;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_kr;
        private DevExpress.XtraGrid.Columns.GridColumn colslogn;
        private DevExpress.XtraGrid.Columns.GridColumn colsek_vyaz14;
        private DevExpress.XtraGrid.Columns.GridColumn colarh;
        private DevExpress.XtraGrid.Columns.GridColumn colsql_pr_add;
        private DevExpress.XtraGrid.Columns.GridColumn coldate_add;
        private DevExpress.XtraGrid.Columns.GridColumn colkomp_name;
        private DevExpress.XtraGrid.Columns.GridColumn colannDateDel;
        private DevExpress.XtraGrid.Columns.GridColumn colannCompDel;
        private DevExpress.XtraGrid.Columns.GridColumn colannID;
        private DevExpress.XtraGrid.Columns.GridColumn colannDateAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colannCompAdd;
    }
}