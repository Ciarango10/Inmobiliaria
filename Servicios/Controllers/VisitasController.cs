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

    public class VisitasController : ApiController
    {
        // GET sin parametros, -- Invoca el ConsultarTodos
        public List<Visita> Get()
        {
            clsVisita _visita = new clsVisita();
            return _visita.ConsultarTodos();
        }

        // GET -- Invoca el Consultar con un parametro: IdVisita
        public Visita Get(int IdVisita)
        {
            clsVisita _visita = new clsVisita();
            return _visita.Consultar(IdVisita);
        }

        [HttpGet]
        [Route("api/Visitas/ConsultarConTodo")]
        public IQueryable ConsultarConTodo()
        {
            clsVisita _visita = new clsVisita();
            return _visita.ConsultarConTodo();
        }

        // POST -- Invoca el metodo Insertar
        public string Post([FromBody] Visita visita)
        {
            clsVisita _visita = new clsVisita();
            _visita.visita = visita;
            return _visita.Insertar();
        }

        // PUT -- Invoca el metodo Actualizar
        public string Put([FromBody] Visita visita)
        {
            clsVisita _visita = new clsVisita();
            _visita.visita = visita;
            return _visita.Actualizar();
        }

        // DELETE -- Invoca el metodo Eliminar
        public string Delete([FromBody] Visita visita)
        {
            clsVisita _visita = new clsVisita();
            _visita.visita = visita;
            return _visita.Eliminar();
        }
    }
}