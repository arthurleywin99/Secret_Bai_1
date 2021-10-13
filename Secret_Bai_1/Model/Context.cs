using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Secret_Bai_1.Model
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DichVu>()
                .Property(e => e.GiaTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);
        }
    }
}
