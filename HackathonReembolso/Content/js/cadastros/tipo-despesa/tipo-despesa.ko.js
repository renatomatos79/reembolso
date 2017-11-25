var TipoDespesaKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.nome = ko.observable("");
    self.requerComprovante = ko.observable(false);
    self.requerTrajeto = ko.observable(false);
    self.requerAutorizacao = ko.observable(false);

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.nome("");
        self.requerComprovante(false);
        self.requerTrajeto(false);
        self.requerAutorizacao(false);
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new TipoDespesaModel(item.Id, item.Nome, item.RequerComprovante, item.RequerTrajeto, item.RequerAutorizacao)
    };

    // define a base para pesquisa
    self._changeSearchStructure("/TipoDespesa/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            Nome: item.nome(),
            RequerComprovante: item.requerComprovante(), 
            RequerTrajeto: item.requerTrajeto(), 
            RequerAutorizacao: item.requerAutorizacao()
        };
        var dataEdit = {
            Id: item.id(),
            Nome: item.nome(),
            RequerComprovante: item.requerComprovante(),
            RequerTrajeto: item.requerTrajeto(),
            RequerAutorizacao: item.requerAutorizacao()
        };
        var fnCreate = function (data) {
            self.resetValues();
            self._loadTable();
        };
        var fnEdit = function (data) {
            self._closeWithMessage("divEdit", "page-body", "Registro salvo com sucesso!", "Atenção!");
            self._loadTable();
        };
        var form = !self.isNew() ? $('#frmEdit') : $('#frmCreate'),
            url = !self.isNew() ? "/TipoDespesa/Edit" : "/TipoDespesa/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.nome(item.nome);
            self.requerComprovante(item.requerComprovante);
            self.requerTrajeto(item.requerTrajeto);
            self.requerAutorizacao(item.requerAutorizacao);
        };
        self._modalDetails(fn);
    };

    // create modal
    self.modalCreate = function () {
        self._modalCreate(self.resetValues);
    };

    // edit modal
    self.modalEdit = function (item) {
        var fn = function () {
            self.id(item.id);
            self.nome(item.nome);
            self.requerComprovante(item.requerComprovante);
            self.requerTrajeto(item.requerTrajeto);
            self.requerAutorizacao(item.requerAutorizacao);
        };
        self._modalEdit(fn);
    };

    // regras de validação
    self.dataBind = function () {
        self._bind();

        self.bindValidate(
            $("#frmCreate"),
            rules = {
                nome: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                nome: { required: true }
            }
        );
        self._loadTable();
    };
};