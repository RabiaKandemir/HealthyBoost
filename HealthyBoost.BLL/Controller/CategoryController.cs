using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class CategoryController
    {
        HealthyDbContext context = new HealthyDbContext();
        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }
        public bool Insert(Category category)
        {
            context.Categories.Add(category);
            return context.SaveChanges() > 0;
        }
        public bool Update(Category category)
        {
            Category category1 = context.Categories.Where(x => x.CategoryID == category.CategoryID).SingleOrDefault();
            category1.Name = category.Name;
            return context.SaveChanges() > 0;
        }
        public Category GetCategoryById(int id)
        {
            return context.Categories.Find(id);
        }
    }
}
