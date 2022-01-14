using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_Consultant_Management_Application.Models
{
    public partial class PCMADBContext : DbContext
    {

        public PCMADBContext(DbContextOptions<PCMADBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Consultant> Consultants { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog = PCMADB;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company_name");
            });

            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.ToTable("consultants");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.HasMany(d => d.Projects)
                    .WithMany(p => p.Consultants)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProjectsConsultant",
                        l => l.HasOne<Project>().WithMany().HasForeignKey("ProjectId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__projects___proje__5629CD9C"),
                        r => r.HasOne<Consultant>().WithMany().HasForeignKey("ConsultantId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__projects___consu__5535A963"),
                        j =>
                        {
                            j.HasKey("ConsultantId", "ProjectId").HasName("PK__projects__03C2F50FD0FD7CE1");

                            j.ToTable("projects_consultants");

                            j.IndexerProperty<long>("ConsultantId").HasColumnName("consultant_Id");

                            j.IndexerProperty<long>("ProjectId").HasColumnName("project_Id");
                        });
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.CompanyId).HasColumnName("company_Id");

                entity.Property(e => e.ProjectDescription)
                    .HasColumnType("text")
                    .HasColumnName("project_description");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("project_name");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__projects__compan__52593CB8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
