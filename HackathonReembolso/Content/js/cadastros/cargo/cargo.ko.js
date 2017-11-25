var CargoKo = function () {
    var self = this;
    ko.utils.extend(self, new ModelBase(self));

    // campos do formulário
    self.id = ko.observable(0);
    self.descricao = ko.observable("");

    // limpa os campos
    self.resetValues = function () {
        self.id(0);
        self.descricao("");
    };

    // pesquisa
    var modelFunction = function (item) {
        console.log(item);
        return new CargoModel(item.Id, item.Descricao)
    };

    // define a base para pesquisa
    self._changeSearchStructure("/Cargo/Search", modelFunction);

    // cria um novo registro
    self.submit = function (item) {
        var dataCreate = {
            descricao: item.descricao()
        };
        var dataEdit = {
            id: item.id(),
            descricao: item.descricao()
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
            url = !self.isNew() ? "/Cargo/Edit" : "/Cargo/Create",
            data = !self.isNew() ? dataEdit : dataCreate,
            fn = !self.isNew() ? fnEdit : fnCreate;

        self._submit(form, null, url, data, fn);
    };

    // details modal
    self.modalDetails = function (item) {
        var fn = function () {
            self.id(item.id);
            self.descricao(item.descricao);
        };
        self._modalDetails(fn);
    };

    // remove um registro do banco    
    self.delete = function (item) {
        var callback = function () {
            var url = Gsnet.Site().URL() + '/Cargo/Delete';
            var data = { id: item.id };
            var fn = function (data) {
                self._loadTable();
            };

            self._delete($("#divDelete"), $('#frmDelete'), url, data, fn);
        };
        Gsnet.Dialogs().confirmDelete("divDelete", "<form id='frmDelete' name='frmDelete' class='form-horizontal'><span>Deseja excluir o item '" + item.descricao + "' ?</span></form>", callback, null);
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
        };
        self._modalEdit(fn);
    };

    // regras de validação
    self.dataBind = function () {
        self._bind();

        self.bindValidate(
            $("#frmCreate"),
            rules = {
                descricao: { required: true }
            }
        );
        self.bindValidate(
            $("#frmEdit"),
            rules = {
                descricao: { required: true }
            }
        );
        self._loadTable();
    };
};