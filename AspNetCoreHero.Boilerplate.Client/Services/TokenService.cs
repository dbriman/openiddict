using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreHero.Boilerplate.Client.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly IOptions<ClientSettings> _clientSettings;

        public TokenService(ILogger<TokenService> logger, IOptions<ClientSettings> clientSettings)
        {
            _logger = logger;
            _clientSettings = clientSettings;

            using var httpClient = new HttpClient();
            _discoveryDocument = httpClient.GetDiscoveryDocumentAsync(clientSettings.Value.AuthorityUrl).Result;
            if (_discoveryDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is: {_discoveryDocument.Error}");
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
            }

        }
        public string GenerateURLForRequestToken()
        {
            //  "https://localhost:44312/connect/authorize?client_id=postman&redirect_uri=https://localhost:44311/home/privacy&scope=openid&response_type=code&response_mode=form_post&nonce=2lw2jd6p7q4"

            string url = string.Format(_discoveryDocument.AuthorizeEndpoint + "?client_id={0}&redirect_uri={1}&scop={2}&response_type={3}&response_mode={4}&nonce={5}",
                                        _clientSettings.Value.ClientId, _clientSettings.Value.RedirectUri,
                                        _clientSettings.Value.Scopes, _clientSettings.Value.ResponseType, _clientSettings.Value.ResponseMode, "2lw2jd6p7q4");
            return url;
        }

        public async Task<Token> GetToken(string authorization_code)
        {
            using var client = new HttpClient();
            var accessTokenResponse = await client.RequestTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = _discoveryDocument.TokenEndpoint,
                GrantType = _clientSettings.Value.GrantType,
                ClientId = _clientSettings.Value.ClientId,
                //ClientSecret = _clientSettings.Value.ClientSecret,

                Parameters =
                {
                    {"redirect_uri" , _clientSettings.Value.RedirectUri},
                    {"code",authorization_code },
                    {"response_type", _clientSettings.Value.ResponseType  }

                }
            });

            if (accessTokenResponse.IsError)
            {
                _logger.LogError($"Unable to get token. Error is: {accessTokenResponse.Error}");
                throw new Exception("Unable to get token", accessTokenResponse.Exception);
            }

            return new Token
            {
                AccessToken = accessTokenResponse.AccessToken,
                IdToken = accessTokenResponse.IdentityToken
            };
        }
    }

    public class Token
    {

        public string AccessToken { get; set; }
        public string IdToken { get; set; }

    }
}