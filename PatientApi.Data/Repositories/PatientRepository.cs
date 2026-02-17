using Microsoft.EntityFrameworkCore;
using PatientApi.Data.Entities;
using PatientApi.Data.Extentions;
using System.ComponentModel.DataAnnotations;

namespace PatientApi.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        protected readonly MedicineDbContext _context;

        public PatientRepository(MedicineDbContext context)
        {
            _context = context;
        }

        public async Task<PatientEntity> GetByIdAsync(string id)
        {
            return await _context.Patients.AsNoTracking().Include(x => x.Name).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PatientEntity> CreateAsync(PatientEntity entity)
        {
            await _context.Patients.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<PatientEntity> UpdateAsync(PatientEntity entity)
        {
            var db = await _context.Patients.FirstOrDefaultAsync(p => p.Id == entity.Id) ?? throw new ValidationException("Patient not found");

            db.BirthDate = entity.BirthDate;
            db.Gender = entity.Gender;
            db.Active = entity.Active;
            db.Name = entity.Name;

            await _context.SaveChangesAsync();

            return db;
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Patients.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<PatientEntity>> GetByBirthDateAsync(string firstFilter, string secondFilter)
        {
            return await _context.Patients
                .AsNoTracking()
                .FilterByBirthDate(firstFilter)
                .FilterByBirthDate(secondFilter)
                .Include(x => x.Name)
                .ToListAsync();
        }
    }
}
