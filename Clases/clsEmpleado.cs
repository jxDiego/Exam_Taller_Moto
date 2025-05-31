using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class clsEmpleado
    {
        private Almacen_Repuesto_MotosEntities db = new Almacen_Repuesto_MotosEntities();
        public Empleado empleado { get; set; }

        public string Insertar()
        {
            try
            {
                db.Empleadoes.Add(empleado);
                db.SaveChanges();
                return "Empleado insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el empleado: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                Empleado emp1 = Consultar(empleado.IdEmpleado);
                if (emp1 == null)
                {
                    return "El empleado con el id ingresado no existe, por lo tanto no se puede actualizar";
                }

                db.Empleadoes.AddOrUpdate(empleado); 
                db.SaveChanges();
                return "Se actualizo el empleado correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo actualizar el empleado: " + ex.Message;
            }
        }
        public string Eliminar()
        {
            try
            {
                //Antes de eliminar se debe verificar si el empleado existe
                Empleado emp1 = Consultar(empleado.IdEmpleado);
                if (emp1 == null)
                {
                    return "El empleado con el id ingresado no existe, por lo tanto no se puede eliminar";
                }

                db.Empleadoes.Remove(emp1);
                db.SaveChanges();  
                return "Se elimino el empleado correctamente";

            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }
        public string Eliminar(int IdEmpleado) 
        {
            try
            {
                
                Empleado emp1 = Consultar(IdEmpleado); 
                if (emp1 == null)
                {
                    return "El empleado con el id ingresado no existe, por lo tanto no se puede eliminar";
                }

                db.Empleadoes.Remove(emp1);
                db.SaveChanges();  
                return "Se elimino el empleado correctamente";

            }
            catch (Exception ex)
            {
                return "No se pudo eliminar el empleado: " + ex.Message;
            }
        }

        public Empleado Consultar(int IdEmpleado)
        {
            return db.Empleadoes.FirstOrDefault(e => e.IdEmpleado == IdEmpleado); 
        }
        public List<Empleado> ConsultarTodos()
        {
            return db.Empleadoes        
                .OrderBy(e => e.PrimerApellido) 
                .ToList();
        }
    }
}