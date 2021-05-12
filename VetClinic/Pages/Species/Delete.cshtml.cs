using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using VetClinic.Models;

namespace VetClinic.Pages.Species
{
    public class DeleteModel : PageModel
    {
        private readonly SpeciesList _speciesList;

        public DeleteModel(SpeciesList speciesList)
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

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Species = _speciesList.GetById(id);

            string message = null;
            if (Species != null)
            {
                try
                {
                    _speciesList.Delete(id);
                }
                catch(InvalidOperationException)
                {
                    message = "Unable to delete species used in patient decription";
                }
            }

            return RedirectToPage("Index", "OnGetAsync", new { message });
        }
    }
}
