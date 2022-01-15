using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreHero.Boilerplate.Web.ViewModels.Authorization

{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}