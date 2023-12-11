using Mamba.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Repositories.Interfaces
{
    public interface IGenericRepository<Tentity>where Tentity : BaseIdentity,new()
    {
        public DbSet<Tentity> Table { get; }

        Task CreateAsync(Tentity entity);
        void DeleteAsync(Tentity entity);
        Task<Tentity> GetByIdAsync(Expression<Func<Tentity, bool>>? expression = null, params string[]? includes);
        Task<List<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>>? expression = null, params string[]? includes);
        Task UpdateAsync(Tentity tentity);

        Task<int> CommitAsync();
    }
}
