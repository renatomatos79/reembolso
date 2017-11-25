var HttpProxy = function (options) {
    'use strict';

    var self = this;

    self.defaultOptions = {
        headers: {},
        method: "GET",
        data: {},
        url: "",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        onBeforeSend: null,
        onComplete: null,
        onSuccess: null,
        onError: null
    };

    self.options = $.extend({}, self.defaultOptions, options);

    self.execute = function () {
        $.ajax({
            url: self.options.url,
            data: self.options.data,
            headers: self.options.header,
            dataType: self.options.dataType,
            contentType: self.options.contentType,
            type: self.options.method,
            async: true,
            beforeSend: function (xhr) {
                if (self.options.onBeforeSend != null && self.options.onBeforeSend !== undefined) {
                    self.options.onBeforeSend(xhr);
                } 
            },
            success: function (data) {
                if (self.options.onSuccess != null && self.options.onSuccess !== undefined) {
                    return self.options.onSuccess(data);
                }
            },
            complete: function (data) {
                if (self.options.onComplete != null && self.options.onComplete !== undefined) {
                    return self.options.onComplete(data);
                }
            },
            error: function (data) {
                if (self.options.onError != null && self.options.onError !== undefined) {
                    return self.options.onError(data);
                }
            }
        });
        return true;
    };

    return self;
}

var HttpHeader = function () {

    var self = this;

    /* get Access Token from LocalStorage */
    self.getATK = function () {
        var storage = window.localStorage;
        return storage.getItem("JWT");
    }

    self.getHeader = function () {
        var token = self.getATK();
        return { "x": token };
    }

    self.getTokenName = function () {
        return "x";
    }

    return self;
}
