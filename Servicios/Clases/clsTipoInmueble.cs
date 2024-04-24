using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsTipoInmueble
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de tipo de inmueble
        public TipoInmueble tipoInmueble { get; set; }

        public List<TipoInmueble> ConsultarTodos()
        {
            return dbInmobiliaria.TipoInmuebles.OrderBy(c => c.Tipo).ToList();
        }
    }
}