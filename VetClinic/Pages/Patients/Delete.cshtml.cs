using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly PatientsList _patientsList;

        public DeleteModel(PatientsList patientsList)
        {
            _patientsList = patientsList;
        }

        [BindProperty]
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

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _patientsList.Delete(id);

            return RedirectToPage("./Index");
        }
    }
}
