﻿using JeopardyWebApp.Data.Entities;
using JeopardyWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JeopardyWebApp.Data.EFCore
{
    public partial class JeopardyDbContext : DbContext
    {

        public JeopardyDbContext(DbContextOptions<JeopardyDbContext> options)
            : base(options)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Players> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriesModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.HasIndex(e => e.CategoryNameEn)
                    .HasName("FJ_UniqueCategory")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryNameEn)
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

            modelBuilder.Entity<PlayersModel>(entity =>
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

            modelBuilder.Entity<QuestionsModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("questions");

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

                entity.Property(e => e.Hint)
                    .HasColumnName("hint")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuestionEn)
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
                    .HasConstraintName("questions_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        
    }
}
