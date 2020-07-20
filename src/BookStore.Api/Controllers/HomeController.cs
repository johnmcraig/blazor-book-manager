using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    public class HomeController : BaseApiController
    {
        public IActionResult Index()
        {
            return Ok("Hello World");
        }
    }
}