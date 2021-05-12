using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;

namespace VetClinic.Pages.Species
{
    public class EditModel : PageModel
    {
        private readonly SpeciesList _speciesList;

        public EditModel(SpeciesList speciesList)
        {
            _speciesList = speciesList;
        }

        [BindProperty]
        public Models.DbModels.Species Species { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Species = _speciesList.GetById(id);

            if (Species == null)
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
                _speciesList.Edit(Species);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_speciesList.GetById(Species.Id) == null)
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
    }
}
