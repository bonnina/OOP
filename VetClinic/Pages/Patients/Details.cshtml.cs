using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class DetailsModel : PageModel
    {
        private readonly PatientsList _patientsList;

        public DetailsModel(PatientsList patientsList)
        {
            _patientsList = patientsList;
        }

        public Patient Patient { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Patient = _patientsList.GetById(id);

            if (Patient == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
