using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VetClinic.Data;
using VetClinic.Models.DbModels;

namespace VetClinic.Models
{
    public class PatientsList : CustomList<Patient>
    {
        private readonly VetClinicContext _context;

        public PatientsList(
            VetClinicContext context)
        {
            _context = context;
        }

        public override List<Patient> GetAll()
        {
            return _context.Patients
                .Include(p => p.Species)
                .ToList();
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
                patient.PhotoPath = item.PhotoPath ?? patient.PhotoPath;
                patient.NotesPath = item.NotesPath ?? patient.NotesPath;

                _context.SaveChanges();
            }
        }

        public override void Delete(int? id)
        {
            var patient = _context.Patients.FirstOrDefault(e => e.Id == id);

            if (patient != null)
            {
                var rootPath = "wwwroot";
                var photoPath = Path.Combine(rootPath, "images", patient.PhotoPath);
                var notesPath = Path.Combine(rootPath, "files", patient.NotesPath);
                File.Delete(photoPath);
                File.Delete(notesPath);

                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }

        public List<Patient> SearchBy(string property)
        {
            return _context.Patients
                .Where(p => p.Name.Contains(property) || p.Species.Name.Contains(property))
                .Distinct()
                .ToList();
        }

        public List<Patient> SearchBy(int age)
        {
            return _context.Patients
                .Where(p => p.Age == age)
                .Distinct()
                .ToList();
        }
    }
}
