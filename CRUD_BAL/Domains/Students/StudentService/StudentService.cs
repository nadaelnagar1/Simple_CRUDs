
namespace CRUD_BAL.Domains.Students.StudentService
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentService;
        public StudentService(IStudentRepository studentService)
        {
            _studentService = studentService;
        }

        public Task<StudentForReadDto> AddStudent(StudentForCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<StudentForReadDto> DeleteStudent(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentForReadDto>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public Task<StudentForReadDto> GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentForReadDto> UpdateStudent(Guid id, StudentForUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
