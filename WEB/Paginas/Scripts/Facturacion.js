jQuery(function () {
    $("#dvMenu").load("../Paginas/Menu.html");
    LLenarComboEmpleados();
    LLenarComboTipoInmueble();
    $("#txtNumeroFactura").val("0");
});

function LLenarComboEmpleados() {
    LlenarComboServiciosAuth("https://localhost:44340/api/Empleados/LlenarCombo", "#cboEmpleado");
}

function LlenarTablaFactura() {
    let NumeroFactura = $("#txtNumeroFactura").val();
    LlenarTablaServiciosAuth("https://localhost:44340/api/Facturacion/LlenarTablaFacturacion?NumeroFactura=" + NumeroFactura, "#tblFactura");
}

async function LLenarComboTipoInmueble() {
    let rpta = await LlenarComboServiciosAuth("https://localhost:44340/api/TipoInmuebles/LlenarCombo", "#cboTipoInmueble");
    LlenarComboInmueble();
}

async function LlenarComboInmueble() {
    let idTipoInmueble = $("#cboTipoInmueble").val();
    let rpta = LlenarComboServiciosAuth("https://localhost:44340/api/Inmuebles/LlenarCombo?idTipoInmueble=" + idTipoInmueble, "#cboInmueble");
    PresentarValorUnitario();
}

function PresentarValorUnitario() {
    let DatosInmueble = $("#cboInmueble").val();
    let CodigoInmueble = DatosInmueble.split('|')[0];
    let ValorUnitario = DatosInmueble.split('|')[1];

    $("#txtValorUnitarioTexto").val(FormatoMiles(ValorUnitario));
    $("#txtValorUnitario").val(ValorUnitario);
    $("#txtCodigoInmueble").val(CodigoInmueble);
    CalcularSubtotal();
}

function GrabarFactura() {
    $("#txtNumeroFactura").val("0");
    $("#txtDocumento").val("");
    $("#txtNombreCliente").val("");
    $("#txtFechaCompra").val("")
    $("#txtTotalCompra").val("");
    var table = new DataTable('#tblFactura');
    table.clear().draw();
}

function CalcularSubtotal() {
    let ValorUnitario = $("#txtValorUnitario").val();
    let Cantidad = $("#txtCantidad").val();
    $("#txtSubtotal").val(FormatoMiles(Cantidad * ValorUnitario));
}

async function Consultar() {
    //Solo se captura la información del documento del empleado y se invoca el servicio
    let Documento = $("#txtDocumento").val();
    //Fetch para grabar en la base de datos
    try {
        let Token = getCookie("token");
        const Respuesta = await fetch("https://localhost:44340/api/Clientes?Documento=" + Documento,
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                }
            });
        //Se lee la respuesta y se convierte a json
        const Resultado = await Respuesta.json();
        //Las respuestas se escriben en el html
        $("#txtNombreCliente").val(Resultado.Nombre + " " + Resultado.PrimerApellido + " " + Resultado.SegundoApellido);
    }
    catch (error) {
        //Se presenta el error en el "dvMensaje" de la interfaz
        $("#dvMensaje").html(error);
    }
}

async function GrabarInmueble() {
    let NumeroFactura = $("#txtNumeroFactura").val();
    let Documento = $("#txtDocumento").val();
    let idEmpleado = $("#cboEmpleado").val();
    let CodigoInmueble = $("#txtCodigoInmueble").val();
    let Cantidad = $("#txtCantidad").val();
    let ValorUnitario = $("#txtValorUnitario").val();

    let DatosFactura = {
        Numero: NumeroFactura,
        Documento: Documento,
        Fecha: "2024/01/01",
        CodigoEmpleado: idEmpleado
    }
    let Token = getCookie("token");
    if (NumeroFactura == 0) {
        //Se graba el encabezado
        try {
            const Respuesta = await fetch("https://localhost:44340/api/Facturacion/GrabarEncabezado",
                {
                    method: "POST",
                    mode: "cors",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + Token
                    },
                    body: JSON.stringify(DatosFactura)
                });
            //Leer la respuesta del servicio
            const Resultado = await Respuesta.json();
            NumeroFactura = Resultado.Numero;
            $("#txtNumeroFactura").val(NumeroFactura);
            $("#txtFechaCompra").val(new Date(Resultado.Fecha).toISOString().split('T')[0])
            $("#txtTotalCompra").val(ValorUnitario * Cantidad);
        }
        catch (_error) {
            //Presentar a respuesta del error en el html
            $("#dvMensaje").html(_error);
        }
    }
    //Se graba el detalle
    let DatosDetalleFactura = {
        Numero: NumeroFactura,
        CodigoInmueble: CodigoInmueble,
        Cantidad: Cantidad,
        ValorUnitario: ValorUnitario
    }

    try {
        const Respuesta = await fetch("https://localhost:44340/api/Facturacion/GrabarDetalle",
            {
                method: "POST",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': 'Bearer ' + Token
                },
                body: JSON.stringify(DatosDetalleFactura)
            });
        //Leer la respuesta del servicio
        const Resultado = await Respuesta.json();
        LlenarTablaFactura();
    }
    catch (_error) {
        //Presentar a respuesta del error en el html
        $("#dvMensaje").html(_error);
    }
}

async function Eliminar(idDetalle) {
    let Token = getCookie("token");
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Facturacion/EliminarDetalle?idDetalleFactura=" + idDetalle,
            {
                method: "DELETE",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': 'Bearer ' + Token
                }
            });
        //Leer la respuesta del servicio
        const Resultado = await Respuesta.json();
        LlenarTablaFactura();
    }
    catch (_error) {
        //Presentar a respuesta del error en el html
        $("#dvMensaje").html(_error);
    }
}