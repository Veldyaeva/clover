using System.Drawing;

namespace SewingProduction.Features.TeamWork.Forms
{
    partial class BaseNodeSaveForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            summaryLabel = new System.Windows.Forms.Label();
            namePanel = new System.Windows.Forms.TableLayoutPanel();
            nameCaptionLabel = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            descriptionPanel = new System.Windows.Forms.TableLayoutPanel();
            descriptionCaptionLabel = new System.Windows.Forms.Label();
            descriptionTextBox = new System.Windows.Forms.TextBox();
            previewGrid = new System.Windows.Forms.DataGridView();
            buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            tableLayoutPanelMain.SuspendLayout();
            namePanel.SuspendLayout();
            descriptionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).BeginInit();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(summaryLabel, 0, 0);
            tableLayoutPanelMain.Controls.Add(namePanel, 0, 1);
            tableLayoutPanelMain.Controls.Add(descriptionPanel, 0, 2);
            tableLayoutPanelMain.Controls.Add(previewGrid, 0, 3);
            tableLayoutPanelMain.Controls.Add(buttonsPanel, 0, 4);
            tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(12);
            tableLayoutPanelMain.RowCount = 5;
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelMain.Size = new System.Drawing.Size(884, 581);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // summaryLabel
            // 
            summaryLabel.AutoSize = true;
            summaryLabel.Dock = System.Windows.Forms.DockStyle.Top;
            summaryLabel.Location = new System.Drawing.Point(15, 12);
            summaryLabel.Name = "summaryLabel";
            summaryLabel.Size = new System.Drawing.Size(854, 20);
            summaryLabel.TabIndex = 0;
            summaryLabel.Text = "summaryLabel";
            // 
            // namePanel
            // 
            namePanel.ColumnCount = 1;
            namePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            namePanel.Controls.Add(nameCaptionLabel, 0, 0);
            namePanel.Controls.Add(nameTextBox, 0, 1);
            namePanel.Dock = System.Windows.Forms.DockStyle.Top;
            namePanel.Location = new System.Drawing.Point(15, 35);
            namePanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            namePanel.Name = "namePanel";
            namePanel.RowCount = 2;
            namePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            namePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            namePanel.Size = new System.Drawing.Size(854, 58);
            namePanel.TabIndex = 1;
            // 
            // nameCaptionLabel
            // 
            nameCaptionLabel.AutoSize = true;
            nameCaptionLabel.Location = new System.Drawing.Point(3, 0);
            nameCaptionLabel.Name = "nameCaptionLabel";
            nameCaptionLabel.Size = new System.Drawing.Size(101, 20);
            nameCaptionLabel.TabIndex = 0;
            nameCaptionLabel.Text = "Название узла";
            // 
            // nameTextBox
            // 
            nameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            nameTextBox.Location = new System.Drawing.Point(3, 23);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.PlaceholderText = "Например: Обработка манжеты";
            nameTextBox.Size = new System.Drawing.Size(848, 27);
            nameTextBox.TabIndex = 1;
            // 
            // descriptionPanel
            // 
            descriptionPanel.ColumnCount = 1;
            descriptionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            descriptionPanel.Controls.Add(descriptionCaptionLabel, 0, 0);
            descriptionPanel.Controls.Add(descriptionTextBox, 0, 1);
            descriptionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            descriptionPanel.Location = new System.Drawing.Point(15, 103);
            descriptionPanel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            descriptionPanel.Name = "descriptionPanel";
            descriptionPanel.RowCount = 2;
            descriptionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            descriptionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            descriptionPanel.Size = new System.Drawing.Size(854, 101);
            descriptionPanel.TabIndex = 2;
            // 
            // descriptionCaptionLabel
            // 
            descriptionCaptionLabel.AutoSize = true;
            descriptionCaptionLabel.Location = new System.Drawing.Point(3, 0);
            descriptionCaptionLabel.Name = "descriptionCaptionLabel";
            descriptionCaptionLabel.Size = new System.Drawing.Size(83, 20);
            descriptionCaptionLabel.TabIndex = 0;
            descriptionCaptionLabel.Text = "Описание";
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            descriptionTextBox.Location = new System.Drawing.Point(3, 23);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            descriptionTextBox.Size = new System.Drawing.Size(848, 75);
            descriptionTextBox.TabIndex = 1;
            // 
            // previewGrid
            // 
            previewGrid.AllowUserToAddRows = false;
            previewGrid.AllowUserToDeleteRows = false;
            previewGrid.AllowUserToResizeRows = false;
            previewGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            previewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            previewGrid.Location = new System.Drawing.Point(15, 216);
            previewGrid.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            previewGrid.MultiSelect = false;
            previewGrid.Name = "previewGrid";
            previewGrid.ReadOnly = true;
            previewGrid.RowHeadersWidth = 51;
            previewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            previewGrid.Size = new System.Drawing.Size(854, 306);
            previewGrid.TabIndex = 3;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(okButton);
            buttonsPanel.Controls.Add(cancelButton);
            buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            buttonsPanel.Location = new System.Drawing.Point(15, 534);
            buttonsPanel.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Size = new System.Drawing.Size(854, 35);
            buttonsPanel.TabIndex = 4;
            // 
            // okButton
            // 
            okButton.AutoSize = true;
            okButton.Location = new System.Drawing.Point(739, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(112, 29);
            okButton.TabIndex = 0;
            okButton.Text = "Сохранить";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.AutoSize = true;
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(643, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(90, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // BaseNodeSaveForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new System.Drawing.Size(884, 581);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(760, 520);
            Name = "BaseNodeSaveForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Сохранить как базовый узел";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            namePanel.ResumeLayout(false);
            namePanel.PerformLayout();
            descriptionPanel.ResumeLayout(false);
            descriptionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewGrid).EndInit();
            buttonsPanel.ResumeLayout(false);
            buttonsPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.TableLayoutPanel namePanel;
        private System.Windows.Forms.Label nameCaptionLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TableLayoutPanel descriptionPanel;
        private System.Windows.Forms.Label descriptionCaptionLabel;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.DataGridView previewGrid;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
