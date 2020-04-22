
using MyLeasing.Common.Models;
using System.Threading.Tasks;

namespace MyLeasing.Common.Services
{
    public interface IApiService
    {
        Task<Response<OwnerResponse>> GetOwnerByEmail(string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);
        //El metodo de abajo por como esta disenada la clase Response permite directamente devolver un TokenResponse cuando se usa el .Result
        Task<Response<TokenResponse>> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
        Task<bool> CheckConnection(string url);

    }
}