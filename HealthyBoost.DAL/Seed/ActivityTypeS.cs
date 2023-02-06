using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Seed
{
    public class ActivityTypeS
    {
        public ActivityTypeS(HealthyDbContext context)
        {
            context.ActivityTypes.Add(new ActivityType() { ActivityName = "Aktivite Yok" });
            context.ActivityTypes.Add(new ActivityType() { ActivityName = "Az Aktif" });
            context.ActivityTypes.Add(new ActivityType() { ActivityName = "Aktif" });
            context.ActivityTypes.Add(new ActivityType() { ActivityName = "Çok Aktif" });
            context.SaveChanges();
        }
    }
}
