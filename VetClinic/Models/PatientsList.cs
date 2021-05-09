using System.Collections.Generic;
using System.Linq;
using VetClinic.Data;
using VetClinic.Models.DbModels;

namespace VetClinic.Models
{
    public class PatientsList : CustomList<Patient>
    {
        private readonly VetClinicContext _context;

        public PatientsList(VetClinicContext context)
        {
            _context = context;
        }

        public override List<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public override Patient GetById(int? id)
        {
            return _context.Patients.FirstOrDefault(e => e.Id == id);

        }

        public override void Add(Patient item)
        {
            _context.Patients.Add(item);
            _context.SaveChanges();
        }

        public override void Edit(Patient item)
        {
            var patient = _context.Patients.FirstOrDefault(e => e.Id == item.Id);

            if (patient != null) {
                patient.Name = item.Name;
                patient.Species = item.Species;
                patient.Age = item.Age;
                patient.PhotoPath = item.PhotoPath;
                patient.NotesPath = item.NotesPath;

                _context.SaveChanges();
            }
        }

        public override void Delete(int? id)
        {
            var patient = _context.Patients.FirstOrDefault(e => e.Id == id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        public override List<Patient> SearchBy(string property)
        {
            return _context.Patients
                .Where(p => p.Name.Contains(property) || p.Species.Name.Contains(property))
                .Distinct()
                .ToList();
        }
    }
}
