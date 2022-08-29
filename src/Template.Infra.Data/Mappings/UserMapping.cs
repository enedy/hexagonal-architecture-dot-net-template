using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasColumnType("varchar(150)");

            builder.Property(x => x.CreationUserId)
                   .IsRequired()
                   .HasColumnType("varchar(150)"); // TODO: Create relation with another entity

            builder.Property(x => x.CreationDate)
                   .IsRequired();

            builder.Property(x => x.CreationProgram)
                   .IsRequired()
                   .HasColumnType("varchar(65)");

            builder.Property(x => x.UpdateUserId)
                   .IsRequired()
                   .HasColumnType("varchar(150)"); // TODO: Create relation with another entity

            builder.Property(x => x.UpdateDate)
                   .IsRequired();

            builder.Property(x => x.UpdateProgram)
                   .IsRequired()
                   .HasColumnType("varchar(65)");

            builder.ToTable("User");
        }
    }
}
