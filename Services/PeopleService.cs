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

    public class PeopleService
    {
        internal async Task<List<ActorModel>> GetActors(MovieParameter movieParameter)
        {
            var actorList = new List<ActorModel>();

            var resource = movieParameter.Resource;
            var response = await Global.Instance.BaseService.Consume<MovieParameter, ResponseListDTO<List<PeopleDTO>>>(movieParameter, resource);
            actorList = response.results.Select(people => new ActorModel
            {
                Name = people.name,
                Photo = people.profile_path.BuildImageURI(),
                ID = people.id,
                Popularity = string.Format("Popularity: {0}", people.popularity),
                Adult = string.Format("Adult: {0}", (people.adult ? "Yes" : "No")),
                DOB = people.birthday,
                Bio = people.biography,
                POB = people.place_of_birth,
                Gender = people.gender.ToString()
            }).ToList();

            return actorList;
        }
        internal async Task<ActorModel> GetActor(MovieParameter movieParameter)
        {
            var actorModel = new ActorModel();

            var resource = string.Format(MoviesApiResourcesConsts.ACTOR, movieParameter.Id);
            var response = await Global.Instance.BaseService.Consume<MovieParameter, PersonDTO>(movieParameter, resource);

            actorModel.DOB = string.Format("Birthday: {0}", response.birthday);
            actorModel.DOD = string.IsNullOrEmpty(response.deathday) ? "Active" : string.Format("Death day: {0}", response.deathday);
            actorModel.ID = response.id;

            actorModel.Name = response.name;
            actorModel.Photo = response.profile_path.BuildImageURI();
            actorModel.Bio = response.biography;
            actorModel.Department = string.Format("Known for: {0}", response.known_for_department);
            actorModel.POB = string.Format("Place of birth: {0}", response.place_of_birth);
            actorModel.Adult = response.adult ? "Yes" : "No";
            actorModel.Gender = string.Format("Gender: {0}", (response.gender.Equals(1) ? "Female" : "Male"));
            actorModel.Popularity = string.Format("Popularity: {0}", response.popularity);
            actorModel.HomePage = response.homepage;


            return actorModel;
        }
    }
}