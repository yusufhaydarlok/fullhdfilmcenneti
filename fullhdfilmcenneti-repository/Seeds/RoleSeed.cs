using fullhdfilmcenneti_core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_repository.Seeds
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
            new Role { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Name = "Admin" },
            new Role { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Name = "User" }
            );
        }
    }
}
