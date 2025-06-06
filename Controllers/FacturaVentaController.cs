using Exam_Taller_Moto.Clases;
using Exam_Taller_Moto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Exam_Taller_Moto.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/FacturasVenta")]
    [Authorize]
    public class FacturaVentaController : ApiController
    {
        [HttpGet]
        [Route("ListarRepuestos")]
        public IQueryable ListarRepuestos(int IdFacturaVenta)
        {
            clsFacturaVenta Factura = new clsFacturaVenta();
            return Factura.ListaRepuesto(IdFacturaVenta);
        }
        [HttpPost]
        [Route("GrabarFactura")]
        public string GrabarFactura([FromBody] FacturaDetalle  facturaDet)//[FromBody] FacturaDetalle factura)
        {
            clsFacturaVenta factura = new clsFacturaVenta();
            factura.facturaVenta = facturaDet.facturaVenta;
            factura.detalleFacturaVenta = facturaDet.detalle;
            return factura.GrabarFacturaVenta();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int IdDetalle)
        {
            clsFacturaVenta Factura = new clsFacturaVenta();
            return Factura.EliminarRepuesto(IdDetalle);
        }
    }
}