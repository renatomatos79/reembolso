var CategoriaKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.descricao = ko.observable("");
    self.tipoDespesaId = ko.observable(0);
    self.valorFixo = ko.observable(false);
    self.valor = ko.observable(0);

    // listas
    self.tipoDespesaList = new ko.observableArray([]);
    self.valorFixoList = new ko.observableArray([{ value: false, text: "Não" }, { value: true, text: "Sim" }]);

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.descricao("");
        self.tipoDespesaId(0);
        self.valorFixo(false);
        self.valor(0);
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new CategoriaModel(item.Id, item.Descricao, item.TipoDespesa, item.ValorFixo, item.Valor)
    };

    // muda o comportamento do metodo getData porque a janela possui busca avançada
    self.getAdvancedData = function () {
        return { tipoDespesaId: 0, query: self.searchValue(), page: self.pageNumber(), records: self.recordsPerPage() };
    };

    self._changeOptionsSearchStructure({
        urlSearch: "/Categoria/Search",
        modelFunction: modelFunction,
        customGetDataMethod: self.getAdvancedData
    });

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            Descricao: item.descricao(),
            TipoDespesa: { Id: item.tipoDespesaId() },
            ValorFixo: item.valorFixo(),
            Valor: item.valor()
        };
        var dataEdit = {
            Id: item.id(),
            Descricao: item.descricao(),
            TipoDespesa: { Id: item.tipoDespesaId() },
            ValorFixo: item.valorFixo(),
            Valor: item.valor()
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
            url = !self.isNew() ? "/Categoria/Edit" : "/Categoria/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.descricao(item.descricao);
            self.tipoDespesaId(item.tipoDespesaId);
            self.valorFixo(item.valorFixo);
            self.valor(item.valor);
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
            self.tipoDespesaId(item.tipoDespesaId);
            self.valorFixo(item.valorFixo);
            self.valor(item.valor);
        };
        self._modalEdit(fn);
    };

    self.loadTipoDespesa = function () {
        self.tipoDespesaList.removeAll();
        return self._ajax({
            url: "/TipoDespesa/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.tipoDespesaList.push({ id: item.Id, nome: item.Nome });
                });
            }
        });
    };

    // regras de validação
    self.dataBind = function () {
        self._bind();

        self.bindValidate(
            $("#frmCreate"),
            rules = {
                descricao: { required: true },
                tipoDespesaId: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                descricao: { required: true },
                tipoDespesaId: { required: true }
            }
        );
        self.loadTipoDespesa();
        self._loadTable();
    };
};