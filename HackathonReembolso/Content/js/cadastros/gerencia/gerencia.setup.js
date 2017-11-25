$(document).ready(function () {
    var model = new GerenciaKo();
    model.dataBind();
    ko.applyBindings(model);
});