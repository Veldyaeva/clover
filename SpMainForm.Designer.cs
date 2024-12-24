namespace SewingProduction
{
    partial class SpMainForm
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
            this.components = new System.ComponentModel.Container();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оборудованиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оборудованиеВБригадахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оборудованиеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.видыОборудованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.матрицыКлассовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видыОперацийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.карточкаРасчетаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.изделияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.производствоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рабочийСтолМастераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TeamWorktoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // popupMenu1
            // 
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1048, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 450);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1048, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 450);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1048, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 450);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.производствоToolStripMenuItem,
            this.TeamWorktoolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оборудованиеToolStripMenuItem,
            this.карточкаРасчетаToolStripMenuItem1,
            this.изделияToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // оборудованиеToolStripMenuItem
            // 
            this.оборудованиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оборудованиеВБригадахToolStripMenuItem,
            this.оборудованиеToolStripMenuItem1,
            this.видыОборудованияToolStripMenuItem,
            this.матрицыКлассовToolStripMenuItem,
            this.видыОперацийToolStripMenuItem});
            this.оборудованиеToolStripMenuItem.Name = "оборудованиеToolStripMenuItem";
            this.оборудованиеToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.оборудованиеToolStripMenuItem.Text = "Оборудование";
            // 
            // оборудованиеВБригадахToolStripMenuItem
            // 
            this.оборудованиеВБригадахToolStripMenuItem.Name = "оборудованиеВБригадахToolStripMenuItem";
            this.оборудованиеВБригадахToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.оборудованиеВБригадахToolStripMenuItem.Text = "Оборудование в бригадах";
            this.оборудованиеВБригадахToolStripMenuItem.Click += new System.EventHandler(this.оборудованиеВБригадахToolStripMenuItem_Click);
            // 
            // оборудованиеToolStripMenuItem1
            // 
            this.оборудованиеToolStripMenuItem1.Name = "оборудованиеToolStripMenuItem1";
            this.оборудованиеToolStripMenuItem1.Size = new System.Drawing.Size(262, 22);
            this.оборудованиеToolStripMenuItem1.Text = "Оборудование";
            this.оборудованиеToolStripMenuItem1.Click += new System.EventHandler(this.оборудованиеToolStripMenuItem_Click);
            // 
            // видыОборудованияToolStripMenuItem
            // 
            this.видыОборудованияToolStripMenuItem.Name = "видыОборудованияToolStripMenuItem";
            this.видыОборудованияToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.видыОборудованияToolStripMenuItem.Text = "Группы оборудования";
            this.видыОборудованияToolStripMenuItem.Click += new System.EventHandler(this.видыОборудованияToolStripMenuItem_Click);
            // 
            // матрицыКлассовToolStripMenuItem
            // 
            this.матрицыКлассовToolStripMenuItem.Name = "матрицыКлассовToolStripMenuItem";
            this.матрицыКлассовToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.матрицыКлассовToolStripMenuItem.Text = "Классы вязального оборудования";
            this.матрицыКлассовToolStripMenuItem.Click += new System.EventHandler(this.матрицаКлассовToolStripMenuItem_Click);
            // 
            // видыОперацийToolStripMenuItem
            // 
            this.видыОперацийToolStripMenuItem.Name = "видыОперацийToolStripMenuItem";
            this.видыОперацийToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.видыОперацийToolStripMenuItem.Text = "Виды операций";
            this.видыОперацийToolStripMenuItem.Click += new System.EventHandler(this.видОперацToolStripMenuItem_Click);
            // 
            // карточкаРасчетаToolStripMenuItem1
            // 
            this.карточкаРасчетаToolStripMenuItem1.Name = "карточкаРасчетаToolStripMenuItem1";
            this.карточкаРасчетаToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.карточкаРасчетаToolStripMenuItem1.Text = "Карточка расчета";
            this.карточкаРасчетаToolStripMenuItem1.Click += new System.EventHandler(this.карточкаРасчетаToolStripMenuItem1_Click);
            // 
            // изделияToolStripMenuItem
            // 
            this.изделияToolStripMenuItem.Name = "изделияToolStripMenuItem";
            this.изделияToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.изделияToolStripMenuItem.Text = "Изделия";
            // 
            // производствоToolStripMenuItem
            // 
            this.производствоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рабочийСтолМастераToolStripMenuItem});
            this.производствоToolStripMenuItem.Name = "производствоToolStripMenuItem";
            this.производствоToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.производствоToolStripMenuItem.Text = "Производство";
            // 
            // рабочийСтолМастераToolStripMenuItem
            // 
            this.рабочийСтолМастераToolStripMenuItem.Name = "рабочийСтолМастераToolStripMenuItem";
            this.рабочийСтолМастераToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.рабочийСтолМастераToolStripMenuItem.Text = "Рабочий стол мастера";
            // 
            // TeamWorktoolStripMenuItem
            // 
            this.TeamWorktoolStripMenuItem.Name = "TeamWorktoolStripMenuItem";
            this.TeamWorktoolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.TeamWorktoolStripMenuItem.Text = "Разделения труда";
            this.TeamWorktoolStripMenuItem.Click += new System.EventHandler(this.TeamWorktoolStripMenuItem_Click);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.PageAdded += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageAdded);
            // 
            // SpMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpMainForm";
            this.Text = "Швейное производство";
            this.Load += new System.EventHandler(this.SpMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem карточкаРасчетаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеВБригадахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оборудованиеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem видыОборудованияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem матрицыКлассовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыОперацийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изделияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem производствоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рабочийСтолМастераToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разделенияТрудаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TeamWorktoolStripMenuItem;
    }
}

