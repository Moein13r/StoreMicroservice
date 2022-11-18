using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.AsyncDataService;
using Products.Data;
using Products.DTOs;
using Products.Models;
using Products.Repositories.ProductRepository;

namespace Products.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMessageBusClient _messageBusClient;
        private readonly IMapper _mapper;
        //private readonly AppDbContext _context;
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repo, ILogger<ProductsController> logger, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient;
            _mapper = mapper;
            _logger = logger;
            _repo = repo;
        }        
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProducts(ProductCreate product)
        {                   
            if (product==null)
            {
                return BadRequest("product Is Not valid Please use Valid product Or Try Again!");
            }
            try
            {
                var item=_mapper.Map<ProductPublishedDto>(product);
                item.Event="Product_Published";
                _messageBusClient.PublishNewProduct(item);
            }
            catch (System.Exception e)
            {
                
                Console.WriteLine("--> Colud not Asyncronisly");
            }
            return Ok();                          
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> ConnectSql(string ConnStr)
        {
            try
            {
                var eonnection=new Microsoft.Data.SqlClient.SqlConnection(ConnStr);
                Console.WriteLine("---->Close SuccessFully!");
                eonnection.Open();
                await Task.Delay(5000);
                Console.WriteLine("---->Close SuccessFully!");
                eonnection.Close();                
            }   
            catch(Exception ex)
            {
                Console.WriteLine("Sql Exception----->>>"+ex.Message);
                return Ok( new {Message=ex.Message});
            }            
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try 
            {
                var items = _repo.GetAllProducts()?.ToList();
                return Ok(items);
            }   
            catch(Exception ex)
            {
                Console.WriteLine("Sql Exception----->>>"+ex.Message);
                return Ok( new {Message=ex.Message});
            }            
        }
        [HttpGet("[action]")]        
        public async Task<ActionResult<Product>> FindProductById(int id)
        {
            if (id<=0)
            {
                return BadRequest("id is not valid!");
            }
            var items = _repo?.FindProductById(id);
            return Ok(items);
        }
    }
}
