﻿using eTicket.Data;
using eTicket.Data.Services.IServices;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMoviesService service, ILogger<MoviesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            _logger.LogInformation("I am currently within the index action of the Movies Controller.");
            return View(allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResult = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                if (filteredResult.Count == 0)
                {
                    //Redirect to Coustom 404 page for filtering

                    //return RedirectToAction("FilterNotFound");

                    _logger.LogInformation("Filter or Searching Not Found.");
                    return View("FilterNotFound");
                    //return View("NotFound");

                }
                _logger.LogInformation("Searching Movie.");

                return View("Index", filteredResult);
            }
            return View("Index", allMovies);
        }

        //GET: Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Мероприятие не добавлено");

                TempData["warning"] = "Мероприятие не добавлено. Попробуйте снова ";
                var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            else
            {
                await _service.AddNewMovieAsync(movie);
                _logger.LogInformation("Мероприятие успешно добавлено");

                TempData["success"] = "Мероприятие успешно добавлено ";
                return RedirectToAction(nameof(Index));
            }
        }


        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Информация не обновлена");

                TempData["warning"] = "Информация не обновлена. Попробуйте снова  ";
                var movieDropdownsData = await _service.GetNewMovieDropdownsValuesAsync();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }
            else
            {
                await _service.UpdateMovieAsync(movie);
                _logger.LogInformation("Информация обновлена");

                TempData["success"] = "Информация обновлена ";
                return RedirectToAction(nameof(Index));
            }
        }


        //Get: Movies/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var MoviesDetails = await _service.GetMovieByIdAsync(id);
            if (MoviesDetails == null) return View("NotFound");
            return View(MoviesDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var MoviesDetails = await _service.GetMovieByIdAsync(id);
            if (MoviesDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            _logger.LogInformation("Мероприятие удалено");

            TempData["warning"] = "Мероприятие удалено ";
            return RedirectToAction(nameof(Index));
        }
    }
}
