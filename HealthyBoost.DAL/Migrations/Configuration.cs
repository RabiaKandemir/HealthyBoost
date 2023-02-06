namespace HealthyBoost.DAL.Migrations
{
    using HealthyBoost.DAL.Entity;
    using HealthyBoost.DAL.Seed;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HealthyBoost.DAL.Entity.HealthyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HealthyDbContext context)
        {
            new ActivityTypeS(context);
            new CategoryS(context);
            new MealTypeS(context);
            new UserS(context);
            new FoodS(context);
        }
    }
}
