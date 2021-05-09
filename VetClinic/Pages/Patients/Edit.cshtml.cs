using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientsList _patientsList;

        public EditModel(PatientsList patientsList)
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

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _patientsList.Edit(Patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(Patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PatientExists(int id)
        {
            return _patientsList.GetById(id) != null;
        }
    }
}
