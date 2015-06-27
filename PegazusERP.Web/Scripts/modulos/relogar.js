var returnUrl = '';
var tentativasLogin = 0;

var modalRelogar = function (msg) {

    if (msg == undefined || msg == null) {
        msg = '';
    }
    else {
        msg = '<span style="color:red;"><strong>' + msg + '</strong></span> ';
    }

    setTimeout(function () { mostrarModalRelogar(msg); }, 250);
}

var mostrarModalRelogar = function (msg) {
    $.SmartMessageBox({
        title: '<span class="MsgTitle"><i class="fa fa-sign-in txt-color-orangeDark"></i> Sua sessão <span class="txt-color-orangeDark"><strong>expirou</strong></span> !</span>',
        content: msg + 'Entre com sua senha novamente para continuar no sistema',
        buttons: "[Sair][Entrar]",
        input: "password",
        placeholder: "Digite sua senha"
    }, function (ButtonPress, Value) {

        if (ButtonPress == "Sair") {
            window.location = '/Acesso/Sair';
            return 0;
        }

        processaRelogar(Value);
    });
}

var processaRelogar = function (Value) {
    tentativasLogin = tentativasLogin + 1;

    if (tentativasLogin >= 3) {
        window.location = '/Acesso/Sair';
        return 0;
    }

    if (Value == "") {
        modalRelogar('Digite sua senha.');
    }
    else {
        var usuario = $('#loginUsuario').val();
        $.ajax({
            type: "POST",
            url: "/Acesso/POSTEntrarAjax",
            data: { usuario: usuario, senha: Value, returnUrl: returnUrl },
            cache: false
        }).done(function (data) {
            if (data.sucesso) {
                CarregarPaginaAjax(data.returnUrl);
            } else {
                modalRelogar(data.erro);
            }
        });
    }
}

var definaReturnUrl = function (url) {
    returnUrl = url;
}

modalRelogar();