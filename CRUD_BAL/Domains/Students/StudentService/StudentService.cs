
namespace CRUD_BAL.Domains.Students.StudentService
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentService;
        public StudentService(IStudentRepository studentService)
        {
            _studentService = studentService;
        }


    }
}
