using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;


namespace SewingProduction.Core.Models
{
    public class ArticulModel : INewable, IModifiable, IDeletable, INotifyPropertyChanged, ISupportInitialize
    {
        [NotMapped]public string Ko { get; set; }
        [NotMapped] public string baz { get; set; }
        [NotMapped] public string Kodd { get; set; }
        public string Kod { get; set; }
        
        public string Grup { get; set; }
        private int _ag_id;

        public int Ag_id{
            get => _ag_id;
            set
            {
                if (_ag_id != value){_ag_id = value; if (!_isInitializing) IsModified = true; }
            }
        }
        private string _articul;
        public string Articul { get=>_articul; set
            { 
                if (_articul != value) 
                { 
                    _articul = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        private string _mod;
        public string Mod { get=>_mod;
            set {
            if (_mod != value) 
                { 
                    _mod = value;
                    if (!_isInitializing) IsModified = true;
                }
            } 
        }
        private string _razm;
        public string Razm { get=>_razm;
            set {
                if (_razm != value) 
                { 
                    _razm = value;
                    if (!_isInitializing) IsModified = true;
                }
            } 
        }
        private string _sost;
        public string Sost { get=>_sost;
            set
            { if (_sost != value) 
                { 
                    _sost = value;
                    if (!_isInitializing) IsModified = true;
                }
            }}

        private string _sost2;
        public string Sost2 { get=>_sost2;
            set {
                if (_sost2 != value) 
                { 
                    _sost2 = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        private string _sost3;
        public string Sost3 { get=>_sost3;
            set {
                if (_sost3 != value) 
                { 
                    _sost3 = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        private int _id_gost ;
        public int Id_gost { get=>_id_gost;
            set { 
            if (_id_gost != value) 
                { 
                    _id_gost = value;
                    if (!_isInitializing) IsModified = true;
                }
            } }
        public string _gost;
        public string Gost { get=>_gost;
            set {
                if (_gost != value) 
                { 
                    _gost = value;
                    if (!_isInitializing) IsModified = true;
                }
            } }

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
            set { if (_kle != value) { _kle = value; if (!_isInitializing) IsModified = true; } }
            }
        private int _grupp;
        public int Grupp { get=>_grupp;
            set { if (_grupp != value){ _grupp = value; if (!_isInitializing) IsModified = true; } }
        }
        public int P { get; set; }
        public int V { get; set; }
        public int S { get; set; }
        public string Kod_tov { get; set; }
        public string Gruppa { get; set; }
        public string P_gruppa { get; set; }
        public string Text_m { get; set; }
        private string _tkb;
        public string Tkb { get=>_tkb;
            set { 
            if (_tkb != value) 
                { 
                    _tkb = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        public string Kod_t1 { get; set; }
        public string Tkb1 { get; set; }
        public decimal Norm_t1 { get; set; }
        public string Opis_t1 { get; set; }
        public string Kod_t2 { get; set; }
        public string Tkb2 { get; set; }
        public decimal Norm_t2 { get; set; }
        public string Opis_t2 { get; set; }
        public string Kod_t3 { get; set; }
        public string Tkb3 { get; set; }
        public decimal Norm_t3 { get; set; }
        public string Opis_t3 { get; set; }
        public string Kod_t4 { get; set; }
        public string Tkb4 { get; set; }
        public decimal Norm_t4 { get; set; }
        public string Opis_t4 { get; set; }
        public int Baza { get; set; }
        private int _kod_v;
        public int Kod_v { get=>_kod_v;
            set { 
            if (_kod_v != value) 
                { 
                    _kod_v = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        public string Kod_t5 { get; set; }
        public string Tkb5 { get; set; }
        public decimal Norm_t5 { get; set; }
        public string Opis_t5 { get; set; }
        public string Kod_t6 { get; set; }
        public string Tkb6 { get; set; }
        public decimal Norm_t6 { get; set; }
        public string Opis_t6 { get; set; }
        public decimal Seb_dop { get; set; }
        public decimal Seb_t1 { get; set; }
        public decimal Seb_t3 { get; set; }
        public decimal Seb_t2 { get; set; }
        public decimal Seb_t4 { get; set; }
        public decimal Seb_t5 { get; set; }
        public decimal Seb_t6 { get; set; }
        public int Sek_vyaz6 { get; set; }
        public int Sek_vyaz10 { get; set; }
        public int Sek_vyazo { get; set; }
        public decimal Normapryz { get; set; }
        
        public int Id_svyaz { get; set; }
        public decimal Koef_pr { get; set; }
        public decimal Ob_izd { get; set; }
        public int Stavka_nds { get; set; }
        public string Kod_t7 { get; set; }
        public string Tkb7 { get; set; }
        public decimal Norm_t7 { get; set; }
        public decimal Seb_t7 { get; set; }
        public string Opis_t7 { get; set; }
        public int Sposob_up { get; set; }
        
        private int _id_country;
        public int Id_country { get=>_id_country;
            set { 
            if (_id_country != value) 
                { 
                    _id_country = value;
                    if (!_isInitializing) IsModified = true;
                }
            }
        }
        public int Old_prch { get; set; }
        public int Sek_vyaz70 { get; set; }
        public int Sek_vyaz3 { get; set; }
        public decimal Koef_d { get; set; }
        public int St_nds { get; set; }
        public int Upd_razm { get; set; }
        public decimal Gl_rekom { get; set; }
        public int Sek_kr { get; set; }
        
        private int _arh;
        public int Arh { get => _arh;
            set { 
            if (_arh != value) 
                { 
                    _arh = value;
                    IsModified = true;
                }
            } 
        }
       
        public int Nds { get; set; }
        public string Ed_izm { get; set; }
        public decimal Brak_t1 { get; set; }
        public decimal Brak_t2 { get; set; }
        public decimal Brak_t3 { get; set; }
        public decimal Brak_t4 { get; set; }
        public decimal Brak_t5 { get; set; }
        public decimal Brak_t6 { get; set; }
        public decimal Brak_t7 { get; set; }
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
        public short Kruj { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        [NotMapped]
        public bool IsModified { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        [NotMapped]
        public bool IsDeleted { get; set; } = false;

        private bool _isInitializing;
        // ISupportInitialize
        public void BeginInit() => _isInitializing = true;

        public void EndInit()
        {
            _isInitializing = false;
            IsModified = false;
        }


    }
}
