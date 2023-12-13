using CRUD_BAL.Domains.Students.DTOs;

namespace CRUD_PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudents();
            if(result!= null)
            return Ok(result);
            return Ok();
        }

        [HttpGet("GetStudentById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var result = await _studentService.GetStudentById(id);
            return result.IsT0 ? Ok(result.AsT0) : NotFound(result.AsT1);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudent(StudentForCreateDto dto)
        {
            var result = await _studentService.AddStudent(dto);
            return result.IsT0 ? Ok(result.AsT0) : BadRequest(result.AsT1);
        }

        [HttpPut("UpdateStudentById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudentById(Guid id ,StudentForUpdateDto dto)
        {
            var result = await _studentService.UpdateStudent(id, dto);
            return result.IsT0 ? Ok(result.AsT0) : NotFound(result.AsT1);
        }
    }
}