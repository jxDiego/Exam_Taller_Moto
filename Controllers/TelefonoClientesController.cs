using Exam_Taller_Moto.Models;
using Servicios_Jue.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Exam_Taller_Moto.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Telefonos")]
    [Authorize]
    public class TelefonoClientesController : ApiController
    {
        [HttpGet]
        [Route("ListadoTelefonosXCliente")]
        public IQueryable ListadoTelefonosXCliente(int IdCliente)
        {
            clsTelefonoClientes telefono = new clsTelefonoClientes();
            return telefono.TelefonosXCliente(IdCliente);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] TelefonoCliente telefono)
        {
            clsTelefonoClientes _telefono = new clsTelefonoClientes();
            _telefono.telefono = telefono;
            return _telefono.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] TelefonoCliente telefono)
        {
            clsTelefonoClientes _telefono = new clsTelefonoClientes();
            _telefono.telefono = telefono;
            return _telefono.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] TelefonoCliente telefono)
        {
            clsTelefonoClientes _telefono = new clsTelefonoClientes();
            _telefono.telefono = telefono;
            return _telefono.Eliminar();
        }
    }
}