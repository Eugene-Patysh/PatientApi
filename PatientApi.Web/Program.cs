using Asp.Versioning;
using PatientApi.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServicesConfiguration.Configure(builder);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

SwaggerConfiguratin.Configure(builder);

// use dates without mandatory conversion to UTC
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

SwaggerConfiguratin.Use(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
