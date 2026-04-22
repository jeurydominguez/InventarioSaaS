using InventarioSaaS.Domain.DTO;
using InventarioSaaS.Domain.Entidades;
using Microsoft.IdentityModel.Abstractions;
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

        public static LeerClienteDtoVenta ALeerClienteDtoVenta(Cliente Modelo)
        {
            return new LeerClienteDtoVenta
            {
                Id = Modelo.Id,
                Nombre = Modelo.Nombre,
                NumeroTelefono = Modelo.NumeroTelefono,
                Direccion = Modelo.Direccion
            };
        }

        public static LeerClienteDto ALeerDto(Cliente modelo)
        {
            var facturas = Mapper.VentasMapper.ALeerVentasDto(modelo.Facturas);
            return new LeerClienteDto
            {
                Id = modelo.Id,
                Nombre = modelo.Nombre,
                NumeroTelefono = modelo.NumeroTelefono,
                Facturas = facturas,
                Direccion = modelo.Direccion
            };
        }

        public static List<LeerClienteDto> AListaLeerCliente(List<Cliente> modelos)
        {
            List<LeerClienteDto> dtos = new List<LeerClienteDto>();

            foreach(var i in modelos)
            {
                var dto = new LeerClienteDto
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    NumeroTelefono = i.NumeroTelefono,
                    Direccion = i.Direccion
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public static ActualizarClienteDto AActualizarClienteDto(Cliente modelo)
        {
            return new ActualizarClienteDto
            {
                Id = modelo.Id,
                Nombre = modelo.Nombre,
                Direccion = modelo.Direccion,
                NumeroTelefono = modelo.NumeroTelefono,
                EmpresaId = modelo.EmpresaId
            };
        }
    }
}
