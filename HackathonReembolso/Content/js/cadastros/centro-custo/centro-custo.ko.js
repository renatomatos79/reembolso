var CentroCustoKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.descricao = ko.observable("");
    self.codigoExterno = ko.observable("");

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.descricao("");
        self.codigoExterno("");
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new CentroCustoModel(item.Id, item.Descricao, item.CodigoExterno);
    };

    // define a base para pesquisa
    self._changeSearchStructure("/CentroCusto/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            Descricao: item.descricao(),
            CodigoExterno: item.codigoExterno()
        };
        var dataEdit = {
            Id: item.id(),
            Descricao: item.descricao(),
            CodigoExterno: item.codigoExterno()
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
            url = !self.isNew() ? "/CentroCusto/Edit" : "/CentroCusto/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.descricao(item.descricao);
            self.codigoExterno(item.descricao);
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
            self.descricao(item.descricao);
            self.codigoExterno(item.codigoExterno);
        };
        self._modalEdit(fn);
    };

    // regras de validação
    self.dataBind = function () {
        self._bind();

        self.bindValidate(
            $("#frmCreate"),
            rules = {
                descricao: { required: true },
                codigoExterno: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                descricao: { required: true },
                codigoExterno: { required: true }
            }
        );
        self._loadTable();
    };
};