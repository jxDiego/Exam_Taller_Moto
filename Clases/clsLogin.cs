using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{

    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();
        public Login login { get; set; }
        private LoginRespuesta loginRespuesta;
        private bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                Usuario usuario = db.Usuarios.FirstOrDefault(u => u.UserName == login.Usuario);
                if (usuario == null)
                {
                    loginRespuesta = new LoginRespuesta();
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta = new LoginRespuesta();
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                Usuario usuario = db.Usuarios.FirstOrDefault(u => u.UserName == login.Usuario && u.Clave == login.Clave);
                if (usuario == null)
                {
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<LoginRespuesta> Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in db.Set<Usuario>()
                       join UP in db.Set<UsuarioPerfil>()
                       on U.IdUsuario equals UP.CodigoUsuario
                       join P in db.Set<Perfil>()
                       on UP.CodigoPerfil equals P.IdPerfil
                       where U.UserName == login.Usuario &&
                               U.Clave == login.Clave
                       select new LoginRespuesta
                       {
                           Usuario = U.UserName,
                           Autenticado = true,
                           Perfil = P.NombrePerfil,
                           PaginaInicio = P.PaginaNavegar,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<LoginRespuesta> List = new List<LoginRespuesta>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }
    }

}