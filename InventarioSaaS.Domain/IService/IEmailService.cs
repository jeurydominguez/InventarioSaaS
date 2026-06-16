using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioSaaS.Domain.IService
{
    public interface IEmailService
    {
        Task EnviarAsync(string para, string asunto, string html);
    }
}
