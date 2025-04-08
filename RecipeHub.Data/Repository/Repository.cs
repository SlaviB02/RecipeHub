using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Repository.Interfaces;

namespace RecipeHub.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RecipeHubDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(RecipeHubDbContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }


        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }



        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteByItemAsync(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {

#pragma warning disable CS8603 // Possible null reference return.
            return await this.dbSet
                .FirstOrDefaultAsync(predicate);
#pragma warning restore CS8603 // Possible null reference return.



        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IQueryable<T> GetAllAttached()
        {
            return dbSet;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await dbSet.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }



        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
 }
