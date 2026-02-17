using PatientApi.Data.Entities;
using PatientApi.Logic.Models;

namespace PatientApi.Logic.Services
{
    public interface IPatientService
    {
        Task<PatientDto> GetByIdAsync(string id);
        Task<PatientDto> CreateAsync(PatientDto dto);
        Task<PatientDto> UpdateAsync(PatientDto dto);
        Task DeleteAsync(string id);
        Task<IEnumerable<PatientEntity>> GetByBirthDateAsync(SearchByBirthDateRequest request);
    }
}
