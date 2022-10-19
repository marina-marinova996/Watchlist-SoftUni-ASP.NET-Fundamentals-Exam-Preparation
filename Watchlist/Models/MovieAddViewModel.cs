using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Entities;
using static Watchlist.Data.DataConstants.Movie;

namespace Watchlist.Models
{
    public class MovieAddViewModel
    {
        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(MaxDirectorLength, MinimumLength = MinDirectorLength)]
        public string Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Precision(18, 2)]
        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
