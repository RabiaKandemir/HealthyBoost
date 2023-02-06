using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Configuration
{
    public class CategoryC :EntityTypeConfiguration<Category>
    {
        public CategoryC()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
}
