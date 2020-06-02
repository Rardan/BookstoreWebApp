using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreWebApp.Data;
using BookstoreWebApp.Models;
using BookstoreWebApp.Services;
using BookstoreWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IBookRepository bookRepository, ShoppingCart shoppingCart, IBookService bookService)
        {
            _bookRepository = bookRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<ViewResult> Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public IActionResult AddToShoppingCart(int? id)
        {
            var selectedProduct = _bookRepository.Books.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddToShoppingCartMultiple(int? id, int quantity)
        {
            ViewData["QuantityAmount"] = 1;
            var selectedProduct = _bookRepository.Books.FirstOrDefault(p => p.Id == id);
            if (selectedProduct != null)
            {
                if(quantity == 1) _shoppingCart.AddToCart(selectedProduct, 1);
                else _shoppingCart.AddToCart(selectedProduct, quantity);
            }
            return RedirectToAction("Index");
        }


        public async Task<RedirectToActionResult> RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _bookRepository.Books.FirstOrDefault(p => p.Id == productId);
            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
        
    }

    public class ShoppingCartViewComponent: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IBookRepository _bookRepository;

        public IViewComponentResult Invoke()
        {
            return View(_shoppingCart);
        }

        public ShoppingCartViewComponent(IBookRepository bookRepository, ShoppingCart shoppingCart)
        {
            _bookRepository = bookRepository;
            _shoppingCart = shoppingCart;
        }
    }
}