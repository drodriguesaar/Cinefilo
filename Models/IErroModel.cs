namespace CinefiloWASM.Models
{
    public interface IErroModel
    {
        bool Error { get; set; }
        string ErrorMessage { get; set; }
    }
}