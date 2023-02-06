using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class MealType
    {
        public MealType()
        {
            Meals = new HashSet<Meal>();
        }

        public int MealTypeID { get; set; }
        public string MealTypeName { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
