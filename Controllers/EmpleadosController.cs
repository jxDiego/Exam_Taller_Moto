using Exam_Taller_Moto.Clases;
using Exam_Taller_Moto.Models;
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
    [RoutePrefix("api/Empleados")]
    [Authorize]
    public class EmpleadosController : ApiController
    {
        [HttpGet] 
        [Route("ConsultarTodos")] 
        public List<Empleado> ConsultarTodos()
        {
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.ConsultarTodos();
        }
        [HttpGet]
        [Route("ConsultarXIdEmpleado")]
        public Empleado ConsultarXIdEmpleado(int IdEmpleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.Consultar(IdEmpleado);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Empleado empleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Empleado empleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar([FromBody] Empleado empleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            Empleado.empleado = empleado;
            return Empleado.Eliminar();
        }
        [HttpDelete]
        [Route("EliminarXIdEmpleado")]
        public string EliminarXDocumento(int IdEmpleado)
        {
            clsEmpleado Empleado = new clsEmpleado();
            return Empleado.Eliminar(IdEmpleado);
        }
    }
}