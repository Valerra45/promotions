using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class EfRepository<T>
        : IEfRepository<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entitys)
        {
            _context.Set<T>().RemoveRange(entitys);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entity = await _context.Set<T>().ToListAsync();

            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<T?> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);

            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var entities = await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entitys)
        {
            _context.Set<T>().UpdateRange(entitys);

            await _context.SaveChangesAsync();
        }
    }
}
