using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsInmueble
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de inmueble
        public Inmueble inmueble { get; set; }

        // Metodos de la clase
        public List<Inmueble> ConsultarTodos()
        {
            return dbInmobiliaria.Inmuebles.OrderBy(i => i.Direccion).ToList();
        }
    }
}