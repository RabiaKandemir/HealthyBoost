using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class MealTypeController
    {
        HealthyDbContext context;
        public MealTypeController()
        {
            context = new HealthyDbContext();
        }
        public List<MealType> GetAllTypes()
        {
            return context.MealTypes.ToList();
        }

        public bool Insert(MealType entity)
        {
            context.MealTypes.Add(entity);
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool Update(MealType entity)
        {
            MealType mealType = context.MealTypes.Find(entity.MealTypeID);
            mealType.MealTypeName = entity.MealTypeName;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }
    }
}
