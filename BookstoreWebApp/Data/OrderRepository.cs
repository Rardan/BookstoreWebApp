using BookstoreWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreWebApp.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookstoreDbContext _bookstoreDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(BookstoreDbContext bookstoreDbContext,
            ShoppingCart shoppingCart)
        {
            _bookstoreDbContext = bookstoreDbContext;
            _shoppingCart = shoppingCart;
        }

        public IEnumerable<Order> Orders => _bookstoreDbContext.Orders.OrderBy(o => o.OrderNumber);

        public void ChangeOrderStatus(Order order, string status)
        {
            if (order != null)
            {
                order.Condidtion = status;
                _bookstoreDbContext.Update(order);
                _bookstoreDbContext.SaveChanges();
            }
        }

        public void CreateOrder(Order order, StoreUser storeUser)
        {
            var transaction = _bookstoreDbContext.Database.BeginTransaction();
            try
            {
                order.OrderDate = DateTime.Now;
                order.OrderNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                    + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString()
                    + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                order.User = storeUser;
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
                order.Condidtion = "Created";
                _bookstoreDbContext.Add(order);

                var shoppingCartItems = _shoppingCart.ShoppingCartItems;

                foreach (var item in shoppingCartItems)
                {
                    var orderItem = new OrderItem()
                    {
                        BookId = item.Book.Id,
                        OrderId = order.Id,
                        Price = item.Book.Price * item.Amount,
                        Amount = item.Amount
                    };
                    var itemStorage = _bookstoreDbContext.Storages.FirstOrDefault(s => s.BookId == item.Book.Id);
                    itemStorage.Amount = itemStorage.Amount - orderItem.Amount;

                    _bookstoreDbContext.Storages.Update(itemStorage);
                    _bookstoreDbContext.OrderItems.Add(orderItem);
                }
                _bookstoreDbContext.SaveChanges();

                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
        }

        public Order GetOrderByNumber(string orderNumber)
        {
            var order = _bookstoreDbContext.Orders
                .Include(i => i.OrderItems)
                .Include("OrderItems.Book")
                .Include("OrderItem.Book.Artist")
                .FirstOrDefault(o => o.OrderNumber == orderNumber);
            return order;
        }

        public IEnumerable<Order> GetOrdersByUser(StoreUser storeUser)
        {
            var orders = _bookstoreDbContext.Orders
                .Include(i => i.OrderItems)
                .Where(o => o.User == storeUser)
                .ToList();
            return orders;
        }
    }
}
