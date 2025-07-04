using PPSProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PPSProject.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Activity> Activities => Set<Activity>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts) { }

        //I am Seeding 40 dummy rows when starting the application for the first time.
        public static void Seed(ApplicationDbContext db)
        {
            if (db.Activities.Any()) return;

            var rnd = new Random();
            var types = new[] { "Commit", "Pull Request", "Issue", "Comment" };

            for (int i = 0; i < 40; i++)
            {
                db.Activities.Add(new Activity
                {
                    Type = types[rnd.Next(types.Length)],
                    Title = $"Sample {types[rnd.Next(types.Length)]} #{i + 1}",
                    Description = "This are my latest updates …",
                    Url = "https://github.com/octocat/Hello-World",
                    DateUtc = DateTime.UtcNow.AddHours(-i),
                    IsReadLater = false,
                    IsFavourite = false
                });
            }
            db.SaveChanges();
        }
    }
}
