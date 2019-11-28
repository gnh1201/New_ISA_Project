// Author: Go Namhyeon <gnh1201@gmail.com>

(function($) {
    $.fn.App = {
        // set routing rules
        "routes": [
            { "path": "/", "tmpl": "main.tmpl.html" },
            { "path": "/notice", "tmpl": "notice.tmpl.html" },
            { "path": "/assign", "tmpl": "assign.tmpl.html" },
            { "path": "/bundle", "tmpl": "bundle.tmpl.html" },
			{ "path": "/license", "tmpl": "license.tmpl.html" },
			{ "path": "/license-form", "tmpl": "license.form.tmpl.html" }
        ],

        // set default title
        "title": "ISA Agent Control Panel",

        // set API URL
        "apiUrl": "http://localhost:8877/api",

		// set debug API URL
		"debugApiUrl": "https://catswords.re.kr/ep/",

        // set page
        "page": 1,

        // set limit per page
        "limit": 20,

        // add script
        "addScript": function(url, callback) {
            var $s = $("<script/>").attr({
                "type": "text/javascript",
                "src": url
            }).appendTo("head");
            $s.promise().done(function() {
                if(typeof(callback) == "function") {
                    callback($s[0], $s[0].innerHTML);
                }
            });
        },

        "addStylesheet": function(url, callback) {
            $s = $("<link/>").attr({
                "rel": "stylesheet",
                "type": "text/css",
                "href": url
            }).appendTo("head");
            $s.promise().done(function() {
                if(typeof(callback) == "function") {
                    callback($s[0], $s[0].innerHTML);
                }
            });
        },

        "initScripts": function() {
            // disabled auto-completable form
            $("form.autocomplete-disabled").attr("autocomplete", "off");

            // disable ime
            $("input.ime-disabled,textarea.ime-disabled").css("ime-mode", "disabled");

            // set required attribute
            $("input.required,textarea.required").prop("required", true);

            // ARIA: aria-hidden, html5 hidden attribute, display is none
            $(".aria-hidden").prop({
                "aria-hidden": true,
                "hidden": true
            }).css({
				"display": "none"
			});
        },

        "initClicks": function(_options) {
            return function(e) {
                if($(this).attr("href").indexOf(":/") < 0) {
                    e.preventDefault();
                    $().App.renderTemplate($(this).attr("href"), $().App.routes, false, _options);
                }
            }
        },

        // main
        "main": function(_options) {
            // get current uri
            var uri = $().App.getCurrentUri();
            var uiContent = $(".ui-content");

            // load template by each routing rule
            $().App.renderTemplate(uri, $().App.routes, false, _options);

            // when click spa links
            $("a[href!='#']").click($().App.initClicks(_options));

            // detect backward button
            window.onpopstate = function(e) {
                if(e.state) {
                    uiContent.html(e.state.html);
                    uiContent.find("a[href!='#']").click($().App.initClicks(_options));
                    $(document).find("title").text(e.state.pageTitle);
                    $().App.initScripts();
                }
            };

            // debugging onerror
            window.onerror = function(message, url, lineNo, colNo, error) {
                var _row = [message, url, lineNo, colNo, (error && error.stack)];
                var data = "('" + _row.join("','") + "')<=(Message,URL,Line,Column,Stack)";
                var qs = $.param({
                    "route": "ajax.error.gif",
                    "data": data
                });
                $("<img/>").attr("src", $().App.debugApiUrl + "?" + qs).appendTo("body");
            };

            // ui-form (ajaxform)
            $("form.ui-form").ajaxForm({
                dataType: "json",
                beforeSubmit: function(requestData, jqForm, options) {
                    if("beforeSubmit" in _options) {
                        _options.beforeSubmit(_path, requestData);
                    }
                },
                success: function(responseData, statusText, xhr, $form)  {
                    var _path = $form.find("input[name='p']").val();
                    $().App.renderTemplate(_path, $().App.routes, responseData, _options);
                    if("afterSubmit" in _options) {
                        _options.afterSubmit(_path, responseData);
                    }
                }
            });

            // init scripts
            $().App.initScripts();
        },
        "getCurrentUri": function() {
            return $(location).attr("href");
        },

        "parseQuery": function(query) {
            var queries = {};
            var _parse = function(_query, _pos) {
                var __query = _query.substring(0, _pos);
                var __sperator = "=";
                var __pos = __query.indexOf(__sperator);
                var __name = "";
                if(__pos > -1) {
                    __name =  __query.substring(0, __pos);
                    if(__name.length > 0) {
                        queries[__name] = __query.substring(__pos + __sperator.length);
                    }
                }
            };

            var _query = query;
            if(!!_query) {
                var _seperator = "&";
                var _pos = _query.indexOf(_seperator);
                while(_pos > -1) {
                    _parse(_query);
                    _query = _query.substring(_pos + _seperator.length);
                    _pos = _query.indexOf(_seperator);
                }
                _parse(_query);
            }

            return queries;
        },

        "renderTemplate": function(uri, routes, _data, _options) {
            var _uri = URI.parse(uri);
            var _path = _uri.path;
            var _query = $().App.parseQuery(_uri.query);
            var _tmpl = "404.tmpl.html";
            var _matched = false;
            var _fail = function(xhr, status, error) {
                var _row = [xhr, status, error, xhr.responseText];
                var data = "('" + _row.join("','") + "')<=(xhr,status,error,responseText)";
                var qs = $.param({"route": "ajax.error.gif", "data": data});
                $("<img/>").attr("src", $().App.debugApiUrl + "?" + qs).appendTo("body");
                console.log(data);
                alert("We're sorry. Please try again in a few minutes.");
            };
            var _success = function(response) {
                var uiContent = $(".ui-content");
                var html = $.render.tmpl(response);
                var pageTitle = ("title" in response) ? response.title : $().App.title;

                // make content
                uiContent.html(html);
                uiContent.find("a[href!='#']").click($().App.initClicks(_options));
                window.history.pushState({"html": html, "pageTitle": pageTitle}, "" , _path);
                $(document).find("title").text(pageTitle);
                $().App.initScripts();

                // after rendering template
                if("afterRenderTemplate" in _options) {
                    _options.afterRenderTemplate(_path, _data);
                }
            };

            // override path
            _path = ("p" in _query) ? _query.p : _path;

            // checking matched rule (try 1)
            for(var i in routes) {
                var paths = routes[i].path.split("/");
                var pos = routes[i].path.indexOf("/:");
                var _paths = _path.split("/");

                // parse URI parameters
                if((paths.length - _paths.length) == 0 && pos > -1) {
                     if(_path.indexOf(routes[i].path.substring(0, pos)) == 0) {
                        _tmpl = routes[i].tmpl;
                        _matched = true;
                        for(var k in paths) {
                            if(paths[k].indexOf(":") == 0) {
                                _query[paths[k].substring(1)] = _paths[k];
                            }
                        }
                    }
                }
            }

            // checking matched rule (try 2)
            if(_matched == false) {
                for(var i in routes) {
                     if(_path.indexOf(routes[i].path) == 0) {
                        _tmpl = routes[i].tmpl;
                        _matched = true;
                     }
                }
            }

            // before rendering template
            if("beforeRenderTemplate" in _options) {
                _options.beforeRenderTemplate(_path, _data);
            }

            // load template file
            $.get("/templates/" + _tmpl, function(response) {
                $.templates({ tmpl: response });

                if(!!_data) {
                    _success(_data);
                } else {
                    $().App.getItems(_path, {
                        "page": $().App.page,
                        "limit": $().App.limit,
                        "query": _query,
                        "referer": $().App.getCurrentUri()
                    }, _success, _fail);
                }
            }, "html").fail(_fail);
        },

        // get items from remote server
        "getItems": function(uri, data, callback, error) {
			if(uri == "/") {
				uri = "/main";
			}

            $.ajax({
                url: $().App.apiUrl + uri,
                type: "post",
                dataType: "json",
                data: data,
                success: callback,
                error: error
            });
        }
    }
}(jQuery));

$(document).ready(function() {
    $().App.main({});
});

$(window).on("load", function() {
    // todo
});
