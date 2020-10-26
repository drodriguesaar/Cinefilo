using System.Collections.Generic;

namespace CinefiloWASM.DTO
{
    public class PeopleDTO
    {
        public int id { get; set; }
        public bool adult { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }
        public string biography { get; set; }
        public string place_of_birth { get; set; }
        public string known_for_department { get; set; }
        public string birthday { get; set; }
        public int gender { get; set; }
        public decimal popularity { get; set; }
        public List<KnownForDTO> known_for { get; set; }
    }
}