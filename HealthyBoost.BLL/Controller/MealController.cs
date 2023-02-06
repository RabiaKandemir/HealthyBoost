using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class MealController
    {
        HealthyDbContext context;
        public MealController()
        {
            context = new HealthyDbContext();
        }
        public Meal GetMealByID(int id)
        {
            return context.Meals.Find(id);
        }

        public List<Meal> GetAll()
        {
            return context.Meals.Where(x => x.State == true).ToList();
        }

        public List<Meal> GetAll(DateTime baslangic, DateTime bitis)
        {
            return context.Meals.Where(x => x.State == true && x.RegisterationDate >= baslangic && x.RegisterationDate <= bitis).ToList();
        }


        public List<Meal> GetUserMeals(int userid)
        {
            return context.Meals.Where(x => x.State == true && x.UserID == userid).ToList();
        }


        public List<Meal> GetUserMeals(int userid, DateTime baslangic, DateTime bitis)
        {
            return context.Meals.Where(x => x.State == true && x.UserID == userid && x.RegisterationDate >= baslangic && x.RegisterationDate <= bitis).ToList();
        }

        public List<MealDetail> GetAllByCategory(int categoryId)
        {
            return context.MealDetails.Where(x => x.Meal.State == true && x.Food.CategoryID == categoryId).ToList();
        }

        public List<MealDetail> GetAllByCategory(int categoryId, DateTime baslangic, DateTime bitis)
        {
            return context.MealDetails.Where(x => x.Meal.State == true && x.Food.CategoryID == categoryId && x.Meal.RegisterationDate >= baslangic && x.Meal.RegisterationDate <= bitis).ToList();
        }

        public bool Insert(Meal entity)
        {

            context.Meals.Add(entity);
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool Update(Meal entity)
        {
            Meal updatedMeal = context.Meals.SingleOrDefault(x => x.MealID == entity.MealID);
            updatedMeal = entity;

            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool Delete(int mealId)
        {
            Meal deletedMeal = context.Meals.Find(mealId);
            deletedMeal.State = false;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }
    }
}
