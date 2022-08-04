using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAppTwo.Models.Entities;

namespace SimpleAppTwo.Models.Context
{
    public class UserConfiguration : IEntityTypeConfiguration<object>
    {
        public void Configure(EntityTypeBuilder<object> builder)
        {

        }
    }
}