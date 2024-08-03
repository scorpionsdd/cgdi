using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestion.gestion.sources.Seguridad
{
    public class Capacidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
    }
    public class Rol
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
    }
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
    }
}