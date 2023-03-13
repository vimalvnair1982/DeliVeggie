using DeliVeggie.Model;
using EasyNetQ;

namespace DeliVeggie.RabbitMQ
{
    public class EasyNetSubscribe : IEasyNetSubscribe
    {
        public void Subscribe()
        {
            Product p = null;

            var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
            
            bus.Rpc.Respond<Product, AddProductResponse>(req => new AddProductResponse
            {
                Id = req.Id,
                Name = req.Name,
                Description= req.Description,
                Brand = req.Brand,
                Price = req.Price,
            });

            bus.Rpc.Respond<GetProductsRequest, GetProductsResponse>(req => new GetProductsResponse());

            bus.Rpc.Respond<GetProductRequest, GetProductResponse>(req => new GetProductResponse()
            {
                Id = req.Id,
               
            });

        }
    }
}
