using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Models
{
    /// <summary>
    ///Список ФИО
    /// </summary>
    public class FioModel
    {
        /// <summary>
        /// Табельный номер
        /// </summary>
        //public int tab { get; set; }

        ///// <summary>
        /////ФИО
        ///// </summary>
        //public string fio { get; set; }
        [Column("tab")]
        public int Tab { get; set; }

        [Column("fio")]
        public string Fio { get; set; }

        public FioModel Clone()
        {
            // MemberwiseClone создает "поверхностную" копию.
            // Для простых типов и строк это нормально.
            // Если есть ссылочные типы (кроме string), которые должны быть независимо скопированы (глубокое клонирование),
            // то их нужно клонировать отдельно внутри этого метода.
            FioModel cloned = (FioModel)this.MemberwiseClone();
            return cloned;
        }
    }
}