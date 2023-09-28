using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Many_To_Many_RelationShip
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, DataBase!");
        }
    }


    #region  Default Convention  
    //// -> Çoka Çok ilişkili tablolarda bir cross table oluşturup 2 adet bir e çok ilişki kurarak dolaylı yoldan çoka çok ilişki oluşturulmuş olunur.
    //// -> Bu Cross tablolarda Composite Primary Key kullanılır yani iki tabloadan gelecek Idler in iki sütun birlikte bir Primary keye sahip olmuş olur.
    //// -> Burada Default Convention kullanır isek Navighation Property ler üzerinden çoğul bağlantıyı kurmalıyız.
    //// -> Cross table ı kendimiz oluşturmak zorunda değiliz EF otomatik olarak composite primary key i ile birlikte oluşturur.
    //public class Kitap
    //{                                         // -> Burada default convention kullanılacak olursa ara tabloyu oluşturmadan tamamlayabiliriz. Ara tabloyu EF kendi oluşturacaktır.
    //    public int KitapId { get; set; }
    //    public string KitapAdi { get; set; }
    //    public ICollection<Yazar> Yazarlar { get; set; }


    //}
    //public class Yazar
    //{
    //    public int YazarId { get; set; }
    //    public string YazarAdi { get; set; }
    //    public ICollection<Kitap> Kitaplar { get; set; }

    //}
    #endregion
    #region Data Annatations
    //class Kitap
    //// -> Burada CrossTable ı kendimiz oluşturmak zorundayız
    //// -> Entitylerde oluşturduğumuz Cross Table ile bire çok bir ilişki kurulmalı
    //// -> Cross Table oluştruduktan sonra benim composite primary key i tanımlamam gerekiyor
    //// -> Cross Table ı context sınıfı içerisinden DbSet property'si olarak bildirmek mecburiyetinde değiliz.
    //// -> Eğer biz  YazarId yada KitapId isimlendirme kurallarına uymadan kullanmak ister isek Foreign Key olarak bildirmemiz gerekiyor.
    //{
    //    public int KitapId { get; set; }
    //    public string KitapAdi { get; set; }
    //    public ICollection<KitapYazar> Yazarlar { get; set; }


    //}
    //class KitapYazar
    //{
    //    public int YazarId { get; set; }
    //    //[ForeignKey(nameof(Yazar)]        // -> İsimlendirme kurallarına uymadığımız taktirde Foreign Key olarak bildirmemiz gerekmekte.
    //    //public int YId { get; set; }
    //    //[ForeignKey(nameof(Kitap)]
    //    //public int KId { get; set; }
    //    public int KitapId { get; set; }
    //    public Kitap Kitap { get; set; }
    //    public Yazar Yazar { get; set; }
    //}
    //class Yazar
    //{
    //    public int YazarId { get; set; }
    //    public string YazarAdi { get; set; }
    //    public ICollection<KitapYazar> Kitaplar { get; set; }

    //}
    #endregion
    #region Fluent Api 
    class Kitap
    // -> Cross Table manuel olarak oluşturulmalı.
    // -> Composite Pk HasKey olarak eklenmeli
 
    {
        public int KitapId { get; set; }
        public string KitapAdi { get; set; }
        public ICollection<KitapYazar> Yazarlar { get; set; }


    }
    class KitapYazar
    {
        public int YazarId { get; set; }
        public int KitapId { get; set; }
        public Kitap Kitap { get; set; }
        public Yazar Yazar { get; set; }
    }
    class Yazar
    {
        public int YazarId { get; set; }
        public string YazarAdi { get; set; }
        public ICollection<KitapYazar> Kitaplar { get; set; }

    }

    #endregion
    class WorksOfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=WorksOfDb;integrated security=true");
        }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }


        //// -> Burada composite primary key oluşturduk.
        //// -> Burayı DataAnnatation kullanırken yapıcaz
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<KitapYazar>().HasKey(x => new { x.YazarId,x.KitapId });  
        //}



        // -> burayı FluentApi kullanırken yapıcaz
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<KitapYazar>().HasKey(x => new { x.YazarId, x.KitapId });
            modelbuilder.Entity<KitapYazar>().HasOne(x => x.Kitap).WithMany(x => x.Yazarlar).HasForeignKey(x => x.KitapId);
            modelbuilder.Entity<KitapYazar>().HasOne(x => x.Yazar).WithMany(x => x.Kitaplar).HasForeignKey(x => x.YazarId);
        }


    }
}