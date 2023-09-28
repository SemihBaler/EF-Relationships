using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace One_To_One_Relationship
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
    //{
    //    public int CalisanId { get; set; }
    //    public string Adi { get; set; }
    //    public CalisanAdres CalisanAdres { get; set; }


    //}
    //public class CalisanAdres
    //{
    //    public int CalisanAdresId { get; set; }
    //    public int CalisanId { get; set; }                 // ->  1-1 tablolarda ilişkinin kurulabilmesi için bağımlı olan class a ait foreign key koymamız gerekiyor.
    //    public string Adres { get; set; }
    //    public Calisan Calisan { get; set; }

    //}
    #endregion
    #region Data Annatations

    //public class Calisan
    //{
    //    public int CalisanId { get; set; }
    //    public string Adi { get; set; }
    //    public CalisanAdres CalisanAdres { get; set; }


    //}
    //public class CalisanAdres
    //{
    //    public int CalisanAdresId { get; set; }
    //    [ForeignKey(nameof(Calisan))]               // -> Eğer bu Id Calısan class ımıza ait Id isminden farklı bir isim olacak sa bunu DataAnnatations ile sağlıyoruz.
    //    public int selam { get; set; }
    //    public string Adres { get; set; }
    //    public Calisan Calisan { get; set; }

    //}
    #endregion
    #region Fluent Api 
    public class Calisan
    {
        public int CalisanId { get; set; }
        public string Adi { get; set; }
        public CalisanAdres CalisanAdres { get; set; }


    }
    public class CalisanAdres
    {
        [Key,ForeignKey(nameof(Calisan))]              // -> Burada da fark olarak class a ait primary keyimizi ek olarak foreign key olarak kullanabiliyoruz.
        public int CalisanAdresId { get; set; }
        public string Adres { get; set; }
        public Calisan Calisan { get; set; }

    }
    #endregion
    public class WorksOfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=WorksOfDb;integrated security=true");
        }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<CalisanAdres> CalisanAdresleri { get; set; }
    }

}