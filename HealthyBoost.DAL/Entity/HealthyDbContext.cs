
using HealthyBoost.DAL.Configuration;
using HealthyBoost.Model.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace HealthyBoost.DAL.Entity
{
    public class HealthyDbContext : DbContext
    {
        public HealthyDbContext()
            : base("name=HealthyDbContext")
        {
        }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealDetail> MealDetails { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new ActivityTypeC());
            modelBuilder.Configurations.Add(new UserC());
            modelBuilder.Configurations.Add(new MealTypeC());
            modelBuilder.Configurations.Add(new CategoryC());
            modelBuilder.Configurations.Add(new MealC());
            modelBuilder.Configurations.Add(new FoodC());

            base.OnModelCreating(modelBuilder);
        }
    }
}