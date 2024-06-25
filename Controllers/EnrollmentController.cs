using AutoMapper;
using CorporateTraningSystm.Model;
using CorporateTraningSystm.Services;
using CorporateTraningSystm.View;
using Microsoft.AspNetCore.Mvc;

namespace CorporateTraningSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentApiController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMapper _mapper;

        public EnrollmentApiController(IEnrollmentService enrollmentService, IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            var enrollmentViewModels = _mapper.Map<IEnumerable<EnrollmentView>>(enrollments);
            return Ok(enrollmentViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            var enrollmentViewModel = _mapper.Map<EnrollmentView>(enrollment);
            return Ok(enrollmentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(EnrollmentView enrollmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentViewModel);
                await _enrollmentService.AddEnrollmentAsync(enrollment);
                return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.EnrollmentId }, enrollment);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, EnrollmentView enrollmentViewModel)
        {
            if (id != enrollmentViewModel.EnrollmentId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentViewModel);
                await _enrollmentService.UpdateEnrollmentAsync(enrollment);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteEnrollmentAsync(id);
            return NoContent();
        }
    }
}
