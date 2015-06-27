var executaScriptsDaPagina = function () {

    $("#btn_excluir").click(function (e) {
        ConfirmaExclusao(function () {
            $.ajax({
                url: "/MarcaProduto/Excluir/" + $('#Id').val(),
            })
        });
        e.preventDefault();
    });

    setTimeout(function () { $('#Nome').focus() }, 100);
}


