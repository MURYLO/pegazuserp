//não está mais sendo usado.
$(document).ready(function () {
    $("#grid").on('click', 'tbody tr', function (e) {
        var url = $(this).find('td:not(:empty):first').find(':hidden').val();
        if (url != undefined && url != null && url != '') {
            CarregarPaginaAjax(url);
        }
    });
});