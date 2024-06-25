using AutoMapper;
using CorporateTraningSystm.Model;
using CorporateTraningSystm.Services;
using CorporateTraningSystm.View;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CorporateTraningSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseApiController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            var courseViewModels = _mapper.Map<IEnumerable<CourseView>>(courses);
            return Ok(courseViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            var courseViewModel = _mapper.Map<CourseView>(course);
            return Ok(courseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseView courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(courseViewModel);
                await _courseService.AddCourseAsync(course);
                return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseView courseViewModel)
        {
            if (id != courseViewModel.CourseId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(courseViewModel);
                await _courseService.UpdateCourseAsync(course);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
