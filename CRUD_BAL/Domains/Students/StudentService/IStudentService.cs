namespace CRUD_BAL.Domains.Students.StudentService
{
    internal interface IStudentService
    {
       Task<StudentForReadDto> GetStudentById(int id);
        Task<List<StudentForReadDto>> GetAllStudents();
        Task<StudentForReadDto> AddStudent(StudentForCreateDto dto);
        Task<StudentForReadDto> UpdateStudent(Guid id, StudentForUpdateDto dto);
        Task<StudentForReadDto> DeleteStudent(Guid id);
    }
}
