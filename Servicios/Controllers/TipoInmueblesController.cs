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
    [Authorize]
    public class TipoInmueblesController : ApiController
    {
        // GET api/<controller>
        public List<TipoInmueble> Get()
        {
            clsTipoInmueble _tipoInmueble = new clsTipoInmueble();
            return _tipoInmueble.ConsultarTodos();
        }

        [HttpGet]
        [Route("api/TipoInmuebles/LlenarCombo")]
        public IQueryable LlenarCombo()
        {
            clsTipoInmueble _tipoInmueble = new clsTipoInmueble();
            return _tipoInmueble.LlenarCombo();
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}