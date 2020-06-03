using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using BookstoreWebApp.ViewModels;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookstoreWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IEmailSender _emailSender;

        public OrderController(IOrderRepository orderRepository,
            ShoppingCart shoppingCart,
            UserManager<StoreUser> userManager,
            IEmailSender emailSender)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromServices] IFluentEmail email, Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Cart is empty");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                _orderRepository.CreateOrder(order, user);
                await _emailSender.SendEmail(email, _shoppingCart, order.Email);
                return RedirectToAction("Payment");
            }
            return View(order);
        }

        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for order!";
            
            return View();
        }
    }
}