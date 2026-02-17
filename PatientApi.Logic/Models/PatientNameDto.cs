namespace PatientApi.Logic.Models
{
    /// <summary>
    /// Patient name dto
    /// </summary>
    public class PatientNameDto
    {
        /// <summary>
        /// Name id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Type of usage
        /// </summary>
        public string Use { get; set; }
        /// <summary>
        /// Family name
        /// </summary>
        public string Family { get; set; }
        /// <summary>
        /// Given names collection
        /// </summary>
        public List<string> Given { get; set; } = new();
    }
}
