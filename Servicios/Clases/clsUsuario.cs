using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios.Clases
{
    public class clsUsuario
    {
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();
        public Usuario usuario { get; set; }

        public string Insertar(int idPerfil)
        {
            try
            {
                //Instanciamos la clase de cifrado
                clsCypher Cifrado = new clsCypher();
                //Pasamos la contraseña del usuario
                Cifrado.Password = usuario.Clave;
                //Ciframos la clave
                if (Cifrado.CifrarClave())
                {
                    //Si el cifrado es exitoso pasamos la contraseña cifrada al usuario nuevamente
                    usuario.Clave = Cifrado.PasswordCifrado;
                    usuario.Salt = Cifrado.Salt;
                    //Para grabar se utiliza el metodo Add
                    dbInmobiliaria.Usuarios.Add(usuario);
                    //Para garantizar la grabacion en la base de datos, se invoca el metodo SaveChanges
                    dbInmobiliaria.SaveChanges();

                    Usuario_Perfil usuario_Perfil = new Usuario_Perfil();
                    usuario_Perfil.IdPerfil = idPerfil;
                    usuario_Perfil.Activo = true;
                    usuario_Perfil.IdUsuario = usuario.IdUsuario;
                    dbInmobiliaria.Usuario_Perfil.Add(usuario_Perfil);
                    dbInmobiliaria.SaveChanges();

                    return "Se ingresó el usuario: " + usuario.UserName;
                }
                else { return "No se pudo generar la clave del usuario"; }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Actualizar(int idPerfil)
        {
            try
            {
                //Consulta el usuario
                Usuario _usuario = dbInmobiliaria.Usuarios.FirstOrDefault(u => u.Documento_Empleado == usuario.Documento_Empleado);
                _usuario.UserName = usuario.UserName;
                dbInmobiliaria.Usuarios.AddOrUpdate(_usuario);
                dbInmobiliaria.SaveChanges();
                Usuario_Perfil usuarioPerfil = dbInmobiliaria.Usuario_Perfil.FirstOrDefault(p => p.IdUsuario == _usuario.IdUsuario);
                usuarioPerfil.IdPerfil = idPerfil;
                dbInmobiliaria.Usuario_Perfil.AddOrUpdate(usuarioPerfil);
                dbInmobiliaria.SaveChanges();
                return "Se actualizó el usuario";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Activar(int idUsuarioPerfil, bool activo)
        {
            try
            {
                Usuario_Perfil usuarioPerfil = dbInmobiliaria.Usuario_Perfil.FirstOrDefault(u => u.IdUsuarioPerfil == idUsuarioPerfil);
                usuarioPerfil.Activo = activo;
                dbInmobiliaria.Usuario_Perfil.AddOrUpdate(usuarioPerfil);
                dbInmobiliaria.SaveChanges();
                return "Se actualizó el estado del usuario";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ResetearClave()
        {
            //Instanciamos la clase de cifrado
            clsCypher Cifrado = new clsCypher();
            //Pasamos la contraseña del usuario
            Cifrado.Password = usuario.Clave;
            //Ciframos la clave
            if (Cifrado.CifrarClave())
            {
                Usuario _usuario = dbInmobiliaria.Usuarios.FirstOrDefault(u => u.Documento_Empleado == usuario.Documento_Empleado);
                //Si el cifrado es exitoso pasamos la contraseña cifrada al usuario nuevamente
                _usuario.Clave = Cifrado.PasswordCifrado;
                _usuario.Salt = Cifrado.Salt;
                dbInmobiliaria.Usuarios.AddOrUpdate(_usuario);
                dbInmobiliaria.SaveChanges();
                return "Se reseteó la clave del usuario";
            } 
            else
            {
                return "No se pudo resetear la clave del usuario";
            }
        }

        public IQueryable ListarUsuariosEmpleados()
        {
            return from E in dbInmobiliaria.Set<Empleado>()
                   join EC in dbInmobiliaria.Set<EmpleadoCargo>()
                   on E.Documento equals EC.Documento_Empleado
                   join C in dbInmobiliaria.Set<Cargo>()
                   on EC.Codigo_Cargo equals C.Codigo
                   join U in dbInmobiliaria.Set<Usuario>()
                   on E.Documento equals U.Documento_Empleado
                   join UP in dbInmobiliaria.Set<Usuario_Perfil>()
                   on U.IdUsuario equals UP.IdUsuario
                   join P in dbInmobiliaria.Set<Perfil>()
                   on UP.IdPerfil equals P.IdPerfil
                   select new
                   {
                       Editar = "<button type=\"button\" id=\"btnEdit\" class=\"btn-block btn-sm btn-danger\" " +
                                 "onclick=\"Editar('" + E.Documento + "', '" + E.Nombre + " " + E.PrimerApellido + " " +
                                 E.SegundoApellido + "', '" + C.Nombre + "', '" + U.UserName + "', " + UP.IdPerfil + "," + UP.IdUsuarioPerfil + "," + UP.Activo.ToString().ToLower() + ")\">EDIT</button>",
                       Documento = E.Documento,
                       Empleado = E.Nombre + " " + E.PrimerApellido + " " + E.SegundoApellido,
                       Cargo = C.Nombre,
                       Usuario = U.UserName,
                       Perfil = P.Nombre,
                       Activo = UP.Activo ? "ACTIVO" : "INACTIVO"
                   };
        }
    }
}