using FinekraTutorial.Business.Interfaces;
using FinekraTutorial.DataAccess.Interfaces.Repositores;
using FinekraTutorial.DataAccess.Repositories;
using FinekraTutorial.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinekraTutorial.Business.Services;
public class OrderService<TContext> : BaseService<Order>, IOrderService
{
    private readonly IGenericRepository<Order> _repository;
    private readonly IGenericRepository<OrderDetail> _detailRepository;
    private readonly IGenericRepository<Perfume> _perfumeRepository;
    private readonly IGenericRepository<UserDetail> _userRepository;
    public OrderService(IGenericRepository<Order> repository, IGenericRepository<OrderDetail> detailRepository, IGenericRepository<Perfume> perfümeRepository, IGenericRepository<UserDetail> userRepository) : base(repository)
    {
        _repository = repository;
        _detailRepository = detailRepository;
        _perfumeRepository = perfümeRepository;
        _userRepository = userRepository;
    }

    public async Task AddProductToBasket(Guid productId, Guid userId, int count)
    {
        var order = await _repository.GetAllFiltered().FirstOrDefaultAsync(p => p.UserDetailId == userId && !p.IsCompleted);
        var user = await _userRepository.Get(userId);
        if (order == null)
        {
            await _repository.AddAsync(new Order
            {
                OrderDate = DateTime.Now,
                Phone = user.Phone,
                ShipAddress = user.Address,
                UserDetailId = userId
            });
            order = await _repository.GetAllFiltered().FirstOrDefaultAsync(p => p.UserDetailId == userId);
        }
        var perfume = await _perfumeRepository.Get(productId);
        await _detailRepository.AddAsync(new OrderDetail { Count = count, OrderId = order!.Id, PerfumeId = perfume.Id, Price = perfume.Price * count });
    }

    public async Task<bool> CompleteOrder(Guid userId)
    {
        var order = await _repository.GetAllFiltered().Include(p => p.OrderDetails).FirstOrDefaultAsync(p => p.UserDetailId == userId && !p.IsCompleted && p.OrderDetails.Any());
        if (order == null)
        {
            return false;
        }
        order!.IsCompleted = true;
        await _repository.SaveAsync(order);
        return true;
    }

    public async Task DeleteProductFromBasket(Guid productId, Guid userId)
    {
        var order = await _repository.GetAllFiltered(p => p.UserDetailId == userId && !p.IsCompleted).FirstOrDefaultAsync();
        var orderProduct = await _detailRepository.GetAllFiltered(p => p.OrderId == order.Id && p.PerfumeId == productId).Include(p => p.Perfume).FirstOrDefaultAsync();
        if (orderProduct != null)
        {
            if (orderProduct.Count > 1)
            {
                orderProduct.Count--;
                orderProduct.Price -= orderProduct.Perfume.Price;
                await _detailRepository.SaveAsync(orderProduct);
            }
            else
            {
                if (orderProduct.Count > 0)
                {
                    await _detailRepository.Delete(orderProduct.Id);
                }
            }
        }
    }

    public async Task<List<OrderDetail>> GetMyOrder(Guid userId)
    {
        var orderId = await _repository.GetAllFiltered(p => p.UserDetailId == userId && !p.IsCompleted).Select(p => p.Id).FirstOrDefaultAsync();
        return await _detailRepository.GetAllFiltered(p => p.OrderId == orderId).Include(p => p.Perfume).ToListAsync();
    }
}
