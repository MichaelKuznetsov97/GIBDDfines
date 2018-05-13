using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GIBDDfines.Models
{
    public partial class modeldbGIBDDContext : DbContext
    {
        public modeldbGIBDDContext(DbContextOptions<modeldbGIBDDContext> options)
            : base(options)
        { }

        public virtual DbSet<Autoes> Autoes { get; set; }
        public virtual DbSet<AutoOwners> AutoOwners { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<LinkOwnCateg> LinkOwnCateg { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<Police> Police { get; set; }
        public virtual DbSet<Punishments> Punishments { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Tscategories> Tscategories { get; set; }
        public virtual DbSet<TypePunishments> TypePunishments { get; set; }
        public virtual DbSet<TypeTs> TypeTs { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autoes>(entity =>
            {
                entity.HasIndex(e => e.IdColor)
                    .HasName("IX_FK_Auto_Color");

                entity.HasIndex(e => e.IdMarkModel)
                    .HasName("IX_FK_Auto_Model");

                entity.HasIndex(e => e.IdOwner)
                    .HasName("IX_FK_Auto_AutoOwner");

                entity.HasIndex(e => e.IdTcateg)
                    .HasName("IX_FK_Auto_TSCategory");

                entity.HasIndex(e => e.IdType)
                    .HasName("IX_FK_Auto_TypeTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateRelease)
                    .HasColumnName("Date_release")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdColor).HasColumnName("ID_Color");

                entity.Property(e => e.IdMarkModel).HasColumnName("ID_MarkModel");

                entity.Property(e => e.IdOwner).HasColumnName("ID_Owner");

                entity.Property(e => e.IdTcateg).HasColumnName("ID_TCateg");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Pengine).HasColumnName("PEngine");

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Autoes)
                    .HasForeignKey(d => d.IdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autoes_Colors");

                entity.HasOne(d => d.IdMarkModelNavigation)
                    .WithMany(p => p.Autoes)
                    .HasForeignKey(d => d.IdMarkModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autoes_Models");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Autoes)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autoes_AutoOwners");

                entity.HasOne(d => d.IdTcategNavigation)
                    .WithMany(p => p.Autoes)
                    .HasForeignKey(d => d.IdTcateg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autoes_TSCategories");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Autoes)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autoes_TypeTS");
            });

            modelBuilder.Entity<AutoOwners>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateConfisc)
                    .HasColumnName("Date_confisc")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateEntered)
                    .HasColumnName("Date_entered")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateReturn)
                    .HasColumnName("Date_return")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LinkOwnCateg>(entity =>
            {
                entity.ToTable("Link_OwnCateg");

                entity.HasIndex(e => e.IdAowner)
                    .HasName("IX_FK_CategPermit_AutoOwner");

                entity.HasIndex(e => e.IdCategory)
                    .HasName("IX_FK_CategPermit_TSCategory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdAowner).HasColumnName("ID_AOwner");

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.HasOne(d => d.IdAownerNavigation)
                    .WithMany(p => p.LinkOwnCateg)
                    .HasForeignKey(d => d.IdAowner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Link_OwnCateg_AutoOwners");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.LinkOwnCateg)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategPermit_AutoOwner");
            });

            modelBuilder.Entity<Marks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Models>(entity =>
            {
                entity.HasIndex(e => e.IdMark)
                    .HasName("IX_FK_Model_Mark");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdMark).HasColumnName("ID_Mark");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMarkNavigation)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.IdMark)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Model_Mark");
            });

            modelBuilder.Entity<Police>(entity =>
            {
                entity.HasIndex(e => e.IdTitle)
                    .HasName("IX_FK_Police_Title");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdTitle).HasColumnName("ID_Title");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTitleNavigation)
                    .WithMany(p => p.Police)
                    .HasForeignKey(d => d.IdTitle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Police_Title");
            });

            modelBuilder.Entity<Punishments>(entity =>
            {
                entity.HasIndex(e => e.IdAuto)
                    .HasName("IX_FK_Punishment_Auto");

                entity.HasIndex(e => e.IdPolice)
                    .HasName("IX_FK_Punishment_Police");

                entity.HasIndex(e => e.IdTpunish)
                    .HasName("IX_FK_Punishment_TypePunishment");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DatePay)
                    .HasColumnName("Date_pay")
                    .HasColumnType("datetime");

                entity.Property(e => e.Describe)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdAowner).HasColumnName("ID_AOwner");

                entity.Property(e => e.IdAuto).HasColumnName("ID_Auto");

                entity.Property(e => e.IdPolice)
                    .IsRequired()
                    .HasColumnName("ID_Police")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdTpunish).HasColumnName("ID_TPunish");

                entity.HasOne(d => d.IdAownerNavigation)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.IdAowner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Punishments_AutoOwners");

                entity.HasOne(d => d.IdAutoNavigation)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.IdAuto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Punishments_Autoes");

                entity.HasOne(d => d.IdPoliceNavigation)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.IdPolice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Punishments_Police");

                entity.HasOne(d => d.IdTpunishNavigation)
                    .WithMany(p => p.Punishments)
                    .HasForeignKey(d => d.IdTpunish)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Punishments_TypePunishments");
            });

            modelBuilder.Entity<Titles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tscategories>(entity =>
            {
                entity.ToTable("TSCategories");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Describe)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypePunishments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeTs>(entity =>
            {
                entity.ToTable("TypeTS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
