using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Contracts;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService _movieService)
        {
            this.movieService = _movieService;
        }
        public async Task<IActionResult> All()
        {
            var movies = await movieService.GetAllAsync();


            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new MovieAddViewModel();
            model.Genres = await movieService.GetGenresAsync();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(MovieAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await movieService.AddMovie(model);
                return RedirectToAction("All", "Movies");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await movieService.AddMovieToCollectionAsync(movieId, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Watched()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = await movieService.GetWatchedAsync(userId);

            return View("Mine", model);

        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int movieId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await movieService.RemoveFromCollectionAsync(movieId, userId);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Watched));
        }

    }
}
