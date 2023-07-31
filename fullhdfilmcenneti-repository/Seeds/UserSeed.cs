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
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
            new User { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Username = "yusufabi98", FirstName = "Yusuf Haydar", LastName = "Lök", Email = "yusufhaydarlok@gmail.com", RoleId = new Guid() },
            new User { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Username = "Sentev", FirstName = "Ahmetcan", LastName = "Kılıç", Email = "ahmetcankilic@gmail.com", RoleId = new Guid() },
            new User { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, Username = "Hasso", FirstName = "Hasan Bera", LastName = "Kabadayı", Email = "hasanberakabadayi@gmail.com", RoleId = new Guid() }
            );
        }
    }
}
