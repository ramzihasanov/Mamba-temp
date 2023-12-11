using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mambaa.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class TeamContoller : Controller
    {
        private readonly ITeamRepository teamRepo;
        private readonly IPosittionRepository posittionRepository;

        public TeamContoller(ITeamRepository teamRepo,IPosittionRepository posittionRepository)
        {
            this.teamRepo = teamRepo;
            this.posittionRepository = posittionRepository;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Posittion = await posittionRepository.GetAllAsync();
            var team = await teamRepo.GetAllAsync();
            return View(team);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Posittion = await posittionRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            ViewBag.Posittion = await posittionRepository.GetAllAsync();
            if (!ModelState.IsValid) return View(team);
            await teamRepo.CreateAsync(team);
            await teamRepo.CommitAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Posittion = await posittionRepository.GetAllAsync();
            if (!ModelState.IsValid) return View();
            var existBook = await teamRepo.GetByIdAsync(x => x.Id == id);
            return View(existBook);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            ViewBag.Posittion = await posittionRepository.GetAllAsync();
            if (!ModelState.IsValid) return View(team);
            await teamRepo.UpdateAsync(team);
            await teamRepo.CommitAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public  IActionResult Delete(Team team)
        {
            
            if (team.Id == null) return NotFound();


             teamRepo.DeleteAsync(team);


            return Ok();
        }
    }
    
}
