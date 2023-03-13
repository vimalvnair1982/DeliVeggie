using DeliVeggie.Data;
using DeliVeggie.Model;
using DeliVeggie.RabbitMQ;
using DeliVeggieProducer.Services;
using EasyNetQ;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace DeliVeggie.Controllers
{
    [ApiController]
    //[Route("api/Products")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
      

        private readonly MongoDbService _mongoDbService;
        private readonly IEasyNetSubscribe _easyNetSubscribe;

        
        public ProductsController(MongoDbService mongoDbService, IEasyNetSubscribe easyNetSubscribe)
        {
           
            this._mongoDbService = mongoDbService;
            this._easyNetSubscribe = easyNetSubscribe;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() {

            _easyNetSubscribe.Subscribe();
            
            var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
            var request = new  GetProductsRequest();
            

            var response = bus.Rpc.Request<GetProductsRequest, GetProductsResponse>(request);
            return Ok(await _mongoDbService.GetProductLists());
        }

        [HttpPost]
        
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Brand = addProductRequest.Brand,
                Price = addProductRequest.Price
            };
            
 _easyNetSubscribe.Subscribe();
            
            var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
           
            var response=bus.Rpc.Request<Product,AddProductResponse>(product);
            Product prod = new Product()
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                Brand = response.Brand,
                Price = response.Price
            };
            await _mongoDbService.AddProduct(prod);
            return Ok(product);
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {

            _easyNetSubscribe.Subscribe();
            GetProductRequest request = new GetProductRequest()
            {
                Id = id,
                
            };



          
            var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
            

            var response = bus.Rpc.Request<GetProductRequest, GetProductResponse>(request);

            id = response.Id;
            var product = await _mongoDbService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


       
    }
}
