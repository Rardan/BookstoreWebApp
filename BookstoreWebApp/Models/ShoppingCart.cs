using BookstoreWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Models
{
    public class ShoppingCart
    {
        private readonly BookstoreDbContext _bookstoreDbContext;

        public ShoppingCart(BookstoreDbContext bookstoreDbContext)
        {
            _bookstoreDbContext = bookstoreDbContext;
        }

        public string ShoppingCartId { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<HttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<BookstoreDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Book book, int amount)
        {
            var shoppingCartItem =
                _bookstoreDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Book.Id == book.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };

                _bookstoreDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bookstoreDbContext.SaveChanges();
        }

        public int RemoveFromCart(Book book)
        {
            var shoppingCartItem = _bookstoreDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bookstoreDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _bookstoreDbContext.SaveChanges();
            return localAmount;
        }

        public ICollection<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _bookstoreDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(b => b.Book)
                .ThenInclude(a=>a.Author)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _bookstoreDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _bookstoreDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _bookstoreDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _bookstoreDbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Book.Price * c.Amount).Sum();
            return total;
        }
    }
}
