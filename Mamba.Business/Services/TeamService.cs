using Mamba.Business.Helpers;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
namespace Mamba.Business.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository teamrepo;

        public TeamService(ITeamRepository teamrepo)
        {
            this.teamrepo = teamrepo;
        }
        public async Task CreateAsync(Team team)
        {
            if (team.Img != null)
            {

                if (team.Img.ContentType != "image/png" && (team.Img.ContentType != "image/jpeg"))
                {
                    throw new Exception();

                }

                if (team.Img.Length > 1048576)
                {
                    throw new Exception();

                }
                string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string newFileName = Helper.GetFileName(path, "upload", team.Img);

                team.Imageurl = newFileName;

               
            };

            if (teamrepo.Table.Any(x => x.FullName == team.FullName))
                throw new Exception();
            await teamrepo.CreateAsync(team);
            await teamrepo.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await teamrepo.GetByIdAsync(x => x.Id == id && x.Isdeleted == false);
            if (entity == null)   throw new Exception();

            teamrepo.DeleteAsync(entity);
            await teamrepo.CommitAsync();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await teamrepo.GetAllAsync();
        }

        public async Task<Team> GetAsync(int id)
        {
            return await teamrepo.GetByIdAsync(x => x.Id == id && x.Isdeleted == false);
        }

        public async Task UpdateAsync(Team team)
        {
            var team1 = await teamrepo.GetByIdAsync(x => x.Id == team.Id && x.Isdeleted == false);
            if (team1 is null) throw new NullReferenceException();

            if (teamrepo.Table.Any(x => x.FullName == team.FullName && team.Id != team.Id))
                throw new Exception();
            if (team.Img != null)
            {

                if (team.Img.ContentType != "image/png" && (team.Img.ContentType != "image/jpeg"))
                {
                    throw new Exception();

                }

                if (team.Img.Length > 1048576)
                {
                    throw new Exception();

                }
                string path = "C:\\Users\\hesen\\OneDrive\\İş masası\\pustokclas\\WebApplication6\\wwwroot\\";
                string newFileName = Helper.GetFileName(path, "upload", team.Img);

                team.Imageurl = newFileName;


            };

            team1.FullName = team.FullName;
            team1.Imageurl=team.Imageurl;
            team.Img=team.Img;
            team1.TwiterReDir = team.TwiterReDir;
            team1.FaceBookReDir = team.FaceBookReDir;
            team1.LinkedinReDir=team.LinkedinReDir;
            team1.InstagramReDir=team.InstagramReDir;           
           await teamrepo.CommitAsync();
        }
    }
}
