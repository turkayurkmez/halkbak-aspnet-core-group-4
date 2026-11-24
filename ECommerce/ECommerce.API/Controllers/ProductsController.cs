using ECommerce.API.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ShopOptions shopOptions;
        public ProductsController(IOptions<ShopOptions> options )
        {
            shopOptions = options.Value;
        }
        //action: Bir isteğin gideceği en son nokta (api için endpoint) - eğer controller kullanılıyorsa - o controller'ın action'udur.
        //bu action, 404, HTML, Json, File döndürebileceğine göre IActionResult bunların hepsini temsil ediyor demektir.
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {message ="Burada ürünler dönecek"});
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
                    
            return Ok(shopOptions);
        }
    }
}
