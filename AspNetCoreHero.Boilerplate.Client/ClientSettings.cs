using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Client
{
    public class ClientSettings
    {
        public string AuthorityUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool Scopes { get; set; }

        public string GrantType { get; set; }

        public bool UsePkce { get; set; }
        public bool SaveTokens { get; set; }

        public string ResponseType { get; set; }
        public string ResponseMode { get; set; }
        public string RedirectUri { get; set; }
    }
}
