using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL
{
    public class MealTypeC: EntityTypeConfiguration<MealType>
    {
        public MealTypeC()
        {
            Property(c => c.MealTypeName).HasMaxLength(50).IsRequired();
        }
    }
}
