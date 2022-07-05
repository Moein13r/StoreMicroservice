// namespace Ordering.Domain.AggregatesModel.OrderAggregate
// {
//     public interface IOrderRepository : IRepository<Order>
//     {
//         Order Add(Order order);

//         void Update(Order order);

//         Task<Order> GetAsync(int orderId);
//     }

//     // Defined at IRepository.cs (Part of the Domain Seedwork)
//     public interface IRepository<T> where T : IAggregateRoot
//     {
//         IUnitOfWork UnitOfWork { get; }
//     }
// }