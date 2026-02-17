using PatientApi.Data.Entities;
using PatientApi.Logic.Models;

namespace PatientApi.Logic.DtoBuilders
{
    public interface IPatientBuilder
    {
        PatientDto Build(PatientEntity entity);
        PatientEntity Build(PatientDto dto);
    }
}
