var initCustomApplication = function () {
    //!function ($) {

        //$(function () {
            $.datepicker.setDefaults($.datepicker.regional["pt-BR"]);

            // button state demo
            /*
            $('.btn-submit').click(function (e) {
                e.preventDefault();
                var btn = $(this);
                btn.button('loading');
                setTimeout(function () {
                    btn.button('reset')
                }, 10000);
                $(this).trigger('submit');
            });
            */

            $('.date').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true,
                language: 'pt-BR'
            });

            $('.date').each(function () {
                if ($(this).hasClass('datetime')) {
                    $(this).datepicker('remove');
                    /*$(this).datepicker("remove").datetimepicker({
                        language: 'pt-BR'
                    });
                    */
                }
            });

            $("[rel='tooltip']").tooltip();

            /* Mascaras */
            $(".cpf").mask("999.999.999-99");
            $(".cnpj").mask("99.999.999/9999-99");
            $(".date").mask("99/99/9999");
            $(".datepicker").mask("99/99/9999");
            $(".datahora").mask("99/99/9999 99:99:99");
            $(".horaminuto").mask("99:99");
            $(".time").mask("99:99:99");
            $(".telefone").mask("(99) 9999-9999?9");
            $(".cep").mask("99.999-999");
            $(".valor").maskMoney({ decimal: ",", thousands: "" });
            $(".umacasadecimal").maskMoney({ decimal: ",", thousands: "", precision: 1 });
            
            $(".conf-delete").click(function (e) {
                e.preventDefault();
                var location = $(this).attr('href');
                /* bootbox.backdrop(false); */
                bootbox.confirm("Confirma exclusão?", "Não", "Sim", function (confirmed) {
                    if (confirmed) {
                        window.location.replace(location);
                    }
                });
            });

            $(".somentenumeros").keydown(function (e) {
                
                // Allow: backspace, delete, tab, escape, enter and ',' = 188
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 188]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });

            sinalizeAbasComInsonsistenciaDeValidacao();
            assinaEventoDisabledButtonOnSubmit();
        //});

    //}(window.jQuery);
}

var assinaEventoDisabledButtonOnSubmit = function () {
    $("[data-loading-text]").unbind("click");
    $("[data-loading-text]").click(function (e) {
        var _this = this;
        e.preventDefault();
        var novoTexto = $(this).data('loading-text');
        var textoAnterior = $(this).html();
        $(_this).prop("disabled", true);
        $(_this).html(novoTexto);
        setTimeout(function () {
            $(_this).html(textoAnterior)
            $(_this).prop("disabled", false);
        }, 10000);
        $(_this).trigger('submit');
        return true;
    });
}

var sinalizeAbasComInsonsistenciaDeValidacao = function () {

    var primeiraAbaComInconsistencia = null;

    $('.input-validation-error').each(function () {
        var propId = $(this).closest('.tab-pane').prop('id');
        if (propId != undefined && propId != null && propId != '') {
            if (primeiraAbaComInconsistencia == null) {
                primeiraAbaComInconsistencia = propId;
            }
            $('a[href$="#' + propId + '"]').css('background-color', '#ffeeee').css('color', '#ff0000');
        }
    });

    if (primeiraAbaComInconsistencia != null) {
        $('.nav-tabs a[href="#' + primeiraAbaComInconsistencia + '"]').tab('show');
    }
}

var OnComplete = function (e) {
    /*
    $(".conf-delete").click(function (e) {
        e.preventDefault();
        var location = $(this).attr('href');
        //bootbox.backdrop(false);
        bootbox.confirm("Confirma exclusão?", "Não", "Sim", function (confirmed) {
            if (confirmed) {
                window.location.replace(location);
            }
        });
    });
    */
}

var montaPaginaAoFinalizarCarregamento = function () {
    pageSetUp();
    initCustomApplication();
    if (typeof executaScriptsDaPagina == 'function') {
        executaScriptsDaPagina();
    }
}

var OnSuccess = function () {
    montaPaginaAoFinalizarCarregamento();
}

var OnFailure = function (response) {
    alert("Falha ao processar a requisição!");
}

var RegLog = function (mensagem) {
    var now = new Date();
    console.log(now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds() + " " + mensagem);
}

var CarregarPaginaAjax = function (url) {
    $("#content").load(url, function () {
        montaPaginaAoFinalizarCarregamento();
    });
}

var ConfirmaExclusao = function (callbackSim, callbackNao) {
    $.SmartMessageBox({
        title: "<i class='fa fa-trash-o txt-color-orangeDark'></i><span class='txt-color-orangeDark'> <strong>Atenção</strong></span> !",
        content: "Tem certeza que deseja excluir?",
        buttons: '[Não][Sim]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Sim") {
            if (callbackSim != undefined && callbackSim != null) {
                callbackSim();
            }
        }
        if (ButtonPressed === "Não") {
            if (callbackNao != undefined && callbackNao != null) {
                callbackNao();
            }
        }
    });
}

