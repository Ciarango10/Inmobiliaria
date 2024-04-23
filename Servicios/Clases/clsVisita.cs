using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsVisita
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de visita
        public Visita visita { get; set; }

        // Metodos de la clase
        public string Insertar()
        {

            try
            {   // Agregamos la visita
                dbInmobiliaria.Visitas.Add(visita);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se agregó la visita con id: " + visita.IdVisita;
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
            {   // Actualizamos la visita
                dbInmobiliaria.Visitas.AddOrUpdate(visita);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se actualizó la visita con id: " + visita.IdVisita;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }

        public IQueryable ConsultarConTodo()
        {
            return from V in dbInmobiliaria.Set<Visita>()
                   join E in dbInmobiliaria.Set<Empleado>()
                   on V.Documento_Empleado equals E.Documento
                   join C in dbInmobiliaria.Set<Cliente>()
                   on V.Documento_Cliente equals C.Documento
                   join I in dbInmobiliaria.Set<Inmueble>()
                   on V.Id_Inmueble equals I.IdInmueble
                   select new
                   {
                       IdVisita = V.IdVisita,
                       Comentarios = V.Comentarios,
                       Fecha = V.Fecha,
                       Empleado = E.Nombre + " "  + E.Apellido,
                       Cliente = C.Nombre + " " + C.Apellido,
                       Inmueble = I.Direccion
                   };
        }

        public Visita Consultar(int IdVisita)
        {
            // Consultamos la visita en la base de datos
            return dbInmobiliaria.Visitas.FirstOrDefault(v => v.IdVisita == IdVisita);
        }

        public List<Visita> ConsultarTodos()
        {
            return dbInmobiliaria.Visitas.OrderBy(v => v.Fecha).ToList();
        }

        public string Eliminar()
        {

            try
            {   //Se consulta la visita
                Visita _visita = Consultar(visita.IdVisita);
                //La visita se remueve de la lista de visitas
                dbInmobiliaria.Visitas.Remove(_visita);
                // Guardamos en la base de datos 
                dbInmobiliaria.SaveChanges();
                return "Se eliminó la visita con id: " + visita.IdVisita;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}