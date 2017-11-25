var GerenciaKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.nome = ko.observable("");
    self.gerenciaSuperiorId = ko.observable(0);
    self.setores = new ko.observableArray([]);
    self.aprovadores = new ko.observableArray([]);

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.nome("");
        self.gerenciaSuperiorId(0);
        self.setores.removeAll();
        self.aprovadores.removeAll();
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new GerenciaModel(item.Id, item.Nome, item.Setores, item.Aprovadores)
    };

    // define a base para pesquisa
    self._changeSearchStructure("/Gerencia/Search", modelFunction);

    // configura o autoComplete para o departamento pai
    self.autoCompleteGerencia = function () {
        var autoComplete = new GerenciaAutoComplete({
            selector: "#createGerenciaSuperiorId,#editGerenciaSuperiorId",
            ajax: self._ajax,
            onSelect: function (ui, item) {
                self.gerenciaSuperiorId(ui.item.value);
            },
            onBlur: function (obj) {
                if (obj !== null) {
                    self.gerenciaSuperiorId(obj.value);
                } else {
                    self.gerenciaSuperiorId(null);
                }
            }
        });
        autoComplete.setup();
    }

    // configura o autoComplete para o usuário aprovador
    self.autoCompleteUsuario = function () {
        var autoComplete = new UsuarioAutoComplete({
            selector: "#tabAprovadores input.usuario-complete",
            ajax: self._ajax,
            onSelect: function (ui, item) {
                console.log(ui);
                //self.gerenciaSuperiorId(ui.item.value);
            },
            onBlur: function (obj) {
                if (obj !== null) {
                    console.log(obj);
                    //self.gerenciaSuperiorId(obj.value);
                } else {
                    //self.gerenciaSuperiorId(null);
                }
            }
        });
        autoComplete.setup();
    }

    self.applyDateMask = function () {
        $("#tabAprovadores input.mask-date").mask("00/00/0000");
    };

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            Nome: item.nome(),
            GerenciaSuperiorId: item.gerenciaSuperiorId(),
            Setores: item.setores(),
            Aprovadores: item.aprovadores()
        };
        var dataEdit = {
            Id: item.id(),
            Nome: item.nome(),
            GerenciaSuperiorId: item.gerenciaSuperiorId(),
            Setores: item.setores(),
            Aprovadores: item.aprovadores()
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
            url = !self.isNew() ? "/Gerencia/Edit" : "/Gerencia/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.nome(item.nome);
            self.gerenciaSuperiorId(item.gerenciaSuperiorId),
            self.setores(item.setores);
            self.aprovadores(item.aprovadores);
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
            self.gerenciaSuperiorId(item.gerenciaSuperiorId),
            self.setores(item.setores);
            self.aprovadores(item.aprovadores);
        };
        self._modalEdit(fn);
    };

    // add setor
    self.addSetor = function () {
        self.setores.push(new GerenciaSetorModel(0, ""));
        return self;
    };

    self.deleteSetor = function (item) {
        self.setores.remove(item);
        return self;
    };

    // add aprovador
    self.addAprovador = function () {
        self.aprovadores.push(new GerenciaAprovadorModel(0, true, "", "", true));
        self.autoCompleteUsuario();
        self.applyDateMask();
        return self;
    };

    self.deleteAprovador = function (item) {
        self.aprovadores.remove(item);
        return self;
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
        self.autoCompleteGerencia();
    };
};