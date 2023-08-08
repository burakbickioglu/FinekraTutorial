using FinekraTutorial.Entity.Entities;

namespace FinekraTutorial.Business.Interfaces;

public interface IOrderService : IBaseService<Order>
{
    Task<List<OrderDetail>> GetMyOrder(Guid userId);
    Task AddProductToBasket(Guid productId, Guid userId, int count);
    Task DeleteProductFromBasket(Guid productId, Guid userId);
    Task<bool> CompleteOrder(Guid userId);
}
