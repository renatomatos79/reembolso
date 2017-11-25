$(document).ready(function () {
    var model = new CategoriaKo();
    model.dataBind();
    ko.applyBindings(model);
});