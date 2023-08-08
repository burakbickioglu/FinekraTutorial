using FinekraTutorial.Business.Interfaces;
using FinekraTutorial.Business.Services;
using FinekraTutorial.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinekraTutorial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IBaseService<UserDetail> _userDetailService;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IOrderService orderService, IBaseService<UserDetail> userDetailService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _userDetailService = userDetailService;
            _logger = logger;
        }

        [HttpGet("GetMyOrder")]
        public async Task<IActionResult> GetMyOrder()
        {
            var user = _userDetailService.GetAllFiltered().FirstOrDefault();
            var order = await _orderService.GetMyOrder(user.Id);
            if(order != null)
            {
                return Ok(order);
            }
            return Ok("Siparişiniz bulunmamaktadır.");
        }

        [HttpPost("AddProductToOrder")]
        public async Task<IActionResult> AddProductToOrder([FromBody] AddProductToOrderModel model)
        {
            var user = _userDetailService.GetAllFiltered().FirstOrDefault();
            await _orderService.AddProductToBasket(model.ProductId, user.Id, model.Count);
            _logger.LogInformation($"{user.FirstName} {user.LastName} kullanıcısı {model.ProductId} id' li ürünü {DateTime.Now} tarihinde sepetine ekledi.");
            return Ok();
        }

        [HttpDelete("DeleteProductFromOrder")]
        public async Task<IActionResult> DeleteProductFromOrder(Guid ProductId)
        {
            var user = _userDetailService.GetAllFiltered().FirstOrDefault();
            await _orderService.DeleteProductFromBasket(ProductId, user.Id);
            _logger.LogInformation($"{user.FirstName} {user.LastName} kullanıcısı {DateTime.Now} tarihinde {ProductId} id' li ürünü sepetinden 1 adet kaldırdı.");
            return Ok();
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder()
        {
            var user = _userDetailService.GetAllFiltered().FirstOrDefault();
            var response = await _orderService.CompleteOrder(user.Id);
            if (response)
            {
                _logger.LogInformation($"{user.FirstName} {user.LastName} kullanıcısı {DateTime.Now} tarihinde siparişini tamamladı");
                return Ok();
            }
            return Ok("Sepetinizde ürün bulunmamaktadır.");
        }

    }
}

public class AddProductToOrderModel
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
}