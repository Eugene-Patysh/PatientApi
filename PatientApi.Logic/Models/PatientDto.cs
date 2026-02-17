using PatientApi.Data.Enums;

namespace PatientApi.Logic.Models
{
    /// <summary>
    /// Patient dto
    /// </summary>
    public class PatientDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name info
        /// </summary>
        public PatientNameDto Name { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Birthday date
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Flag marking whether the patient is active
        /// </summary>
        public bool Active { get; set; }
    }
}
