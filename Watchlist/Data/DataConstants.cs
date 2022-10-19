namespace Watchlist.Data
{
    public class DataConstants
    {
        public static class User
        {
            public const int MinUsernameLength = 5;
            public const int MaxUsernameLength = 20;

            public const int MinEmailLength = 10;
            public const int MaxEmailLength = 60;

            public const int MinPasswordLength = 5;
            public const int MaxPasswordLength = 20;
        }

        public static class Movie
        {
            public const int MinTitleLength = 10;
            public const int MaxTitleLength = 50;

            public const int MinDirectorLength = 5;
            public const int MaxDirectorLength = 50;

            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 5000;

        }

        public static class Genre
        {
            public const int MinGenreLength = 5;
            public const int MaxGenreLength = 50;
        }
    }
}
