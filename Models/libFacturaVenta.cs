using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Taller_Moto.Models
{
    public class FacturaDetalle
    {
        public FacturaVenta facturaVenta { get; set; }

        public DetalleFacturaVenta detalle { get; set; }
        
    }
}