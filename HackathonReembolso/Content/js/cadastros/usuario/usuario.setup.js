$(document).ready(function () {
    var model = new UsuarioKo();
    model.dataBind();
    ko.applyBindings(model);
});