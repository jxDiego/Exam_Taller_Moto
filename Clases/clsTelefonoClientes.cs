using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Jue.Clases
{
    public class clsTelefonoClientes
    {
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();
        public TelefonoCliente telefono { get; set; }
        public IQueryable TelefonosXCliente(int IdCliente)
        {
            return from C in db.Set<Cliente>()
                   join T in db.Set<TelefonoCliente>()
                   on C.IdCliente equals T.IdCliente
                   join TT in db.Set<TipoTelefono>()
                   on T.CodigoTipoTel equals TT.Codigo
                   where C.IdCliente == IdCliente
                   orderby TT.Nombre
                   select new
                   {
                       Edit = "<img src=\"../Imagenes/Editar.png\" onclick=\"EditarTelefono(" + T.CodigoTipoTel + ", " + TT.Codigo + ", " + T.Numero + ") \"style=\"cursor:grab\"/>",
                       Tipo_Telefono = TT.Nombre,
                       Numero = T.Numero
                   };
        }
        public string Insertar()
        {
            db.TelefonoClientes.Add(telefono);
            db.SaveChanges();
            return "Se insertó el teléfono: " + telefono.Numero;
        }
        public string Eliminar()
        {
            TelefonoCliente _telefono = db.TelefonoClientes.FirstOrDefault(t => t.CodigoTipoTel == telefono.CodigoTipoTel);
            db.TelefonoClientes.Remove(_telefono);
            db.SaveChanges();
            return "Se eliminó el teléfono con código: " + telefono.CodigoTipoTel;
        }
        public string Actualizar()
        {
            TelefonoCliente _telefono = db.TelefonoClientes.FirstOrDefault(t => t.CodigoTipoTel == telefono.CodigoTipoTel);
            _telefono.CodigoTipoTel = telefono.CodigoTipoTel;
            _telefono.Numero = telefono.Numero;

            db.SaveChanges();
            return "Se actualizó el teléfono con código: " + telefono.CodigoTipoTel;
        }
    }
}