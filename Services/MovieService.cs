using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinefiloWASM.Consts;
using CinefiloWASM.DTO;
using CinefiloWASM.Models;
using CinefiloWASM.Parameter;
using CinefiloWASM.Singleton;
using CinefiloWASM.Utils;

namespace CinefiloWASM.Services
{
    public class MovieService
    {
        internal async Task<List<MovieModel>> GetMovies(MovieParameter movieParameter)
        {
            var moviesList = new List<MovieModel>();

            var resource = movieParameter.Resource;
            var response = await Global.Instance.BaseService.Consume<MovieParameter, ResponseListDTO<List<MovieDTO>>>(movieParameter, resource);
            moviesList = response.results.Select(movie => new MovieModel
            {
                ReleaseDate = string.IsNullOrEmpty(movie.release_date) ? "not available" : DateTime.Parse(movie.release_date).ToShortDateString(),
                Name = movie.title,
                OverView = BuildAbreviatedMovieOverView(movie.overview),
                Poster = movie.poster_path.BuildImageURI(),
                Score = movie.vote_average.ToString(),
                Votes = movie.vote_count.ToString(),
                Id = movie.id
            }).ToList();
            return moviesList;
        }

        internal async Task<MovieModel> GetMovie(MovieParameter movieParameter)
        {
            MovieModel movieModel = new MovieModel();

            var movieresource = string.Format(MoviesApiResourcesConsts.MOVIE, movieParameter.Id);
            var movieID = movieParameter.Id;
            movieParameter.Id = null;
            var response = await Global.Instance.BaseService.Consume<MovieParameter, MovieDTO>(movieParameter, movieresource);

            var releaseDate = new DateTime();
            DateTime.TryParse(response.release_date, out releaseDate);
            var releaseDateFormatted = releaseDate == DateTime.MinValue ? string.Empty : releaseDate.ToShortDateString();

            movieModel.ReleaseDate = releaseDateFormatted;
            movieModel.Name = response.title;
            movieModel.OverView = response.overview;

            movieModel.Genres = response.genres.Select(g => new GenreModel { ID = g.id, Name = g.name }).ToList();

            movieModel.Poster = response.poster_path.BuildImageURI(size: "500");
            movieModel.Id = response.id;
            movieModel.Score = response.vote_average.ToString();
            movieModel.Votes = response.vote_count.ToString();
            movieModel.Language = response.original_language;

            movieModel.HomePage = string.IsNullOrEmpty(response.homepage) ? "Not available" : response.homepage;

            movieModel.ProducedBy = string.Join(", ", response.production_companies.Select(r => r.name));
            movieModel.ProducedIn = string.Join(", ", response.production_countries.Select(r => r.name));

            movieModel.Adult = response.adult;
            movieModel.BackDropPath = response.backdrop_path.BuildImageURI(size: "500");

            movieresource = string.Format(MoviesApiResourcesConsts.MOVIE_IMAGES, movieID);
            var imagesresponse = await Global.Instance.BaseService.Consume<MovieParameter, MovieDTO>(movieParameter, movieresource);
            if (imagesresponse.posters != null && imagesresponse.posters.Any())
            {
                movieModel.Images.AddRange(imagesresponse.posters.Select(p => new ImageModel
                {
                    Height = p.height,
                    Width = p.width,
                    Path = p.file_path.BuildImageURI()
                }));
            }
            return movieModel;
        }
        string BuildAbreviatedMovieOverView(string movieDescription)
        {
            if (string.IsNullOrEmpty(movieDescription))
            {
                return string.Empty;
            }

            var words = movieDescription.Split(' ').Take(15);
            var abreviatedOverView = string.Join(" ", words);
            abreviatedOverView = string.Concat(abreviatedOverView, "...");

            return abreviatedOverView;
        }
    }
}