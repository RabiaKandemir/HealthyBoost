using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Seed
{
    public class MealTypeS
    {
        public MealTypeS(HealthyDbContext context)
        {
            context.MealTypes.Add(new MealType() { MealTypeName = "Kahvaltı" });
            context.MealTypes.Add(new MealType() { MealTypeName = "Öğle Yemeği" });
            context.MealTypes.Add(new MealType() { MealTypeName = "Akşam Yemeği" });
            context.MealTypes.Add(new MealType() { MealTypeName = "Ara Öğün" });

            context.SaveChanges();
        }
    }
}
