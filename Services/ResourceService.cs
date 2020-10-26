using System.Threading.Tasks;
using CinefiloWASM.Consts;

namespace CinefiloWASM.Services{

internal class ResourceService : ServiceBase, IService
{
    public ResourceService()
        : base(MoviesApiResourcesConsts.RESOURCE, MoviesApiResourcesConsts.APIKEY)
    {
    }
    public async Task<TResult> Consume<TData, TResult>(TData data, string resource)
    {
        EndPoint = string.Format("{0}/{1}?api_key={2}&language=en-US", EndPointDomain, resource, EndPointAPIKey);
        return await Get<TData, TResult>(data);
    }
}
}