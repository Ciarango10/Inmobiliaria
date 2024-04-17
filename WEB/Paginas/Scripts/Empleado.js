jQuery(function () {
    //Registrar los botones para responder al evento click
    $("#dvMenu").load("../Paginas/Menu.html");

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

async function Consultar() {
    let Documento = $("#txtDocumento").val();
    try {
        const Respuesta = await fetch(`https://localhost:44340/api/Empleados?Documento=${Documento}`,
            {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                }
            });
        //Leer la respuesta y presentarla en el div
        const Resultado = await Respuesta.json();
        //La respuesta se escribe en los campos
        $("#txtNombre").val(Resultado.Nombre);
        $("#txtApellido").val(Resultado.Apellido);
        $("#txtEmail").val(Resultado.Email);
        $("#txtTelefono").val(Resultado.Telefono);
        $("#dtFechaNacimiento").val(Resultado.FechaNacimiento);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }
}

async function EjecutarComando(Comando) {

    //Capturar los datos de entrada
    let Documento = $("#txtDocumento").val();
    let Nombre = $("#txtNombre").val();
    let Apellido = $("#txtApellido").val();
    let Email = $("#txtEmail").val();
    let Telefono = $("#txtTelefono").val();
    let FechaNacimiento = $("#dtFechaNacimiento").val();

    //Construir la estructura JSON para enviar la informacion al servidor
    let DatosEmpleado = {
        Documento: Documento,
        Nombre: Nombre,
        Apellido: Apellido,
        Email: Email,
        Telefono: Telefono,
        FechaNacimiento: FechaNacimiento,
    }

    //Invocar la función fetch para grabar en la base de datos, a través del servicio
    //La función fetch de javascript, permite invocar un servicio en la nube
    //fetch("Ruta del servicio", Opciones para la ejecución: Comando que se ejecuta: POST, PUT, GET, DELETE, modo de conexión,
    //el tipo de dato que se envía, los datos)
    //En el body, se envían los datos al servicio, en formato json
    //Javascript, tiene una librería JSON, que permite convertir la variable en formato json
    try {
        const Respuesta = await fetch("https://localhost:44340/api/Empleados",
            {
                method: Comando,
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(DatosEmpleado)

            });
        //Leer la respuesta y presentarla en el div
        const Resultado = await Respuesta.json();
        $("#dvMensaje").html(Resultado);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }
    
}