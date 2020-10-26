using System.Collections.Generic;

namespace CinefiloWASM.DTO
{
    public class MovieDTO
    {
        public MovieDTO()
        {
            production_companies = new List<CompanyDTO>();
            production_countries = new List<CountryDTO>();
            genres = new List<GenreDTO>();
            backdrops = new List<ImageDTO>();
            posters = new List<ImageDTO>();
        }

        public int id { get; set; }
        
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string homepage { get; set; }
        
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }
        
        public string original_language { get; set; }

        public bool adult { get; set; }

        public string backdrop_path { get; set; }

        public List<CompanyDTO> production_companies { get; set; }
        public List<CountryDTO> production_countries { get; set; }
        public List<GenreDTO> genres { get; set; }
        public List<ImageDTO> backdrops { get; set; }
        public List<ImageDTO> posters { get; set; }
    }
}