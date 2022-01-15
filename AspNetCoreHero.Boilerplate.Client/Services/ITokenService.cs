using System.Threading.Tasks;
using IdentityModel.Client;

namespace AspNetCoreHero.Boilerplate.Client.Services
{
  public interface ITokenService
  {
    Task<Token> GetToken(string authcode);
    string GenerateURLForRequestToken();
    }
}