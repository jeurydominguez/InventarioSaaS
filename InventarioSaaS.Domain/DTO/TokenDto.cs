using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Domain.DTO
{
    public class TokenDto
    {
        public required string Token { get; set; }

        public required DateTime Expiracion { get; set; }

        public required string Role { get; set; }
    }
}
