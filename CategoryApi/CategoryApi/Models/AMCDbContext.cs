using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CategoryApi.Models
{
    public partial class AMCDbContext : DbContext
    {
        public AMCDbContext()
        {
        }

        public AMCDbContext(DbContextOptions<AMCDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=ssjdnv;database=jeopardy", x => x.ServerVersion("10.4.12-mariadb"));
                optionsBuilder.UseMySql("server=feltgame.mariadb.database.azure.com;port=3306;user=mariadbadmin@feltgame;password=azuremariaDb!2020;database=feltgame", x => x.ServerVersion("10.4.12-mariadb"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TitleEn)
                    .HasColumnName("categoryName_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TitleFr)
                    .HasColumnName("categoryName_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("questions");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_id");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnswerEn)
                    .HasColumnName("answer_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnswerFr)
                    .HasColumnName("answer_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DifficultyVal)
                    .HasColumnName("points")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TextEn)
                    .HasColumnName("question_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TextFr)
                    .HasColumnName("question_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("questions_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
