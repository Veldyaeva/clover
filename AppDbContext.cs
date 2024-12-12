using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SewingProduction;

using System.Linq.Expressions;
using System.Windows.Forms;
using System;


namespace SewingProduction
{
    
       public class ArtNormN
       {
           [StringLength(7)]
           public string kod { get; set; }              // NCHAR(7) NULL
           [StringLength(35)] 
           public string grup { get; set; }              //NVARCHAR(35) NULL
           [StringLength(25)]
           public string articul { get; set; }//NVARCHAR(25) NULL

           [StringLength(50)]
           public string mod { get; set; }// NVARCHAR(50) NULL

           [StringLength(1)]
           public string po { get; set; }               //NCHAR(1) NULL
           public int sek_shv { get; set; }              //INT NULL
           public int sek_vyaz3 { get; set; }              //INT NULL
           public int sek_vyaz5 { get; set; }              //INT NULL
           public int sek_vyaz6 { get; set; }              //INT NULL
           public int sek_vyaz7 { get; set; }              //INT NULL                   
           public int sek_vyaz10 { get; set; }              //INT NULL
           public int sek_vyaz12 { get; set; }              //INT NULL
           public int sek_vyaz62 { get; set; }              //INT NULL
           public int sek_vyaz71 { get; set; }              //INT NULL
           public int sek_vyaz72 { get; set; }              //INT NULL
           public int sek_vyazo { get; set; }              //INT NULL
           public int sek_vyaz { get; set; }              //INT NULL
           public int sek { get; set; }              //INT NULL
           public int seb { get; set; }              //NUMERIC(13, 5) NULL
           public int st { get; set; }              //INT NULL
           public char po1 { get; set; }              //NCHAR(1) NULL
           public char komment { get; set; }              //NVARCHAR(MAX) NULL
           public DateTime data_sozd { get; set; }              //DATETIME NULL
           public int diz { get; set; }              //INT NULL
           public int constr { get; set; }              //INT NULL
           public DateTime data_obn { get; set; }              //DATETIME NULL
           public int sek_vyaz70 { get; set; }              //INT NULL
           public int sek_kr { get; set; }              //INT NULL
           public int slogn { get; set; }              //INT NULL
           public int sek_vyaz14 { get; set; }              //INT NULL
           public int arh { get; set; }              //INT NULL
           public int sql_pr_add { get; set; }              //INT NULL
           public DateTime date_add { get; set; }              //DATETIME NULL
           public char komp_name { get; set; }              //NCHAR(50) NULL
           public DateTime annDateDel { get; set; }              //DATETIME NULL
           public char annCompDel { get; set; }              //NVARCHAR(50) NULL
           [Key]
           public int annID { get; set; }              //INT IDENTITY
           public DateTime annDateAdd { get; set; }              //DATETIME NULL CONSTRAINT DF_art_norm_n_annDateAdd DEFAULT(GETDATE())
           public char annCompAdd { get; set; }              //NVARCHAR(50) NULL CONSTRAINT DF_art_norm_n_annCompAdd DEFAULT(HOST_NAME())

       }

    //using System.Data.Entity;
    //public class DatabaseContext : DbContext { public DatabaseContext() : base("DefaultConnection") { } public DbSet<ArtNormN> ArtNormNs { get; set; } }


    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.Connection.ConnectionString = _connectionString; // Установка строки подключения здесь
        }

        public DbSet<ArtNormN> ArtNormNs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtNormN>().HasKey(r => r.annID);
            // ... другие настройки модели, если необходимы ...
        }
    }
    /*    public class AppDbContext : DbContext
        {
            private string _connectionString;
            private string _provider;

            public AppDbContext(string provider, string connectionString)
            {
                _provider = provider;
                _connectionString = connectionString;
            }
            public DbSet<ArtNormN> ArtNormNs { get; set; }


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ArtNormN>().HasKey(r => r.annID); // Установка первичного ключа
                //modelBuilder.Entity<ArtNormN>().Property(r => r.kod).

                    }
            }
            /*
                    public class BlankTriggerAddingConvention : IModelFinalizingConvention
                    {
                        public virtual void ProcessModelFinalizing(
                            IConventionModelBuilder modelBuilder,
                            IConventionContext<IConventionModelBuilder> context)
                        {
                            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
                            {
                                var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                                if (table != null
                                    && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null)
                                    && (entityType.BaseType == null
                                        || entityType.GetMappingStrategy() != RelationalAnnotationNames.TphMappingStrategy))
                                {
                                    entityType.Builder.HasTrigger("trg_Rascrr_Change");
                                }

                                foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                                {
                                    if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                                    {
                                        entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                                    }
                                }
                            }
                        }
                    }
                    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
                    {
                        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
                    }*/

}
