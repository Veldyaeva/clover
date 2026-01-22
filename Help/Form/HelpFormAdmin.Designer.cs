using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace SewingProduction.HelpAdmin.Forms
{
    partial class AdminHelpEditorForm
    {
        private System.ComponentModel.IContainer components = null;

        private PanelControl panelTop;
        private SplitContainerControl splitContainer;
        private MemoEdit memoHtml;
        private WebBrowser webBrowserPreview;

        private SimpleButton buttonUseActiveForm;
        private SimpleButton buttonLoad;
        private SimpleButton buttonTemplate;
        private SimpleButton buttonSave;
        private SimpleButton buttonOpenFolder;
        private SimpleButton buttonRefreshPreview;
        private CheckEdit checkAutoPreview;

        private LabelControl labelHelpPath;
        private LabelControl labelStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            panelTop = new PanelControl();
            splitContainer = new SplitContainerControl();
            memoHtml = new MemoEdit();
            webBrowserPreview = new WebBrowser();

            buttonUseActiveForm = new SimpleButton();
            buttonLoad = new SimpleButton();
            buttonTemplate = new SimpleButton();
            buttonSave = new SimpleButton();
            buttonOpenFolder = new SimpleButton();
            buttonRefreshPreview = new SimpleButton();
            checkAutoPreview = new CheckEdit();

            labelHelpPath = new LabelControl();
            labelStatus = new LabelControl();

            ((System.ComponentModel.ISupportInitialize)panelTop).BeginInit();
            panelTop.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer.Panel1).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer.Panel2).BeginInit();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)memoHtml.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)checkAutoPreview.Properties).BeginInit();

            SuspendLayout();

            // Form
            this.Text = "Редактор инструкций (Админ)";
            this.ClientSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += AdminHelpEditorForm_Load;

            // panelTop
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 78;

            buttonUseActiveForm.Location = new Point(12, 10);
            buttonUseActiveForm.Size = new Size(160, 24);
            buttonUseActiveForm.Text = "По активной форме";
            buttonUseActiveForm.Click += buttonUseActiveForm_Click;

            buttonLoad.Location = new Point(178, 10);
            buttonLoad.Size = new Size(90, 24);
            buttonLoad.Text = "Загрузить";
            buttonLoad.Click += buttonLoad_Click;

            buttonTemplate.Location = new Point(274, 10);
            buttonTemplate.Size = new Size(90, 24);
            buttonTemplate.Text = "Шаблон";
            buttonTemplate.Click += buttonTemplate_Click;

            buttonSave.Location = new Point(370, 10);
            buttonSave.Size = new Size(110, 24);
            buttonSave.Text = "Сохранить";
            buttonSave.Click += buttonSave_Click;

            buttonOpenFolder.Location = new Point(486, 10);
            buttonOpenFolder.Size = new Size(130, 24);
            buttonOpenFolder.Text = "Открыть папку";
            buttonOpenFolder.Click += buttonOpenFolder_Click;

            checkAutoPreview.Location = new Point(622, 12);
            checkAutoPreview.Size = new Size(150, 20);
            checkAutoPreview.Text = "Автопредпросмотр";
            checkAutoPreview.Checked = true;

            buttonRefreshPreview.Location = new Point(780, 10);
            buttonRefreshPreview.Size = new Size(110, 24);
            buttonRefreshPreview.Text = "Обновить";
            buttonRefreshPreview.Click += buttonRefreshPreview_Click;

            labelHelpPath.Location = new Point(12, 40);
            labelHelpPath.Size = new Size(1000, 16);
            labelHelpPath.Text = "(путь инструкции)";

            labelStatus.Location = new Point(12, 58);
            labelStatus.Size = new Size(500, 16);
            labelStatus.Text = "";

            panelTop.Controls.Add(buttonUseActiveForm);
            panelTop.Controls.Add(buttonLoad);
            panelTop.Controls.Add(buttonTemplate);
            panelTop.Controls.Add(buttonSave);
            panelTop.Controls.Add(buttonOpenFolder);
            panelTop.Controls.Add(checkAutoPreview);
            panelTop.Controls.Add(buttonRefreshPreview);
            panelTop.Controls.Add(labelHelpPath);
            panelTop.Controls.Add(labelStatus);

            // splitContainer
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.SplitterPosition = 600;

            // memoHtml
            memoHtml.Dock = DockStyle.Fill;
            memoHtml.Properties.Appearance.Font = new Font("Consolas", 10F);
            memoHtml.Properties.ScrollBars = ScrollBars.Both;
            memoHtml.Properties.WordWrap = false;
            splitContainer.Panel1.Controls.Add(memoHtml);

            // webBrowserPreview
            webBrowserPreview.Dock = DockStyle.Fill;
            splitContainer.Panel2.Controls.Add(webBrowserPreview);

            // add to form
            this.Controls.Add(splitContainer);
            this.Controls.Add(panelTop);

            ((System.ComponentModel.ISupportInitialize)checkAutoPreview.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)memoHtml.Properties).EndInit();

            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer.Panel1).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer.Panel2).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);

            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelTop).EndInit();

            ResumeLayout(false);
        }
    }
}
