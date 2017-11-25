$(document).ready(function() {

$("#goToPageNumber").keyup(function (e) {

        if ($("#goToPageNumber").val() == "") {
            $("#goToPageButton").prop('disabled', true);
            return false;
        } else {
            $("#goToPageButton").prop('disabled', false);
            return true;
        }
    });

    $("#goToPageNumber").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }

        return true;
    });
});
