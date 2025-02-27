//using SewingProduction.BdContext;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Runtime.Remoting.Contexts;

//public class AppDbContext : DbContext
//{
//    public AppDbContext() : base("name=YourConnectionString") { }

//    public DbSet<NormOper> NormOpers { get; set; }

//    protected override void OnModelCreating(DbModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.KodO)
//            .IsFixedLength()
//            .IsUnicode();

//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.Text)
//            .IsFixedLength()
//            .IsUnicode();

//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.Po)
//            .IsFixedLength()
//            .IsUnicode();

//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.New)
//            .IsFixedLength()
//            .IsUnicode();

//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.Spec)
//            .IsFixedLength()
//            .IsUnicode();

//        modelBuilder.Entity<NormOper>()
//            .Property(e => e.Obor)
//            .IsFixedLength()
//            .IsUnicode();

//        base.OnModelCreating(modelBuilder);
//    }
//}
