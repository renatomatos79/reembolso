var ModelBase = function (model) {
    var self = this;

    /* ---- Grid default variables ----*/

    // navigation list
    self.table = ko.observableArray([]);
    self.listLoaded = ko.observable(false);
    self.isNew = ko.observable(false);

    // search text
    self.searchValue = ko.observable("");
    self.pageNumber = ko.observable(1);
    self.divFilter = "";

    // pagination
    self.recordsPerPage = ko.observable(0);
    self.totalPages = ko.observable(0);

    // sort
    self.columnName = ko.observable('');
    self.sortingDirection = ko.observable('A');

    // global variables
    var urlSearchVar = null;
    var modelFunctionVar = null;
    var customSearchCallbackVar = null;
    var customGetDataMethodVar = null;
    var customClearFilterVar = null;
    var customValidateFilterVar = null;

    self.isNull = function (obj) {
        return obj === null || obj === undefined;
    };

    // validation rules
    self.bindValidate = function (form, rules) {
        form.validate({
            rules: rules,
            highlight: function (element, errorClass, validClass) {
                $(element).addClass(errorClass);
                $(element).closest('.form-group').addClass('has-error');
                $(element).after('<i class="form-control-feedback fa fa-remove"></i>');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass(errorClass);
                $(element).closest('.form-group').removeClass('has-error');
                $(element).parent().find('.fa-remove').remove();
            }
        });
    };

    self.formValidate = function (idForm) {
        Gsnet.Alerts().clearAlerts();
        // valida os campos obrigatórios do forms de pesquisa
        if (!$("#" + idForm).valid()) {
            return false;
        }
        return true;
    }

    self.cleanBindValidate = function (form) {
        $(form).parent().find('.error').removeClass("error");
        $(form).parent().find('.has-error').removeClass('has-error');
        $(form).parent().find('.fa-remove').remove();
    };

    // paginação
    self.updateRecordsPerPage = function (data, event) {
        self.pageNumber(1);
        self.recordsPerPage(event.target.value);
        return self.internalSearch();
    };

    // load in second plane
    self._load = function (form, url, params, type, callBack) {
        $.ajax({
            url: url,
            headers: new HttpHeader().getHeader(),
            data: params,
            type: type,
            async: true,
            complete: function (data) {
                Gsnet.Alerts().clearAlerts();
                if (Gsnet.Alerts().hasJsonError(data)) {
                    Gsnet.Alerts().showJsonResponseError(form, data);
                    if (callBack) callBack(data);
                    return false;
                } else {
                    if (callBack) callBack(JSON.parse(data.responseText).Data);
                    return true;
                }
            }
        });
    };

    self._submit = function (form, mensageContainer, url, data, callBack, callBackError) {
        Gsnet.Alerts().clearAlerts();
        if (!self.validate(form)) {
            return false;
        }
        self.listLoaded(false);
        $.ajax({
            url: Gsnet.Site().URL() + url,
            data: JSON.stringify(data),
            headers: new HttpHeader().getHeader(),
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            async: true,
            complete: function (data) {
                self.listLoaded(true);
                var container = mensageContainer != null ? mensageContainer : form.attr('id');
                if (Gsnet.Alerts().hasJsonError(data)) {
                    Gsnet.Alerts().showJsonResponseError(container, data);
                    if (callBackError) {
                        callBackError(data);
                    }
                    return false;
                } else {
                    Gsnet.Alerts().addSuccess(container, "Registro salvo com sucesso!");
                    if (callBack) {
                        var temp = [];
                        if (data && data.responseText) {
                            if (JSON.parse(data.responseText).Data) {
                                temp = JSON.parse(data.responseText).Data;
                            } else {
                                temp = JSON.parse(data.responseText);
                            }
                        };
                        callBack(temp);
                    }
                    return true;
                }
            }
        });
    };

    self._delete = function (div, form, url, data, callBack) {
        Gsnet.Alerts().clearAlerts();
        $.ajax({
            url: url,
            data: data,
            headers: new HttpHeader().getHeader(),
            contentType: 'application/json; charset=utf-8',
            type: "GET",
            async: true,
            complete: function (data) {
                var container = form.attr('id');
                if (self.printedError(url, container, data) === false) {
                    Gsnet.Alerts().addSuccess(container, "Registro excluído com sucesso!");
                    setTimeout(function () {
                        Gsnet.Dialogs().closeModal(div.attr('id'));
                    }, 2000);
                    if (callBack) {
                        callBack(data);
                    }
                    return true;
                }
            }
        });
    };

    // validate form
    self.validate = function (form) {
        $('select.select2').on('change', function () {
            $(this).valid();
        });

        if (!form.valid()) {
            return false;
        }

        return true;
    };

    self._addInfo = function (container, msg) {

        var exists = $("#" + container).find("#gsnet-alert-ok").size() > 0;

        if (!exists) {
            $("#" + container).prepend('<div id="gsnet-alert-ok"/>');
            $("#" + container).find("#gsnet-alert-ok").addClass("alert alert-info alert-dismissable");
            $("#" + container).find("#gsnet-alert-ok").append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
            $("#" + container).find("#gsnet-alert-ok").append('<p><strong>Info</strong></p>');
        }

        $("#" + container).find("#gsnet-alert-ok").append('<p>' + msg + '</p>');
        $("#" + container).find("#gsnet-alert-ok").show();

        setTimeout(function () {
            $("#" + container).find("#gsnet-alert-ok").fadeOut("slow");
        }, 5000);
    }

    self._addDanger = function (container, msg, title) {

        var exists = $("#" + container).find("#gsnet-alert-nok").size() > 0;

        if (!exists) {
            $("#" + container).prepend('<div id="gsnet-alert-nok"/>');
            $("#" + container).find("#gsnet-alert-nok").addClass("alert alert-danger alert-dismissable");
            $("#" + container).find("#gsnet-alert-nok").append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
            $("#" + container).find("#gsnet-alert-nok").append('<p><strong>' + title + '</strong></p>');
        }

        $("#" + container).find("#gsnet-alert-nok").append('<p>' + msg + '</p>');
        $("#" + container).find("#gsnet-alert-nok").show();

        setTimeout(function () {
            $("#" + container).find("#gsnet-alert-nok").fadeOut("slow");
        }, 5000);
    }

    self._addSuccess = function (container, msg, title) {

        var exists = $("#" + container).find("#gsnet-alert-success").size() > 0;

        if (!exists) {
            $("#" + container).prepend('<div id="gsnet-alert-success"/>');
            $("#" + container).find("#gsnet-alert-success").addClass("alert alert-success alert-dismissable");
            $("#" + container).find("#gsnet-alert-success").append('<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>');
            $("#" + container).find("#gsnet-alert-success").append('<p><strong>' + title + '</strong></p>');
        }

        $("#" + container).find("#gsnet-alert-success").append('<p>' + msg + '</p>');
        $("#" + container).find("#gsnet-alert-success").show();

        setTimeout(function () {
            $("#" + container).find("#gsnet-alert-success").fadeOut("slow");
        }, 5000);
    }

    self._closeWithMessage = function (div, containerMessage, message, titleMessage) {
        Gsnet.Dialogs().closeModal(div);
        self._addSuccess(containerMessage, message, titleMessage);
    }

    /* ---- Grid default functions ----*/

    // select with numbers of registers per page
    $("#recordsPerPage").change(function () {
        self.pageNumber(1);
        self.recordsPerPage(Number($("#recordsPerPage").val()));

        return self.internalSearch();
    });

    self._changeSearchStructure = function (urlSearch, modelFunction, customSearchCallback) {
        urlSearchVar = urlSearch;
        modelFunctionVar = modelFunction;
        customSearchCallbackVar = customSearchCallback;
        customGetDataMethodVar = null;
        customClearFilterVar = null;
    };

    self._changeOptionsSearchStructure = function (options) {
        urlSearchVar = options.urlSearch;
        modelFunctionVar = options.modelFunction;
        customSearchCallbackVar = options.customSearchCallback;
        customGetDataMethodVar = options.customGetDataMethod;
        customClearFilterVar = options.customClearFilter;
        customValidateFilterVar = options.customValidateFilter;
    };

    self.internalSearch = function () {
        self.listLoaded(false);
        self._search(urlSearchVar, modelFunctionVar, customSearchCallbackVar);
    };

    self.modalFilter = function () {
        Gsnet.Alerts().clearAlerts();
        self.searchValue("");
        Gsnet.Dialogs().showModal(self.divFilter);
    }

    self.advFilter = function () {
        if (customValidateFilterVar) {
            if (customValidateFilterVar() === false) {
                return;
            }
        }
        self.internalSearch();
        Gsnet.Dialogs().closeModal(self.divFilter);
    };

    self.advancedSearch = function (idDiv, dataBindFilter) {
        if (!idDiv) self.divFilter = "divFilter";
        if (!dataBindFilter) dataBindFilter = "click: advFilter";

        $('button:contains("OK")').before('<button class="btn btn-default" style="margin-top: 0.25px;" type="button" data-bind="click: modalFilter">...</button>');
        $("#" + self.divFilter).find("#btnEdit").text("Filtrar").attr("data-bind", dataBindFilter);
    };

    self._loadTable = function () {
        self.pageNumber(1);
        self.recordsPerPage(Number($("#recordsPerPage").val()));
        self.totalPages(1);

        return self.internalSearch();
    };

    self.resetSearchKeyPress = function (item, event) {
        if (event.keyCode == 13) {
            self.searchValue($(event.target).val());

            return self.resetSearch();
        }

        return true;
    }

    self.resetSearch = function () {
        self.pageNumber(1);
        if (customClearFilterVar) customClearFilterVar();
        return self.internalSearch();
    };

    self.goToPage = function (item, event) {
        if (event.keyCode == 13) {
            self.pageNumber(parseInt($(event.target).val(), 10));

            return self.internalSearch();
        }

        return true;
    };

    self.goToFirstPage = function () {
        if (self.pageNumber() != 1) {
            self.pageNumber(1);

            return self.internalSearch();
        }
    };

    self.goToLastPage = function () {
        if (self.pageNumber() != self.totalPages()) {
            self.pageNumber(self.totalPages());

            return self.internalSearch();
        }
    };

    self.nextPage = function () {
        if (self.pageNumber() != self.totalPages()) {
            self.pageNumber(self.pageNumber() + 1);

            return self.internalSearch();
        }
    };

    self.previousPage = function () {
        if (self.pageNumber() > 1) {
            self.pageNumber(self.pageNumber() - 1);

            return self.internalSearch();
        }
    };

    self.sortColumn = function (item, event) {
        var column = $(event.target);
        var direction = column.hasClass('sorting_desc') ? 'D' : 'A';

        $('.datatable th.sorting_asc, .datatable th.sorting_desc').addClass('sorting');
        $('.datatable th').removeClass('sorting_asc');
        $('.datatable th').removeClass('sorting_desc');

        if (direction == 'A') {
            column.addClass('sorting_desc');
        }
        else {
            column.addClass('sorting_asc');
        }

        self.columnName(column.data('column-name'));
        self.sortingDirection(direction);

        self.internalSearch();
    };

    // se customGetDataMethod for null a consulta usuário como pesquisa o conteúdo do método self.getDefaultData
    self.getDefaultData = function () {
        return { query: self.searchValue(), page: self.pageNumber(), records: self.recordsPerPage(), columnName: self.columnName(), sortingDirection: self.sortingDirection().charCodeAt(0) };
    }

    self._search = model._search || function (url, fn, customSearchCallback) {
        self.table.removeAll();
        self.listLoaded(false);
        var queryData = customGetDataMethodVar ? customGetDataMethodVar() : self.getDefaultData();
        return self._ajax({
            url: url,
            method: "GET",
            data: queryData,
            onSuccess: function (data, responseData) {
                if (customSearchCallback != null) {
                    customSearchCallback(data);
                } else {
                    $.each(data, function (index, item) {
                        self.table.push(fn(item));
                    });
                }
                self.totalPages(responseData.TotalPages);
                self.listLoaded(true);
                return true;
            }
        });
    };

    self._validateResponse = function (url, container, responseData, onError, onSuccess) {
        if (self.printedError(url, container, responseData) === true) {
            if (onError != undefined && onError != undefined) {
                onError(responseData);
            }
            return false;
        } else {
            if ((onSuccess != null) && (onSuccess != undefined)) {
                var data = [];
                if (!self.isNull(responseData.responseJSON) && !self.isNull(responseData.responseJSON.Data)) {
                    data = responseData.responseJSON.Data;
                }
                if (self.isNull(responseData.Data) === false) {
                    data = responseData.Data;
                }
                onSuccess(data, responseData);
            }
            return true;
        }
    };

    // _url, _verb, _payload, _container, onBeforeSend, onComplete, onSuccess, onError
    self._ajax = function (opt) {
        // limpa as mensagens de alerta e executa a chamada ajax
        // Gsnet.Alerts().clearAlerts();

        // se o container não for informado então será definido o body da página como padrão para output message
        if (opt.container === null || opt.container === undefined) {
            opt.container = "page-body";
        };
        // formata o conteúdo da requisição
        if (opt.data === null || opt.data === undefined) {
            opt.data = {};
        } 
        // se o método não for informado então usa GET como padrão
        if (opt.method === null || opt.method === undefined) {
            opt.method = "GET";
        };
        // se for uma requisição diferente de GET será necessário converter o data
        if (opt.method !== "GET") {
            opt.data = JSON.stringify(opt.data);
        }
        // dados que serão enviados em todas as requisições
        var requestedURL = Gsnet.Site().URL() + opt.url;
        var requestedHeader = new HttpHeader().getHeader();
        var tokenName = new HttpHeader().getTokenName();
        var tokenValue = requestedHeader[tokenName];
        return new HttpProxy({
            url: requestedURL,
            headers: requestedHeader,
            data: opt.data,
            method: opt.method,
            onBeforeSend: function (xhr) {
                xhr.setRequestHeader(tokenName, tokenValue);
                if (opt.onBeforeSend !== null && opt.onBeforeSend !== undefined) {
                    opt.onBeforeSend(xhr);
                }
            },
            onComplete: function (data) {
                if (opt.onComplete !== null && opt.onComplete !== undefined) {
                    return self._validateResponse(requestedURL, opt.container, data, opt.onError, opt.onComplete);
                }
            },
            onSuccess: function (data) {
                if (opt.onSuccess !== null && opt.onSuccess !== undefined) {
                    return self._validateResponse(requestedURL, opt.container, data, opt.onError, opt.onSuccess);
                }
            },
            onError: function (data) {
                self.printedError(requestedURL, opt.container, data);
            }
        }).execute();
    };

    self.printedError = function (url, container, data) {
        // verifica se há uma resposta
        if (self.isNull(data) === true) {
            return false;
        }
        // verifica se há uma resposta em formato JSON com resultado OK
        if (self.isNull(data.responseJSON) === false) {
            if (data.responseJSON.Result === true) {
                return false;
            } else {
                Gsnet.Alerts().addError(container, data.responseJSON.Message);
                return true;
            }
        }
        // verifica se há uma resposta em formato texto
        if (self.isNull(data.responseText) === false) {
            var text = $(data.responseText).find("div.content-container").find("h3").html();
            if (text !== "") {
                Gsnet.Alerts().addError(container, text);
                return true;
            }
        }
        // verifica se o objeto informado contém o campo Result com valor sucesso... neste caso nenhum erro foi impresso
        if (self.isNull(data.Result) === false) {
            if (data.Result === false) {
                Gsnet.Alerts().addError(container, data.Message);
            }
            return data.Result === false;
        }
        // verifica se a propriedade Data foi informada
        Gsnet.Alerts().addError(container, "Não foi possível processar a requisição - " + url);
        return true;
    };

    self._hasAccess = function (_area, _controle, _acao, callback) {
        return self._ajax({
            url: "/Cadastros/Custom/HasAccess",
            data: { area: _area, controle: _controle, acao: _acao },
            onComplete: function (data, responseData) {
                var hasAccessResult = data;
                callback(hasAccessResult);
            }
        });
    };

    self._modalCreate = function (resetValuesFn) {
        Gsnet.Alerts().clearAlerts();
        resetValuesFn();
        self.cleanBindValidate();
        Gsnet.Dialogs().showModal("divCreate");
        self.isNew(true);
        return true;
    };

    self._modalEdit = function (fn) {
        Gsnet.Alerts().clearAlerts();
        self.cleanBindValidate();
        fn();
        Gsnet.Dialogs().showModal("divEdit");
        self.isNew(false);
        return true;
    };

    self._modalDetails = function (fn) {
        fn();
        Gsnet.Dialogs().showModal("divDetails");
        self.isNew(false);
        return true;
    };

    self._bind = function (form) {
        $("#divCreate").find("#btnCreate").attr("data-bind", "click: submit");
        $("#divEdit").find("#btnEdit").attr("data-bind", "click: submit");
    };
}