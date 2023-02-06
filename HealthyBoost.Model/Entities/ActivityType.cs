using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.Model.Entities
{
    public class ActivityType
    {
        public ActivityType()
        {
            Users = new HashSet<User>();
        }

        public int ActivityTypeID { get; set; }
        public string ActivityName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
