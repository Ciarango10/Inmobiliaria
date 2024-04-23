using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsCliente
    {
        // Instanciamos el modelo de base de datos
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();

        // Llamamos al modelo de cliente
        public Cliente cliente { get; set; }

        // Metodos de la clase
        public string Insertar()
        {

            try
            {   // Agregamos el cliente
                dbInmobiliaria.Clientes.Add(cliente);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se agregó el cliente con documento: " + cliente.Documento;
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
            {   // Actualizamos el cliente
                dbInmobiliaria.Clientes.AddOrUpdate(cliente);
                // Guardamos en la base de datos
                dbInmobiliaria.SaveChanges();
                return "Se actualizó el cliente con documento: " + cliente.Documento;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<Cliente> ConsultarTodos()
        {
            return dbInmobiliaria.Clientes.OrderBy(c => c.Nombre).ToList();
        }

        public Cliente Consultar(string Documento)
        {
            // Consultamos al cliente en la base de datos
            return dbInmobiliaria.Clientes.FirstOrDefault(c => c.Documento == Documento);
        }

        public string Eliminar()
        {

            try
            {   //Se consulta el cliente
                Cliente _cliente = Consultar(cliente.Documento);
                //El cliente se remueve de la lista de clientes
                dbInmobiliaria.Clientes.Remove(_cliente);
                // Guardamos en la base de datos 
                dbInmobiliaria.SaveChanges();
                return "Se eliminó el cliente con documento: " + cliente.Documento;
            }
            catch
            (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}