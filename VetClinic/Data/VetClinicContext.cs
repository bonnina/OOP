using System;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models.DbModels;

namespace VetClinic.Data
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext (DbContextOptions<VetClinicContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Species> Species { get; set; }
    }
}
