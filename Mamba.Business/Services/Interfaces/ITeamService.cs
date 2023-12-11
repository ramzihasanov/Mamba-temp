using Mamba.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Interfaces
{
    public interface ITeamService
    {
        Task CreateAsync(Team team);
        Task DeleteAsync(int id);
        Task<List<Team>> GetAllAsync();

        Task<Team> GetAsync(int id);
        Task UpdateAsync(Team team);
    }
}
