using System.Net.Http.Json;
using Bogus;
using PatientApi.Logic.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var apiUrl = "http://localhost:8080/api/v1/Patient/create";
var dataLength = 100;

using var httpClient = new HttpClient();

Console.WriteLine("Start seeding...");

// Configuration
var patientFaker = new Faker<PatientDto>("ru")
    .RuleFor(p => p.BirthDate, f => f.Date.Past(5, DateTime.Now))
    .RuleFor(p => p.Active, true)
    .RuleFor(p => p.Name, f => new PatientNameDto
    {
        Use = "official",
        Family = f.Name.LastName(),
        Given = new List<string> { f.Name.FirstName() }
    });

// Seeding in loop
for (int i = 0; i < dataLength; i++)
{
    var patient = patientFaker.Generate();

    try
    {
        var response = await httpClient.PostAsJsonAsync(apiUrl, patient);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Line {i + 1}: Created successfully: {patient.Name.Family}");
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Line {i + 1}: Error: {patient.Name.Family}: {response.StatusCode} - {error}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Line {i + 1}: Error: {patient.Name.Family}: {ex.Message}");
    }
}

Console.WriteLine("Seeding completed");
Console.ReadLine();
