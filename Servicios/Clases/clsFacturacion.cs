using Servicios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios_Jueves.Clases
{
    public class clsFacturacion
    {
        private DBInmobiliariaEntities dbInmobiliaria = new DBInmobiliariaEntities();
        public  Factura factura { get; set; }
        public  DetalleFactura detalleFactura { get; set; }
        public Factura GrabarFactura()
        {
            factura.Numero = CalcularNumeroFactura();
            factura.Fecha = DateTime.Now;
            dbInmobiliaria.Facturas.Add(factura);
            dbInmobiliaria.SaveChanges();
            return factura;
        }
        private int CalcularNumeroFactura()
        {
            return dbInmobiliaria.Facturas.Select(f => f.Numero).DefaultIfEmpty(0).Max() + 1;
        }
        public string GrabarDetalle()
        {
            dbInmobiliaria.DetalleFacturas.Add(detalleFactura);
            dbInmobiliaria.SaveChanges();
            return detalleFactura.Numero.ToString();
        }
        public IQueryable LlenarTablaFactura(int Numero)
        {
            return from F in dbInmobiliaria.Set<Factura>()
                   join DF in dbInmobiliaria.Set<DetalleFactura>()
                   on F.Numero equals DF.Numero
                   join I in dbInmobiliaria.Set<Inmueble>()
                   on DF.CodigoInmueble equals I.IdInmueble
                   join TI in dbInmobiliaria.Set<TipoInmueble>()
                   on I.Id_TipoInmueble equals TI.IdTipoInmueble
                   where F.Numero == Numero
                   select new
                   {
                       Eliminar = "<button type=\"button\" id=\"btnEliminar\" class=\"btn-block btn-sm btn-danger\" " +
                                "onclick=\"Eliminar(" + DF.Codigo + ")\">ELIMINAR</button>",
                       Cod_tipo_inmueble = TI.IdTipoInmueble,
                       Tipo_Inmueble= TI.Tipo,
                       Codigo = I.IdInmueble,
                       Dirección = I.Direccion,
                       Cantidad = DF.Cantidad,
                       ValorUnitario = DF.ValorUnitario
                   };
        }
        public string Eliminar(int idCodigoDetalleFactura)
        {
            DetalleFactura dEtalleFActura = dbInmobiliaria.DetalleFacturas.FirstOrDefault(d => d.Codigo == idCodigoDetalleFactura);
            dbInmobiliaria.DetalleFacturas.Remove(dEtalleFActura);
            dbInmobiliaria.SaveChanges();
            return "Se eliminó el detalle";
        }
    }
}