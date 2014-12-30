var min = typeof (appConfig)!="undefined" && appConfig.minJs ? '.min' : '';
var jqPath = 'ace/js/jquery' + min;

var rBrowser = {
    uaMatch: function (ua) {
        ua = ua.toLowerCase();
        var match = /(chrome)[ \/]([\w.]+)/.exec(ua) ||
            /(webkit)[ \/]([\w.]+)/.exec(ua) ||
            /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
            /(msie) ([\w.]+)/.exec(ua) ||
            ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
            [];

        return {
            browser: match[1] || "",
            version: match[2] || "0"
        };
    },
    init: function () {
        var matched, browser;
        matched = this.uaMatch(navigator.userAgent);
        browser = {};
        if (matched.browser) {
            browser[matched.browser] = true;
            browser.version = matched.version;
        }
        // Chrome is Webkit, but Webkit is also Safari.
        if (browser.chrome) {
            browser.webkit = true;
        } else if (browser.webkit) {
            browser.safari = true;
        }
        this.browser = browser;
    }
};
rBrowser.init();
if ((rBrowser.browser.mozilla || rBrowser.browser.msie) && rBrowser.browser.version <= 9)
    jqPath = "ace/js/jquery1x" + +min;

require.config({
    baseUrl: '/scripts',
    waitSeconds: 30,
    paths: {
        "jquery": jqPath,
        "html5shiv": 'ace/js/html5shiv' + min,
        "respond": 'ace/js/respond' + min,
        "bootstrap": 'ace/js/bootstrap' + min,
        "jquery-ui": 'ace/js/jquery-ui.custom' + min,
        "jquery.ui.touch-punch": 'ace/js/jquery.ui.touch-punch' + min,
        "fullcalendar": 'ace/js/fullcalendar' + min,
        "bootbox": 'ace/js/bootbox' + min,
        "dropzone": 'ace/js/dropzone' + min,
        "ace-elements": "ace/js/ace-elements" + min,
        "ace-extra": "ace/js/ace-extra" + min,
        "aceJs": "ace/js/ace" + min,
        "json2": "ace/js/json2" + min,
        "jquery.slimscroll": "ace/js/jquery.slimscroll" + min,
        "jquery.mobile": "ace/js/jquery.mobile.custom" + min,
        "fullcalendar": "ace/js/fullcalendar" + min,
        //"jquery.gritter": "ace/js/jquery.gritter",
        "jquery.gritter": "ace/js/jquery.gritter" + min,
        "jq.browsers": "util/browsers" + min,
        "aceCheck": "util/aceCheck" + min,
        "loading": "util/loading" + min,
        "jquery.dataTables": "ace/js/dataTables/jquery.dataTables" + min,
        "jquery.dataTables.bootstrap": "ace/js/dataTables/jquery.dataTables.bootstrap" + min,
        "jquery.jqGrid": "ace/js/jqGrid/i18n/grid.locale-cn" + min,
        "metro-uiJs": "metro-ui/metro" + min,
        "jq.nestable": "ace/js/jquery.nestable" + min,
        "jq.headroom": "jqPlus/headroom" + min,
        "bt.datepicker": "ace/js/date-time/bootstrap-datepicker.zh-CN" + min,
        "jq.form": "metro-ui/jquery.form" + min,
        "fuelux.tree": "ace/js/fuelux/fuelux.tree" + min,
        "excanvas": "ace/js/excanvas" + min,
        "select2": "ace/js/select2" + min,
        "bt.timepicker": "ace/js/date-time/bootstrap-timepicker" + min,
        "knockoutJs": "knockout/knockout" + min,
        "kk.mapping": "knockout/knockout.mapping" + min,
        "moment": "knockout/moment/zh-cn" + min,
        "magicodes": "magicodes" + min,
        "kk.validate": "jquery.validate.unobtrusive",
        "jq.inputlimiter": "ace/js/jquery.inputlimiter.1.3.1" + min,
        "jq.autosize": "ace/js/jquery.autosize" + min,
        "jq.easy-pie-chart": "ace/js/jquery.easy-pie-chart" + min,
        "jq.signalR": "/signalr/hubs?noext",
        "bt.Wysiwyg": "ace/js/bootstrap-wysiwyg" + min,
        "jq.colorbox": "ace/js/colorbox/jquery.colorbox" + min
    },
    shim: {
        "jquery-ui": ["jquery"],
        "jquery.ui.touch-punch": ["jquery", 'jquery-ui'],
        "jquery.slimscroll": ["jquery"],
        "bootstrap": ["jquery"],
        //"bootstrap": ["jquery", 'css!ace/js/styles/bootstrap.min'],
        "ace-elements": ["jquery", "bootstrap"],
        "aceJs": ["jquery", "ace-elements", "ace-extra", "bootstrap", "aceCheck"],
        //"aceJs": ['css!ace/js/styles/font-awesome'+min, "css!ace/js/styles/jquery.gritter", 'css!ace/js/styles/ace'+min, 'css!ace/js/styles/ace-skins'+min, 'css!ace/js/styles/ace-rtl'+min, "jquery", "ace-elements", "ace-extra", "bootstrap", "aceCheck"],
        "jquery.mobile": ["jquery"],
        "fullcalendar": ["jquery", "jquery-ui", "css!ace/js/styles/fullcalendar" + min],
        "jquery.gritter": ["jquery", "css!ace/js/styles/jquery.gritter" + min],
        "dropzone": ["jquery", "css!ace/js/styles/dropzone" + min],
        "jq.colorbox": ["jquery", "css!ace/js/colorbox/colorbox" + min],
        "bootbox": ["jquery", "aceJs"],
        "aceCheck": ["jquery", "jq.browsers"],
        "jquery.dataTables": ["jquery", "bootstrap"],
        "jquery.dataTables.bootstrap": ["jquery", "jquery.dataTables", "aceJs"],
        "jquery.jqGrid": ["jquery", "css!ace/js/styles/ui.jqgrid" + min, "ace/js/jqGrid/jquery.jqGrid.src" + min, "aceJs"],
        "jq.nestable": ["jquery"],
        "jq.headroom": ["jquery"],
        "bt.datepicker": ["jquery", "css!ace/js/styles/datepicker" + min, "bootstrap", "ace/js/date-time/bootstrap-datepicker" + min],
        "jq.form": ["jquery"],
        "fuelux.tree": ["jquery"],
        "select2": ["jquery", "css!ace/js/styles/select2" + min],
        "bt.timepicker": ["jquery", "css!ace/js/styles/bootstrap-timepicker" + min],
        "excanvas": "ace/js/excanvas" + min,
        "moment": ["knockout/moment/moment" + min],
        "knockoutJs": ["jquery", "moment"],
        "magicodes": ["jquery", "aceJs", "knockoutJs"],
        "jq.validation": ["jquery", "jqPlus/jquery-validation/jquery.validate" + min],
        "kk.validate": ["jq.validation"],
        "jq.easy-pie-chart": ["jquery", "aceJs"],
        "jquery.signalR-2.1.2.min": ["jquery"],
        "jq.signalR": ["jquery", "jquery.signalR-2.1.2" + min],
        "bt.Wysiwyg": ["jquery", "aceJs", "ace/js/jquery.hotkeys" + min]
    },
    map: {
        '*': {
            'css': 'css'
        }
    }
});
'ontouchstart' in document.documentElement && require(["jquery.mobile", "jquery.ui.touch-punch"]);
if ((rBrowser.browser.mozilla || rBrowser.browser.msie) && rBrowser.browser.version <= 9) {
    require(["jquery"], function () {
        $(function () {
            $('body').prepend('<h2 style="color:yellow;padding:10px;">当前系统不支持此浏览器，请下载最新安全浏览器。<br /><a href="http://baoku.360.cn/soft/show/appid/105189">点此下载</a></h2>');
        });
    });
}