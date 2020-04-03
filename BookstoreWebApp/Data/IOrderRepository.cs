using BookstoreWebApp.Models;
using System.Collections.Generic;

namespace BookstoreWebApp.Data
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }

        void ChangeOrderStatus(Order order, string status);
        void CreateOrder(Order order, StoreUser storeUser);
        Order GetOrderByNumber(string orderNumber);
        IEnumerable<Order> GetOrdersByUser(StoreUser storeUser);
    }
}