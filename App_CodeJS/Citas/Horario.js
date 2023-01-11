$(function () {
    var medicoId = "",
        fechaAtencion = null;
    var cita = {};
    var model = {

        ObtenerParametros: function () {
            vacunadorId = $("#IdVacundor").val();

            var temp = $("#fechaAtencion").val();

            fechaAtencion = temp.substr(6, 2) + "/" + temp.substr(4, 2) + "/" + temp.substr(0, 4);
        },

        ListarDatos: function () {

            util.Ajax("../../ListarCitasMedico", JSON.stringify({medicoId: medicoId, fechaAtencion: fechaAtencion }),
                function (data) {
                    var lista = [];
                    var evento = {};
                    if (data != null) {
                        lista = data;
                        console.log(lista);
                    }
                    else {
                        util.MsgAlert("No se encontraron registros");
                    }
                    $('#calendario').fullCalendar('addEventSource', lista)
                });
        },

        ObtenerCita: function (id) {
            util.Ajax("../../ObtenerCita", JSON.stringify({ citaId: id }),
                function (data) {
                    var item = data.obj;
                    model.LimpiarDatosFormulario();
                    if (item != null) {
                        $("#txtIdentificador").val(item.IdCita);
                        $("#txtEstado").val(item.EstadoCita);
                        debugger;
                        $("#txtObservacion").val(item.Observacion);
                        $("#txtIdCanino").val(item.IdCanino);
                        $("#txtNombre").val(item.Nombre);
                        

                        var f = new Date(parseInt(item.FechaAtencion.substr(6)));
                        var d = f.getDate();
                        var m = f.getMonth();
                        if (d < 10) d = '0' + d;
                        if (m < 10) m = '0' + m;
                        $("#txtFechaAtencion").datepicker("option", "dateFormat", "dd/mm/yy");
                        $("#txtFechaAtencion").datepicker("setDate", new Date(f.getFullYear(), m, d));

                        $("#txtHoraI").val(util.FormatJsonTimeSpan(item.HoraI));
                        $("#txtHoraF").val(util.FormatJsonTimeSpan(item.HoraF));
                        $("#formularioRegistrar").dialog("open");
                    }
                });
        },

        ConfigurarCalendario: function () {
            var date = new Date()
            var d = date.getDate(),
                m = date.getMonth(),
                y = date.getFullYear()
            $('#calendario').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                buttonText: {
                    today: 'Hoy',
                    month: 'Mes',
                    week: 'Semana',
                    day: 'Dia'
                },
                height: 500,
                editable: false,
                eventClick: function (calEvent, jsEvent, view) {
                    model.ObtenerCita(calEvent.citaId);
                }
            });
        },

        LimpiarDatosFormulario: function () {
            $("#txtIdentificador").val("-1");
            $("#txtIdCanino").val("-1");
            $("#txtEstado").val("PENDIENTE");
            $("#txtObservacion").val("");
            $("#txtNombre").val("");
            $("#txtFechaAtencion").val("");
            $("#txtHoraI").val("");
            $("#txtHoraF").val("");
        },

        NuevaCita: function () {
            model.LimpiarDatosFormulario();
            $("#formularioRegistrar").dialog("open");
        },

        Cancelar: function () {
            $("#formularioRegistrar").dialog("close");
        },

        RecogerDatosFormulario: function () {
            cita = {
                IdCita: $("#txtIdentificador").val(),
                IdVacunador: $("#IdVacunador").val(),
                Medico: $("#txtMedico").val(),
                PacienteId: $("#txtIdCanino").val(),
                Paciente: $("#txtNombre").val(),
                FechaAtencion: $("#txtFechaAtencion").val(),
                HoraInicio: $("#txtHoraI").val(),
                HoraFin: $("#txtHoraF").val(),
                Estado: $("#txtEstado").val(),
                Observaciones: $("#txtObservacion").val(),
                Activo: true
            };
            debugger;
        },

        Guardar: function () {
            model.RecogerDatosFormulario();

            util.Ajax("../../GrabarCita", JSON.stringify({ item: cita }),
                function (data) {
                    var resultado = data.obj;

                    if (resultado.Correcto == true) {
                        $('#calendario').fullCalendar('removeEvents');
                        model.ListarDatos();
                        model.LimpiarDatosFormulario();
                        if (cita.IdCita > 0) {
                            util.MsgInfo(resultado.Mensaje);
                        }
                        else {
                            util.MsgInfo(resultado.Mensaje);
                        }
                        $("#formularioRegistrar").dialog("close");
                    }
                    else {
                        util.MsgAlert(resultado.Mensaje);
                    }
                });
        },

        AutocompletarPaciente: function () {

            var asignaValores = function (object) {
       
                $("#txtIdCanino").val(object.IdCanino);
                $("#txtNombre").val(object.Nombre);
            };

            var asignaValoresEnBlanco = function (value) {
             
                $("#txtIdCanino").val("-1");
                $("#txtNombre").val("");
            };
            util.Autocompletar("txtIdCanino", 2, 150, '../../BuscarPaciente', asignaValores, null, null, asignaValoresEnBlanco);
        }, //Lineaa arriba -Buscarpaciente

        MarcarMenu: function () {

            $("#otpGestionCita")
                .addClass("active")
                .addClass("menu-open");

            $("#optRegistrarCita")
                .addClass("active");
        },

        Inicio: function () {
            this.MarcarMenu();

            this.ObtenerParametros();

            $("#btnNuevaCita").button();
            $("#btnNuevaCita").on("click", this.NuevaCita);

            $("#btnGuardar").button();
            $("#btnGuardar").on("click", this.Guardar);

            $("#btnCancelar").button();
            $("#btnCancelar").on("click", this.Cancelar);

            this.AutocompletarPaciente();

            //$("#txtDni").on("keyup", function () { return model.AutocompletarPaciente(); });

            $('#txtFechaAtencion').datepicker({ autoclose: true });

            util.Dialog("formularioRegistrar", 'Registrar Cita', 600, 'auto', 10, null, false);

            this.ConfigurarCalendario();
            this.ListarDatos();
            $("#contenidoPagina").css('display', 'block');
        }
    }

    model.Inicio();
});