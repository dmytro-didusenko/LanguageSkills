using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class LanguageSkillsDBContext : DbContext
    {
        public LanguageSkillsDBContext()
        {
        }

        public LanguageSkillsDBContext(DbContextOptions<LanguageSkillsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LanguageTranslation> LanguageTranslations { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<SubCategoryTranslation> SubCategoryTranslations { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestTranslation> TestTranslations { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordTranslation> WordTranslations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LanguageSkillsDB;Username=postgres;Password=1111");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_Ukraine.1251");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryImagePath).IsRequired();

                entity.Property(e => e.CategoryName).IsRequired();
            });

            modelBuilder.Entity<CategoryTranslation>(entity =>
            {
                entity.Property(e => e.CategoryTranslationName).IsRequired();

                entity.HasOne(d => d.Categoty)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.CategotyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoryTranslations_fk0");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CategoryTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoryTranslations_fk1");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.ShortName).IsRequired();
            });

            modelBuilder.Entity<LanguageTranslation>(entity =>
            {
                entity.Property(e => e.LanguageTranslationName).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.LanguageTranslationLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LanguageTranslations_fk1");

                entity.HasOne(d => d.LanguageWord)
                    .WithMany(p => p.LanguageTranslationLanguageWords)
                    .HasForeignKey(d => d.LanguageWordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LanguageTranslations_fk0");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryImagePath).IsRequired();

                entity.Property(e => e.SubCategoryName).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategories_fk0");
            });

            modelBuilder.Entity<SubCategoryTranslation>(entity =>
            {
                entity.Property(e => e.SubCategoryTranslationName).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.SubCategoryTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategoryTranslations_fk1");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.SubCategoryTranslations)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategoryTranslations_fk0");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TestName).IsRequired();
            });

            modelBuilder.Entity<TestTranslation>(entity =>
            {
                entity.Property(e => e.TestTranslationName).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.TestTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestTranslations_fk1");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestTranslations)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TestTranslations_fk0");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.WordImagePath).IsRequired();

                entity.Property(e => e.WordName).IsRequired();

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Words)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Words_fk0");
            });

            modelBuilder.Entity<WordTranslation>(entity =>
            {
                entity.Property(e => e.WordTranslationName).IsRequired();

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.WordTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WordTranslations_fk1");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.WordTranslations)
                    .HasForeignKey(d => d.WordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WordTranslations_fk0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
