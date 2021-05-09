using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetClinic.Models;
using VetClinic.Models.DbModels;

namespace VetClinic.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly PatientsList _patientsList;

        public IndexModel(PatientsList patientsList)
        {
            _patientsList = patientsList;
        }

        public IList<Patient> PatientList { get;set; }

        public void OnGetAsync()
        {
            PatientList = _patientsList.GetAll();
        }
    }
}
