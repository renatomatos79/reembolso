$(document).ready(function () {
    $('input.searchinput', '#sidebar').on('keyup', function () {
        var str = $(this).val().toLowerCase();
        $('ul.nav.sidebar-menu li', '#sidebar').each(function () {
            $(this).html().toLowerCase().indexOf(str) < 0 ? $(this).hide() : $(this).show();
        });
    });

    $(".dateMask").mask("00/00/0000");
    $(".monthYearMask").mask("00/0000");
    $(".yearMask").mask("0000");
    $(".phoneMask").mask("(00) 0000-00009");
    $(".amountMask").mask("00000,00", { reverse: true });
    $(".currencyMask").mask("000.000.000.000.000,00", { reverse: true });
    $(".cpfMask").mask("000.000.000-00");
    $(".cnpjMask").mask("99.999.999/9999-99");
    $(".mask-hour").mask("00:00");
});