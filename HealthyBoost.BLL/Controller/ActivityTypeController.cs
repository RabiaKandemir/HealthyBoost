using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class ActivityTypeController
    {
        HealthyDbContext context;
        public ActivityTypeController()
        {
            context = new HealthyDbContext();
        }
        public List<ActivityType> GetActivities()
        {
            return context.ActivityTypes.ToList();
        }
    }
}
