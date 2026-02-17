using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientApi.Data.Entities;

namespace PatientApi.Data.Configurations
{
    internal class PatientNameConfiguration : IEntityTypeConfiguration<PatientNameEntity>
    {
        public void Configure(EntityTypeBuilder<PatientNameEntity> builder)
        {
            builder.ToTable("PatientNames").HasKey(t => t.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.Family).IsRequired();

            builder.Property(t => t.Given)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}
