$(document).ready(function () {
    $("#EstadoId").change(function () {
        $("#MunicipioId").empty();
        $("#MunicipioId").append('<option value="0">[--Selecciona un Municipio--]</option>');
        $.ajax({
            type: 'POST',
            url: Url,
            dataType: 'json',
            data: { EstadoId: $("#EstadoId").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#MunicipioId").append('<option value="'
                        + data.MunicipioId + '">'
                        + data.Nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Fallo la consulta de municipios.' + ex);
            }
        });
        return false;
    })
});

$(document).ready(function () {
    $("#MunicipioId").change(function () {
        $("#LocalidadId").empty();
        $("#LocalidadId").append('<option value="0">[--Selecciona una Localidad--]</option>');
        $.ajax({
            type: 'POST',
            url: Urlc,
            dataType: 'json',
            data: { MunicipioId: $("#MunicipioId").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#LocalidadId").append('<option value="'
                        + data.LocalidadId + '">'
                        + data.Nombre + '</option>');
                });
            },
            error: function (ex) {
                alert('Fallo la consulta de Localidades.' + ex);
            }
        });
        return false;
    })
});