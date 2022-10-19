
using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Entities;
using Watchlist.Data.Models;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext context;

        public MovieService(WatchlistDbContext _context)
        {
            this.context = _context;    
        }


        public async Task AddMovie(MovieAddViewModel model)
        {
            var movie = new Movie()
            {
                Director = model.Director,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title,
            };

            await context.Movies.AddAsync(movie);

            await context.SaveChangesAsync();
        }

        public async Task AddMovieToCollectionAsync(int movieId, string userId)
        {
            var movie = await context.Movies
                .Where(m => m.Id == movieId)
                .FirstOrDefaultAsync();

            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID");
            }

            if(!user.UsersMovies.Any(m=> m.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    UserId = user.Id,
                    MovieId = movie.Id,
                });
            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllAsync()
        {
            var movies = await context.Movies
                                        .Include(m => m.Genre)
                                        .ToListAsync();

            return movies
                    .Select(m => new MovieViewModel()
                    {
                        Director = m.Director,
                        Genre = m.Genre.Name,
                        Id = m.Id,
                        ImageUrl = m.ImageUrl,
                        Rating = m.Rating,
                        Title = m.Title,
                    });
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await context.Genres.ToListAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
        {
            var user = await context.Users
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.UsersMovies)
                                    .ThenInclude(um => um.Movie)
                                    .ThenInclude(m => m.Genre)
                                    .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UsersMovies
                        .Select(m => new MovieViewModel()
                        {
                            Director = m.Movie.Director,
                            Genre = m.Movie.Genre?.Name,
                            Id = m.MovieId,
                            ImageUrl = m.Movie.ImageUrl,
                            Title = m.Movie.Title,
                            Rating = m.Movie.Rating,
                        });
        }

        public async Task RemoveFromCollectionAsync(int movieId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

            if (user == null)
            {
                throw new ArgumentException("Invalid User ID");
            }

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }
            else
            {
                user.UsersMovies.Remove(movie);
            }

            await context.SaveChangesAsync();
        }
    }
}
