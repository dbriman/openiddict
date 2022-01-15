using AspNetCoreHero.Boilerplate.Client.Models;
using AspNetCoreHero.Boilerplate.Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            var url = _tokenService.GenerateURLForRequestToken();
            ViewBag.url = url;
            return View();
        }

        public async Task<IActionResult> SuccessAsync()
        {
            string auth_code;
            auth_code = Request.Query["code"].ToString();
            Token  token = await _tokenService.GetToken(auth_code.ToString());
            ViewBag.accessToken = token.AccessToken;
            ViewBag.idToken = token.IdToken;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
