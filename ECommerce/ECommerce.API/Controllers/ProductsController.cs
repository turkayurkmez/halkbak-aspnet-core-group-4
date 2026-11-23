using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        //action: Bir isteğin gideceği en son nokta (api için endpoint) - eğer controller kullanılıyorsa - o controller'ın action'udur.
        //bu action, 404, HTML, Json, File döndürebileceğine göre IActionResult bunların hepsini temsil ediyor demektir.
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {message ="Burada ürünler dönecek"});
        }
    }
}
