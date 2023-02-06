using HealthyBoost.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }
        public int Height { get; set; }
        public int Weight { get; set; }
        public double KiloGoal { get; set; }
        public int ActivityTypeID { get; set; }
        public bool State { get; set; }
        public UserType UserType { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public User()
        {
            Meals = new HashSet<Meal>();
            Foods = new HashSet<Food>();
        }
    }
}
