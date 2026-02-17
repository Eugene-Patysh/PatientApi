using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientApi.Data.Entities;

namespace PatientApi.Data.Configurations
{
    internal class PatientConfiguration : IEntityTypeConfiguration<PatientEntity>
    {        
        public void Configure(EntityTypeBuilder<PatientEntity> builder)
        {
            builder.ToTable("Patients").HasKey(t => t.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.BirthDate).IsRequired();
            builder.HasOne(_ => _.Name)
               .WithOne()
               .HasForeignKey<PatientNameEntity>(n => n.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
