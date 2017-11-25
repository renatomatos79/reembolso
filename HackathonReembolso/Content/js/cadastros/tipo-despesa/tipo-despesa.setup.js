$(document).ready(function () {
    var model = new TipoDespesaKo();
    model.dataBind();
    ko.applyBindings(model);
});