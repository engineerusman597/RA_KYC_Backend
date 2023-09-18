using Infrastructure.Content.Data;
using RA_KYC_BE.Application.Interfaces.Repositories;
using System.Linq.Expressions;

namespace RA_KYC_BE.Infrastructure.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext _context { get; set; }
        public GenericRepository(AppDbContext repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public async Task AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<T> GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
