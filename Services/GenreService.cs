using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinefiloWASM.Consts;
using CinefiloWASM.DTO;
using CinefiloWASM.Models;
using CinefiloWASM.Parameter;
using CinefiloWASM.Singleton;

namespace CinefiloWASM.Services
{
    public class GenreService
    {
        internal async Task<List<GenreModel>> GetGenres(MovieParameter movieParameter)
        {
            var genresList = new List<GenreModel>();
            try
            {
                var resource = MoviesApiResourcesConsts.GENRES;
                var response = await Global
                                        .Instance
                                        .BaseService
                                        .Consume<MovieParameter, ResponseListDTO<List<GenreDTO>>>
                                        (movieParameter, resource);

                genresList = response.genres.Select(genre => new GenreModel
                {
                    ID = genre.id,
                    Name = genre.name
                }).ToList();
            }
            catch
            {
            }
            return genresList;
        }
    }
}