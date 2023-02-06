using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Configuration
{
    public class ActivityTypeC:EntityTypeConfiguration<ActivityType>
    {
        public ActivityTypeC()
        {
            Property(c => c.ActivityName).IsRequired().HasMaxLength(100);
        }
    }
}
