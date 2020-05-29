using System.Threading.Tasks;
using BookstoreWebApp.Models;
using BookstoreWebApp.ViewModels;
using FluentEmail.Core;

namespace BookstoreWebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmail(IFluentEmail email, ShoppingCart model, string mailTo);
    }
}