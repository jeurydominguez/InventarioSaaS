using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventarioSaaS.Application.Mapper
{
    public class ClienteMapper
    {
        public static Cliente AModelo(CrearClienteDto dto, int EmpresaId)
        {
            return new Cliente
            {
                Nombre = dto.Nombre,
                NumeroTelefono = dto.NumeroTelefono,
                Direccion = dto.Direccion,
                EmpresaId = EmpresaId
            };
        }

        public static LeerClienteDto ALeerDto(Cliente modelo)
        {
            return new LeerClienteDto
            {
                Id = modelo.Id,
                Nombre = modelo.Nombre,
                NumeroTelefono = modelo.NumeroTelefono,
                Direccion = modelo.Direccion
            };
        }
    }
}
