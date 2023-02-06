using HealthyBoost.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class Food
    {
        public Food()
        {
            MealDetails = new HashSet<MealDetail>();
        }

        public int FoodID { get; set; }
        public string Name { get; set; }
        public int Per100Cal { get; set; }
        public int Grams { get; set; } = 100;
        public bool State { get; set; } = true;
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        public PortionSize PortionSize { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<MealDetail> MealDetails { get; set; }
    }
}
