using Products.DTOs;

namespace Products.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewProduct(ProductPublishedDto productPublishedDto);
    }
}