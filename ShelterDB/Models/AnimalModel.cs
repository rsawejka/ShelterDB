using System.ComponentModel;

namespace ShelterDB.Models
{
    public class AnimalModel
    {
        [DisplayName("Animal ID")]
        public int Id { get; set; }
        [DisplayName("Animal Name")]

        public string Name { get; set; }
        [DisplayName("Animal Type")]

        public string Type { get; set; }
        [DisplayName("Animal Breed")]

        public string Breed { get; set; }
        [DisplayName("Animal Date of Birth")]

        public string AnimalDob { get; set; }
        [DisplayName("Animal Color")]

        public string Color { get; set; }
        [DisplayName("Animal Microchip")]

        public string Microchip { get; set; }
        [DisplayName("Animal Img Url")]

        public string AnimalImg { get; set; }
        [DisplayName("Animal Status")]

        public string AnimalStatus { get; set; }
    }
}
