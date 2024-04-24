var oTabla = $("#tblInmuebles").DataTable();
jQuery(function () {
    //Registrar los botones para responder al evento click
    $("#dvMenu").load("../Paginas/Menu.html");

    //Llenar la tabla de Inmuebles
    LlenarTablaInmuebles();

    //Llenar el combo de Ciudades
    LlenarComboCiudades();

    //Llenar el combo de TipoInmuebles
    LlenarComboTipoInmuebles();


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

async function LlenarTablaInmuebles() {
    LlenarTablaXServicios("https://localhost:44340/api/Inmuebles/ConsultarConTodo", "#tblInmuebles");
}

async function LlenarComboCiudades() {
    //Debe ir a la base de datos y llenar la información del combo de tipo producto
    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Ciudades",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json"
                }
            });
        const Rpta = await Respuesta.json();
        //Se debe limpiar el combo
        $("#cboCiudad").empty();
        //Se recorre en un ciclo para llenar el select con la información
        for (i = 0; i < Rpta.length; i++) {
            $("#cboCiudad").append('<option value=' + Rpta[i].IdCiudad + '>' + Rpta[i].Nombre + '</option>');
        }
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").html(error);
    }
}

async function LlenarComboTipoInmuebles() {
    //Debe ir a la base de datos y llenar la información del combo de tipo producto
    //Invocamos el servicio a través del fetch, usando el método fetch de javascript
    try {
        const Respuesta = await fetch("https://localhost:44340/api/TipoInmuebles",
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json"
                }
            });
        const Rpta = await Respuesta.json();
        //Se debe limpiar el combo
        $("#cboTipoInmueble").empty();
        //Se recorre en un ciclo para llenar el select con la información
        for (i = 0; i < Rpta.length; i++) {
            $("#cboTipoInmueble").append('<option value=' + Rpta[i].idTipoInmueble + '>' + Rpta[i].Tipo+ '</option>');
        }
    }
    catch (error) {
        //Se presenta la respuesta en el div mensaje
        $("#dvMensaje").html(error);
    }
}


async function Consultar() {

    let idInmueble = $("#txtIdInmueble").val();
    try {
        const Respuesta = await fetch(`https://localhost:44340/api/Inmuebles?IdInmueble=${IdInmueble}`,
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                }
            });
        //Leer la respuesta y presentarla en el div
        const Resultado = await Respuesta.json();
        console.log(Resultado);
        //La respuesta se escribe en los campos
        $("#txtDireccion").val(Resultado.Direccion);
        $("#txtPrecio").val(Resultado.Precio);
        $("#cboCiudad").val(Resultado.Id_Ciudad);
        $("#cboTipoInmueble").val(Resultado.Id_TipoInmueble);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }

}

async function EjecutarComando(Comando) {

    //Capturar los datos de entrada
    let IdInmueble = $("#txtIdInmueble").val();
    let Direccion = $("#txtDireccion").val();
    let Precio = $("#txtPrecio").val();
    let Id_Ciudad = $("#cboCiudad").val();
    let Id_TipoInmueble = $("#cboTipoInmueble").val();

    if (Id_Ciudad == 0) {
        $("#dvMensaje").html("Debe elegir una ciudad");
        return;
    }

    if (Id_TipoInmueble == 0) {
        $("#dvMensaje").html("Debe elegir un tipo de inmueble");
        return;
    }


    //Construir la estructura JSON para enviar la informacion al servidor
    let DatosInmueble = {
        IdInmueble: IdInmueble,
        Direccion: Direccion,
        Precio: Precio,
        Id_Ciudad: Id_Ciudad,
        Id_TipoInmueble: Id_TipoInmueble,
    }

    //Invocar la función fetch para grabar en la base de datos, a través del servicio
    //La función fetch de javascript, permite invocar un servicio en la nube
    //fetch("Ruta del servicio", Opciones para la ejecución: Comando que se ejecuta: POST, PUT, GET, DELETE, modo de conexión,
    //el tipo de dato que se envía, los datos)
    //En el body, se envían los datos al servicio, en formato json
    //Javascript, tiene una librería JSON, que permite convertir la variable en formato json
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Inmuebles",
            {
                method: Comando,
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(DatosInmueble)

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