using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;

namespace VetClinic.Pages.Species
{
    public class IndexModel : PageModel
    {
        private readonly SpeciesList _speciesList;

        public IndexModel(SpeciesList speciesList)
        {
            _speciesList = speciesList;
        }

        public IList<Models.DbModels.Species> Species { get;set; }

        public void OnGetAsync(string message)
        {
            ViewData["Message"] = message;
            Species = _speciesList.GetAll();
        }
    }
}
