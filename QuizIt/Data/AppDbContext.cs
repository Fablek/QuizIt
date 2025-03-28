using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizIt.Models;

namespace QuizIt.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardQuestion> FlashcardQuestions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuizIt",
                "quizit.db");

            // Upewnij się że folder istnieje
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            options.UseSqlite($"Data Source={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flashcard>()
                .HasMany(f => f.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
