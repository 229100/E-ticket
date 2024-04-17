using eTicket.Data;
using eTicket.Data.Services.IServices;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        private readonly ILogger<ProducersController> _logger;

        public ProducersController(IProducersService service, ILogger<ProducersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            _logger.LogInformation("I am currently within the Index action of the Producers Controller.");
            return View(allProducers);
        }

        //Get: Producers/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        //Get: Producer/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL, FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Организатор не добавлен");
                TempData["warning"] = "Организатор не добавлен. Попробуйте снова ";
                return View(producer);
            }
            _logger.LogInformation("Организатор успешно добавлен");

            await _service.AddAsync(producer);
            TempData["success"] = "Организатор успешно добавлен  ";
            return RedirectToAction(nameof(Index));
        }


        //Get: Producer/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL, FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Информация не обновлена ";
                return View(producer);
            }

            if (id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                _logger.LogInformation("Информация обновлена");

                TempData["success"] = "Информация обновлена ";
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        //Get: Producer/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            _logger.LogInformation("Организатор удален");

            TempData["warning"] = "Организатор удален ";
            return RedirectToAction(nameof(Index));
        }

    }
}
