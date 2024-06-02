var oTabla = $("#tblVisitas").DataTable();
let Token = getCookie("token");

jQuery(function () {
    //Registrar los botones para responder al evento click
    $("#dvMenu").load("../Paginas/Menu.html");

    //Llenar la tabla de visitas
    LlenarTablaVisitas();

    //Llenar el combo de Empleados
    LlenarComboEmpleados();

    //Llenar el combo de Clientes
    LlenarComboClientes();

    //Llenar el combo de Inmuebles
    LlenarComboInmuebles()

    //Levantar el evento click del boton insertar
    $("#btnInsertar").on("click", () => {
        EjecutarComando("POST");
    });

    $("#btnActualizar").on("click", () => {
        EjecutarComando("PUT");
    });

    $("#btnEliminar").on("click", () => {
        EjecutarComando("DELETE");
    });

    $("#btnConsultar").on("click", () => {
        Consultar();
    });
});

async function LlenarTablaVisitas() {
    LlenarTablaServiciosAuth("https://localhost:44340/api/Visitas/ConsultarConTodo", "#tblVisitas");
}

async function LlenarComboEmpleados() {
    //Debe ir a la base de datos y llenar la información del combo de tipo producto
    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Empleados",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                }
            });
        const Rpta = await Respuesta.json();
        //Se debe limpiar el combo
        $("#cboEmpleado").empty();
        //Se recorre en un ciclo para llenar el select con la información
        for (i = 0; i < Rpta.length; i++) {
            $("#cboEmpleado").append('<option value=' + Rpta[i].Documento + '>' + Rpta[i].Nombre + " " + Rpta[i].PrimerApellido + '</option>');
        }
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html(error);
    }
}

async function LlenarComboClientes() {
    //Debe ir a la base de datos y llenar la información del combo de tipo producto
    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Clientes",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                }
            });
        const Rpta = await Respuesta.json();
        //Se debe limpiar el combo
        $("#cboCliente").empty();
        //Se recorre en un ciclo para llenar el select con la información
        for (i = 0; i < Rpta.length; i++) {
            $("#cboCliente").append('<option value=' + Rpta[i].Documento + '>' + Rpta[i].Nombre + " " + Rpta[i].PrimerApellido + '</option>');
        }
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html(error);
    }
}

async function LlenarComboInmuebles() {
    //Debe ir a la base de datos y llenar la información del combo de tipo producto
    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Inmuebles",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                }
            });
        const Rpta = await Respuesta.json();
        //Se debe limpiar el combo
        $("#cboInmueble").empty();
        //Se recorre en un ciclo para llenar el select con la información
        for (i = 0; i < Rpta.length; i++) {
            $(cboInmueble).append('<option value=' + Rpta[i].IdInmueble + '>' + Rpta[i].Direccion + '</option>');
        }

        $("#cboInmueble").change(function () {
            // Obtenemos la opcion seleccionada
            var selectedOption = $(this).children("option:selected").val();

            // Encontramos el objeto en el data
            var selectedObject = Rpta.find(obj => obj.IdInmueble == selectedOption);

            // Actualizamos el valor de los inputs
            if (selectedObject) {
                $("#txtTipoInmueble").val(selectedObject.TipoInmueble.Tipo);
                $("#txtCiudad").val(selectedObject.Ciudad.Nombre);
                $("#txtPrecio").val(selectedObject.Precio);
                $("#txtArea").val(selectedObject.Area);
                $("#txtNroHabitaciones").val(selectedObject.NroHabitaciones);
                $("#txtNroBaños").val(selectedObject.NroBaños);
                $("#txtCaracteristicas").val(selectedObject.Caracteristicas);
            } else {
                // Si ningun objeto esta seleccionado se limpian los inputs
                $("#txtTipoInmueble").val("");
                $("#txtCiudad").val("");
                $("#txtPrecio").val("");
                $("#txtArea").val("");
                $("#txtNroHabitaciones").val("");
                $("#txtNroBaños").val("");
                $("#txtCaracteristicas").val("");
            }
        });

        // Disparar manualmente el evento change
        $("#cboInmueble").trigger('change');
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html(error);
    }
}

async function Consultar() {

    let IdVisita = $("#txtIdVisita").val();
    try {
        const Respuesta = await fetch(`https://localhost:44340/api/Visitas?IdVisita=${IdVisita}`,
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                }
            });
        //Leer la respuesta y presentarla en el div
        const Resultado = await Respuesta.json();
        //La respuesta se escribe en los campos
        $("#dtFechaVisita").val(new Date(Resultado.Fecha).toISOString().split('T')[0]);
        $("#txtComentarios").val(Resultado.Comentarios);
        $("#cboCliente").val(parseInt(Resultado.Documento_Cliente));
        $("#cboEmpleado").val(parseInt(Resultado.Documento_Empleado));
        $("#cboInmueble").val(Resultado.Id_Inmueble);

        // Disparar manualmente el evento change
        $("#cboInmueble").trigger('change');
    }
    catch (error) {
        $("#dvMensaje").removeClass("alert alert-success");
        $("#dvMensaje").addClass("alert alert-danger");
        $("#dvMensaje").html(error);
    }

}

async function EjecutarComando(Comando) {

    //Capturar los datos de entrada
    let IdVisita = $("#txtIdVisita").val();
    let Fecha = $("#dtFechaVisita").val();
    let Comentarios = $("#txtComentarios").val();
    let Documento_Cliente = $("#cboCliente").val();
    let Documento_Empleado = $("#cboEmpleado").val();
    let Id_Inmueble = $("#cboInmueble").val();

    if (Documento_Cliente == 0) {
        $("#dvMensaje").html("Debe elegir un cliente");
        return;
    }

    if (Documento_Empleado == 0) {
        $("#dvMensaje").html("Debe elegir un empleado");
        return;
    }

    if (Id_Inmueble == 0) {
        $("#dvMensaje").html("Debe elegir un inmueble");
        return;
    }

    //Construir la estructura JSON para enviar la informacion al servidor
    let DatosVisita = {
        IdVisita: IdVisita,
        Fecha: Fecha,
        Comentarios: Comentarios,
        Documento_Cliente: Documento_Cliente,
        Documento_Empleado: Documento_Empleado,
        Id_Inmueble: Id_Inmueble,
    }

    //Invocar la función fetch para grabar en la base de datos, a través del servicio
    //La función fetch de javascript, permite invocar un servicio en la nube
    //fetch("Ruta del servicio", Opciones para la ejecución: Comando que se ejecuta: POST, PUT, GET, DELETE, modo de conexión,
    //el tipo de dato que se envía, los datos)
    //En el body, se envían los datos al servicio, en formato json
    //Javascript, tiene una librería JSON, que permite convertir la variable en formato json
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Visitas",
            {
                method: Comando,
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + Token
                },
                body: JSON.stringify(DatosVisita)

            });
        //Leer la respuesta y presentarla en el div
        const Resultado = await Respuesta.json();
        LlenarTablaVisitas();
        $("#dvMensaje").html(Resultado);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }

}