var ConfirmaSimNao = function (titulo, texto, callbackSim, callbackNao) {
    $.SmartMessageBox({
        title: titulo,
        content: texto,
        buttons: '[Não][Sim]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Sim") {
            if (callbackSim != undefined && callbackSim != null) {
                callbackSim();
            }
        }
        if (ButtonPressed === "Não") {
            if (callbackNao != undefined && callbackNao != null) {
                callbackNao();
            }
        }
    });
}

var MensagemSucesso = function (mensagem) {
    $.smallBox({
        title: "Sucesso!",
        content: "<i>" + mensagem + "</i>",
        color: "#659265",
        iconSmall: "fa fa-check fa-2x fadeInRight animated",
        timeout: 4000
    });
}

var MensagemErro = function (mensagem) {
    $.smallBox({
        title: "Erro!",
        content: "<i>" + mensagem + "</i>",
        color: "#C46A69",
        iconSmall: "fa fa-times fa-2x fadeInRight animated",
        timeout: 4000
    });
}

var ConfirmaLogout = function () {
    $.SmartMessageBox({
        title: "<i class='fa fa-sign-out txt-color-orangeDark'></i> Sair <span class='txt-color-orangeDark'><strong>" + $("#show-shortcut").text() + "</strong></span> ?",
        content: "Você pode melhorar sua segurança ainda mais depois de sair fechando a janela do navegador",
        buttons: '[Não][Sim]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Sim") {
            $.root_.addClass("animated fadeOutUp");
            setTimeout(function () {
                window.location = "/Acesso/Sair";
            }, 1e3);
        }
    });
}

var iniciaSearchBar = function () {
    $('#select_search_pessoas').select2({
        width: '300px',
        placeholder: "Buscar pessoas...",
        allowClear: true,
        ajax: {
            url: "/Home/GetPessoas",
            dataType: 'json',
            quietMillis: 250,
            data: function (term, page) {
                return {
                    q: term,
                    page: page
                };
            },
            results: function (data, page) {
                var more = (page * 30) < data.total_count;
                return { results: data.items, more: more };
            },
            cache: false
        }
    });

    $('#btn-header-search').click(function (e) {
        e.preventDefault();
    });

    $('#s2id_select_search_pessoas').find('.select2-arrow').find('b').remove();

    $('#select_search_pessoas').on("select2-selecting", function (e) {
        $.ajax({
            type: "GET",
            url: "/Home/AbrirPessoa",
            data: { id: e.val }
        }).done(function (returnUrl) {
            if (returnUrl == "false") {
                alert("Não foi possível completar a operação.")
            } else {
                CarregarPaginaAjax(returnUrl);
            }
        });
    });
}

/**
 * Carrega arquivos javascript dinamicamente e executa uma funcao de callback.
 * @param {string ou array} scripts 
 * @param {function} callback
 */
var carregarScripts = function (scripts, callback) {

    if (scripts == null) {
        if (callback != undefined && callback != null && $.isFunction(callback)) {
            callback();
        }
        return;
    }

    if (Array.isArray(scripts)) {

        if (scripts.length == 0) {
            callback();
            return;
        }

        var script = scripts[0];
        scripts = scripts.slice(1);
        $.getScript(script, function () { carregarScripts(scripts, callback); });
    }
    else {
        var script = scripts;
        scripts = null;
        $.getScript(script, function () { carregarScripts(scripts, callback); });
    }
}

var desabilitarCampos = function (elementoId) {
    $('#' + elementoId + ' :input').attr('readonly', 'readonly');
    $('#' + elementoId + ' :input').attr('disabled', true);
}

var habilitarCampos = function (elementoId) {
    $('#' + elementoId + ' :input').removeAttr('readonly');
    $('#' + elementoId + ' :input').removeAttr('disabled');
}

var onGridLoadCallBack = function (id) {
    $("#" + id).unbind("click");
    $("#" + id).off("click");
    $("#" + id).on('click', 'tbody tr', function (e) {
        var url = $(this).find('td:not(:empty):first').find(':hidden').val();
        if (url != undefined && url != null && url != '') {
            CarregarPaginaAjax(url);
        }
    });
}


$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};


function selectElementContents(el) {
    var range = document.createRange();
    range.selectNodeContents(el);
    var sel = window.getSelection();
    sel.removeAllRanges();
    sel.addRange(range);
}

var makeFormReadonly = function (formId) {
    $("#" + formId + " :input").attr("readonly", true);
    $("#" + formId + " :input").attr("disabled", true);
}