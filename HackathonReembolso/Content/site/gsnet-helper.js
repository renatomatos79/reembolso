Gsnet.Helper = function () {

    var self = this;

    self.formatDate = function (date) {
        date = date.replace(/[^0-9 +]/g, '');
        var dateObj = new Date(parseInt(date));

        var year = dateObj.getFullYear();
        var month = (dateObj.getMonth() + 1);
        var day = dateObj.getUTCDate()

        var formatedMonth = (month < 10) ? "0" + month : month;
        var formatedDay = (day < 10) ? "0" + day : day;

        return day + "/" + formatedMonth + "/" + year;
    }   

    return self;

}