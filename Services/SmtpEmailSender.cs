using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Mini_Project_Kredit.Services;

namespace Mini_Project_Kredit.Services
{
    public class SmtpOptions
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string FromName { get; set; } = "";
        public string FromEmail { get; set; } = "";
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _opt;

        public SmtpEmailSender(IOptions<SmtpOptions> opt)
        {
            _opt = opt.Value;
        }

        public async Task SendRegistrationConfirmationAsync(string toEmail, string username)
        {
            using var client = new SmtpClient(_opt.Host, _opt.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_opt.Username, _opt.Password)
            };

            var msg = new MailMessage
            {
                From = new MailAddress(_opt.FromEmail, _opt.FromName),
                Subject = "Konfirmasi Pendaftaran",
                Body = $@"
Halo {username},

Pendaftaran kamu telah berhasil.

Berikut untuk autorisasi 

Terima kasih telah mendaftar di Mini Project Kredit.

".Trim(),
                IsBodyHtml = false
            };

            msg.To.Add(toEmail);

            await client.SendMailAsync(msg);
        }
    }
}