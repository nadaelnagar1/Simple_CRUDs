namespace CRUD_BAL.Domains.Students.StudentServices
{
    public interface IStudentService
    {
        Task<OneOf<StudentForReadDto, Response>> GetStudentById(Guid id);
        Task<List<StudentForReadDto>> GetAllStudents();
        Task<StudentForReadDto> AddStudent(StudentForCreateDto dto);
        Task<OneOf<StudentForReadDto, Response>> UpdateStudent(Guid id, StudentForUpdateDto dto);
        Task<OneOf<StudentForReadDto, Response>> DeleteStudent(Guid id);
    }
}
