using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services
{
    public class PosittionService : IPosittionService
    {
        private readonly IPosittionRepository posittionRepository;

        public PosittionService(IPosittionRepository posittionRepository)
        {
            this.posittionRepository = posittionRepository;
        }
        public async Task CreateAsync(Posittion team)
        {

            if (posittionRepository.Table.Any(x => x.Name == team.Name))
                throw new Exception();
            await posittionRepository.CreateAsync(team);
            await posittionRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await posittionRepository.GetByIdAsync(x => x.Id == id && x.Isdeleted == false);
            if (entity == null) throw new Exception();

            posittionRepository.DeleteAsync(entity);
            await posittionRepository.CommitAsync();
        }

        public async Task<List<Posittion>> GetAllAsync()
        {
            return await posittionRepository.GetAllAsync();
        }

        public async Task<Posittion> GetAsync(int id)
        {
            return await posittionRepository.GetByIdAsync(x => x.Id == id && x.Isdeleted == false);
        }

        public async Task UpdateAsync(Posittion po)
        {
            var team1 = await posittionRepository.GetByIdAsync(x => x.Id == po.Id && x.Isdeleted == false);
            if (team1 is null) throw new NullReferenceException();

            if (posittionRepository.Table.Any(x => x.Name == po.Name && po.Id != po.Id))
                throw new Exception();

            team1.Name = po.Name;
            await posittionRepository.CommitAsync();
        }
    }
}
