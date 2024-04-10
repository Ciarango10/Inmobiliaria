using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Servicios.Clases
{
    public class clsEmpleado
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de empleado
        public Empleado empleado { get; set; }

        // Metodos de la clase
        public string Insertar()
        {

            try
            {   // Agregamos el empleado
                dbInmobiliaria.Empleadoes.Add(empleado);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se agregó el empleado con documento: " + empleado.Documento;
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
            {   // Actualizamos el empleado
                dbInmobiliaria.Empleadoes.AddOrUpdate(empleado);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se actualizó el empleado con documento: " + empleado.Documento;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<Empleado> ConsultarTodos()
        {
            return dbInmobiliaria.Empleadoes.OrderBy(e => e.Nombre).ToList();
        }

        public Empleado Consultar(string Documento)
        {
            // Consultamos al empleado en la base de datos
            return dbInmobiliaria.Empleadoes.FirstOrDefault(e => e.Documento == Documento);
        }

        public string Eliminar()
        {

            try
            {   //Se consulta el empleado
                Empleado _empleado = Consultar(empleado.Documento);
                //El empleado se remueve de la lista de mepleados
                dbInmobiliaria.Empleadoes.Remove(_empleado);
                // Guardamos en la base de datos 
                dbInmobiliaria.SaveChanges();
                return "Se eliminó el empleado con documento: " + empleado.Documento;

            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}