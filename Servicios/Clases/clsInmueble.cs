using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        public string Insertar()
        {

            try
            {   // Agregamos el inmueble
                dbInmobiliaria.Inmuebles.Add(inmueble);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se agregó el inmueble con id: " + inmueble.IdInmueble;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Actualizar()
        {

            try
            {   // Actualizamos la inmueble
                dbInmobiliaria.Inmuebles.AddOrUpdate(inmueble);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se actualizó el inmueble con id: " + inmueble.IdInmueble;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable ConsultarConTodo()
        {
            return from I in dbInmobiliaria.Set<Inmueble>()
                   join T in dbInmobiliaria.Set<TipoInmueble>()
                   on I.Id_TipoInmueble equals T.IdTipoInmueble
                   join C in dbInmobiliaria.Set<Ciudad>()
                   on I.Id_Ciudad equals C.IdCiudad
                   select new
                   {
                       IdInmueble = I.IdInmueble,
                       Direccion = I.Direccion,
                       Precio = I.Precio,
                       TipoInmueble = T.Tipo,
                       Ciudad = C.Nombre 
                   };
        }


        public Inmueble Consultar(int IdInmueble)
        {
            // Consultamos el inmueble en la base de datos
            return dbInmobiliaria.Inmuebles.FirstOrDefault(i => i.IdInmueble == IdInmueble);
        }

         public List<Inmueble> ConsultarTodos()
        {
            return dbInmobiliaria.Inmuebles.OrderBy(i => i.Direccion).ToList();
        }

        public string Eliminar()
        {

            try
            {   //Se consulta el inmueble
                Inmueble _inmueble = Consultar(inmueble.IdInmueble);
                //El inmueble se remueve de la lista de inmuebles
                dbInmobiliaria.Inmuebles.Remove(_inmueble);
                // Guardamos en la base de datos 
                dbInmobiliaria.SaveChanges();
                return "Se eliminó el inmueble con id: " + inmueble.IdInmueble;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}