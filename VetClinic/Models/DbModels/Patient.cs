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

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        [Display(Name = "Notes")]
        public string NotesPath { get; set; }
    }
}
