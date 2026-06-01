using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{
	public class ViewRzuRzv
	{
		public string dost_zeh { get; set; }
		public DateTime? data_zeh { get; set; }
		public int? n_zeh { get; set; }
		public string nom_zad { get; set; }
		public string mod { get; set; }
		public string articul { get; set; }
		public decimal? kol_pach { get; set; }
		public string kod_izd { get; set; }
		public string kod_pach { get; set; }
		public int? nom_pach { get; set; }
		public int? nom_n_pach { get; set; }
		public int? SOURCE { get; set; }
		public int? proizvType { get; set; }
		public DateTime? data_r { get; set; }
		public string razm_pach { get; set; }
		public int? n_pach_nz { get; set; }
		public string pach_kod { get; set; }
		public string articul_izd { get; set; }
		public string grup_izd { get; set; }
		public string mod_izd { get; set; }
		public string grup_pach { get; set; }
		public string kod_k_pach { get; set; }
		public string grup_k_pach { get; set; }
		public string articul_k_pach { get; set; }
		public string mod_k_pach { get; set; }
		public string razm_k_pach { get; set; }
		public string iz_nakl_pach { get; set; }
		public DateTime? data_cd { get; set; }
		public DateTime? data_plan { get; set; }
		public string h_reestr { get; set; }
		public string mest_zeh { get; set; }
		public int? tab_r1 { get; set; }
		public int? id_sbit { get; set; }
		public string mg_zakr { get; set; }
		public int? id_brig { get; set; }
		public string kod7 { get; set; }
		public string kodK7 { get; set; }
		public string kodIzd7 { get; set; }
		public int? yearPach { get; set; }
		public string n_zvet { get; set; }
		public string RzuShtrStart { get; set; }
		public string razm_izd { get; set; }
		[NotMapped]
		public bool IsSelected { get; set; }
	}
}
