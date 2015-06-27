$(function () {

    // CarregarDadosEscola();

    $('#EscolaId').change(function () {
        CarregarDadosEscola();
    });

    $("#toggleAllCheckBox").click(function () {

        if ($(this).attr("checked")) {
            $(".check-box").attr("checked", true);
        } else {
            $(".check-box").attr("checked", false);
        }

    });

    $('#frmEditarDesativacaoAlunos').submit(function () {

        var chklist = new generic.list();

        $('.check-box').each(function (i) {

            if (this.checked) {
                chklist.add(this.value);
            } else {
                chklist.remove(this.value);
            }

        });

        $('#alunoIds').val(chklist.join(";"));

        return true;
    });

});

function CarregarDadosEscola() {

    var escolaId = $('#EscolaId').val();

    if (escolaId > 0) {
        $.getJSON('/Turno/GetTurnosByEscola?escolaId=' + escolaId, function (j) {
            var options = '<option value="">Todos os turnos</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].id + '">' + j[i].text + '</option>';
            }
            $('#TurnoId').html(options).show();
        });
    } else {
        $('#TurnoId').html('<option value="">Todos os turnos</option>');
    }

    if (escolaId > 0) {
        $.getJSON('/Serie/GetSeriesByEscola?escolaId=' + escolaId, function (j) {
            var options = '<option value="">Todas as séries</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].id + '">' + j[i].text + '</option>';
            }
            $('#SerieId').html(options).show();
        });
    } else {
        $('#SerieId').html('<option value="">Todas as séries</option>');
    }

    if (escolaId > 0) {
        $.getJSON('/Turma/GetTurmasByEscola?escolaId=' + escolaId, function (j) {
            var options = '<option value="">Todas as turmas</option>';
            for (var i = 0; i < j.length; i++) {
                options += '<option value="' + j[i].id + '">' + j[i].text + '</option>';
            }
            $('#TurmaId').html(options).show();
        });
    } else {
        $('#TurmaId').html('<option value="">Todas as turmas</option>');
    }
}