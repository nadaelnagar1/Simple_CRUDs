using CRUD_BAL.Domains.Students.Validators;
using FluentValidation;

namespace CRUD_BAL.Domains.Students.StudentServices
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGenericService _genericService;
        private readonly ApplicationDbContext _context;
        private readonly StudentForCreateDtoValidator _createValidator;
        private readonly StudentForUpdateDtoValidator _updateValidator;

        public StudentService(IStudentRepository studentRepository, IGenericService genericService, ApplicationDbContext context, StudentForCreateDtoValidator createValidator, StudentForUpdateDtoValidator updateValidator)
        {
            _studentRepository = studentRepository;
            _genericService = genericService;
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<OneOf<StudentForReadDto,Response>> AddStudent(StudentForCreateDto dto)
        {
            var validationResult = _createValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                string concatenatedErrors = string.Join("; ", errors);
                return await _genericService.CreateResponse(ResponseMessages.ValidationError, concatenatedErrors);
            }

            var adaptedStudent =dto.Adapt<Student>();
            var createdStudent = await _studentRepository.AddAsync(adaptedStudent);
            if(createdStudent == null)
            {
                return await _genericService.CreateResponse(ResponseMessages.Error, ResponseMessages.StudentCreationFailed);
            }
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
            return await _genericService.CreateResponse(ResponseMessages.Error, ResponseMessages.NotFound(ResponseMessages.Student));
        }

        public async Task<OneOf<StudentForReadDto, Response>> UpdateStudent(Guid id, StudentForUpdateDto dto)
        {
            var validationResult = _updateValidator.Validate(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                string concatenatedErrors = string.Join("; ", errors);
                return await _genericService.CreateResponse(ResponseMessages.ValidationError, concatenatedErrors);
            }

            var student = await _studentRepository.GetByIdAsync(id);
            if (student != null)
            {
                _context.Entry(student).State = EntityState.Detached;
                var adaptedStudent = dto.Adapt<Student>();
                adaptedStudent.Id = id;
                adaptedStudent.UpdatedAt = DateTime.UtcNow;
                var updatedStudent = await _studentRepository.Update(adaptedStudent);
                if (updatedStudent != null)
                {
                    updatedStudent.BirthDate = student.BirthDate;
                    updatedStudent.Gender = student.Gender;

                    return updatedStudent.Adapt<StudentForReadDto>();
                }
            }
            return await _genericService.CreateResponse(ResponseMessages.Error, ResponseMessages.NotFound(ResponseMessages.Student));
        }
    }
}
