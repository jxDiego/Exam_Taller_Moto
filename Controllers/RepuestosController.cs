using Exam_Taller_Moto.Clases;
using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;

namespace Exam_Taller_Moto.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Repuestos")]
    //[Authorize]
    public class RepuestosController : ApiController
    {
        [HttpGet]
        [Route("ConsultarImagenes")]
        public IQueryable ConsultarImagenes(int IdRepuesto)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            return Repuesto.ListarImagenes(IdRepuesto);
        }
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Repuesto> ConsultarTodos()
        {
            clsRepuesto Repuesto = new clsRepuesto();
            return Repuesto.ConsultarTodos();
        }
        [HttpGet]
        [Route("Consultar")]
        public Repuesto Consultar(int Codigo)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            return Repuesto.Consultar(Codigo);
        }
        [HttpGet]
        [Route("ConsultarXNombre")]
        public List<Repuesto> ConsultarXNombre(String Nombre)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            return Repuesto.ConsultarXNombre(Nombre);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Repuesto repuesto)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            Repuesto.repuesto = repuesto;
            return Repuesto.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Repuesto repuesto)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            Repuesto.repuesto = repuesto;
            return Repuesto.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Repuesto repuesto)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            Repuesto.repuesto = repuesto;
            return Repuesto.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXCodigo")]
        public string EliminarXCodigo(int Codigo)
        {
            clsRepuesto Repuesto = new clsRepuesto();
            return Repuesto.Eliminar(Codigo);
        }



    }
}