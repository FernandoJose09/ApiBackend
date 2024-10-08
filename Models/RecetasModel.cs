using System;
using System.Runtime.CompilerServices;
namespace reportesApi.Models
{
    public class GetRecetasModel
    {
        public int Id { get; set; }
        public string Usuarioregistra { get; set; }
        public string Nombre { get; set; }
        public string Estatus {get; set;}
        public string FechaCreacion { get; set; }
    }

    public class InsertRecetaModel 
    { 
        public string UsuarioRegistra { get; set; }
        public string Nombre{ get; set; }
        public int Estatus { get; set; }
    }

    public class UpdateRecetaModel
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public int Estatus { get; set; }
    }

}