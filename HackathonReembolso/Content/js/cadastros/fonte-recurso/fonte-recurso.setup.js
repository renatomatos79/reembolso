$(document).ready(function () {
    var model = new FonteRecursoKo();
    model.dataBind();
    ko.applyBindings(model);
});