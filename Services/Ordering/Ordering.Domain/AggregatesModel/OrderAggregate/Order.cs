
namespace Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order
    {
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    }
}