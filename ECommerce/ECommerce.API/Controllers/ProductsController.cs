using ECommerce.API.Options;
using ECommerce.Application;
using ECommerce.Application.Services;
using ECommerce.Domain;
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

        private readonly IProductService productService;
        public ProductsController(IOptions<ShopOptions> options, IProductService productService )
        {
            shopOptions = options.Value;
            this.productService = productService;
        }
        //action: Bir isteğin gideceği en son nokta (api için endpoint) - eğer controller kullanılıyorsa - o controller'ın action'udur.
        //bu action, 404, HTML, Json, File döndürebileceğine göre IActionResult bunların hepsini temsil ediyor demektir.
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //controller, productService'in ne yaptığını biliyor mu?
            var products = await productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(Product product)
        {
            var created = await productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById),new {id=created.Id}, created);

        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
                    
            return Ok(shopOptions);
        }
    }
}
