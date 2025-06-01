using Exam_Taller_Moto.Clases;
using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Exam_Taller_Moto.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Usuarios")]
    [Authorize]
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("CrearUsuario")]
        public string CrearUsuario([FromBody] Usuario usuario, int idPerfil)
        {
            clsUsuario Usuario = new clsUsuario();
            Usuario.usuario = usuario;
            return Usuario.CrearUsuario(idPerfil);
        }
    }
}