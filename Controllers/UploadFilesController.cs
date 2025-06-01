using Exam_Taller_Moto.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Exam_Taller_Moto.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/UploadFiles")]
    //[Authorize]
    public class UploadFilesController : ApiController  // esta clase se encarga de subir y descargar archivos, es la gestion de los archivos
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CargarArchivo(HttpRequestMessage request, string Datos, string Proceso)
        {
            clsUpload upload = new clsUpload();
            upload.Datos = Datos;
            upload.Proceso = Proceso;
            upload.request = request;
            return await upload.GrabarArchivo(false);
        }
        [HttpGet]
        public HttpResponseMessage ConsultarArchivo(string NombreImagen)
        {
            clsUpload upload = new clsUpload();
            return upload.DescargarArchivo(NombreImagen);
        }
        [HttpPut]
        public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage request)
        {
            clsUpload upload = new clsUpload();
            upload.request = request;
            return await upload.GrabarArchivo(true);
        }
        [HttpDelete]
        public HttpResponseMessage EliminarArchivo(string NombreImagen, string Proceso, string Datos)
        {
            clsUpload upload = new clsUpload();
            upload.request = Request;
            return upload.EliminarArchivo(NombreImagen, Proceso, Datos);
        }



    }
}