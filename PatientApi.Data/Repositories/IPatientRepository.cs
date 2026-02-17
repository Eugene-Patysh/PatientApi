using PatientApi.Data.Entities;

namespace PatientApi.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<PatientEntity> GetByIdAsync(string id);
        Task<PatientEntity> CreateAsync(PatientEntity entity);
        Task<PatientEntity> UpdateAsync(PatientEntity entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<PatientEntity>> GetByBirthDateAsync(string firstFilter, string secondFilter);
    }
}
