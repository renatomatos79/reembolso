Gsnet.Alerts = function () {

    var self = this;

    /*
        Cria uma div, dentro do container informado, usando o conceito de BootStrap para a classe "alert-danger"
        Se a div já existir novas mensagens de erro serão adicionadas como se fosse um sumário
    */
    self.addError = function (container, msg) {

        var existe = $("#" + container).find("#gsnet-alert-danger").size() > 0;

        if (!existe) {
            $("#" + container).prepend('<div id="gsnet-alert-danger"/>');
            $("#" + container).find("#gsnet-alert-danger").addClass("alert alert-danger alert-dismissable");
            $("#" + container).find("#gsnet-alert-danger").append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
            $("#" + container).find("#gsnet-alert-danger").append('<p><strong>Atenção</strong></p>');
        }

        $("#" + container).find("#gsnet-alert-danger").append('<p>' + msg + '</p>');
        $("#" + container).show();
    }

    /*
        Cria uma div, dentro do container informado, usando o conceito de BootStrap para a classe "alert-success"
        As mensagens serão adiciona
    */
    self.addSuccess = function (container, msg) {

        var existe = $("#gsnet-alert-ok").size() > 0;

        if (!existe) {
            $("#" + container).prepend('<div id="gsnet-alert-ok"/>');
            $("#gsnet-alert-ok").addClass("alert alert-success alert-dismissable");
            $("#gsnet-alert-ok").append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
            $("#gsnet-alert-ok").append('<p><strong>Atenção</strong></p>');
        }

        $("#gsnet-alert-ok").append('<p>' + msg + '</p>');
        $("#" + container).show();

        setTimeout(function () {
            $("#gsnet-alert-ok").fadeOut("slow");
        }, 5000);
    }

    /*
        Dentro da página atual, Limpa o conteúdo e esconde os objetos que usam a classe "alert" 
    */
    self.clearAlerts = function () {
        //$(".alert").empty().hide();
        $(".alert").remove();
    }

    /*
        Cria um alerta, estilo BootStrap alert-danger, dentro do container informado com o conteúdo da propriedade Message
        do objeto JsonResponse retornado.
    */
    self.showJsonResponseError = function (container, json) {

        if (json.responseJSON !== undefined) {
			if (json.responseJSON.Errors != null && json.responseJSON.Errors.length > 0) {
                $.each(json.responseJSON.Errors, function (index, item) {
                    Gsnet.Alerts().addError(container, item);
                });
            }
            else {
                if (json.responseJSON.Message !== undefined) {
                    Gsnet.Alerts().addError(container, json.responseJSON.Message);
                } else {
                    Gsnet.Alerts().addError(container, "Houve um erro no processamento da sua requisição!");
                }
            }
        } else {
            if (json.Message !== undefined) {
                Gsnet.Alerts().addError(container, json.Message);
            } else {
                Gsnet.Alerts().addError(container, "Houve um erro no processamento da sua requisição!");
            }
        }
    }

    /*
        Verifica se houve um erro na resposta do objeto json.
        Se Result for false então houve um erro
    */
    self.hasJsonError = function (json) {
        if (json.responseJSON !== undefined) {
            if (json.responseJSON.Errors != null && json.responseJSON.Errors.length > 0) {
                return true;
            }
            else {
                if (json.responseJSON.Result !== undefined) {
                    return json.responseJSON.Result === false;
                } else {
                    return true;
                }
            }
        } else {
            if (json.Result !== undefined) {
                return json.Result === false;
            } else {
                return true;
            }
        }
    }

    return self;
}