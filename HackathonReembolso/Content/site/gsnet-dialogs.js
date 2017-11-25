Gsnet.Dialogs = function () {

    var self = this;

    /*
        Abre uma janela modal com o contéudo do objeto informado em ID
    */
    self.showModal = function (id) {
        $("#" + id).modal('show');
    }

    self.showModal = function (id, callbackYes, callbackNo) {
        $("#" + id).find("#btnConfirm").unbind("click");
        $("#" + id).find("#btnConfirm").bind("click", function () {
            if (callbackYes != null)
                callbackYes();
            else
                self.closeModal(id);
        });
        $("#" + id).modal('show');

    }

    /*
        Fecha a janela modal informada em ID
    */
    self.closeModal = function (id) {
        $("#" + id).modal('hide');
    }

    /*
        Retorna o maior z-index dentro de um seletor especifico
    */
    self.MaxZIndex = function (selector) {
        return Math.max.apply(null, $(selector).map(function () {
            var z;
            return isNaN(z = parseInt($(this).css("z-index"), 10)) ? 0 : z;
        }));
    };

    /*
        Definie o z-index de um controle com o maior valor
    */
    self.SetZIndexToMax = function (id, selector) {
        var zIndex = self.MaxZIndex(selector) + 1;
        $("#" + id).css("z-index", zIndex);
    };


    /*
        Cria uma janela modal de confirmação com o layout informado pela DIV e mensagem informada em msg.
        Em caso de confirmação o método callbackYes será carregado
    */
    self.confirmDelete = function (div, msg, callbackYes, callbackNo) {

        self.showModal(div);

        $("#" + div).find("div.modal-body").html(msg);
        $("#" + div).find("#btnConfirm").unbind("click");
        $("#" + div).find("#btnConfirm").bind("click", function () {
            callbackYes();
        });

    };

    self.CalcularWidth = function (tamanho) {
        var width = $(window).width();
        if (width > tamanho)
            width = (width - tamanho) / 2;
        else
            width = 0;

        return width + "px";
        
    }   

    return self;

}