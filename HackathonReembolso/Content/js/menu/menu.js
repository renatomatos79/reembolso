var MainMenu = function (options) {
    'use strict';
    var self = this;
    self.defaultOptions = {
        id: "",
        datasource: []
    };
    self.options = $.extend({}, self.defaultOptions, options);
    self.clear = function () {
        $(self.options.id).empty();
        return self;
    }
    self.draw = function () {
        var html = [];
        var items = new MenuJsonHelper().sort(self.options.datasource);
        $.each(items, function (index, item) {
            html.push(item.toString());
        });
        $(self.options.id).append(html.join(""));
        return self;
    };
    return self;
}