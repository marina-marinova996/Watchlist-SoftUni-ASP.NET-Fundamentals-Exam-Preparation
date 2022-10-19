using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Models;
using static Watchlist.Data.DataConstants.User;

namespace Watchlist.Data.Entities
{
    public class User : IdentityUser
    {
        public List<UserMovie> UsersMovies { get; set; }
    }
}
