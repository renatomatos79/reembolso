var TipoDespesaKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable("");
    self.tipoDespesaId = ko.observable(0);
    self.categoriaId = ko.observable(0);
    self.centroCustoId  = ko.observable(0);
    self.usuarioId  = ko.observable(0);
    self.gerenciaId = ko.observable(0);
    self.projetoId  = ko.observable(0);
    self.statusId  = ko.observable(0);
    self.numeroDocumento =  ko.observable("");
    self.placa = ko.observable("");
    self.descricao =  ko.observable("");
    self.dataDespesa =  ko.observable("");
    self.valor = ko.observable(0);
    self.observacao =  ko.observable("");
    self.dataCompetencia = ko.observable("");
    self.dataRealizacao = ko.observable("");

    // listas
    self.tipoDespesaList = new ko.observableArray([]);
    self.categoriaList = new ko.observableArray([]);
    self.centroCustoList = new ko.observableArray([]);
    self.projetoList = new ko.observableArray([]);
    self.statusList = new ko.observableArray([]);
    self.gerenciaList = new ko.observableArray([]);

    // informativo
    self.gerenciaNome = ko.observable("");

    // limpa os campos
    self.resetValues = function () {
        self.id("");
        self.tipoDespesaId(0);
        self.categoriaId(0);
        self.centroCustoId(0);
        self.usuarioId(0);
        self.gerenciaId(0);
        self.projetoId(0);
        self.statusId(0);
        self.numeroDocumento("");
        self.placa("");
        self.descricao("");
        self.dataDespesa("");
        self.valor(0);
        self.observacao("");
        self.dataCompetencia("");
        self.dataRealizacao("");
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new DespesaModel(item.Id, item.Categoria.TipoDespesa, item.Categoria, item.CentroCusto, item.Usuario, item.Gerencia.Id, item.Projeto.Id, item.Status.Id, item.NumeroDocumento, item.Placa, item.Descricao, item.DataDespesa, item.Valor, item.Observacao, item.DataCompetencia, item.DataRealizacao)
    };

    // define a base para pesquisa
    self._changeSearchStructure("/Despesa/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            TipoDespesa: { Id: self.tipoDespesaId() },
            Categoria: { Id: self.categoriaId() },
            CentroCusto: { Id: self.centroCustoId() },
            Usuario: { Id: self.usuarioId() },
            Gerencia: { Id: self.gerenciaId() },
            Projeto: { Id: self.projetoId() },
            Status: { Id: self.statusId() },
            NumeroDocumento: self.numeroDocumento(),
            Placa: self.placa(),
            Descricao: self.descricao(),
            DataDespesa: self.dataDespesa(),
            Valor: self.valor(),
            Observacao: self.observacao(),
            DataCompetencia: self.dataCompetencia(),
            DataRealizacao: self.dataRealizacao()
        };
        var dataEdit = {
            Id: item.id(),
            TipoDespesa: { Id: self.tipoDespesaId() },
            Categoria: { Id: self.categoriaId() },
            CentroCusto: { Id: self.centroCustoId() },
            Usuario: { Id: self.usuarioId() },
            Gerencia: { Id: self.gerenciaId() },
            Projeto: { Id: self.projetoId() },
            Status: { Id: self.statusId() },
            NumeroDocumento: self.numeroDocumento(),
            Placa: self.placa(),
            Descricao: self.descricao(),
            DataDespesa: self.dataDespesa(),
            Valor: self.valor(),
            Observacao: self.observacao(),
            DataCompetencia: self.dataCompetencia(),
            DataRealizacao: self.dataRealizacao()
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
            url = !self.isNew() ? "/Despesa/Edit" : "/Despesa/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.tipoDespesaId(item.tipoDespesaId);
            self.categoriaId(item.categoriaId);
            self.centroCustoId(item.centroCustoId);
            self.usuarioId(item.usuarioId);
            self.gerenciaId(item.gerenciaId);
            self.projetoId(item.projetoId);
            self.statusId(item.statusId);
            self.numeroDocumento(item.numeroDocumento);
            self.placa(item.placa);
            self.descricao(item.descricao);
            self.dataDespesa(item.dataDespesa);
            self.valor(item.valor);
            self.observacao(item.observacao);
            self.dataCompetencia(item.dataCompetencia);
            self.dataRealizacao(item.dataRealizacao);
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
            self.tipoDespesaId(item.tipoDespesaId);
            self.categoriaId(item.categoriaId);
            self.centroCustoId(item.centroCustoId);
            self.usuarioId(item.usuarioId);
            self.gerenciaId(item.gerenciaId);
            self.projetoId(item.projetoId);
            self.statusId(item.statusId);
            self.numeroDocumento(item.numeroDocumento);
            self.placa(item.placa);
            self.descricao(item.descricao);
            self.dataDespesa(item.dataDespesa);
            self.valor(item.valor);
            self.observacao(item.observacao);
            self.dataCompetencia(item.dataCompetencia);
            self.dataRealizacao(item.dataRealizacao);
        };
        self._modalEdit(fn);
    };

    self.loadCentroCusto = function () {
        self.centroCustoList.removeAll();
        return self._ajax({
            url: "/CentroCusto/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.centroCustoList.push(new CentroCustoModel(item.Id, item.Nome, item.CodigoExterno));
                });
            }
        });
    };

    self.loadGerencia = function () {
        self.gerenciaList.removeAll();
        return self._ajax({
            url: "/Gerencia/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.gerenciaList.push(new GerenciaModel(item.Id, item.Nome, item.Setores));
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
                tipoDespesaId: { required: true },
                categoriaId: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                tipoDespesaId: { required: true },
                categoriaId: { required: true }
            }
        );
        self.loadGerencia();
        self.loadCentroCusto();
        self._loadTable();
    };
};