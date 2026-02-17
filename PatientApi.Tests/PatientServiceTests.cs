using Moq;
using PatientApi.Data.Entities;
using PatientApi.Data.Repositories;
using PatientApi.Logic.DtoBuilders;
using PatientApi.Logic.Models;
using PatientApi.Logic.Services;
using Xunit;

namespace PatientApi.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> _repositoryMock;
        private readonly Mock<IPatientBuilder> _builderMock;
        private readonly PatientService _service;

        public PatientServiceTests()
        {
            _repositoryMock = new Mock<IPatientRepository>();
            _builderMock = new Mock<IPatientBuilder>();
            _service = new PatientService(_repositoryMock.Object, _builderMock.Object);
        }

        [Fact]
        public async Task GetByBirthDateAsync_ShouldCallRepositoryWithParsedParameters()
        {
            var request = new SearchByBirthDateRequest
            {
                StartFilter = "ge2010-01-01",
                EndFilter = "le2011-12-31"
            };

            _repositoryMock
                .Setup(x => x.GetByBirthDateAsync(request.StartFilter, request.EndFilter))
                .ReturnsAsync(new List<PatientEntity>());

            await _service.GetByBirthDateAsync(request);

            _repositoryMock.Verify(x => x.GetByBirthDateAsync("ge2010-01-01", "le2011-12-31"), Times.Once);
        }
    }
}
