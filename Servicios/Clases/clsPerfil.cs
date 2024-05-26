using Servicios.Models;
using System.Linq;

namespace Servicios.Controllers
{
    public class clsPerfil
    {
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();
        public IQueryable ListarPerfiles()
        {
            return from P in dbInmobiliaria.Set<Perfil>()
                   select new
                   {
                       Codigo = P.IdPerfil,
                       Nombre = P.Nombre
                   };
        }
    }
}