﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public UserController(UserManager<StoreUser> userManager,
            IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var orders = _orderRepository.GetOrdersByUser(user);
            return View(orders);
        }

        [HttpGet("User/Orders/Details")]
        public async Task<IActionResult> Details(string orderNumber)
        {
            var order = _orderRepository.GetOrderByNumber(orderNumber);
            return View(order);
        }
    }
}