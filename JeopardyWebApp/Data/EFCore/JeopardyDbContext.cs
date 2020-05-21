using JeopardyWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JeopardyWebApp.Data.EFCore
{
    public partial class JeopardyDbContext : DbContext
    {
        public JeopardyDbContext()
        {
        }

        public JeopardyDbContext(DbContextOptions<JeopardyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryName_En)
                    .HasName("FJ_UniqueCategory")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryName_En)
                    .HasColumnName("categoryName_en")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoryName_Fr)
                    .HasColumnName("categoryName_fr")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.PlayerId)
                    .HasName("PRIMARY");

                entity.ToTable("players");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("FJ_UniqueEmail")
                    .IsUnique();

                entity.Property(e => e.PlayerId)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("emailAddress")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Ranking)
                    .HasColumnName("ranking")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("questions");

                entity.HasIndex(e => e.Category_Id)
                    .HasName("category_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Answer_En)
                    .HasColumnName("answer_en")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Answer_Fr)
                    .HasColumnName("answer_fr")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Category_Id)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Hint)
                    .HasColumnName("hint")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Question_En)
                    .HasColumnName("question_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Question_Fr)
                    .HasColumnName("question_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TimeLimit)
                    .HasColumnName("timeLimit")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.Category_Id)
                    .HasConstraintName("questions_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
