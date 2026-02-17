namespace PatientApi.Data.Entities
{
    public class PatientNameEntity
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Use { get; set; }
        public string Family { get; set; }
        public List<string> Given { get; set; } = new();
    }
}
