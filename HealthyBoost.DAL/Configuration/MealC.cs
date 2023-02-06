using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Configuration
{
    public class MealC:EntityTypeConfiguration<Meal>
    {
        public MealC()
        {
            HasRequired(a => a.User).WithMany(b => b.Meals).HasForeignKey(c => c.UserID).WillCascadeOnDelete(false);
        }
    }
}
