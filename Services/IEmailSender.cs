using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_Project_Kredit.Services
{
    public interface IEmailSender
{
    Task SendRegistrationConfirmationAsync(string toEmail, string username);
}
}