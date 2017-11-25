$(document).ready(function () {
    var model = new CargoKo();
    model.dataBind();
    ko.applyBindings(model);
});