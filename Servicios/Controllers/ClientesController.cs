using Servicios.Clases;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Servicios.Controllers
{
    [EnableCors(origins: "https://localhost:44320", headers: "*", methods: "*")]
    public class ClientesController : ApiController
    {
        // GET sin parámetros -- Invoca el consultar todos
        public List<Cliente> Get()
        {
            clsCliente _cliente = new clsCliente();
            return _cliente.ConsultarTodos();
        }

        // GET -- Invoca el consultar con un parámetro: Documento
        public Cliente Get(string Documento)
        {
            clsCliente _cliente = new clsCliente();
            return _cliente.Consultar(Documento);
        }

        // POST -- Invoca el método insertar 
        public string Post([FromBody] Cliente cliente)
        {
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = cliente;
            return _cliente.Insertar();
        }

        // PUT -- Invoca el método actualizar
        public string Put([FromBody] Cliente cliente)
        {
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = cliente;
            return _cliente.Actualizar();
        }

        // DELETE -- Invoca el método Eliminar
        public string Delete([FromBody] Cliente cliente)
        {
            clsCliente _cliente = new clsCliente();
            _cliente.cliente = cliente;
            return _cliente.Eliminar();
        }
    }
}