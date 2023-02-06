using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class MealDetail
    {
        public int MealDetailID { get; set; }
        public int? MealID { get; set; }
        public int FoodID { get; set; }
        public int Calory { get; set; }
        public int Grams { get; set; }

        public virtual Meal Meal { get; set; }
        public virtual Food Food { get; set; }
    }
}
