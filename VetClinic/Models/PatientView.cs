using Microsoft.AspNetCore.Http;
using VetClinic.Models.DbModels;

namespace VetClinic.Models
{
    public class PatientView : Patient
    {
        public new string Species { get; set; }

        public IFormFile Photo { get; set; }

        public IFormFile Notes { get; set; }
    }
}
