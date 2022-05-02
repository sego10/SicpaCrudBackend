using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SicpaCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Aplicacion corriendo...";

        }
    }
}
