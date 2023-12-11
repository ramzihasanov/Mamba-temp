using Humanizer.Localisation;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mambaa.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PosittionController : Controller
    {
        private readonly IPosittionRepository posittionRepository;

        public PosittionController(IPosittionRepository posittionRepository)
        {
            this.posittionRepository = posittionRepository;
        }
        public async Task<IActionResult> Index()
        {
            var pos = await posittionRepository.GetAllAsync();
            return View(pos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Posittion pos)
        {
            if (!ModelState.IsValid) return View(pos);


            await posittionRepository.CreateAsync(pos);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Posittion wanted = await posittionRepository.GetByIdAsync(x=>x.Id==id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Posittion pos)
        {
            if (!ModelState.IsValid) return View();

            await posittionRepository.UpdateAsync(pos);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Posittion pos)
        {
            if (pos.Id == null) return NotFound();

             posittionRepository.DeleteAsync(pos);

            return Ok();
        }



    }
}
