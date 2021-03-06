using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Data;
using VetClinic.Models.DbModels;

namespace VetClinic.Models
{
    public class SpeciesList : CustomList<Species>
    {
        private readonly VetClinicContext _context;

        public SpeciesList(VetClinicContext context)
        {
            _context = context;
        }

        public override List<Species> GetAll()
        {
            return _context.Species.ToList();
        }

        public override Species GetById(int? id)
        {
            return _context.Species.FirstOrDefault(e => e.Id == id);

        }

        public override void Add(Species item)
        {
            if (IsValid(item.Name) && !Exists(item.Name))
            {
                _context.Species.Add(item);
                _context.SaveChanges();
            }
        }

        public override void Edit(Species item)
        {
            var species = _context.Species.FirstOrDefault(e => e.Id == item.Id);

            if (species != null && IsValid(item.Name))
            {
                species.Name = item.Name;

                _context.SaveChanges();
            }
        }

        public override void Delete(int? id)
        {
            var species = _context.Species.FirstOrDefault(e => e.Id == id);
            bool hasPatients = _context.Patients.Any(p => p.Species.Id == id);

            if (species != null && !hasPatients)
            {
                _context.Species.Remove(species);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public Species SearchBy(string property)
        {
            return _context.Species
                .Where(s => s.Name.Contains(property))
                .SingleOrDefault();
        }

        private bool IsValid(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !string.IsNullOrEmpty(name);
        }

        private bool Exists(string name)
        {
            var species = _context.Species.FirstOrDefault(e => e.Name == name);
            return species != null;
        }
    }
}
