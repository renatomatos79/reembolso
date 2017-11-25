var FontePagadoraKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.nome = ko.observable("");
    self.prioridade = ko.observable(0);

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.nome("");
        self.prioridade(0);
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new FontePagadoraModel(item.Id, item.Nome, item.Prioridade)
    };

    // define a base para pesquisa
    self._changeSearchStructure("/FontePagadora/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            nome: item.nome(),
            Prioridade: item.prioridade()
        };
        var dataEdit = {
            Id: item.id(),
            nome: item.nome(),
            Prioridade: item.prioridade()
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
            url = !self.isNew() ? "/FontePagadora/Edit" : "/FontePagadora/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.nome(item.nome);
            self.prioridade(item.prioridade);
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
            self.prioridade(item.prioridade);
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