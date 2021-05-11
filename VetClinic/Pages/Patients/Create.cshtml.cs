using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;
using VetClinic.Models.DbModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace VetClinic.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientsList _patientsList;
        private readonly SpeciesList _speciesList;
        private IHostingEnvironment _environment;

        public CreateModel(
            PatientsList patientsList,
            SpeciesList speciesList,
            IHostingEnvironment environment)
        {
            _patientsList = patientsList;
            _speciesList = speciesList;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PatientView PatientView { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string photoFileName = null;
            string notesFileName = null;

            if (PatientView.Photo != null)
            {
                photoFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(PatientView.Photo.FileName);
                var imagePath = Path.Combine(_environment.WebRootPath, "images", photoFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await PatientView.Photo.CopyToAsync(fileStream);
                }
            }

            if (PatientView.Notes != null)
            {
                notesFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(PatientView.Notes.FileName);
                var notesPath = Path.Combine(_environment.WebRootPath, "files", notesFileName);

                using (var fileStream = new FileStream(notesPath, FileMode.Create))
                {
                    await PatientView.Notes.CopyToAsync(fileStream);
                }
            }

            var speciesName = PatientView.Species.ToLowerInvariant();
            var species = _speciesList.SearchBy(speciesName);

            if (species == null)
            {
                _speciesList.Add(new Species
                {
                    Name = speciesName
                });

                species = _speciesList.SearchBy(speciesName);
            }

            var patient = new Patient
            {
                Name = PatientView.Name, 
                Species = species,
                Age = PatientView.Age,
                PhotoPath = photoFileName,
                NotesPath = notesFileName
            };

            _patientsList.Add(patient);

            return RedirectToPage("./Index");
        }
    }
}
