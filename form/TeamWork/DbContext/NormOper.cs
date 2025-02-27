using System;
//using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SewingProduction.BdContext
{
    [Table("norm_oper", Schema = "dbo")]
    public class NormOper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nchar(3)")]
        public string KodO { get; set; }

        [Column(TypeName = "nchar(60)")]
        public string Text { get; set; }

        [Column(TypeName = "nchar(1)")]
        public string Po { get; set; }

        public int? N { get; set; }
        public int? N1 { get; set; }
        public int? Sek { get; set; }

        [Column(TypeName = "nchar(1)")]
        public string New { get; set; }

        public int? Razryd { get; set; }

        [Column(TypeName = "nchar(3)")]
        public string Spec { get; set; }

        [Column(TypeName = "nchar(35)")]
        public string Obor { get; set; }

        public int? KodOb { get; set; }
        public int? KodProizv { get; set; }

    }
}

public class NormOperTable
{
    public DataTable Table { get; private set; }

    public NormOperTable()
    {
        Table = new DataTable("NormOper");

        // Определение столбцов (соответствует структуре NormOperData)
        Table.Columns.Add("Id", typeof(int));
        Table.Columns.Add("KodO", typeof(string));   // nchar(3)
        Table.Columns.Add("Text", typeof(string));   // nchar(60)
        Table.Columns.Add("Po", typeof(string));     // nchar(1)
        Table.Columns.Add("N", typeof(int));
        Table.Columns.Add("N1", typeof(int));
        Table.Columns.Add("Sek", typeof(int));
        Table.Columns.Add("New", typeof(string));    // nchar(1)
        Table.Columns.Add("Razryd", typeof(int));
        Table.Columns.Add("Spec", typeof(string));   // nchar(3)
        Table.Columns.Add("Obor", typeof(string));   // nchar(35)
        Table.Columns.Add("KodOb", typeof(int));
        Table.Columns.Add("KodProizv", typeof(int));
        Table.Columns.Add("IsChecked", typeof(bool)); // Поле для UI

        // Установка автоинкремента для ID (если локальная таблица)
        Table.Columns["Id"].AutoIncrement = true;
        Table.Columns["Id"].AutoIncrementSeed = 1;
        Table.Columns["Id"].AutoIncrementStep = 1;
    }

    /// <summary>
    /// Добавление новой строки в таблицу
    /// </summary>
    public void AddRow(string kodO, string text, string po, int? n, int? n1, int? sek,
                       string newVal, int? razryd, string spec, string obor, int? kodOb, int? kodProizv)
    {
        DataRow row = Table.NewRow();
        row["KodO"] = kodO;
        row["Text"] = text;
        row["Po"] = po;
        row["N"] = (object)n ?? DBNull.Value;
        row["N1"] = (object)n1 ?? DBNull.Value;
        row["Sek"] = (object)sek ?? DBNull.Value;
        row["New"] = newVal;
        row["Razryd"] = (object)razryd ?? DBNull.Value;
        row["Spec"] = spec;
        row["Obor"] = obor;
        row["KodOb"] = (object)kodOb ?? DBNull.Value;
        row["KodProizv"] = (object)kodProizv ?? DBNull.Value;
        row["IsChecked"] = false;

        Table.Rows.Add(row);
    }
}
