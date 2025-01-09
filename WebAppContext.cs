using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Entities;

namespace WebApp;

public class WebAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Training> Trainings { get; set; }

    public DbSet<StrengthExercise> StrengthExercises { get; set; }
    public DbSet<Muscle> Muscles { get; set; }
    public DbSet<StrengthExerciseInTraining> StrengthExercisesInTraining{ get; set; }
    public DbSet<StrengthExerciseParams> StrengthExerciseParams { get; set; }
    public DbSet<CardioExercise> CardioExercises { get; set; }
    public DbSet<CardioExerciseInTraining> CardioExerciseInTraining { get; set; }
    public DbSet<Met> Mets { get; set; }
    public DbSet<CardioExerciseParams> CardioExerciseParams { get; set; }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<MealIngredient> MealIngredients { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    
    public WebAppContext(DbContextOptions<WebAppContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // strenth i cardio exercises in diffrent tables
        modelBuilder.Entity<StrengthExercise>()
            .ToTable("StrengthExercises")
            .HasBaseType((Type)null);
        
        modelBuilder.Entity<CardioExercise>()
            .ToTable("CardioExercises")
            .HasBaseType((Type)null);
        
        //cascade deleting when removing training
        modelBuilder.Entity<Training>()
            .HasMany(t => t.StrengthExercises)
            .WithOne(e => e.Training)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Training>()
            .HasMany(t => t.CardioExercises)
            .WithOne(e => e.Training)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StrengthExerciseInTraining>()
            .HasMany(e => e.Params)
            .WithOne(p => p.StrengthExerciseInTraining)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CardioExerciseInTraining>()
            .HasMany(e => e.Params)
            .WithOne(p => p.CardioExerciseInTraining)
            .OnDelete(DeleteBehavior.Cascade);
        
        //cascade deleting when removing user
        modelBuilder.Entity<User>()
            .HasMany(u => u.Trainings)
            .WithOne(t => t.User)
            .OnDelete(DeleteBehavior.Cascade);
    }
}