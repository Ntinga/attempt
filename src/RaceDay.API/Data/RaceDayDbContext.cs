using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;

namespace RaceDay.API.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options)
        {
        }

        // DbSets corresponding to the 6 core domain entities (Microsoft, 2024)
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Enrolment> Enrolments { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table mappings and explicit foreign key relationships (Microsoft, 2024)

            // User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event -> User (Organiser)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organiser)
                .WithMany(u => u.OrganisedEvents)
                .HasForeignKey(e => e.OrganiserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category -> Event
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enrolment -> Category
            modelBuilder.Entity<Enrolment>()
                .HasOne(en => en.Category)
                .WithMany(c => c.Enrolments)
                .HasForeignKey(en => en.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Enrolment -> User (Participant)
            modelBuilder.Entity<Enrolment>()
                .HasOne(en => en.Participant)
                .WithMany(u => u.Enrolments)
                .HasForeignKey(en => en.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Result -> Enrolment
            modelBuilder.Entity<Result>()
                .HasOne(r => r.Enrolment)
                .WithOne(en => en.Result)
                .HasForeignKey<Result>(r => r.EnrolmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurations for decimal properties (Microsoft, 2024)
            modelBuilder.Entity<Category>()
                .Property(c => c.DistanceKM)
                .HasColumnType("decimal(18,2)");
        }
    }
}