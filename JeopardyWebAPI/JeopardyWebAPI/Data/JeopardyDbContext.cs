using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JeopardyWebAPI.Models
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

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                 optionsBuilder.UseMySql("server=feltgame.mariadb.database.azure.com;port=3306;user=mariadbadmin@feltgame;password=azuremariaDb!2020;database=feltgame", x => x.ServerVersion("10.2.31-mariadb"));
                 //optionsBuilder.UseMySql(options.GetConnectionString("FeltGameContext"), x => x.ServerVersion("10.2.31-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryNameEn)
                    .HasName("FJ_UniqueCategory")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryNameEn)
                    .IsRequired()
                    .HasColumnName("categoryName_en")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoryNameFr)
                    .HasColumnName("categoryName_fr")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.ToTable("players");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("FJ_UniqueEmail")
                    .IsUnique();

                entity.Property(e => e.Id)
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
                entity.ToTable("questions");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnswerEn)
                    .HasColumnName("answer_en")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AnswerFr)
                    .HasColumnName("answer_fr")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CategoryId)
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

                entity.Property(e => e.QuestionEn)
                    .IsRequired()
                    .HasColumnName("question_en")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.QuestionFr)
                    .HasColumnName("question_fr")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TimeLimit)
                    .HasColumnName("timeLimit")
                    .HasColumnType("int(11)");

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
