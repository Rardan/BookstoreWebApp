using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class EmployeeController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public EmployeeController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageOrderStatus()
        {
            var orders = _orderRepository.Orders;
            return View(orders);
        }

        public async Task<RedirectToActionResult> ChangeOrderStatus(string orderNumber)
        {
            var order = _orderRepository.GetOrderByNumber(orderNumber);

            if (order != null)
            {
                if(order.Condidtion == "Created")
                {
                    _orderRepository.ChangeOrderStatus(order, "Packed");
                }
                else if (order.Condidtion == "Packed")
                {
                    _orderRepository.ChangeOrderStatus(order, "Shipped");
                }
            }
            return RedirectToAction("ManageOrderStatus");
        }
    }
}