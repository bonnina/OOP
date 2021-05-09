using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models.DbModels
{
    public class Species
    {
        public int Id { get; set; }

        [Required, StringLength(24)]
        public string Name { get; set; }
    }
}
