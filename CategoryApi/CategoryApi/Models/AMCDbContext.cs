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

        public virtual DbSet<Categories> Category { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=ssjdnv;database=jeopardy", x => x.ServerVersion("10.4.12-mariadb"));
                optionsBuilder.UseMySql("FeltGameContext", x => x.ServerVersion("10.4.12-mariadb"));

            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryNameEn)
                    .HasColumnName("categoryName_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CategoryNameFr)
                    .HasColumnName("categoryName_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Questions>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Questions>>)(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PRIMARY");

                entity.ToTable("questions");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_id");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionEn)
                    .HasColumnName("question_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuestionFr)
                    .HasColumnName("question_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnswerEn)
                    .HasColumnName("answer_en")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnswerFr)
                    .HasColumnName("answer_fr")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasColumnType("int (11)");

                entity.Property(e => e.TimeLimit)
                    .HasColumnName("timeLimit")
                    .HasColumnType("int (11)");

                entity.Property(e => e.Hint)
                    .HasColumnName("hint")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("questions_ibfk_1");
            }));

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
