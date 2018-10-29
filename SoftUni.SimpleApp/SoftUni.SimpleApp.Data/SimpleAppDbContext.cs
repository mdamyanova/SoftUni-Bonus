namespace SoftUni.SimpleApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SimpleAppDbContext : IdentityDbContext
    {
        public SimpleAppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Journal>()
                .HasOne(j => j.User)
                .WithMany(u => u.Journals)
                .HasForeignKey(j => j.UserId);

            builder
             .Entity<Task>()
             .HasOne(t => t.Journal)
             .WithMany(j => j.Tasks)
             .HasForeignKey(t => t.JournalId);

            base.OnModelCreating(builder);
        }
    }
}