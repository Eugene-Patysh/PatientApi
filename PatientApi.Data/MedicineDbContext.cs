using Microsoft.EntityFrameworkCore;
using PatientApi.Data.Configurations;
using PatientApi.Data.Entities;

namespace PatientApi.Data
{
    public class MedicineDbContext : DbContext
    {
        public MedicineDbContext(DbContextOptions<MedicineDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<PatientNameEntity> PatientNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PatientNameConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
        }
    }
}
