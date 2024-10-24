using Microsoft.EntityFrameworkCore;
using Assignment_2_WPF.Models;
using Microsoft.Extensions.Options;

namespace Assignment_2_WPF
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public AppDbContext()
        {
            // Only create the database if it doesn't exist
            Database.EnsureDeleted();
            Database.EnsureCreated();

 
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("pet");
                entity.HasKey(e => e.Id);

                // Configure nullable properties
                entity.Property(e => e.PetName).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Breed).IsRequired();
                entity.Property(e => e.Dob).IsRequired();
                entity.Property(e => e.Weight).IsRequired();

                // Configure relationship with Activities
                entity.HasMany(p => p.Activities)
                      .WithOne(a => a.Pet)
                      .HasForeignKey(a => a.PetId)
                      .IsRequired()  // Make the relationship nullable
                      .OnDelete(DeleteBehavior.SetNull);  // Set FK to null when pet is deleted
                entity.HasOne<User>()
                       .WithMany(u => u.Pets)
                       .HasForeignKey(p => p.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activity");
                entity.HasKey(e => e.Id);

                // Make PetId nullable in activities table
                entity.Property(e => e.PetId).IsRequired(false);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e=>e.Id);
                entity.Property(e=>e.Name).IsRequired();
                entity.Property(e=>e.Password).IsRequired();

            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PetApp.db");

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();

        }
        public async Task CheckTableSchemas()
        {
            var connection = Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT name, sql 
            FROM sqlite_master 
            WHERE type='table' AND (name='pet' OR name='activity');";

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var tableName = reader.GetString(0);
                var createSql = reader.GetString(1);
                System.Diagnostics.Debug.WriteLine($"\nTable: {tableName}");
                System.Diagnostics.Debug.WriteLine(createSql);
            }
        }
    }
}