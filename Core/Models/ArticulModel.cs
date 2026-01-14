using DevExpress.Xpo.Logger.Transport;
using SewingProduction.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;


namespace SewingProduction.Core.Models
{
    public class ArticulModel : INewable, IModifiable, IDeletable, INotifyPropertyChanged, ISupportInitialize
    {
        private bool _isInitializing;

        // ISupportInitialize
        public void BeginInit() => _isInitializing = true;

        public void EndInit()
        {
            _isInitializing = false;
            IsModified = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [NotMapped]public bool IsModified { get; set; } = false;
        [NotMapped]public bool IsNew { get; set; } = false;
        [NotMapped]public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// применение параметров для отслеживания  изменений в модели
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value,[CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;

            if (!_isInitializing)
                IsModified = true;

            OnPropertyChanged(propertyName!);

            return true;
        }
        public void AcceptChanges()
        {
            IsModified = false;
        }
        //основные поля 
        [NotMapped]public string Ko { get; set; }
        [NotMapped] public string baz { get; set; }
        [NotMapped] public string Kodd { get; set; }
        public string Kod { get; set; }
        
        public string Grup { get; set; }
        private int _ag_id;
        public int Ag_id{get => _ag_id; set=> SetField(ref _ag_id, value); }

        private string _articul;
        public string Articul { get=>_articul; 
            set => SetField(ref _articul, value);
        }
        private string _mod;
        public string Mod { get=>_mod;
            set => SetField(ref _mod, value);
        }
        private string _razm;
        public string Razm { get=>_razm;
            set => SetField(ref _razm, value);
        }
        private string _sost;
        public string Sost { get => _sost;
            set => SetField(ref _sost, value);
        }

        private string _sost2;
        public string Sost2 { 
            get=>_sost2;
            set => SetField(ref _sost2, value);
        }
        private string _sost3;
        public string Sost3 { get=>_sost3;
            set =>SetField(ref _sost3, value);
        }
        private int _id_gost ;
        public int Id_gost { get=>_id_gost;
            set=> SetField(ref _id_gost, value);
        }
        public string _gost;
        public string Gost { 
            get=>_gost;
            set => SetField(ref _gost, value);
        }

        public decimal Norm_t { get; set; }
        public decimal Norm_r { get; set; }
        public int Sek_shv { get; set; }
        public decimal Sh_r { get; set; }
        public double Seb_r { get; set; }
        public decimal Norm_n { get; set; }
        public decimal Kat_n { get; set; }
        public decimal Seb_n { get; set; }
        public decimal Seb_z { get; set; }
        public decimal Koef { get; set; }
        public decimal Seb_rekom { get; set; }
        public decimal Seb_proizv { get; set; }
        public string Po { get; set; }
        public decimal Seb_z_s { get; set; }
        public int Sek { get; set; }
        public int Sek_vyaz5 { get; set; }
        public int Sek_vyaz7 { get; set; }
        public int Sek_vyaz12 { get; set; }
        public int Sek_vyaz { get; set; }
        public string Pict { get; set; }
        public string Kod_shtr { get; set; }
        public string Kod_shtr_k { get; set; }
        public string Gr { get; set; }
        public string X { get; set; }
        public string Text { get; set; }
        public string _kle;
        public string Kle { get=>_kle;
            set => SetField(ref _kle, value);
            }
        private int _grupp;
        public int Grupp { get=>_grupp;
            set => SetField(ref _grupp, value);
        }
        public int P { get; set; }
        public int V { get; set; }
        public int S { get; set; }
        public string Kod_tov { get; set; }
        public string Gruppa { get; set; }
        public string P_gruppa { get; set; }
        public string Text_m { get; set; }
        private string _tkb;
        public string Tkb { 
            get=>_tkb;
            set => SetField(ref _tkb, value);
        }
        private string _kod_t1;
        public string Kod_t1
        {
            get => _kod_t1;
            set => SetField(ref _kod_t1, value);
        }

        private string _tkb1;
        public string Tkb1
        {
            get => _tkb1;
            set => SetField(ref _tkb1, value);
        }

        private decimal _norm_t1;
        public decimal Norm_t1
        {
            get => _norm_t1;
            set {
                if (!SetField(ref _norm_t1, value)) return;
                if (_norm_t1 == 0m && _brak_t1 != 0m)
                    Brak_t1 = 0m;
            }
        }

        private string _opis_t1;
        public string Opis_t1
        {
            get => _opis_t1;
            set => SetField(ref _opis_t1, value);
        }

        // === t2 ===
        private string _kod_t2;
        public string Kod_t2
        {
            get => _kod_t2;
            set => SetField(ref _kod_t2, value);
        }

        private string _tkb2;
        public string Tkb2
        {
            get => _tkb2;
            set => SetField(ref _tkb2, value);
        }

        private decimal _norm_t2;
        public decimal Norm_t2
        {
            get => _norm_t2;
            set
            {
                if (!SetField(ref _norm_t2, value)) return;
                if (_norm_t2 == 0m && _brak_t2 != 0m)
                    Brak_t2 = 0m;
            }
        }

        private string _opis_t2;
        public string Opis_t2
        {
            get => _opis_t2;
            set => SetField(ref _opis_t2, value);
        }

        // === t3 ===
        private string _kod_t3;
        public string Kod_t3
        {
            get => _kod_t3;
            set => SetField(ref _kod_t3, value);
        }

        private string _tkb3;
        public string Tkb3
        {
            get => _tkb3;
            set => SetField(ref _tkb3, value);
        }

        private decimal _norm_t3;
        public decimal Norm_t3
        {
            get => _norm_t3;
            set
            {
                if (!SetField(ref _norm_t3, value)) return;
                if (_norm_t3 == 0m && _brak_t3 != 0m)
                    Brak_t3 = 0m;
            }
        }

        private string _opis_t3;
        public string Opis_t3
        {
            get => _opis_t3;
            set => SetField(ref _opis_t3, value);
        }

        // === t4 ===
        private string _kod_t4;
        public string Kod_t4
        {
            get => _kod_t4;
            set => SetField(ref _kod_t4, value);
        }

        private string _tkb4;
        public string Tkb4
        {
            get => _tkb4;
            set => SetField(ref _tkb4, value);
        }

        private decimal _norm_t4;
        public decimal Norm_t4
        {
            get => _norm_t4;
            set
            {
                if (!SetField(ref _norm_t4, value)) return;
                if (_norm_t4 == 0m && _brak_t4 != 0m)
                    Brak_t4 = 0m;
            }
        }

        private string _opis_t4;
        public string Opis_t4
        {
            get => _opis_t4;
            set => SetField(ref _opis_t4, value);
        }

        // === t5 ===
        private string _kod_t5;
        public string Kod_t5
        {
            get => _kod_t5;
            set => SetField(ref _kod_t5, value);
        }

        private string _tkb5;
        public string Tkb5
        {
            get => _tkb5;
            set => SetField(ref _tkb5, value);
        }

        private decimal _norm_t5;
        public decimal Norm_t5
        {
            get => _norm_t5;
            set
            {
                if (!SetField(ref _norm_t5, value)) return;
                if (_norm_t5 == 0m && _brak_t5 != 0m)
                    Brak_t5 = 0m;
            }
        }

        private string _opis_t5;
        public string Opis_t5
        {
            get => _opis_t5;
            set => SetField(ref _opis_t5, value);
        }

        // === t6 ===
        private string _kod_t6;
        public string Kod_t6
        {
            get => _kod_t6;
            set => SetField(ref _kod_t6, value);
        }

        private string _tkb6;
        public string Tkb6
        {
            get => _tkb6;
            set => SetField(ref _tkb6, value);
        }

        private decimal _norm_t6;
        public decimal Norm_t6
        {
            get => _norm_t6;
            set
            {
                if (!SetField(ref _norm_t6, value)) return;
                if (_norm_t6 == 0m && _brak_t6 != 0m)
                    Brak_t6 = 0m;
            }
        }

        private string _opis_t6;
        public string Opis_t6
        {
            get => _opis_t6;
            set => SetField(ref _opis_t6, value);
        }

        // === t7 ===
        private string _kod_t7;
        public string Kod_t7
        {
            get => _kod_t7;
            set => SetField(ref _kod_t7, value);
        }

        private string _tkb7;
        public string Tkb7
        {
            get => _tkb7;
            set => SetField(ref _tkb7, value);
        }

        private decimal _norm_t7;
        public decimal Norm_t7
        {
            get => _norm_t7;
            set
            {
                if (!SetField(ref _norm_t7, value)) return;
                if (_norm_t7 == 0m && _brak_t7 != 0m)
                    Brak_t7 = 0m;
            }
        }

        private decimal _seb_t7;
        public decimal Seb_t7
        {
            get => _seb_t7;
            set => SetField(ref _seb_t7, value);
        }

        private string _opis_t7;
        public string Opis_t7
        {
            get => _opis_t7;
            set => SetField(ref _opis_t7, value);
        }


        public decimal Seb_dop { get; set; }
        public decimal Seb_t1 { get; set; }
        public decimal Seb_t3 { get; set; }
        public decimal Seb_t2 { get; set; }
        public decimal Seb_t4 { get; set; }
        public decimal Seb_t5 { get; set; }
        public decimal Seb_t6 { get; set; }

        public int Baza { get; set; }
        private int _kod_v;
        public int Kod_v { get => _kod_v; set => SetField(ref _kod_v, value); }

        public int Sek_vyaz6 { get; set; }
        public int Sek_vyaz10 { get; set; }
        public int Sek_vyazo { get; set; }
        public decimal Normapryz { get; set; }
        
        public int Id_svyaz { get; set; }
        public decimal Koef_pr { get; set; }
        public decimal Ob_izd { get; set; }
        public int Stavka_nds { get; set; }
        
        public int Sposob_up { get; set; }
        
        private int _id_country;
        public int Id_country { get => _id_country; set => SetField(ref _id_country, value); }
        public int Old_prch { get; set; }
        public int Sek_vyaz70 { get; set; }
        public int Sek_vyaz3 { get; set; }
        public decimal Koef_d { get; set; }
        public int St_nds { get; set; }
        public int Upd_razm { get; set; }
        public decimal Gl_rekom { get; set; }
        public int Sek_kr { get; set; }
        
        private int _arh;
        public int Arh { get => _arh; set => SetField(ref _arh, value); }

        public int Nds { get; set; }
        public string Ed_izm { get; set; }
        private decimal _brak_t1;
        public decimal Brak_t1 { get => _brak_t1; set => SetField(ref _brak_t1, value); }

        private decimal _brak_t2;
        public decimal Brak_t2 { get => _brak_t2; set => SetField(ref _brak_t2, value); }

        private decimal _brak_t3;
        public decimal Brak_t3 { get => _brak_t3; set => SetField(ref _brak_t3, value); }

        private decimal _brak_t4;
        public decimal Brak_t4 { get => _brak_t4; set => SetField(ref _brak_t4, value); }

        private decimal _brak_t5;
        public decimal Brak_t5 { get => _brak_t5; set => SetField(ref _brak_t5, value); }

        private decimal _brak_t6;
        public decimal Brak_t6 { get => _brak_t6; set => SetField(ref _brak_t6, value); }

        private decimal _brak_t7;
        public decimal Brak_t7 { get => _brak_t7; set => SetField(ref _brak_t7, value); }
        public decimal Brak_avg { get; set; }
        public decimal Cena_prdc { get; set; }
        private string _komb_det;
        public string Komb_det { get=>_komb_det;
            set { 
            if (_komb_det != value) 
                { 
                    _komb_det = value;
                    if (!_isInitializing) IsModified = true;
                }
            }}
        private string _komb_izd;
        public string Komb_izd { get=>_komb_izd;
            set {
            if (_komb_izd != value) 
                { 
                    _komb_izd = value;
                    if (!_isInitializing) IsModified = true;
                } 
            }}
        public string Sostav { get; set; }
        public string Kod_t { get; set; }
        public decimal K_kg_m1 { get; set; }
        public decimal K_kg_m2 { get; set; }
        public decimal K_kg_m3 { get; set; }
        public decimal K_kg_m4 { get; set; }
        public decimal K_kg_m5 { get; set; }
        public decimal K_kg_m6 { get; set; }
        public decimal K_kg_m7 { get; set; }
        
        public int Is_furnit { get; set; }
        public int Is_upak { get; set; }
        public int Sek_vyaz14 { get; set; }
        public int gcg_id { get; set; }
        public int Gbm_id { get; set; }
        public int Gcgp_id { get; set; }
        public int Gbt_id { get; set; }
        public int Bus { get; set; }
        public int Stra { get; set; }
        public int P_pres { get; set; }
        public decimal Seb_usl { get; set; }
        public decimal Art_segm { get; set; }
        public decimal Art_family { get; set; }
        public decimal Art_class { get; set; }
        public decimal Art_block { get; set; }
        public int Scid_n { get; set; }
        public string Kod_tnved { get; set; }
        public DateTime? DateOpis { get; set; }
        public string Mtrl_up { get; set; }
        public string Mtrl_pdkl { get; set; }
        public decimal Vid_obuv { get; set; }
        public string Mtrl_down { get; set; }
        public int Sql_pr_add { get; set; }
        public int Sql_pr_upd { get; set; }
        public string Komp_name { get; set; }
        public DateTime? Date_add { get; set; }
        public int Sek_vyaz62 { get; set; }
        public int Sek_vyaz71 { get; set; }
        public int Sek_vyaz72 { get; set; }

        private short _kruj;
        public short Kruj { get => _kruj; set => SetField(ref _kruj, value); }

        public string Kod_lv3 { get; set; }
        public int Sek_cord { get; set; }
        public decimal Norm_cord { get; set; }
        public int Tgm_id_n { get; set; }
        public DateTime? Dateutvkk { get; set; }
        public decimal Sum_zarpl { get; set; }
        public decimal? Sum_dopopl { get; set; }
        public decimal? Sum_strvznos { get; set; }
        public decimal? Sum_sebraskr { get; set; }
        public decimal? Sum_komplnum { get; set; }
        public decimal Sek_vyaz57 { get; set; }
        public decimal Sek_vyaz18 { get; set; }
        public int? Annid { get; set; }

        

        


    }
}
