using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;

namespace VetClinic.Pages.Species
{
    public class CreateModel : PageModel
    {
        private readonly SpeciesList _speciesList;

        public CreateModel(SpeciesList speciesList)
        {
            _speciesList = speciesList;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.DbModels.Species Species { get; set; }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _speciesList.Add(Species);

            return RedirectToPage("./Index");
        }
    }
}
