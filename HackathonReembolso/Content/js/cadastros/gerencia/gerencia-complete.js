var GerenciaAutoComplete = function (options) {
    var self = this;
    self.defaults = {
        selector: "",
        ajax: null,
        onSelect: function (ui, item) {},
        onBlur: function (obj) {}
    };
    self.options = $.extend({}, self.defaults, options);
    self.setup = function () {
        // configuração do autocomplete select
        $(self.options.selector).autocomplete({
            minLength: 3,
            source: function (request, response) {
                self.options.ajax({
                    url: "/Gerencia/GetGerenciaList?nome=" + request.term,
                    data: {},
                    onComplete: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.Nome,
                                value: item.Id
                            };
                        }));
                    }
                });
                $(".ui-autocomplete").css('z-index', 9999);
            },
            select: function (e, ui) {
                e.preventDefault();
                $(e.target).val(ui.item.label);
                self.options.onSelect(ui, ui.item);
                //self.gerenciaSuperiorId(ui.item.value);
            }
        });

        // configuração do autocomplete blur
        $(self.options.selector).on("blur", function (event) {
            Gsnet.Alerts().clearAlerts();
            var nome = $(this).val().trim();
            var controle = $(this);
            var form = controle.parent().parent().parent().attr("name");
            if (nome === "") {
                self.options.onBlur(null);
            } else {
                var fn = function (gerencia) {
                    if (gerencia.value === "") {
                        self._addDanger(form, "Gerência não encontrada!", "Atenção!");
                        self.options.onBlur(null);
                        controle.val("");
                    } else {
                        controle.val(gerencia.label);
                        self.options.onBlur(gerencia);                        
                    }
                }
                self.getGerenciaByName(nome, fn);
            }
        });
    };

    // localiza a gerência por nome
    self.getGerenciaByName = function (name, callback) {
        var result = { value: "", label: "" };
        if ($.trim(name) === '') {
            callback(result);
            return;
        }
        return self.options.ajax({
            url: "/Gerencia/GetGerenciaList",
            data: { nome: name },
            onComplete: function (data) {
                if (data.length > 0) {
                    result = { value: data[0].Id, label: data[0].Nome };
                }
                callback(result);
            }
        });
    }

    return self;
}