using e_school.Models;
using e_school.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace e_school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IActionResult GetClasses()
        {
            List<Class> classes = _teacherService.GetClasses();

            return Ok(classes);
        }

        [HttpGet("GetClassByName")]
        public IActionResult GetClassByName(string name) 
        {
            var result = _teacherService.GetClassByName(name);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Nem létező osztály");
        }

        [HttpDelete("DeleteGrade")]
        public async Task<IActionResult> DeleteGradeAsync(int gradeId, string studentId) 
        {
            var result =await _teacherService.DeleteGradeAsync(gradeId, studentId);

            if (result)
            {
                return Ok($"A Következő jegy törlésre kerül: {gradeId}, {studentId}");
            }

            return BadRequest($"Érvénytelen adatok {result}");
        }

        [HttpPost("ModifyGrade")]
        public async Task<IActionResult> ModifyGradeAsync(int gradeId, int newValue) 
        {
            var result = await _teacherService.ModifyGradeAsync(gradeId, newValue);
            
            if (result) 
            {
                return Ok($"A {gradeId} frissítésre került: {newValue}");
            }

            return BadRequest("Érvénytelen adatok vagy nem létező jegy");
        }

        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGradeAsync(int value, string studentId, string teacherId, int subjectId)
        {
            var result = await _teacherService.AddGradeAsync(value, studentId, teacherId, subjectId);

            if (result) 
            {
                return Ok($"A jegy feltöltésre került :{value}");
            }

            return BadRequest("Az adatok kitöltése nem megfelelő");
        }
    }
}
