$(document).ready(function () {
    //limpa o Token toda vez que a aplicação carrega
    localStorage.setItem('jtoken', '');

    $().on('click', function () {

        //parametros  requisição do Token
        var data = {

            grant_type: 'password',
            username: $("#campoUsuario").val(),
            password: $("campoSenha").val()
        }

        $.ajax({
            url: "http://localhost/api/security/token",
            type: 'post',
            contentType: 'x-www-form-urlencoded',
            data: data
        })
        .done(function (data) {
            //armazena o token no cookie do endereço
            localStorage.setItem('jtoken', data.acces_token);
        })
        .erro(function (data) {
            //comandos para mostrar uma messagem de erro para o usuario.
        });

    });


});