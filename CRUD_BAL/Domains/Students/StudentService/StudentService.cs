namespace CRUD_BAL.Domains.Students.StudentServices
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGenericService _genericService;
        public StudentService(IStudentRepository studentRepository, IGenericService genericService)
        {
            _studentRepository = studentRepository;
            _genericService = genericService;
        }

        public async Task<StudentForReadDto> AddStudent(StudentForCreateDto dto)
        {
            var adaptedStudent =dto.Adapt<Student>();
            var createdStudent = await _studentRepository.AddAsync(adaptedStudent);
           return createdStudent.Adapt<StudentForReadDto>();
        }

        public async Task<OneOf<StudentForReadDto, Response>> DeleteStudent(Guid id)
        {
          var deletedStudent = await _studentRepository.Delete(id);
            if (deletedStudent != null)
            {
                return deletedStudent.Adapt<StudentForReadDto>();
            }
            return await _genericService.CreateResponse(ResponseMessages.Error,ResponseMessages.Deleted(ResponseMessages.Student));
        }

        public async Task<List<StudentForReadDto>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Adapt<List<StudentForReadDto>>();
        }

        public async Task<OneOf<StudentForReadDto, Response>> GetStudentById(Guid id)
        {
          var student = await _studentRepository.GetByIdAsync(id);
            if(student != null)
            {
                return student.Adapt<StudentForReadDto>();
            }
            return await _genericService.CreateResponse(ResponseMessages.Error, ResponseMessages.NotFound(ResponseMessages.notFound));
        }

        public async Task<OneOf<StudentForReadDto, Response>> UpdateStudent(Guid id, StudentForUpdateDto dto)
        {
            var student = await _studentRepository.Update(id, dto.Adapt<Student>());
            if (student != null)
            {
                return student.Adapt<StudentForReadDto>();
            }
            return await _genericService.CreateResponse(ResponseMessages.Error, ResponseMessages.NotFound(ResponseMessages.notFound));
        }
    }
}
