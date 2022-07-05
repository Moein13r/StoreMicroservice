using System.Text.Json;
using AutoMapper;
using Products.DTOs;
using Products.Models;
using Products.Repositories.ProductRepository;

namespace Products.EventProcessing
{
    public class EventProcessor:IEventProcessor
    {
        private IServiceScopeFactory _scopefactory;
        private IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,AutoMapper.IMapper mapper)
        {
            _scopefactory=scopeFactory;
            _mapper=mapper;
        }
        
        public void ProcessEvent(string message)
        {
            var eventType=DetermineEvent(message);
            switch (eventType)
            {
                case EventTypes.PlatformPublished:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }
        private EventTypes DetermineEvent(string notifcationmessage)
        {
            Console.WriteLine("--> Determining Event");
            var eventType=JsonSerializer.Deserialize<GenericEvent>(notifcationmessage);
            switch (eventType.Event)
            {
                case "Product_Published":
                    Console.WriteLine("--> Product Published Event Detected");
                    return EventTypes.PlatformPublished;
                default:
                Console.WriteLine("--> Colud not detect event type");
                    return EventTypes.Undetermined;
            }
        }
        private void addPlatform(string ProductsPublishedMessage)        
        {
            using  (var scope = _scopefactory.CreateScope())
            {
                var repo=scope.ServiceProvider.GetRequiredService<IProductRepository>();            
                var ProductPublish=JsonSerializer.Deserialize<Product>(ProductsPublishedMessage);                
                try
                {                    
                    repo.AddProduct(ProductPublish);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine($"--< Colud Not Add Product to Db {e.Message}");
                }
            }
        }
    }
}