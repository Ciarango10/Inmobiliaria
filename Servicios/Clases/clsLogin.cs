using Servicios.Models;
using Servicios.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsLogin
    {
        private DBInmobiliariaEntities dBInmobiliaria = new DBInmobiliariaEntities();
        public LoginRequest login { get; set; }
        public IQueryable<LoginRespuesta> Consultar()
        {
            if (Validar())
            {
                string tokenGenerado = TokenGenerator.GenerateTokenJwt(login.Usuario);
                return from U in dBInmobiliaria.Set<Usuario>()
                       join UP in dBInmobiliaria.Set<Usuario_Perfil>()
                       on U.IdUsuario equals UP.IdUsuario
                       join P in dBInmobiliaria.Set<Perfil>()
                       on UP.IdPerfil equals P.IdPerfil
                       where U.UserName == login.Usuario &&
                             U.Clave == login.Password

                       select new LoginRespuesta
                       {
                           Usuario = U.UserName,
                           Perfil = P.Nombre,
                           Token = tokenGenerado,
                           Autenticado = true,
                           PaginaNavegar = P.PaginaNavegar
                       };
            }
            else
            {
                return null;
            }
        }
        private bool Validar()
        {
            //Consulta el usuario
            Usuario usuario = dBInmobiliaria.Usuarios.FirstOrDefault(u=> u.UserName == login.Usuario);
            if (usuario == null)
            {
                return false;
            }
            else
            {
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);
                clsCypher cifrar = new clsCypher();

                string ClaveCifrada = cifrar.HashPassword(login.Password, arrBytesSalt);
                login.Password = ClaveCifrada;
                return true;
            }
        }
    }
}