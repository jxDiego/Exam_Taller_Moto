using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class clsRepuesto
    {
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();
        public Repuesto repuesto { get; set; }
        public string Insertar()
        {
            try
            {
                db.Repuestoes.Add(repuesto); 
                db.SaveChanges(); 
                return "Repuesto insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el Repuesto: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                Repuesto repu = Consultar(repuesto.IdRepuesto);
                if (repu == null)
                {
                    return "El repuesto con el codigo ingresado no existe, por lo tanto no se puede actualizar";
                }
                db.Repuestoes.AddOrUpdate(repuesto); 
                return "Se actualizo el repuesto correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el repuesto" + ex.Message;
            }
        }
        public Repuesto Consultar(int IdRepuesto)
        {
            return db.Repuestoes.FirstOrDefault(p => p.IdRepuesto == IdRepuesto);
        }
        public List<Repuesto> ConsultarTodos()
        {
            return db.Repuestoes
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public List<Repuesto> ConsultarXNombre(String Nombre)
        {
            return db.Repuestoes
                .Where(p => p.Nombre == Nombre)
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public string Eliminar()
        {
            try
            {
                Repuesto prod = Consultar(repuesto.IdRepuesto);
                if (prod == null)
                {
                    return "El producto con el codigo ingresado no existe, por lo tanto no se puede eliminar";
                }
                db.Repuestoes.Remove(prod);
                db.SaveChanges();
                return "Se elimino el producto correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el producto" + ex.Message;
            }
        }
        public string Eliminar(int IdRepuesto)
        {
            try
            {
                Repuesto prod = Consultar(IdRepuesto);
                if (prod == null)
                {
                    return "El producto con el codigo ingresado no existe, por lo tanto no se puede eliminar";
                }
                db.Repuestoes.Remove(prod);
                db.SaveChanges();
                return "Se elimino el producto correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el producto" + ex.Message;
            }
        }

        //clase despues del parcial
        public string GrabarImagenProducto(int idProducto, List<string> Imagenes)
        {
            try
            {
                foreach (string imagen in Imagenes)
                {
                    ImagenesProducto imagenProducto = new ImagenesProducto();
                    imagenProducto.idProducto = idProducto;
                    imagenProducto.NombreImagen = imagen;
                    db.ImagenesProductoes.Add(imagenProducto);
                    db.SaveChanges();
                }
                return "Se grabo la informacion en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        // hasta aqui
        public IQueryable ListarImagenes(int idProducto)
        {
            return from P in db.Set<Repuesto>()
                   join I in db.Set<ImagenesProducto>()
                   on P.IdRepuesto equals I.idProducto
                   where P.IdRepuesto == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idProducto = P.IdRepuesto,
                       Producto = P.Nombre,
                       Imagen = I.NombreImagen
                   };
        }
        //eliminar 
        public string EliminarImagenProducto(int idProducto, string NombreImagen)
        {
            try
            {
                // Buscar la imagen en la base de datos
                ImagenesProducto imagen = db.ImagenesProductoes.FirstOrDefault(
                    i => i.idProducto == idProducto && i.NombreImagen == NombreImagen);

                if (imagen == null)
                {
                    return "La imagen no existe en la base de datos";
                }

                // Eliminar la imagen de la base de datos
                db.ImagenesProductoes.Remove(imagen);
                db.SaveChanges();

                return "Se eliminó la información de la imagen en la base de datos";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la imagen de la base de datos: " + ex.Message;
            }
        }


    }
}