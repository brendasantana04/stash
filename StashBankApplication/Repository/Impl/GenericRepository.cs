
using Microsoft.EntityFrameworkCore;
using StashBankApplication.Model;
using StashBankApplication.Model.Base;
using StashBankApplication.Model.Context;
using System.Linq.Expressions;

namespace StashBankApplication.Repository.Impl
{
    public class GenericRepository <T> : IRepository<T> where T : BaseEntity
    {
        private MSSQLContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MSSQLContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public T Create(T item)
        {
            _context.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Delete(T item)
        {
            var existingItem = _dataset.Find(item.id);
            if (existingItem == null) return;
            _context.Remove(existingItem);
            _context.SaveChanges();
        }

        public List<T> FindAll()
        {
            return _dataset.ToList();
        }

        public T FindById(long id)
        {
            return _dataset.Find(id);
        }

        public T Update(T item)
        {
            var existingItem = _dataset.Find(item.id);
            if (existingItem == null) return null;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return item;
        }

        public async Task AddAsync(T entity)
        {
            await _dataset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dataset.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dataset.FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dataset;

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(e => e.id == id);
        }
    }
}