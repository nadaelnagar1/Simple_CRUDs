namespace CRUD_DAL.Repositories.GenericRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<TEntity> Delete(Guid id)
        {
            var entity =await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
               await SaveAsync();
                return entity;
            }
            return null;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();

        }

        public async Task<TEntity> Update(TEntity entity)
        {
            // _context.Entry(entity).State = EntityState.Modified;
            //await SaveAsync();
            //return entity;

           
              // Detach the existing entity
            

            _context.Entry(entity).State = EntityState.Modified; // Attach the modified entity
            await SaveAsync();
            return entity;
            //_context.Set<TEntity>().Update(entity);
            //await SaveAsync();
        }
    }
}
