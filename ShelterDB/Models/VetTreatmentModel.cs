namespace ShelterDB.Models
{
    public class VetTreatmentModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string DateGiven { get; set; }
        public string DateDue { get; set; }

        public int AnimalId { get; set; }
    }
}
