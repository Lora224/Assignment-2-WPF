using Microsoft.EntityFrameworkCore;
using Assignment_2_WPF.Models;
using Microsoft.Extensions.Options;
using System.IO;

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
           // Database.EnsureDeleted();
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


                entity.HasOne<User>()
                       .WithMany(u => u.Pets)
                       .HasForeignKey(p => p.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activity");
                entity.HasKey(e => e.Id);

               entity.HasIndex(e => e.PetId)
                      .HasDatabaseName("IX_activity_PetId")
                      .IsUnique(false);  //set as non-unique

                // Configure required properties
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.PetId).IsRequired();
                entity.Property(e => e.PetName).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Type).IsRequired()
                      .HasConversion<string>();
                entity.Property(e => e.Level).IsRequired(false)
                      .HasConversion<string>();
                entity.Property(e => e.Distance).IsRequired(false);
                entity.Property(e => e.Duration).IsRequired(false);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PetApp.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath};Pooling=False");
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();

        }
        public override void Dispose()
        {
            try
            {
                Database.CloseConnection();
            }
            catch { }
            finally
            {
                base.Dispose();
            }
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