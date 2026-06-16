using InventarioSaaS.Domain.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resend;

namespace InventarioSaaS.Application.service
{
    public class EmailService : IEmailService
    {
        private readonly ResendClient resend;

        public EmailService(ResendClient resend)
        {
            this.resend = resend;
        }
        public async Task EnviarAsync(string para, string asunto, string html)
        {
            var mensaje = new EmailMessage
            {
                From = "onboarding@resend.dev",
                To = para,
                Subject = asunto,
                HtmlBody = html
            };

            await resend.EmailSendAsync(mensaje);
        }
    }
}
