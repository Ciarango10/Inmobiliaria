using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsCiudad
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de ciudad
        public Ciudad ciudad { get; set; }

        public List<Ciudad> ConsultarTodos()
        {
            return dbInmobiliaria.Ciudads.OrderBy(c => c.Nombre).ToList();
        }
    }
}