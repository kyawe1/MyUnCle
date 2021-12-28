using UncleApp.Models.ViewModel;

namespace UncleApp.Models
{
    public interface IJsonHandler
    {
        Task<string> GenerateJsonTokenAsync(LoginViewModel login);
    }
}
