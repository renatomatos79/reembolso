$(document).ready(function () {
    var model = new CentroCustoKo();
    model.dataBind();
    ko.applyBindings(model);
});