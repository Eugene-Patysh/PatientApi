using PatientApi.Data.Enums;

namespace PatientApi.Data.Entities
{
    public class PatientEntity
    {
        public string Id { get; set; }
        public PatientNameEntity Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}
