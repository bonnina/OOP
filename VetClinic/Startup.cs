using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetClinic.Data;
using Microsoft.EntityFrameworkCore;
using VetClinic.Models;

namespace VetClinic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<VetClinicContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("VetClinicContext")));

            var optionsBuilder = new DbContextOptionsBuilder<VetClinicContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("VetClinicContext"));

            var context = new VetClinicContext(optionsBuilder.Options);
            var patientList = new PatientsList(context);
            var speciesList = new SpeciesList(context);
            services.AddSingleton<PatientsList>(patientList);
            services.AddSingleton<SpeciesList>(speciesList);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
