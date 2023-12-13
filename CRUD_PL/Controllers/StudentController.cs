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
        public async Task<IActionResult> GetAllEntities()
        {
            var result = await _studentService.GetAllStudents();
            if(result!= null)
            return Ok(result);
            return Ok();
           // return result.IsT0 ? Ok(result.AsT0) : NotFound(result.AsT1);
        }
    }
}