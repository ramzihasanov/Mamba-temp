using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Mamba.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repositories.Implementation
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity>
      where Tentity : BaseIdentity, new()
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<Tentity> Table => _context.Set<Tentity>();

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(Tentity entity)
        {
            await Table.AddAsync(entity);
        }

        public void DeleteAsync(Tentity entity)
        {
            Table.Remove(entity);
        }

        public async Task<List<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                        ? await query.Where(expression).ToListAsync()
                        : await query.ToListAsync();
        }

        public async Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                    ? await query.Where(expression).FirstOrDefaultAsync()
                    : await query.FirstOrDefaultAsync();
        }

        public Task UpdateAsync(Tentity tentity)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Tentity> GetQuery(string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }
    }
}
