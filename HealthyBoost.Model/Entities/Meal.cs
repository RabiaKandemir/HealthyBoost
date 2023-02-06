using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class Meal
    {
        public Meal()
        {
            MealDetails = new HashSet<MealDetail>();
        }

        public int MealID { get; set; }
        public int UserID { get; set; }
        public int MealTypeID { get; set; }
        public DateTime RegisterationDate { get; set; }
        public bool State { get; set; }

        public virtual User User { get; set; }
        public virtual MealType MealType { get; set; }
        public virtual ICollection<MealDetail> MealDetails { get; set; }
    }
}
