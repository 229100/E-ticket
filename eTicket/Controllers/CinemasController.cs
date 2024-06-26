﻿using eTicket.Data;
using eTicket.Data.Services.IServices;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;
        private readonly ILogger<CinemasController> _logger;

        public CinemasController(ICinemaService service, ILogger<CinemasController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("I am currently within the index action of the Cinemas Controller.");
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //Get: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(cinema);
                _logger.LogInformation("Место успешно создано");

                TempData["success"] = "Место успешно создано ";
                return RedirectToAction(nameof(Index));
            }

            TempData["warning"] = "Место не создано. Попробуйте снова ";
            return View(cinema);
        }

        //Get: Cinemas/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Информация не обновлена. Попробуйте снова ";
                return View(cinema);
            }

            await _service.UpdateAsync(id, cinema);
            _logger.LogInformation("Информация обновлена");

            TempData["success"] = "Информация обновлена  ";
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            _logger.LogInformation("Место удалено");

            TempData["warning"] = "Место удалено ";
            return RedirectToAction(nameof(Index));
        }
    }
}
