var executaScriptsDaPagina = function () {

    $("#btn_excluir").click(function (e) {
        ConfirmaExclusao(function () {
            $.ajax({
                url: "/Produto/Excluir/" + $('#Id').val(),
            })
        });
        e.preventDefault();
    });

    setTimeout(function () { $('#Nome').focus() }, 100);
}


