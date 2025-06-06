using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Clases
{
    public class clsFacturaVenta
    {
        private Almacen_Repuesto_MotosEntities almacen_Repuesto_Motos = new Almacen_Repuesto_MotosEntities();
        public FacturaVenta facturaVenta { get; set; }
        public DetalleFacturaVenta detalleFacturaVenta { get; set; }
        public string GrabarFacturaVenta(DetalleFacturaVenta detalleFacturaVenta)
        {
            if (facturaVenta.IdFacturaVenta == 0)
            {
                int NroFacturaVenta = Convert.ToInt32(GrabarEncabezado());
            }

            detalleFacturaVenta.IdDetalle = facturaVenta.IdFacturaVenta;
            return GrabarDetalle();
        }
        
        private string GrabarEncabezado()
        {
            try
            {
                facturaVenta.IdFacturaVenta = GenerarNumeroFacturaVenta();
                facturaVenta.Fecha = DateTime.Now;
                almacen_Repuesto_Motos.FacturaVentas.Add(facturaVenta);
                almacen_Repuesto_Motos.SaveChanges();
                return facturaVenta.IdFacturaVenta.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private int GenerarNumeroFacturaVenta()
        {
            return almacen_Repuesto_Motos.FacturaVentas.Select(f => f.IdFacturaVenta).DefaultIfEmpty(0).Max() + 1;
        }
        private string GrabarDetalle()
        {
            try
            {
                almacen_Repuesto_Motos.DetalleFacturaVentas.Add(detalleFacturaVenta);
                almacen_Repuesto_Motos.SaveChanges();
                return facturaVenta.IdFacturaVenta.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ListaRepuesto(int NumeroFactura)
        {
            return from D in almacen_Repuesto_Motos.Set<DetalleFacturaVenta>()
                   join R in almacen_Repuesto_Motos.Set<Repuesto>()
                   on D.IdRepuesto equals R.IdRepuesto
                   where D.IdFacturaVenta == NumeroFactura
                   select new
                   {
                       Eliminar = "<img src=\"../Imagenes/Eliminar.png\" onclick=\"Eliminar(" + D.IdDetalle + ", " + D.Cantidad + ", " + D.PrecioUnitario + ")\"/>",
                       Repuesto = R.Nombre,
                       Cantidad = D.Cantidad,
                       Valor_Unitario = D.PrecioUnitario,
                       Subtotal = D.Cantidad * D.PrecioUnitario
                   };

        }

        public string EliminarRepuesto(int Codigo)
        {
            try
            {
                detalleFacturaVenta = almacen_Repuesto_Motos.DetalleFacturaVentas.FirstOrDefault(d => d.IdDetalle == Codigo);
                almacen_Repuesto_Motos.DetalleFacturaVentas.Remove(detalleFacturaVenta);
                almacen_Repuesto_Motos.SaveChanges();
                return "Se eliminó";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        internal string GrabarFacturaVenta()
        {
            throw new NotImplementedException();
        }
    }
}