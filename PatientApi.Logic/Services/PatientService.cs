using PatientApi.Data.Entities;
using PatientApi.Data.Repositories;
using PatientApi.Logic.DtoBuilders;
using PatientApi.Logic.Models;
using System.ComponentModel.DataAnnotations;

namespace PatientApi.Logic.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientBuilder _patientBuilder;

        public PatientService(IPatientRepository patientRepository,
            IPatientBuilder patientBuilder)
        {
            _patientRepository = patientRepository;
            _patientBuilder = patientBuilder;
        }

        public async Task<PatientDto> GetByIdAsync(string id)
        {
            var entity = (await _patientRepository.GetByIdAsync(id)) ?? throw new ValidationException("Patient not found");

            return _patientBuilder.Build(entity);
        }

        public async Task<PatientDto> CreateAsync(PatientDto dto)
        {
            var entity = _patientBuilder.Build(dto);

            var db = await _patientRepository.CreateAsync(entity);

            return _patientBuilder.Build(db);
        }

        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            var entity = _patientBuilder.Build(dto);

            await _patientRepository.UpdateAsync(entity);

            return dto;
        }

        public async Task DeleteAsync(string id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PatientEntity>> GetByBirthDateAsync(SearchByBirthDateRequest request)
        {
            return await _patientRepository.GetByBirthDateAsync(request.StartFilter, request.EndFilter);
        }
    }
}
