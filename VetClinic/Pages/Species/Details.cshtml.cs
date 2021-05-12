using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;

namespace VetClinic.Pages.Species
{
    public class DetailsModel : PageModel
    {
        private readonly SpeciesList _speciesList;

        public DetailsModel(SpeciesList speciesList)
        {
            _speciesList = speciesList;
        }

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
    }
}
