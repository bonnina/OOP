using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VetClinic.Data;
using VetClinic.Models.DbModels;

namespace VetClinic.Models
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VetClinicContext(
                serviceProvider.GetRequiredService<DbContextOptions<VetClinicContext>>()))
            {
                if (!context.Species.Any())
                {
                    context.Species.AddRange(
                        new Species() { Name = "cat" },
                        new Species() { Name = "dog" },
                        new Species() { Name = "snake" },
                        new Species() { Name = "horse" }
                    );

                    context.SaveChanges();
                }

                if (context.Patients.Any())
                {
                    return;
                }

                context.Patients.AddRange(
                    new Patient()
                    {
                        Name = "Leo",
                        Species = context.Species.Where(s => s.Name == "cat").FirstOrDefault(),
                        Age = 3,
                        PhotoPath = "cat.png",
                        NotesPath = "cat.txt"
                    },
                    new Patient()
                    {
                        Name = "Tim",
                        Species = context.Species.Where(s => s.Name == "dog").FirstOrDefault(),
                        Age = 2,
                        PhotoPath = "dog.jpg",
                        NotesPath = "dog.txt"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
