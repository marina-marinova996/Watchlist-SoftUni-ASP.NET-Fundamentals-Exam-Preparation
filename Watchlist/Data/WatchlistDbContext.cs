using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data.Entities;
using Watchlist.Data.Models;
using static Watchlist.Data.DataConstants.User;

namespace Watchlist.Data
{
    public class WatchlistDbContext : IdentityDbContext<User>
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserMovie>()
                    .HasKey(x => new { x.UserId, x.MovieId });

            builder.Entity<User>()
                    .Property(u => u.UserName)
                    .HasMaxLength(MaxUsernameLength);

            builder.Entity<User>()
                    .Property(u => u.Email)
                    .HasMaxLength(MaxEmailLength);

            builder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Adventure"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romantic"
                });

             builder
            .Entity<Movie>()
            .HasData(new Movie()
            {
                Id = 1,
                Title = "Harry Potter and the Philosopher's Stone",
                Director = "Chris Columbus",
                ImageUrl = "https://media.harrypotterfanzone.com/philosophers-stone-theatrical-poster.jpg",
                Rating = 7.60m,
                GenreId = 3,
            },
            new Movie()
            {
                Id = 2,
                Title = "The Hobbit: The Battle of the Five Armies",
                Director = "Peter Jackson",
                ImageUrl = "https://i1.wp.com/www.my-sf.com/wp-content/uploads/2015/01/The-Hobbit-The-Battle-of-the-Five-Armies-theatrical-teaser-poster.jpg",
                Rating = 7.40m,
                GenreId = 3,
            });


            base.OnModelCreating(builder);
        }
    }
}