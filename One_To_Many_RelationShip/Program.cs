using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace One_To_Many_RelationShip
{

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    #region  Default Convention                         
    //public class Calisan
    //{                                         // -> Burada calisan departmana bağlıdır çalışan classımıza departmanId sini eklemesek bile EF bunu tablolarda oluşturacaktır.
    //    public int CalisanId { get; set; }
    //    public string Adi { get; set; }
    //    public int DepartmanId { get; set; }      // -> DepartmanId olarak tanımlasakta olur tanımlamasakta kendi oluşturuyor
    //    public Departman Departman { get; set; }


    //}
    //public class Departman
    //{
    //    public int DepartmanId { get; set; }
    //    public string DepartmanAdi { get; set; }
    //    public ICollection<Calisan> Calisanlar { get; set; }

    //}
    #endregion
    #region Data Annatations

    //public class Calisan
    //{
    //    public int CalisanId { get; set; }  // -> DataAnnations kullanmamızın tek amacı foreign key kullanarak ve isimlendirme geleneğine karşı bir isim kullanmak istiyor isek  
    //    [ForeignKey(nameof(Departman))]     // -> Örnek olarak ta yandaki ifade 
    //    public int DId { get; set; }
    //    public string Adi { get; set; }
    //    public Departman Departman { get; set; }


    //}
    //public class Departman
    //{
    //    public int DepartmanId { get; set; }
    //    public string DepartmanAdi { get; set; }
    //    public ICollection<Calisan> Calisanlar { get; set; }

    //}
    #endregion
    #region Fluent Api 
    public class Calisan
    {
        public int CalisanId { get; set; }
        public int DId { get; set; }    // -> Fakat burada olduğu gibi foreign key kullanmak ve ismini de isimlendirme kurallarından farklı olarak kullanır isek te aşağıdaki gibi HasForeignKey olarak eklemelerimizi yapmamız gerekmekte.
        public string Adi { get; set; }
        public Departman Departman { get; set; }


    }
    public class Departman
    {
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }
        public ICollection<Calisan> Calisanlar { get; set; }

    }

    #endregion


    public class WorksOfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=WorksOfDb;integrated security=true");
        }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Departman> Departmanlar { get; set; }


        //-> Fluent Api ile tanımlamak ister isek foreign key kullanmadan OnModelCreating Metodu ile aşağıdaki gibi tanımlamalarımızı yaparız

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Calisan>().HasOne(x => x.Departman).WithMany(x => x.Calisanlar);
            modelBuilder.Entity<Calisan>().HasOne(x => x.Departman).WithMany(x => x.Calisanlar).HasForeignKey(x => x.DId);  
             ////-> Burada farklı bir Id ismi ile tanımlamak istersek tanımlanacak.
        }

    }


}