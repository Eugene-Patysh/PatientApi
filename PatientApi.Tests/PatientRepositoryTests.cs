using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Data.Entities;
using PatientApi.Data.Repositories;
using Xunit;

public class PatientRepositoryTests : IDisposable
{
    private readonly MedicineDbContext _context;
    private readonly PatientRepository _repository;

    private readonly PatientEntity[] patients = new PatientEntity[]
    {
        new PatientEntity { Id = Guid.NewGuid().ToString(), BirthDate = new DateTime(2013, 1, 14), Name = new PatientNameEntity { Family = "Ivanov" } },
        new PatientEntity { Id = Guid.NewGuid().ToString(), BirthDate = new DateTime(2013, 1, 14, 10, 0, 0), Name = new PatientNameEntity { Family = "Ivanov" } },
        new PatientEntity { Id = Guid.NewGuid().ToString(), BirthDate = new DateTime(2013, 1, 15), Name = new PatientNameEntity { Family = "Ivanov" } }
    };

    public PatientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MedicineDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new MedicineDbContext(options);
        _repository = new PatientRepository(_context);

        _context.Patients.AddRange(patients);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithRange_ShouldReturnCorrectPatients()
    {
        var result = await _repository.GetByBirthDateAsync("ge2013-01-14", "le2013-01-14");

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Id == patients[0].Id);
        Assert.Contains(result, p => p.Id == patients[1].Id);
        Assert.DoesNotContain(result, p => p.Id == patients[2].Id);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WhenNoMatches_ShouldReturnEmpty()
    {
        var result = await _repository.GetByBirthDateAsync("gt2025-01-01", "");

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithEqualFilter_ShouldReturnCorrectPatient()
    {
        var result = await _repository.GetByBirthDateAsync("eq2013-01-14", null);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Id == patients[0].Id);
        Assert.Contains(result, p => p.Id == patients[1].Id);
        Assert.DoesNotContain(result, p => p.Id == patients[2].Id);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithNotEqualFilter_ShouldReturnCorrectPatient()
    {
        var result = await _repository.GetByBirthDateAsync("ne2013-01-14", null);

        Assert.Single(result);
        Assert.Contains(result, p => p.Id == patients[2].Id);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithGreaterThanFilter_ShouldReturnCorrectPatient()
    {
        var result = await _repository.GetByBirthDateAsync("gt2013-01-14T10:00", null);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Id == patients[1].Id);
        Assert.Contains(result, p => p.Id == patients[2].Id);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithStartsAfterFilter_ShouldReturnCorrectPatient()
    {
        var result = await _repository.GetByBirthDateAsync("sa2013-01-14", null);

        Assert.Single(result);
        Assert.Contains(result, p => p.Id == patients[2].Id);
    }

    [Fact]
    public async Task GetByBirthDateAsync_WithEndsBeforeFilter_ShouldReturnCorrectPatient()
    {
        var result = await _repository.GetByBirthDateAsync("eb2013-01-15", null);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Id == patients[0].Id);
        Assert.Contains(result, p => p.Id == patients[1].Id);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
