
namespace CinefiloWASM.DTO
{
    internal class ResponseListDTO<T>
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public T results { get; set; }
        public T genres { get; set; }
    }
}