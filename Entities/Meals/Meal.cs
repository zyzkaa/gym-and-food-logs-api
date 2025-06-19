using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class Meal
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public int CreatorID { get; set; }
    public User Creator { get; set; }
    
    public int? EditorID { get; set; }
    public User? Editor { get; set; }


    public DateTime CreatedAt { get; set; }

    [Url]
    public string? ImageURL { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    // Optional reference to the original meal (if this is a clone/variant)
    public int? OriginalMealID { get; set; }
    public Meal? OriginalMeal { get; set; }

    // Inverse navigation to meals that were cloned from this one
    public ICollection<Meal> ClonedMeals { get; set; } = new List<Meal>();

    // Indicates whether this meal is publicly visible to other users
    public bool IsShared { get; set; } = false;

    // Many-to-many relationship with ingredients via join table
    public ICollection<MealIngredient> Ingredients { get; set; } = new List<MealIngredient>();

    // Computed properties
    public double CalculatedCalories => Ingredients.Sum(i => i.TotalCalories);
    public double CalculatedProtein => Ingredients.Sum(i => i.TotalProtein);
    public double CalculatedCarbs => Ingredients.Sum(i => i.TotalCarbs);
    public double CalculatedFat => Ingredients.Sum(i => i.TotalFat);

    // Relation to meal plans (e.g. for scheduling)
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
}