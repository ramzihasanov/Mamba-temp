using Mamba.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Interfaces
{
    public interface IPosittionService
    {
        Task CreateAsync(Posittion team);
        Task DeleteAsync(int id);
        Task<List<Posittion>> GetAllAsync();

        Task<Posittion> GetAsync(int id);
        Task UpdateAsync(Posittion po);
    }
}
