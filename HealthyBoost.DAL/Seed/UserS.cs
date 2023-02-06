using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using HealthyBoost.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Seed
{
    public class UserS
    {
        public UserS(HealthyDbContext context)
        {
            User admin = new User()
            {
                Name = "admin",
                LastName = "admin",
                UserName = "admin",
                Password = "admin",
                UserType = UserType.Admin,
                ActivityTypeID = 1,
                BirthDate = DateTime.Now,
                Height = 0,
                Weight = 0,
                KiloGoal = 0,
                Gender = Gender.Diğer,
                State = true
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
