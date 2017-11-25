$(document).ready(function () {
    var model = new FontePagadoraKo();
    model.dataBind();
    ko.applyBindings(model);
});