using CRUD_DAL.Database.ApplicationDbContext;
using CRUD_DAL.Repositories.GenericRepository;

namespace CRUD_DAL.Repositories.Non_GenericRepository.StudentRepository
{
    public class StudentRepository: BaseRepository<Student> , IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        
    }
}
