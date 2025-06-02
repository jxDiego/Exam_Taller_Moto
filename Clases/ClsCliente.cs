using Exam_Taller_Moto.Models;
using System;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class ClsCliente
    {
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();

        public Cliente cliente { get; set; }

        public string Insertar()
        {
            try
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return "Cliente insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el cliente: " + ex.Message;
            }
        }

        public string actualizar()
        {
            try
            {
                Cliente cli1 = Consultar(cliente.IdCliente);
                if (cli1 == null)
                {
                    return "El cliente con el id ingresado no existe, por lo tanto no se puede actualizar";
                }
                db.Clientes.AddOrUpdate(cliente);
                db.SaveChanges();
                return "Se actualizo el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el cliente: " + ex.Message;
            }
        }

        public string Eliminar()
        {
            try
            {
                // Antes de eliminar se debe verificar si el cliente existe
                Cliente cli1 = Consultar(cliente.IdCliente);
                if (cli1 == null)
                {
                    return "El cliente con el id ingresado no existe, por lo tanto no se puede eliminar";
                }
                db.Clientes.Remove(cli1);
                db.SaveChanges();
                return "Se elimino el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el cliente: " + ex.Message;
            }
        }

        public string eliminar(int idCliente)
        {
            try
            {
                // Antes de eliminar se debe verificar si el cliente existe
                Cliente cli1 = Consultar(idCliente);
                if (cli1 == null)
                {
                    return "El cliente con el id ingresado no existe, por lo tanto no se puede eliminar";
                }
                db.Clientes.Remove(cli1);
                db.SaveChanges();
                return "Se elimino el cliente correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el cliente: " + ex.Message;
            }
        }

        public Cliente Consultar(int idCliente)
        {
            try
            {
                return db.Clientes.Find(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar el cliente: " + ex.Message);
            }
        }

        public List<Cliente> ConsultarTodos()
        {
            try
            {
                return db.Clientes.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todos los clientes: " + ex.Message);
            }
        }
    }
}
