using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Data.Repositories;
using PatientApi.Logic.DtoBuilders;
using PatientApi.Logic.Models;
using PatientApi.Logic.Services;
using PatientApi.Logic.Validators;

namespace PatientApi.Web.Configurations
{
    public static class ServicesConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            // Database
            var connectionString = builder.Configuration.GetValue<string>("MedicineDbConnection");

            builder.Services.AddDbContext<MedicineDbContext>(options => {
                options.UseNpgsql(connectionString);
            });
            builder.Services.AddTransient<MedicineDbContext>();

            // Repositories
            builder.Services.AddTransient<IPatientRepository, PatientRepository>();

            // Dto builders
            builder.Services.AddTransient<IPatientBuilder, PatientBuilder>();
            
            // Services
            builder.Services.AddTransient<IPatientService, PatientService>();

            // Validators
            builder.Services.AddTransient<IValidator<PatientDto>, PatientValidator>();
            builder.Services.AddTransient<ICustomValidator<PatientDto>, CustomValidator<PatientDto>>();
            builder.Services.AddTransient<IValidator<SearchByBirthDateRequest>, SearchByBirthDateRequestValidator>();
            builder.Services.AddTransient<ICustomValidator<SearchByBirthDateRequest>, CustomValidator<SearchByBirthDateRequest>>();
        }
    }
}
