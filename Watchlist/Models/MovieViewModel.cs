using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;
using static Watchlist.Data.DataConstants.Movie;
using Watchlist.Data.Entities;

namespace Watchlist.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }

        public string ImageUrl { get; set; }

        public decimal Rating { get; set; }

        public string Genre { get; set; }

    }
}
