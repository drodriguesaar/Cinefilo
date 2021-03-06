namespace CinefiloWASM.Consts
{
    public static class MoviesApiResourcesConsts
    {
        public const string RESOURCE = "https://api.themoviedb.org/3";
        public const string APIKEY = "20e51c2f0cf89c0daebe84193d4383e7";
        public const string GENRES = "genre/movie/list";

        public const string MOVIE = "movie/{0}";
        public const string MOVIE_IMAGES = "movie/{0}/images";
        public const string NOW_PLAYING = "movie/now_playing";
        public const string UPCOMING_MOVIES = "movie/upcoming";

        public const string ACTOR = "person/{0}";
        public const string POPULAR_ACTORS = "person/popular";

        public const string SEARCH_MOVIE = "search/movie";
        public const string SEARCH_PERSON = "search/person";
    }
}