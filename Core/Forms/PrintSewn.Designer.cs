namespace SewingProduction.Core.Forms
{
    partial class PrintSewn
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSewn));
			customHeaderLabel1 = new SewingProduction.Core.Class.CustomHeaderLabel();
			customLayoutControl1 = new SewingProduction.Core.Class.CustomLayoutControl();
			gridControlBlVsh = new SewingProduction.Core.Class.CustomGridControl();
			gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			gridControlRazmKol = new SewingProduction.Core.Class.CustomGridControl();
			gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			customSimpleButtonNabor = new SewingProduction.Core.Class.CustomSimpleButton();
			customSimpleButtonKompl = new SewingProduction.Core.Class.CustomSimpleButton();
			customSimpleButtonACE = new SewingProduction.Core.Class.CustomSimpleButton();
			Root = new DevExpress.XtraLayout.LayoutControlGroup();
			layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
			layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
			emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
			((System.ComponentModel.ISupportInitialize)customLayoutControl1).BeginInit();
			customLayoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControlBlVsh).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridControlRazmKol).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)Root).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).BeginInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).BeginInit();
			SuspendLayout();
			// 
			// customHeaderLabel1
			// 
			customHeaderLabel1.Appearance.Font = new System.Drawing.Font("Arial", 17F, System.Drawing.FontStyle.Bold);
			customHeaderLabel1.Appearance.Options.UseFont = true;
			customHeaderLabel1.Appearance.Options.UseTextOptions = true;
			customHeaderLabel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			customHeaderLabel1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			customHeaderLabel1.Location = new System.Drawing.Point(12, 12);
			customHeaderLabel1.Name = "customHeaderLabel1";
			customHeaderLabel1.Size = new System.Drawing.Size(174, 27);
			customHeaderLabel1.StyleController = customLayoutControl1;
			customHeaderLabel1.TabIndex = 1;
			customHeaderLabel1.Text = "Печать вшивок";
			customHeaderLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// customLayoutControl1
			// 
			customLayoutControl1.Controls.Add(gridControlBlVsh);
			customLayoutControl1.Controls.Add(gridControlRazmKol);
			customLayoutControl1.Controls.Add(customSimpleButtonNabor);
			customLayoutControl1.Controls.Add(customSimpleButtonKompl);
			customLayoutControl1.Controls.Add(customHeaderLabel1);
			customLayoutControl1.Controls.Add(customSimpleButtonACE);
			customLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			customLayoutControl1.Font = new System.Drawing.Font("Arial", 10F);
			customLayoutControl1.Location = new System.Drawing.Point(0, 0);
			customLayoutControl1.Name = "customLayoutControl1";
			customLayoutControl1.Root = Root;
			customLayoutControl1.Size = new System.Drawing.Size(617, 497);
			customLayoutControl1.TabIndex = 2;
			customLayoutControl1.Text = "customLayoutControl1";
			// 
			// gridControlBlVsh
			// 
			gridControlBlVsh.Font = new System.Drawing.Font("Arial", 10F);
			gridControlBlVsh.Location = new System.Drawing.Point(312, 185);
			gridControlBlVsh.MainView = gridView2;
			gridControlBlVsh.Name = "gridControlBlVsh";
			gridControlBlVsh.Size = new System.Drawing.Size(293, 300);
			gridControlBlVsh.TabIndex = 5;
			gridControlBlVsh.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
			// 
			// gridView2
			// 
			gridView2.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			gridView2.Appearance.FocusedRow.Options.UseFont = true;
			gridView2.GridControl = gridControlBlVsh;
			gridView2.Name = "gridView2";
			// 
			// gridControlRazmKol
			// 
			gridControlRazmKol.Font = new System.Drawing.Font("Arial", 10F);
			gridControlRazmKol.Location = new System.Drawing.Point(12, 185);
			gridControlRazmKol.MainView = gridView1;
			gridControlRazmKol.Name = "gridControlRazmKol";
			gridControlRazmKol.Size = new System.Drawing.Size(296, 300);
			gridControlRazmKol.TabIndex = 4;
			gridControlRazmKol.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
			// 
			// gridView1
			// 
			gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			gridView1.Appearance.FocusedRow.Options.UseFont = true;
			gridView1.GridControl = gridControlRazmKol;
			gridView1.Name = "gridView1";
			// 
			// customSimpleButtonNabor
			// 
			customSimpleButtonNabor.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonNabor.Appearance.Options.UseFont = true;
			customSimpleButtonNabor.Location = new System.Drawing.Point(12, 144);
			customSimpleButtonNabor.Name = "customSimpleButtonNabor";
			customSimpleButtonNabor.Size = new System.Drawing.Size(296, 37);
			customSimpleButtonNabor.StyleController = customLayoutControl1;
			customSimpleButtonNabor.TabIndex = 3;
			customSimpleButtonNabor.Text = "Набор одежды";
			customSimpleButtonNabor.Click += customSimpleButtonNabor_Click;
			// 
			// customSimpleButtonKompl
			// 
			customSimpleButtonKompl.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonKompl.Appearance.Options.UseFont = true;
			customSimpleButtonKompl.Location = new System.Drawing.Point(12, 104);
			customSimpleButtonKompl.Name = "customSimpleButtonKompl";
			customSimpleButtonKompl.Size = new System.Drawing.Size(296, 36);
			customSimpleButtonKompl.StyleController = customLayoutControl1;
			customSimpleButtonKompl.TabIndex = 2;
			customSimpleButtonKompl.Text = "Комплекты";
			customSimpleButtonKompl.Click += customSimpleButtonKompl_Click;
			// 
			// customSimpleButtonACE
			// 
			customSimpleButtonACE.Appearance.Font = new System.Drawing.Font("Arial", 10F);
			customSimpleButtonACE.Appearance.Options.UseFont = true;
			customSimpleButtonACE.Location = new System.Drawing.Point(12, 64);
			customSimpleButtonACE.Name = "customSimpleButtonACE";
			customSimpleButtonACE.Size = new System.Drawing.Size(296, 36);
			customSimpleButtonACE.StyleController = customLayoutControl1;
			customSimpleButtonACE.TabIndex = 0;
			customSimpleButtonACE.Text = "широкие ЭЙС, Клевер (новый)";
			customSimpleButtonACE.Click += customSimpleButtonACE_Click;
			// 
			// Root
			// 
			Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			Root.GroupBordersVisible = false;
			Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3, layoutControlItem4, emptySpaceItem2, emptySpaceItem6, emptySpaceItem7, emptySpaceItem8, layoutControlItem5, layoutControlItem6 });
			Root.Name = "Root";
			Root.Size = new System.Drawing.Size(617, 497);
			Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			layoutControlItem1.Control = customSimpleButtonACE;
			layoutControlItem1.Location = new System.Drawing.Point(0, 52);
			layoutControlItem1.MaxSize = new System.Drawing.Size(300, 40);
			layoutControlItem1.MinSize = new System.Drawing.Size(300, 40);
			layoutControlItem1.Name = "layoutControlItem1";
			layoutControlItem1.Size = new System.Drawing.Size(300, 40);
			layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			layoutControlItem2.Control = customHeaderLabel1;
			layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			layoutControlItem2.Name = "layoutControlItem2";
			layoutControlItem2.Size = new System.Drawing.Size(597, 31);
			layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			layoutControlItem3.Control = customSimpleButtonKompl;
			layoutControlItem3.Location = new System.Drawing.Point(0, 92);
			layoutControlItem3.MaxSize = new System.Drawing.Size(300, 40);
			layoutControlItem3.MinSize = new System.Drawing.Size(300, 40);
			layoutControlItem3.Name = "layoutControlItem3";
			layoutControlItem3.Size = new System.Drawing.Size(300, 40);
			layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			layoutControlItem4.Control = customSimpleButtonNabor;
			layoutControlItem4.Location = new System.Drawing.Point(0, 132);
			layoutControlItem4.MaxSize = new System.Drawing.Size(300, 41);
			layoutControlItem4.MinSize = new System.Drawing.Size(300, 41);
			layoutControlItem4.Name = "layoutControlItem4";
			layoutControlItem4.Size = new System.Drawing.Size(300, 41);
			layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			layoutControlItem4.TextVisible = false;
			// 
			// emptySpaceItem2
			// 
			emptySpaceItem2.Location = new System.Drawing.Point(300, 92);
			emptySpaceItem2.Name = "emptySpaceItem2";
			emptySpaceItem2.Size = new System.Drawing.Size(297, 40);
			// 
			// emptySpaceItem6
			// 
			emptySpaceItem6.Location = new System.Drawing.Point(300, 132);
			emptySpaceItem6.Name = "emptySpaceItem6";
			emptySpaceItem6.Size = new System.Drawing.Size(297, 41);
			// 
			// emptySpaceItem7
			// 
			emptySpaceItem7.Location = new System.Drawing.Point(300, 52);
			emptySpaceItem7.Name = "emptySpaceItem7";
			emptySpaceItem7.Size = new System.Drawing.Size(297, 40);
			// 
			// emptySpaceItem8
			// 
			emptySpaceItem8.Location = new System.Drawing.Point(0, 31);
			emptySpaceItem8.Name = "emptySpaceItem8";
			emptySpaceItem8.Size = new System.Drawing.Size(597, 21);
			// 
			// layoutControlItem5
			// 
			layoutControlItem5.Control = gridControlRazmKol;
			layoutControlItem5.Location = new System.Drawing.Point(0, 173);
			layoutControlItem5.Name = "layoutControlItem5";
			layoutControlItem5.Size = new System.Drawing.Size(300, 304);
			layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			layoutControlItem6.Control = gridControlBlVsh;
			layoutControlItem6.Location = new System.Drawing.Point(300, 173);
			layoutControlItem6.Name = "layoutControlItem6";
			layoutControlItem6.Size = new System.Drawing.Size(297, 304);
			layoutControlItem6.TextVisible = false;
			// 
			// emptySpaceItem3
			// 
			emptySpaceItem3.Location = new System.Drawing.Point(298, 31);
			emptySpaceItem3.Name = "emptySpaceItem2";
			emptySpaceItem3.Size = new System.Drawing.Size(299, 40);
			// 
			// emptySpaceItem4
			// 
			emptySpaceItem4.Location = new System.Drawing.Point(298, 71);
			emptySpaceItem4.Name = "emptySpaceItem2";
			emptySpaceItem4.Size = new System.Drawing.Size(299, 26);
			// 
			// emptySpaceItem5
			// 
			emptySpaceItem5.Location = new System.Drawing.Point(0, 453);
			emptySpaceItem5.Name = "emptySpaceItem2";
			emptySpaceItem5.Size = new System.Drawing.Size(597, 24);
			// 
			// PrintSewn
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(617, 497);
			Controls.Add(customLayoutControl1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Name = "PrintSewn";
			Text = "Печать вшивок";
			Load += PrintSewn_Load;
			((System.ComponentModel.ISupportInitialize)customLayoutControl1).EndInit();
			customLayoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)gridControlBlVsh).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
			((System.ComponentModel.ISupportInitialize)gridControlRazmKol).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)Root).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem7).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem8).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem4).EndInit();
			((System.ComponentModel.ISupportInitialize)emptySpaceItem5).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Class.CustomHeaderLabel customHeaderLabel1;
        private Class.CustomLayoutControl customLayoutControl1;
        private Class.CustomSimpleButton customSimpleButtonACE;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Class.CustomSimpleButton customSimpleButtonNabor;
        private Class.CustomSimpleButton customSimpleButtonKompl;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private Class.CustomGridControl gridControlBlVsh;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private Class.CustomGridControl gridControlRazmKol;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}