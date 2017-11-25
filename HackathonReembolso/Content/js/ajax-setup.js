var InternalSite = new Gsnet.Site();

InternalSite.SetLoginURL = function (url) {
    this.LoginURL = url;
}

InternalSite.GetLoginURL = function () {
    return this.LoginURL;
}

$.ajaxSetup({
    statusCode: {
        401: function (data) {
            window.location.href = InternalSite.GetLoginURL() + "/home/index";
        }
    }
});