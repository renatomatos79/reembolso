var UsuarioKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.centroCustoId = ko.observable(0);
    self.gerenciaId = ko.observable(0);
    self.perfilId = ko.observable(0);
    self.nome = ko.observable("");
    self.matricula = ko.observable("");
    self.cpf = ko.observable("");
    self.telefone = ko.observable("");
    self.cargoId = ko.observable(0);
    self.bancoId = ko.observable(0);
    self.agencia = ko.observable("");
    self.contaCorrente = ko.observable("");
    self.senha = ko.observable("");
    self.senhaConfirmar = ko.observable("");

    // listas
    self.centroCustoList = new ko.observableArray([]);
    self.gerenciaList = new ko.observableArray([]);
    self.cargoList = new ko.observableArray([]);
    self.bancoList = new ko.observableArray([]);
    self.perfilList = new ko.observableArray([]);

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.centroCustoId(0);
        self.gerenciaId(0);
        self.perfilId(0);
        self.nome("");
        self.matricula("");
        self.cpf("");
        self.telefone("");
        self.cargoId(0);
        self.bancoId(0);
        self.agencia("");
        self.contaCorrente("");
        self.senha("");
        self.senhaConfirmar("");
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new UsuarioModel(item.Id, item.CentroCustoId, item.GerenciaId, item.PerfilId, item.Nome, item.Matricula, item.Cpf, item.Telefone, item.CargoId, item.BancoId, item.Agencia, item.ContaCorrente, item.Senha);
    };

    // define a base para pesquisa
    self._changeSearchStructure("/Usuario/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            CentroCustoId : item.centroCustoId(),
            GerenciaId : item.gerenciaId(),
            PerfilId : item.perfilId(),
            Nome : item.nome(),
            Matricula : item.matricula(),
            Cpf : item.cpf(),
            Telefone : item.telefone(),
            CargoId : item.cargoId(),
            BancoId : item.bancoId(),
            Agencia : item.agencia(),
            ContaCorrente : item.contaCorrente(),
            Senha : item.senha()
        };
        var dataEdit = {
            Id: item.id(),
            CentroCustoId: item.centroCustoId(),
            GerenciaId: item.gerenciaId(),
            PerfilId: item.perfilId(),
            Nome: item.nome(),
            Matricula: item.matricula(),
            Cpf: item.cpf(),
            Telefone: item.telefone(),
            CargoId: item.cargoId(),
            BancoId: item.bancoId(),
            Agencia: item.agencia(),
            ContaCorrente: item.contaCorrente(),
            Senha: item.senha()
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
            url = !self.isNew() ? "/Usuario/Edit" : "/Usuario/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.centroCustoId(item.centroCustoId);
            self.gerenciaId(item.gerenciaId);
            self.perfilId(item.perfilId);
            self.nome(item.nome);
            self.matricula(item.matricula);
            self.cpf(item.cpf);
            self.telefone(item.telefone);
            self.cargoId(item.cargoId);
            self.bancoId(item.bancoId);
            self.agencia(item.agencia);
            self.contaCorrente(item.contaCorrente);
            self.senha(item.senha);
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
            self.centroCustoId(item.centroCustoId);
            self.gerenciaId(item.gerenciaId);
            self.perfilId(item.perfilId);
            self.nome(item.nome);
            self.matricula(item.matricula);
            self.cpf(item.cpf);
            self.telefone(item.telefone);
            self.cargoId(item.cargoId);
            self.bancoId(item.bancoId);
            self.agencia(item.agencia);
            self.contaCorrente(item.contaCorrente);
            self.senha(item.senha);
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

    self.loadCargo = function () {
        self.cargoList.removeAll();
        return self._ajax({
            url: "/Cargo/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.cargoList.push(new CargoModel(item.Id, item.Descricao));
                });
            }
        });
    };

    self.loadBanco = function () {
        self.bancoList.removeAll();
        return self._ajax({
            url: "/Banco/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.bancoList.push({ id: item.Id, codigo: item.Codigo, nome: item.Nome });
                });
            }
        });
    };

    self.loadPerfil = function () {
        self.perfilList.removeAll();
        return self._ajax({
            url: "/Perfil/GetAll",
            method: "GET",
            onComplete: function (data) {
                $.each(data, function (index, item) {
                    self.perfilList.push({ id: item.Id, nome: item.Nome });
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
                nome: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                nome: { required: true }
            }
        );

        self.loadBanco();
        self.loadCargo();
        self.loadCentroCusto();
        self.loadGerencia();
        self.loadPerfil();
        self._loadTable();
    };
};