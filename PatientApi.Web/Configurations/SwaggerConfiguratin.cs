using Microsoft.OpenApi.Models;
using System.Reflection;

namespace PatientApi.Web.Configurations
{
    public static class SwaggerConfiguratin
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Patient API",
                    Description = "CRUD api for patient collection",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Eugene Patysh",
                        Email = "eugene.patysh@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.EnableAnnotations();
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
        }

        public static void Use(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
                // Hide schema information
                options.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
