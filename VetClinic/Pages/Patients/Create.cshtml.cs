using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientsList _patientsList;

        public CreateModel(PatientsList patientsList)
        {
            _patientsList = patientsList;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _patientsList.Add(Patient);

            return RedirectToPage("./Index");
        }
    }
}
