//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam_Taller_Moto.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class UsuarioPerfil
    {
        public int CodigoUs { get; set; }
        public Nullable<int> CodigoUsuario { get; set; }
        public Nullable<int> CodigoPerfil { get; set; }
        public bool Activo { get; set; }
        [JsonIgnore]

        public virtual Perfil Perfil { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
