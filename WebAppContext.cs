using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp;

public class WebAppContext : DbContext
{
    public DbSet<CardioExercise> CardioExercises { get; set; }
    public DbSet<StrengthExercise> StrengthExercises { get; set; }
    public DbSet<Muscle> Muscles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<StrExerciseInTraining> StrExerciseInTrainings { get; set; }
    public DbSet<StrExerciseParameters> StrExerciseParameters { get; set; }
    public DbSet<CarExerciseInTraining> CarExerciseInTrainings { get; set; }
    public DbSet<CarExerciseParameters> CarExerciseParameters { get; set; }
    public DbSet<Met> Mets { get; set; }
    
    public WebAppContext(DbContextOptions<WebAppContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StrengthExercise>()
            .ToTable("StrengthExercises")
            .HasBaseType((Type)null);
        
        modelBuilder.Entity<CardioExercise>()
            .ToTable("CardioExercises")
            .HasBaseType((Type)null);

        modelBuilder.Entity<StrExerciseInTraining>()
            .HasKey(e => new { e.StrengthExerciseId, e.TrainingId });
        
        modelBuilder.Entity<StrExerciseInTraining>()
            .HasOne(e => e.StrengthExercise)
            .WithMany(s => s.StrExerciseInTrainings)
            .HasForeignKey(e => e.StrengthExerciseId);
        
        modelBuilder.Entity<StrExerciseInTraining>()
            .HasOne(e => e.Training)
            .WithMany(s => s.StrExerciseInTrainings)
            .HasForeignKey(e => e.TrainingId);

        modelBuilder.Entity<StrExerciseParameters>()
            .HasKey(e => new { e.Set, e.StrExerciseInTrainingTrainingId, e.StrExerciseInTrainingStrengthExerciseId  });
        
        modelBuilder.Entity<StrExerciseParameters>()
            .HasOne(e => e.StrExerciseInTraining)
            .WithMany(s => s.StrExerciseParameters)
            .HasForeignKey(e => new {e.StrExerciseInTrainingTrainingId, e.StrExerciseInTrainingStrengthExerciseId });

        // modelBuilder.Entity<StrengthExercise>()
        //     .HasMany(e => e.Muscles)
        //     .WithMany(e => e.StrengthExercises);
        
        modelBuilder.Entity<CarExerciseInTraining>()
            .HasKey(e => new { e.CardioExerciseId, e.TrainingId });

        // Relacja CarExerciseInTraining -> CardioExercise
        modelBuilder.Entity<CarExerciseInTraining>()
            .HasOne(e => e.CardioExercise)
            .WithMany(s => s.CarExercisesInTrainings)
            .HasForeignKey(e => e.CardioExerciseId);

        // Relacja CarExerciseInTraining -> Training
        modelBuilder.Entity<CarExerciseInTraining>()
            .HasOne(e => e.Training)
            .WithMany(s => s.CarExerciseInTrainings)
            .HasForeignKey(e => e.TrainingId);

        // Konfiguracja klucza głównego dla CarExerciseParameters
        modelBuilder.Entity<CarExerciseParameters>()
            .HasKey(e => new { e.Interval, e.CarExerciseInTrainingTrainingId, e.CarExerciseInTrainingCardioExerciseId });

        // Relacja CarExerciseParameters -> CarExerciseInTraining
        modelBuilder.Entity<CarExerciseParameters>()
            .HasOne(e => e.CarExerciseInTraining)
            .WithMany(s => s.CarExerciseParameters)
            .HasForeignKey(e => new { e.CarExerciseInTrainingTrainingId, e.CarExerciseInTrainingCardioExerciseId });

        modelBuilder.Entity<Met>()
            .HasKey(e => new { e.CardioExerciseId, e.StartSpeed });
        
        modelBuilder.Entity<Met>()
            .HasOne(e => e.CardioExercise)
            .WithMany(s => s.Mets)
            .HasForeignKey(e => e.CardioExerciseId);
    }
}