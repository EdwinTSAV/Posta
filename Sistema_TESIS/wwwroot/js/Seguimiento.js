
function seguimiento() {
    $("#seguimiento").html('<p>aqui ira el seguimiento</p>');
}
$(document).ready(function () {
    //var estado = $('.badge').attr("id");
    //if (estado == "Positivo") {
    //    seguimiento();
    //}
    var idPersona = $('#hClinId').val();
    function historial() {
        //$.ajax({
        //    url: '/Prueba/index?historiaClinicaId=' + historiaclinica
        //}).done(function (e) {
        //    $('#Pruebas').html(e);
        //});
        $.ajax({
            url: '/CuadroClinico/index?idUser=' + idPersona
        }).done(function (x) {
            $('#CuadroClinico').html(x);
        });
        //$.ajax({
        //    url: '/Seguimiento/Index?historiaClinicaId='+historiaclinica
        //}).done(function (e) {
        //    $('#Contactos').html(e);
        //});
        
    }
    historial();
    
});