var root = "";
var cdn = "";

Gsnet.Site = function () {

    var self = this;
	
    self.SetURL = function (url) {
        root = url;
    };

    self.SetCDN = function (url) {
        cdn = url;
    };

    self.URL = function () {
        return root;
    };

    self.CDN = function () {
        return cdn;
    }

    return self;
}