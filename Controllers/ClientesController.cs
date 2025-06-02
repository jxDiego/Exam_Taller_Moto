using Exam_Taller_Moto.Models;
using Exam_Taller_Moto.Clases;
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
    [RoutePrefix("api/Cliente")]
    [Authorize]
    public class ClientesController : ApiController
    {

        [HttpGet]
        [Route("Consultar")]
        public Cliente Consultar(int idCliente)
        {
            ClsCliente cliente = new ClsCliente();
            return cliente.Consultar(idCliente);
        }
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Cliente> ConsultarTodos()
        {
            ClsCliente clsCliente = new ClsCliente();
            return clsCliente.ConsultarTodos();
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Cliente cliente)
        {
            ClsCliente clsCliente = new ClsCliente();
            clsCliente.cliente = cliente;
            return clsCliente.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Cliente cliente)
        {
            ClsCliente clsCliente = new ClsCliente();
            clsCliente.cliente = cliente;
            return clsCliente.actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int idCliente)
        {
            ClsCliente clsCliente = new ClsCliente();
            return clsCliente.eliminar(idCliente);
        }
    }
}