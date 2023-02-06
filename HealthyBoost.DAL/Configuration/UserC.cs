using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.DAL.Configuration
{
    public class UserC:EntityTypeConfiguration<User>
    {
        public UserC()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(50);
            Property(c => c.LastName).IsRequired().HasMaxLength(50);
            Property(c => c.UserName).IsRequired().HasMaxLength(50);
            Property(c => c.Password).IsRequired();
            Ignore(c => c.Age);
        }
    }
}
