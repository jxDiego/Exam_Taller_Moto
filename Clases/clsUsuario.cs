using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class clsUsuario
    {
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();
        public Usuario usuario { get; set; }
        public string CrearUsuario(int idPerfil)
        {
            try
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave;
                if (cypher.CifrarClave())
                {
                    //Grabar el usuario, se deben leer los datos de la clase cypher con la información encriptada
                    usuario.Clave = cypher.PasswordCifrado;
                    usuario.Salt = cypher.Salt;
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    //se debe grabar el perfil del usuario
                    UsuarioPerfil UsuarioPerfil = new UsuarioPerfil();
                    UsuarioPerfil.CodigoPerfil = idPerfil;
                    UsuarioPerfil.Activo = true;
                    UsuarioPerfil.CodigoUsuario = usuario.IdUsuario; //El id del Usuario queda grabado en la clase usuario al grabar en la base de datos.
                    db.UsuarioPerfils.Add(UsuarioPerfil);
                    db.SaveChanges();
                    return "Se creó el usuario correctamente";
                }
                else
                {
                    return "No se pudo encriptar la clave del usuario";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}