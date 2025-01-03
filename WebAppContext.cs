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
    public DbSet<StrExerciseInTraining> StrExerciseInTrainings { get; set; }
    public DbSet<StrParams> StrParams { get; set; }
    
    public DbSet<CardioExercise> CardioExercises { get; set; }
    public DbSet<CarExerciseInTraining> CardioExercisesInTrainings { get; set; }
    public DbSet<Met> Mets { get; set; }
    
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
        
        modelBuilder.Entity<StrengthExercise>()
            .ToTable("StrengthExercises")
            .HasBaseType((Type)null);
        
        modelBuilder.Entity<CardioExercise>()
            .ToTable("CardioExercises")
            .HasBaseType((Type)null);

        
        // modelBuilder.Entity<StrengthExercise>()
        //     .HasMany(e => e.Muscles)
        //     .WithMany(e => e.StrengthExercises);
    }
}