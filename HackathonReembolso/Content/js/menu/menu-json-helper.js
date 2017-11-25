var MenuJsonHelper = function (options) {
    'use strict';
    var self = this;
    self.defaultOptions = {
        token: "",
        tokenName: ""
    };
    self.options = $.extend({}, self.defaultOptions, options);
    self.formatURL = function (url) {
        var result = url || "#";
        if (self.options.token === "") {
            return result;
        }
        // se a URL contém "?" então concatena o TOKEN usando & senão ?
        if (result.indexOf("?") >= 0) {
            result += "&" + self.options.tokenName + "=" + self.options.token;
        } else {
            result += "?" + self.options.tokenName + "=" + self.options.token;
        }
        return result;
    };
    self.toOptions = function (jsonItem) {
        var options = {
            id: jsonItem.id,
            href: self.formatURL(jsonItem.Url),
            icon: jsonItem.CdIcon,
            text: jsonItem.Titulo,
            active: jsonItem.active,
            expand: jsonItem.expand,
            order: jsonItem.NrOrdem || 0,
            datasource: []
        };
        if (jsonItem.datasource !== null && jsonItem.datasource !== undefined) {
            $.each(jsonItem.datasource, function (index, dtsItem) {
                var mni = self.toMenuItem(dtsItem);
                options.datasource.push(mni);
            });
        }
        return options;
    };
    self.toMenuItem = function (jsonItem) {
        var options = self.toOptions(jsonItem);
        return new MenuItem(options);
    };
    self.toDataSource = function (json) {
        var list = [];
        $.each(json, function (index, jsonItem) {
            var mni = self.toMenuItem(jsonItem);
            list.push(mni);
        });
        return list;
    };
    self.sort = function (list) {
        return (list || []).sort(function (a, b) { return a.options.order - b.options.order });
    }
    return self;
}