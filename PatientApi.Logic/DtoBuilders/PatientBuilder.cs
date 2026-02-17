using PatientApi.Data.Entities;
using PatientApi.Logic.Models;

namespace PatientApi.Logic.DtoBuilders
{
    public class PatientBuilder : IPatientBuilder
    {
        public PatientBuilder() { }

        public PatientDto Build(PatientEntity entity)
        {
            return new PatientDto
            {
                Id = entity.Id,
                BirthDate = entity.BirthDate,
                Gender = entity.Gender,
                Active = entity.Active,
                Name = new PatientNameDto
                {
                    Id = entity.Name?.Id,
                    Family = entity.Name?.Family,
                    Use = entity.Name?.Use,
                    Given = entity.Name?.Given
                }
            };
        }

        public PatientEntity Build(PatientDto dto)
        {
            return new PatientEntity
            {
                Id = dto.Id,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Active = dto.Active,
                Name = new PatientNameEntity
                {
                    Id = dto.Name?.Id,
                    PatientId = dto.Id,
                    Family = dto.Name?.Family,
                    Use = dto.Name?.Use,
                    Given = dto.Name?.Given
                }
            };
        }
    }
}
