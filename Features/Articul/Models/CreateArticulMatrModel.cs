using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Models
{
    public class CreateArticulMatrModel :  INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Nn { get; set; }
        /// <summary>
        /// при нормализации становится Mod
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// повторный артикул
        /// </summary>
        public string RepeatArticle { get; set; }
        /// <summary>
        /// найденная модель по модели с учетом исключения FromAceToCle
        /// </summary>
        public string FoundMod { get; set; }
        /// <summary>
        /// менеджер
        /// </summary>
        public string Grupmen_name { get; set; }
        /// <summary>
        /// сезон
        /// </summary>
        public string Tsn_name { get; set; }
        /// <summary>
        /// товарная группа
        /// </summary>
        public int Men_int { get; set; }
        /// <summary>
        /// идентификатор сезона
        /// </summary>
        public int Baza { get; set; }
        /// <summary>
        /// блок
        /// </summary>
        public string Tb_id { get; set; }
        /// <summary>
        /// модель
        /// </summary>
        public string Mod { get; set; }
        /// <summary>
        /// расширенная модель с буковками из матрицы
        /// </summary>
        public string ModMatrix { get; set; }
        /// <summary>
        /// артикул
        /// </summary>
        public string Articul { get; set; }
        public string MatrixGrupName { get; set; }
        public DateTime? DatePublic { get; set; }
        /// <summary>
        /// торговая марка
        /// </summary>
        public string Tm_name { get; set; }
        /// <summary>
        /// Торговая марка (*, -, +, , 5)
        /// </summary>
        public string Kle {  get; set; }
        /// <summary>
        /// группа
        /// </summary>
        public string Grup { get; set; }
        /// <summary>
        /// модельный признак
        /// </summary>
        public string Text_mo { get; set; }
        /// <summary>
        /// ассортимент
        /// </summary>
        public int Kod_v { get; set; }
        public string AssortName { get; set; }
        public string Tat_name { get; set; }
        /// <summary>
        /// тип принта при печати на принтере, сейчас почти не используется
        /// </summary>
        public int Printer { get; set; }
        /// <summary>
        /// бусины
        /// </summary>
        public int Bus { get; set; }
        /// <summary>
        /// стразы
        /// </summary>
        public int Stra { get; set; }
        /// <summary>
        /// пресс
        /// </summary>
        public int P_pres { get; set; }
        /// <summary>
        /// принты
        /// </summary>
        public int P { get; set; }
        /// <summary>
        /// дополнительные символы для торговой марки, типа № принта...
        /// </summary>
        public string Mod_v { get; set; }
        /// <summary>
        /// вышивка
        /// </summary>
        public int V { get; set; }
        /// <summary>
        /// круж
        /// </summary>
        public int Kruj { get; set; }
        /// <summary>
        /// полное название полотна
        /// </summary>
        public string Tkan { get; set; }
        /// <summary>
        /// основа
        /// </summary>
        public string Sost { get; set; }
        public string RazmNames { get; set; }
        /// <summary>
        /// описание
        /// </summary>
        public string Komment { get; set; }
        /// <summary>
        /// не используется
        /// </summary>
        public string Sost1 { get; set; }
        /// <summary>
        /// отделка
        /// </summary>
        public string Sost2 { get; set; }
        /// <summary>
        /// подклад/наполнитель
        /// </summary>
        public string Sost3 { get; set; }
        public int Ag_id { get; set; }
        /// <summary>
        /// гост
        /// </summary>
        public int Id_gost { get; set; }
        public string GostName { get; set; }
        /// <summary>
        /// Полотно
        /// </summary>
        public string Tkb {  get; set; }
        public DateTime? DateCertificationApproval { get; set; }
        [NotMapped] public string Unic_IdGost_idAg { get => $"{Ag_id}|{Id_gost}";
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                var parts = value.Split('|');
                if (parts.Length != 2)
                    return;

                Ag_id = int.Parse(parts[0]);

                if (int.TryParse(parts[1], out int idGost))
                    Id_gost = idGost;
            } // уникальное поле для поиска гост + группа по госту
        }
    }
}
