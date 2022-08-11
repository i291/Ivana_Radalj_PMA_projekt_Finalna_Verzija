using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MvcDemo.DbModels
{
    public partial class travelingContext : DbContext
    {
        public travelingContext()
        {
        }

        public travelingContext(DbContextOptions<travelingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAdmin> TblAdmin { get; set; }
        public virtual DbSet<TblTravel> TblTravel { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUsertravel> TblUsertravel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-IPNBQ6U\\RADALJSQL;Database=traveling;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAdmin>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("tbl_admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminName)
                    .IsRequired()
                    .HasColumnName("admin_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("admin_password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TravelIdFk).HasColumnName("travel_id_fk");

                entity.HasOne(d => d.TravelIdFkNavigation)
                    .WithMany(p => p.TblAdmin)
                    .HasForeignKey(d => d.TravelIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_admin_tbl_travel");
            });

            modelBuilder.Entity<TblTravel>(entity =>
            {
                entity.HasKey(e => e.TravelId);

                entity.ToTable("tbl_travel");

                entity.Property(e => e.TravelId).HasColumnName("travel_id");

                entity.Property(e => e.TravelDuration)
                    .IsRequired()
                    .HasColumnName("travel_duration")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TravelName)
                    .IsRequired()
                    .HasColumnName("travel_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TravelPrice)
                    .IsRequired()
                    .HasColumnName("travel_price")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tbl_user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("user_password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin)
                  .HasColumnName("user_isadmin");
            });

            modelBuilder.Entity<TblUsertravel>(entity =>
            {
                entity.HasKey(e => new { e.UserIdFk, e.TravelIdFk });

                entity.ToTable("tbl_usertravel");

                entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");

                entity.Property(e => e.TravelIdFk).HasColumnName("travel_id_fk");

                entity.HasOne(d => d.TravelIdFkNavigation)
                    .WithMany(p => p.TblUsertravel)
                    .HasForeignKey(d => d.TravelIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usertravel_tbl_travel");

                entity.HasOne(d => d.UserIdFkNavigation)
                    .WithMany(p => p.TblUsertravel)
                    .HasForeignKey(d => d.UserIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_usertravel_tbl_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
