
// exemplo de como usar token armazenado no localstorage com jquery
$(document).ready(function () {
    //resgatando token armazenado
    var token = localStorage.getItem('jtoken');

    $.ajax({
        url: 'http://localhost/api/values',
        type: 'get',
        contentType: 'application/json',
        headers: {
            "Authorization" : "Bearer " + token
        }
    })
    .done(function(data){
        //comandos para manipular dos dados passados pela Api    

    })
    .erro(function () {
        // mostrar erro para usuasrio
    })


});