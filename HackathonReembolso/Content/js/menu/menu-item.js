var MenuItem = function (options) {
    'use strict';
    var self = this;
    self.defaultOptions = {
        id: "mni",
        href: "#",
        icon: "",
        text: "",
        order: 0,
        active: false,
        expand: false,
        datasource: []
    };
    self.options = $.extend({}, self.defaultOptions, options);
    self.clear = function () {
        $(self.options.id).empty();
        return self;
    };
    self.getItemClass = function (item) {
        var html = [];
        if (item.options.active) {
            html.push("active");
        }
        return html.length > 0 ? "class=\"" + html.join(" ") + "\"" : "";
    }
    self.getAnchorClass = function (item) {
        var html = [];
        if (item.options.datasource.length > 0) {
            html.push("menu-dropdown");
        }
        return html.length > 0 ? "class=\"" + html.join(" ") + "\"" : "";
    }
    self.getHTML = function (item) {
        var itemClass = self.getItemClass(item);
        var anchorClass = self.getAnchorClass(item);
        var hasChildren = item.options.datasource.length > 0;

        var html = [];
        html.push("<li " + itemClass + ">");
        html.push("     <a href=\"" + item.options.href + "\" " + anchorClass + ">");
        html.push("         <i class=\"" + item.options.icon + "\"></i>");
        html.push("         <span class=\"menu-text\">" + item.options.text + "</span>");
        if (hasChildren && item.options.expand) {
            html.push("         <i class=\"menu-expand\"></i>");
        }
        html.push("     </a>");

        if (hasChildren) {
            html.push("     <ul class=\"submenu\">");
            var sorted = new MenuJsonHelper().sort(item.options.datasource);
            $.each(sorted, function (index, subitem) {
                html.push(subitem.toString());
            });
            html.push("     </ul>");
        }

        html.push("</li>");
        return html.join("");
    };
    self.toString = function () {
        return self.getHTML(self);
    };
    return self;
}