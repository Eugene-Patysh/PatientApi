using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Logic.Models;
using PatientApi.Logic.Models.Exceptions;
using PatientApi.Logic.Services;
using PatientApi.Logic.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientApi.Web.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "V1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ICustomValidator<PatientDto> _validator;
        private readonly ICustomValidator<SearchByBirthDateRequest> _birthDateValidator;

        public PatientController(IPatientService patientService,
            ICustomValidator<PatientDto> validator,
            ICustomValidator<SearchByBirthDateRequest> birthDateValidator)
        {
            _patientService = patientService;
            _validator = validator;
            _birthDateValidator = birthDateValidator;
        }

        /// <summary>
        /// Returns patient record by id.
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Patient found.", typeof(PatientDto))]
        [SwaggerResponse(400, "Patient id is not valid.")]
        [SwaggerResponse(404, "Patient doesn`t exist.")]
        [SwaggerResponse(500, "Something wrong.")]
        public async Task<ActionResult<PatientDto>> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new AppValidationException("Patient id is not valid.");
            }

            return Ok(await _patientService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates new patient entity.
        /// </summary>
        /// <param name="patient">patient dto</param>
        /// <returns></returns>
        [HttpPost("create")]
        [SwaggerResponse(200, "Patient record created successfully.", typeof(PatientDto))]
        [SwaggerResponse(400, "Patient dto is not valid.")]
        [SwaggerResponse(500, "Something wrong.")]
        public async Task<ActionResult<PatientDto>> CreateAsync([FromBody] PatientDto patient)
        {
            await _validator.ValidateAsync(patient, "Add");

            return Ok(await _patientService.CreateAsync(patient));
        }

        /// <summary>
        /// Updates new patient entity.
        /// </summary>
        /// <param name="patient">patient dto</param>
        /// <returns></returns>
        [HttpPut("update")]
        [SwaggerResponse(200, "Patient record updated successfully.", typeof(PatientDto))]
        [SwaggerResponse(400, "Patient dto is not valid.")]
        [SwaggerResponse(500, "Something wrong.")]
        public async Task<ActionResult<PatientDto>> UpdateAsync([FromBody] PatientDto patient)
        {
            await _validator.ValidateAsync(patient, "Update");

            return Ok(await _patientService.UpdateAsync(patient));
        }

        /// <summary>
        /// Removes patient record by id.
        /// </summary>
        /// <param name="id">patient id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(200, "Patient record deleted successfully.", typeof(PatientDto))]
        [SwaggerResponse(400, "Patient id is not valid.")]
        [SwaggerResponse(404, "Patient doesn`t exist.")]
        [SwaggerResponse(500, "Something wrong.")]
        public async Task<ActionResult> DeleteByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new AppValidationException("Patient id is not valid.");
            }

            await _patientService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// Search patients by date of birth.
        /// </summary>
        /// <remarks>
        /// ### Available prefixes:
        /// * **eq**: equal
        /// * **ne**: not equal
        /// * **gt**: greater than
        /// * **lt**: less than
        /// * **ge**: greater or equal
        /// * **le**: less or equal
        /// * **sa**: starts after
        /// * **eb**: ends before
        /// * **ap**: approximately
        /// 
        /// ### Date formates: 
        /// * `yyyy-mm-ddThh:mm:ss`
        /// * `yyyy-mm-ddThh:mm`
        /// * `yyyy-mm-ddThh`
        /// * `yyyy-mm-dd`
        /// * `yyyy-mm`
        /// * `yyyy`
        /// 
        /// ### Example: `ge2013-01-14`
        /// </remarks>
        /// <param name="request">list of date filters (supports prefixes ge, le, gt, lt, etc.)</param>
        /// <returns></returns>
        [HttpPost("birthdate")]
        [SwaggerResponse(200, "Patients found.", typeof(List<PatientDto>))]
        [SwaggerResponse(400, "Request is not valid.")]
        [SwaggerResponse(500, "Something wrong.")]
        public async Task<ActionResult<List<PatientDto>>> SearchByBirthDateAsync([FromBody] SearchByBirthDateRequest request)
        {
            await _birthDateValidator.ValidateAsync(request, "");

            return Ok(await _patientService.GetByBirthDateAsync(request));
        }
    }
}
