using System.Threading.Tasks;

namespace CinefiloWASM.Services
{
    public interface IService
    {
        Task<TResult> Consume<TData, TResult>(TData data, string resource);
    }

}