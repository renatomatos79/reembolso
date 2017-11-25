(function ( $ ) {

    $.fn.Pagination = function (options) {
        
        // valor padrão
        var settings = $.extend({
            PageNumber: 1,
            Records: 0,
            RecordsPerPage: 5,
            Options: [5, 10, 20, 30, 40, 50, 100],
            PageChange: null
        }, options);

        // evento interno disparado quando o valor da página muda
        var OnPageChange = function (page, opt) {

            console.log("Pagination:OnPageChange");
            console.log("Page: " + page);
            console.log("Option: " + opt);

            if (settings.PageChange != null) {
                console.log("Pagination:Changing");
                settings.PageChange(page, opt);
            }

        };

        // retorna a pagina atual como um numero inteiro
        var CurrentPage = function () {

            var val = $("#txtPage").val();

            return parseInt(val);

        };

        // retorna a quantidade de registros por pagina
        var CurrentOption = function () {

            var opt = $("#cbxPages").find("option:selected").val();

            return parseInt(opt);

        };

        var DoPageChange = function() {
        
            var pg = CurrentPage();
            var opt = CurrentOption();

            OnPageChange(pg, opt);
        }

        // altera o valor do input e dispara o evento OnPageChange
        // val: numero da pagina
        var ApplyValue = function (val) {

            var opt = CurrentOption();

            // atualiza a pagina atual
            $("#txtPage").val(val);

            // atualiza o total de paginas
            $("#spnPages").text(GetPages());

            // calcula o primeiro e ultimo registro da pagina
            $("#spnFirstRecord").text(GetFirstRecord(val));
            $("#spnLastRecord").text(GetLastRecord(val));

            // total de linhas retornadas pelo filtro
            $("#spnRecords").text(settings.Records);
        };

        // calcula o primeiro registro da pagina
        var GetFirstRecord = function (page) {
            var rpp = parseInt(settings.RecordsPerPage);
            var p = parseInt(page) - 1;
            return (rpp * p) + 1;
        };

        // calcula a quantidade de paginas
        var GetPages = function () {

            // qtd de registros
            var reg = parseInt(settings.Records);
            // reg por pagina
            var rpp = parseInt(settings.RecordsPerPage);
            // total de paginas
            var pags = parseInt(reg / rpp);
            if ((reg % rpp) != 0) {
                pags++;
            }
            return pags;

        }

        // calcula o ultimo registro da pagina
        var GetLastRecord = function (page) {
            var fr = parseInt(GetFirstRecord(page));
            var rpp = parseInt(settings.RecordsPerPage);
            return (fr + rpp) - 1;
        };

        // lista de opções de paginação
        var GetOptions = function ()  {

            var result = "";

            for(var i = 0; i <= settings.Options.length - 1; i++)
            {
                var val = settings.Options[i];

                if (result == "") {
                    result = "<option value='" + val + "'>" + val + "</option>";
                } else {
                    result += "<option value='" + val + "'>" + val + "</option>";
                }
            }
            return result;
        }

        var Setup = function (div) {

            var btnFirst = "<button id='btnFirst' type='button' class='btn btn-default btn-xs' title='Primeira pagina'><i class='icon-double-angle-left'></i></button>";
            var btnPrev = "<button id='btnPrev' type='button' class='btn btn-default btn-xs' title='Pagina anterior'><i class='icon-angle-left'></i></button>";
            var btnNext = "<button id='btnNext' type='button' class='btn btn-default btn-xs' title='Proxima pagina'><i class='icon-angle-right'></i></button>";
            var btnLast = "<button id='btnLast' type='button' class='btn btn-default btn-xs' title='Ultima pagina'><i class='icon-double-angle-right'></i></button>";

            var input = "<input type='text' id='txtPage' role='textbox' size='2' maxlength='3' style='font-size: inherit; width: 24px; height: 23px; line-height: 16px; -moz-box-sizing: content-box; text-align: center; padding-top: 1px; padding-bottom: 1px;' >";
            var combo = "<select id='cbxPages' class='ui-pg-selbox' role='listbox'></select>";

            var html = "<div style='background-color: #f5f5f5; height: 45px; padding: 7px 0 0 0'>" +
                       "    <div class='col-md-12' >" +
                       "        <div class='col-md-4' style='margin-top: -1px'>" +
                       "            &nbsp; " +
                       "        </div>" +
                       "        <div class='col-md-4' style='margin-top: -1px'>" +
                       "             " + btnFirst + " " + btnPrev + "| Pagina " + input + " de <span id='spnPages'>10</span> | " + btnNext + " " + btnLast + " " + combo +
                       "        </div>" +
                       "        <div class='col-md-4' style='text-align:right; margin-top:6px;'>" +
                       "            Linhas: <span id='spnFirstRecord'>1</span>-<span id='spnLastRecord'>10</span> de <span id='spnRecords'>5</span> registro(s)" +
                       "        </div>" +
                       "    </div>";
                       "</div>";



            $(div).empty();
            $(div).append(html);

        }

        
        // aplica o plugin a todos os controles usado no seletor
        return this.each(function(){

            Setup(this);

            $("#cbxPages").empty();
            $("#cbxPages").append(GetOptions());
            $("#cbxPages").val(settings.RecordsPerPage);

            // bind nos objetos associados
            ApplyValue(settings.PageNumber);

            // captura a troca do page length
            $("#cbxPages").bind("change", function () {

                var option = $(this).find("option:selected").val();
                
                // atualiza a quantidade de registros por pagina
                settings.RecordsPerPage = parseInt(option);

                // vai para a primeira página
                ApplyValue(1);

                // dispara o evento
                DoPageChange();

            });

            // bind para a primeira pagina
            $("#btnFirst").bind("click", function () {

                ApplyValue(1);

                // dispara o evento
                DoPageChange();

            });

            // bind para a proxima pagina
            $("#btnNext").bind("click", function () {

                var val = CurrentPage() + 1;

                if (val <= GetPages()) {

                    ApplyValue(val);

                    // dispara o evento
                    DoPageChange();

                }

            });

            // bind para a pagina anterior
            $("#btnPrev").bind("click", function () {

                var val = CurrentPage() - 1;

                if (val >= 1) {

                    ApplyValue(val);

                    // dispara o evento
                    DoPageChange();

                }

            });

            // bind para a ultima pagina
            $("#btnLast").bind("click", function () {

                ApplyValue(GetPages());

                // dispara o evento
                DoPageChange();

            });

        });

    };

}(jQuery));


self.GetCurrentPage = function(div) {
    return parseInt($("#" + div).find("#txtPage").val());
}

function GetRecordsPerPage(div) {
    return parseInt($("#" + div).find("#cbxPages option:selected").val());
}

function InitializePagination(div, page, records, recordsPerPage, callback) {

    $("#" + div).Pagination({
        PageNumber: page,
        Records: records,
        RecordsPerPage: recordsPerPage,
        PageChange: function (p, o) {
            callback(p, 0)
        }
    });

}
