//using SewingProduction.form.TeamWork;
//using SewingProduction.form.TeamWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.ChartRangeControlClient.Core;
using DevExpress.CodeParser;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly Services.ArtNormService _artNormService;
        private DataTable table1, table2, table3, table4;
        private SqlDataAdapter adapter1, adapter2, adapter3, adapter4;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly int _newAnnId = -1;
        private int selectedRowHandle = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;
        public TeamWork_AdvanceTW(int Id, int bufferWorkDivision, int mode)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
            _newAnnId = Id;

        }
        private void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            //gridControl5.DataSource = table1;
            gridControl2.DataSource = table2;
            gridControl2.DataSource = table3;
            gridControl3.DataSource = table4;
            //norm_raszTableAdapter.Fill(aCE_backupDataSet.norm_rasz); // Загружаем данные из БД в DataSet
            normraszBindingSource.DataSource = aCE_backupDataSet.norm_rasz; // Привязываем BindingSource к DataSet
            gridControl5.DataSource = normraszBindingSource;
            //_raszDataTable = new DataTable();
            //_raszDataTable.Columns.Add("kod_o", typeof(string));
            //_raszDataTable.Columns.Add("text", typeof(string));
            //_raszDataTable.Columns.Add("spec", typeof(string));
            //_raszDataTable.Columns.Add("razryd", typeof(int));
            //_raszDataTable.Columns.Add("obor", typeof(string));
            //_raszDataTable.Columns.Add("kod_proizv", typeof(string));

            //// Привязываем DataTable к BindingSource
            //normraszBindingSource.DataSource = _raszDataTable;



            switch (_mode)
            {
                case (int)Mode.NewWorkDivision:
                    //                  richTextBox1.Text = $"группа: {ANNgridView.GetRowCellValue(_bufferWorkDivision, "grup")}, модель {ANNgridView.GetRowCellValue(_bufferWorkDivision, "mod")}, артикул: {ANNgridView.GetRowCellValue(_bufferWorkDivision, "articul")}";
                    this.Text = "Добавить предварительное";
                    break;
                case (int)Mode.ArchAndCopy:
                    this.Text = "Архив+копия";
                    bufferLoad();
                    break;
                case (int)Mode.Archive:
                    this.Text = "В архив";
                    bufferLoad();
                    break;
                case (int)Mode.Edit:
                    this.Text = "Редактировать";
                    bufferLoad();
                    break;
            }
        }

        #region load
        private void bufferLoad()
        {
            try
            {
                if (_bufferWorkDivision > 0)
                {
                    var data = _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
                    normraszBindingSource.DataSource = data;
                    gridControl5.DataSource = normraszBindingSource;
                    data = _artNormService.GetRelatedNormRask(_bufferWorkDivision);
                    normraskBindingSource.DataSource = data;
                    gridControl2.DataSource = normraskBindingSource;
                    data = _artNormService.GetRelatedNormKont(_bufferWorkDivision);
                    normkontBindingSource.DataSource = data;
                    gridControl3.DataSource = normkontBindingSource;
                    data = _artNormService.GetRelatedNormDopObr(_bufferWorkDivision);
                    normdopobrBindingSource.DataSource = data;
                    gridControl4.DataSource = normdopobrBindingSource;
                }
                else { MessageBox.Show("В буфер ничего не скопировано", "внимание", MessageBoxButtons.OK); }
            }
            catch (Exception ex)
            { }
        }


        #endregion
        #region Norm_rasz

        private void customButton1_Click(object sender, EventArgs e)
        {
            gridView5.AddNewRow();
            int newRowHandle = gridView5.RowCount-1;
            gridView5.FocusedRowHandle = newRowHandle; 

            using (NormOperNew selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;

                    gridView5.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
                    gridView5.SetRowCellValue(newRowHandle, "text", selectedData.Text);
                    gridView5.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
                    gridView5.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
                    gridView5.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
                    gridView5.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);
                    gridView5.SetRowCellValue(newRowHandle, "text_proizv", selectedData.Text_proizv);
                    gridView5.SetRowCellValue(newRowHandle, "text_ob", selectedData.Text_ob);
                    gridView5.SetRowCellValue(newRowHandle, "text_vyaz", selectedData.Text_vyaz);
                            
                    gridView5.UpdateCurrentRow();
                            
                   // gridView5.ShowPopupEditForm();
                }
                else
                {
                    gridView5.DeleteRow(newRowHandle);
                }
            }

        }



        private DataTable _raszDataTable;

        private void bufferButton_Click(object sender, EventArgs e)
        {
            bufferLoad();
        }

        private void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            using (NormOperNew selectionForm = new NormOperNew())
            {
                int newRowHandle = e.RowHandle;
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        // Заполняем новую строку выбранными значениями
                        gridView5.SetRowCellValue(newRowHandle, "annId", _newAnnId);
                        gridView5.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
                        gridView5.SetRowCellValue(newRowHandle, "text", selectedData.Text);
                        gridView5.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
                        gridView5.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
                        gridView5.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
                        gridView5.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);
                        gridView5.SetRowCellValue(newRowHandle, "text_proizv", selectedData.Text_proizv);
                        gridView5.SetRowCellValue(newRowHandle, "text_ob", selectedData.Text_ob);
                        gridView5.SetRowCellValue(newRowHandle, "text_vyaz", selectedData.Text_vyaz);

                        gridView5.UpdateCurrentRow();//применяем изменения
                        normraszBindingSource.EndEdit();//заканчиваем редактирование
                        SaveData();//сохраняем данные в бд
                                   // Обновляем `DataSet`
                        norm_raszTableAdapter.Update(aCE_backupDataSet.norm_rasz);
                        norm_raszTableAdapter.Fill(aCE_backupDataSet.norm_rasz);
                        gridControl5.RefreshDataSource();
                        gridView5.RefreshData();

                        //// Ждём обновления данных перед поиском строки
                        //Task.Delay(100).Wait();

                        //// Ищем строку в `GridView`
                        //int realRowHandle = gridView5.LocateByValue("annId", _newAnnId);
                        //if (realRowHandle >= 0)
                        //{
                        //    gridView5.FocusedRowHandle = realRowHandle;
                        //    gridView5.ShowPopupEditForm();
                        //}

                        // Ожидаем обновления данных перед поиском строки
                        Task.Delay(100).ContinueWith(_ =>
                        {
                            if (!gridControl5.IsDisposed && gridControl5.IsHandleCreated)
                            {
                                Invoke(new Action(() =>
                                {
                                    int realRowHandle = gridView5.LocateByValue("annId", _newAnnId);
                                    if (realRowHandle >= 0)
                                    {
                                        gridView5.FocusedRowHandle = realRowHandle;
                                        gridView5.ShowPopupEditForm();
                                    }
                                }));
                            }
                        });
                    }
                }
                else
                {
                    gridView5.CancelUpdateCurrentRow();
                }
            }
        }


        private void SaveData()
        {
           // this.Validate(); // Завершаем редактирование текущей строки
            normraszBindingSource.EndEdit(); // Применяем изменения из BindingSource в DataTable
            norm_raszTableAdapter.Update(aCE_backupDataSet.norm_rasz); // Отправляем изменения в БД
            gridControl5.RefreshDataSource();
        }

        private void gridView5_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }


        #endregion

        #region Norm_rask

        private void customButton2_Click(object sender, EventArgs e)
        {
            AddRaskRow();
            int newRowHandle = gridView2.FocusedRowHandle;

            // Открываем форму выбора данных
            using (NormOperNew selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    // Получаем выбранные данные
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        // Заполняем `EditForm` значениями
                        gridView2.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
                        gridView2.SetRowCellValue(newRowHandle, "text", selectedData.Text);
                        gridView2.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
                        gridView2.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
                        gridView2.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
                        gridView2.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);
                        
                    }
                }
            }

            // Теперь вызываем EditForm для новой строки
            gridView2.ShowEditForm();

        }

        private void gridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //AddNewRow();
            //int newRowHandle = gridView1.FocusedRowHandle;

            //// Открываем форму выбора данных
            //using (NormOperNew selectionForm = new NormOperNew())
            //{
            //    if (selectionForm.ShowDialog() == DialogResult.OK)
            //    {
            //        // Получаем выбранные данные
            //        var selectedData = selectionForm.SelectedRowData;
            //        if (selectedData != null)
            //        {
            //            // Заполняем `EditForm` значениями
            //            gridView1.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
            //            gridView1.SetRowCellValue(newRowHandle, "text", selectedData.Text);
            //            gridView1.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
            //            gridView1.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
            //            gridView1.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
            //            gridView1.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);

            //        }
            //    }
            //}

            //// Теперь вызываем EditForm для новой строки
            //gridView1.ShowEditForm();

        }


        private DataTable _raskDataTable;

        private void AddRaskRow()
        {
            if (_raszDataTable == null) return;

            DataRow newRow = _raskDataTable.NewRow();
            //newRow["kod_o"] = "";
            //newRow["text"] = "";
            //newRow["spec"] = "";
            //newRow["razryd"] = 0;
            //newRow["obor"] = "";
            //newRow["kod_proizv"] = "";

            _raskDataTable.Rows.Add(newRow);
            normraskBindingSource.ResetBindings(false); // Обновляем привязку

            // Устанавливаем фокус на новую строку
            gridView2.MoveLast(); // Перемещаемся на последнюю строку
        }
        #endregion

        #region Norm_kont

        #endregion

        #region Norm_dop_obr
        private void gridView4_ShowingEditor(object sender, CancelEventArgs e)
        {

            gridView4.AddNewRow();
            if (gridView4.RowCount >= 2)
            {
                // MessageBox.Show("Вы не можете добавить больше двух строк!", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Устанавливаем результат
            this.Close(); // Закрываем окно
        }
    }
}
