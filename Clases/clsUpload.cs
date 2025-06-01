using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class clsUpload
    {
        public string Datos { get; set; }
        public string Proceso { get; set; }
        public HttpRequestMessage request { get; set; }
        private List<String> Archivos;
        public async Task<HttpResponseMessage> GrabarArchivo(bool Actualizar)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envio un archivo para procesar");
            }
            string root = HttpContext.Current.Server.MapPath("~/Archivos");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                bool Existe = false;
                //Lee el contenido de los archivos
                await request.Content.ReadAsMultipartAsync(provider);
                if (provider.FileData.Count > 0)
                {
                    Archivos = new List<string>();
                    foreach (MultipartFileData file in provider.FileData)
                    {
                        string fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        if (File.Exists(Path.Combine(root, fileName)))
                        {
                            if (Actualizar)
                            {
                                //El archivo ya existe en el servidor, no se va a cargar , se va a eliminar el temporal y se devolvera un error
                                File.Delete(Path.Combine(root, fileName));
                                //Actualiza el nombre del primer archivo
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                                return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se actualizo la imagen ");
                            }
                            else
                            {
                                //El archivo ya existe en el servidor, no se va a cargar , se va a eliminar el temporal y se devolvera un error
                                File.Delete(Path.Combine(root, file.LocalFileName));
                                Existe = true;
                            }
                            //return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo ya existe en el servidor");
                        }
                        else
                        {
                            if (!Actualizar)
                            {
                                Existe = false;
                                //Agrego en una lista el nombre de los archivos que se cargaron
                                Archivos.Add(fileName);
                                //renombra el archivo temporal 
                                File.Move(file.LocalFileName, Path.Combine(root, fileName));
                            }
                            else
                            {
                                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "El archivo no existe, se debe agregar");
                            }
                        }
                    }
                    if (!Existe)
                    {
                        //Se genera el proceso de gestion en la base de datos
                        string RptaBD = ProcesarBD();
                        //Termina el ciclo, responde que se cargo el archivo correctamente
                        return request.CreateResponse(System.Net.HttpStatusCode.OK, "Se cargaron los archivos en el servidor, " + RptaBD);
                    }
                    else
                    {
                        return request.CreateErrorResponse(System.Net.HttpStatusCode.Conflict, "El archivo ya existe en el servidor");
                    }
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, "No se envio un archivo para procesar");
                }

            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage DescargarArchivo(string Imagen)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Imagen);
                if (File.Exists(Archivo))
                {
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    var stream = new FileStream(Archivo, FileMode.Open);
                    response.Content = new StreamContent(stream);
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = Imagen;
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    return response;
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "El archivo no existe en el servidor");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private string ProcesarBD()
        {
            switch (Proceso.ToUpper())
            {
                case "REPUESTO":
                    clsRepuesto repuesto = new clsRepuesto();
                    return repuesto.GrabarImagenProducto(Convert.ToInt32(Datos), Archivos);
                //case "EMPLEADO":
                //    clsEmpleado empleado = new clsEmpleado();
                //    return empleado.GrabarImagenEmpleado(Convert.ToInt32(Datos), Archivos);
                default:
                    return "No se ha definido el proceso en la base de datos";
            }
        }

        //eliminar archivo 
        public HttpResponseMessage EliminarArchivo(string Imagen, string Proceso, string Datos)
        {
            try
            {
                string Ruta = HttpContext.Current.Server.MapPath("~/Archivos");
                string Archivo = Path.Combine(Ruta, Imagen);

                // Verificar si el archivo existe físicamente
                if (File.Exists(Archivo))
                {
                    // Eliminar el archivo físico
                    File.Delete(Archivo);

                    // Eliminar el registro en la base de datos
                    string RptaBD = EliminarDeBD(Imagen, Proceso, Datos);

                    return request.CreateResponse(System.Net.HttpStatusCode.OK,
                        "Se eliminó el archivo del servidor correctamente. " + RptaBD);
                }
                else
                {
                    return request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound,
                        "El archivo no existe en el servidor");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private string EliminarDeBD(string Imagen, string Proceso, string Datos)
        {
            switch (Proceso.ToUpper())
            {
                case "PRODUCTO":
                    clsRepuesto repuesto = new clsRepuesto();
                    return repuesto.EliminarImagenProducto(Convert.ToInt32(Datos), Imagen);
                // Puedes agregar más casos aquí si tienes otros procesos
                default:
                    return "No se ha definido el proceso en la base de datos";
            }
        }
    }
}