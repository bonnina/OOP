using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models.DbModels
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, StringLength(24)]
        public string Name { get; set; }

        [Required]
        public Species Species { get; set; }

        [Required]
        public float Age { get; set; }

        public string PhotoPath { get; set; }

        public string NotesPath { get; set; }
    }
}
