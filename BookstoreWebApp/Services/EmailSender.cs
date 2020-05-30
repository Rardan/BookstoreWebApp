using System.IO;
using System.Threading.Tasks;
using BookstoreWebApp.Models;
using BookstoreWebApp.ViewModels;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;

namespace BookstoreWebApp.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmail(IFluentEmail email, ShoppingCart model, string mailTo)
        {
            var template = $"{Directory.GetCurrentDirectory()}/Views/Email/order_confirmation.cshtml";
            await email
                .To(mailTo)
                .Subject("Order Confirmation")
                .UsingTemplateFromFile(template, model)
                .SendAsync();
            var a = 23;
        }
    }
}