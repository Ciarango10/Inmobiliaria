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

    public class InmueblesController : ApiController
    {
        // GET api/<controller>
        public List<Inmueble> Get()
        {
            clsInmueble _inmueble = new clsInmueble();
            return _inmueble.ConsultarTodos();
        }

        // GET -- Invoca el Consultar con un parametro: IdInmueble
        public Inmueble Get(int idInmueble)
        {
            clsInmueble _inmueble = new clsInmueble();
            return _inmueble.Consultar(idInmueble);
        }

        [HttpGet]
        [Route("api/Inmuebles/ConsultarConTodo")]
        public IQueryable ConsultarConTodo()
        {
            clsInmueble _inmueble = new clsInmueble();
            return _inmueble.ConsultarConTodo();
        }

        // POST -- Invoca el método insertar 
        public string Post([FromBody] Inmueble inmueble)
        {
            clsInmueble _inmueble = new clsInmueble();
            _inmueble.inmueble = inmueble;
            return _inmueble.Insertar();
        }

        // PUT -- Invoca el metodo Actualizar
        public string Put([FromBody] Inmueble inmueble)
        {
            clsInmueble _inmueble = new clsInmueble();
            _inmueble.inmueble = inmueble;
            return _inmueble.Actualizar();

        }

        // DELETE -- Invoca el metodo Eliminar
        public string Delete([FromBody] Inmueble inmueble)
        {
            clsInmueble _inmueble = new clsInmueble();
            _inmueble.inmueble = inmueble;
            return _inmueble.Eliminar();
        }
    }
}