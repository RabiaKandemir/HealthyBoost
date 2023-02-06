using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class FoodController
    {
        HealthyDbContext context;
        public FoodController()
        {
            context = new HealthyDbContext();
        }
        public List<Food> GetAll()
        {
            return context.Foods.OrderBy(c => c.CategoryID).ToList();
        }


        public List<Food> GetAllByUser(int userid)
        {
            return context.Foods.Where(x => (x.UserID == 1 || x.UserID == userid) && x.State == true).OrderBy(c => c.CategoryID).ToList();
        }

        public List<Food> FindFood(string word)
        {
            return context.Foods.Where(x => x.Name.Contains(word)).ToList();
        }

        public List<Food> FindFoodByUser(int userid, string word)
        {
            return context.Foods.Where(x => x.Name.Contains(word) && (x.UserID == 1 || x.UserID == userid) && x.State == true).ToList();
        }

        public Food FindFoodByID(int id)
        {
            return context.Foods.Find(id);
        }

        public bool Insert(Food food)
        {
            context.Foods.Add(food);
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool Update(Food food)
        {
            Food updatedFood = context.Foods.SingleOrDefault(x => x.FoodID == food.FoodID);
            updatedFood.Name = food.Name;
            updatedFood.CategoryID = food.CategoryID;
            updatedFood.PortionSize = food.PortionSize;
            updatedFood.Per100Cal = food.Per100Cal;
            updatedFood.Grams = food.Grams;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool DelistFood(int foodid)
        {
            Food food = context.Foods.SingleOrDefault(x => x.FoodID == foodid);
            context.Foods.Remove(food);
            int affRows = context.SaveChanges();
            return affRows > 0;
        }
    }
}
