using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientsList _patientsList;
        private readonly SpeciesList _speciesList;
        private IHostingEnvironment _environment;

        public EditModel(PatientsList patientsList,
            SpeciesList speciesList,
            IHostingEnvironment environment)
        {
            _patientsList = patientsList;
            _speciesList = speciesList;
            _environment = environment;
        }

        [BindProperty]
        public PatientView PatientView { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientsList.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            PatientView = new PatientView { 
                Id = patient.Id,
                Name = patient.Name,
                Species = patient.Species.Name,
                Age = patient.Age,
                PhotoPath = patient.PhotoPath,
                NotesPath = patient.NotesPath
            };

            return Page();
        }

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
                _speciesList.Add(new Models.DbModels.Species
                {
                    Name = speciesName
                });

                species = _speciesList.SearchBy(speciesName);
            }

            var patient = new Patient
            {
                Id = PatientView.Id,
                Name = PatientView.Name,
                Species = species,
                Age = PatientView.Age,
                PhotoPath = photoFileName,
                NotesPath = notesFileName
            };

            try
            {
                _patientsList.Edit(patient); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(PatientView.Id))
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
