/// <reference path="ace/bootstrap-wysiwyg.min.js" />
/// <reference path="jquery.signalR-2.1.2.min.js" />
var jqPath = 'ace/jquery-2.0.3.min';
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
    jqPath = "ace/jquery-1.11.1.min";

require.config({
    baseUrl: '/scripts',
    waitSeconds: 30,
    paths: {
        "jquery": jqPath,
        "html5shiv": 'ace/html5shiv',
        "respond": 'ace/respond.min',
        "bootstrap": 'ace/bootstrap.min',
        "jquery-ui": 'ace/jquery-ui-1.10.3.custom.min',
        "jquery.ui.touch-punch": 'ace/jquery.ui.touch-punch.min',
        "fullcalendar": 'ace/fullcalendar.min',
        "bootbox": 'ace/bootbox.min',
        "dropzone": 'ace/dropzone.min',
        "ace-elements": "ace/ace-elements.min",
        "ace-extra": "ace/ace-extra.min",
        "aceJs": "ace/ace.min",
        "json2": "ace/json2.min",
        "jquery.slimscroll": "ace/jquery.slimscroll.min",
        "jquery.mobile": "ace/jquery.mobile.custom.min",
        "fullcalendar": "ace/fullcalendar.min",
        //"jquery.gritter": "ace/jquery.gritter",
        "jquery.gritter": "ace/jquery.gritter.min",
        "jq.colorbox": "ace/colorbox/i18n/jquery.colorbox-zh-CN",
        "jq.browsers": "util/browsers",
        "aceCheck": "util/aceCheck",
        "loading": "util/loading",
        "jquery.dataTables": "ace/jquery.dataTables.min",
        "jquery.dataTables.bootstrap": "ace/jquery.dataTables.bootstrap",
        "jquery.jqGrid": "ace/jqGrid/i18n/grid.locale-cn",
        "metro-uiJs": "metro-ui/metro.min",
        "jquery.ui.widget": "metro-ui/jquery.ui.widget",
        "jq.plupload": "plupload-2.1.1/i18n/zh_CN",
        "jq.nestable": "ace/jquery.nestable.min",
        "jq.headroom": "jqPlus/headroom.min",
        "bt.datepicker": "ace/date-time/bootstrap-datepicker.zh-CN",
        "jq.form": "metro-ui/jquery.form.min",
        "fuelux.tree": "ace/fuelux/fuelux.tree.min",
        "jq.mousewheel": "/SitePages/QL.Resources/Scripts/jquery.mousewheel.min",
        "excanvas": "ace/excanvas.min",
        "select2": "ace/select2.min",
        "bt.timepicker": "ace/date-time/bootstrap-timepicker.min",
        "jq.tmpl": "jqPlus/jquery.tmpl/jquery.tmpl",
        "knockoutJs": "knockout/knockout",
        "kk.mapping": "knockout/knockout.mapping.min",
        "moment": "knockout/moment/zh-cn.min",
        "magicodes": "magicodes",
        "jq.validation": "jqPlus/jquery-validation/localization/messages_zh",
        //knockout.jq.validate
        "kk.validate": "jquery.validate.unobtrusive",
        "jq.inputlimiter": "ace/jquery.inputlimiter.1.3.1.min",
        "jq.autosize": "ace/jquery.autosize.min",
        "jq.easy-pie-chart": "ace/jquery.easy-pie-chart.min",
        "jq.signalR": "/signalr/hubs?noext",
        "bt.Wysiwyg": "ace/bootstrap-wysiwyg.min"
    },
    shim: {
        "jquery-ui": ["jquery"],
        "jquery.ui.touch-punch": ["jquery", 'jquery-ui'],
        "jquery.slimscroll": ["jquery"],
        "bootstrap": ["jquery"],
        //"bootstrap": ["jquery", 'css!ace/styles/bootstrap.min'],
        "ace-elements": ["jquery", "bootstrap"],
        "aceJs": ["jquery", "ace-elements", "ace-extra", "bootstrap", "aceCheck"],
        //"aceJs": ['css!ace/styles/font-awesome.min', "css!ace/styles/jquery.gritter", 'css!ace/styles/ace.min', 'css!ace/styles/ace-skins.min', 'css!ace/styles/ace-rtl.min', "jquery", "ace-elements", "ace-extra", "bootstrap", "aceCheck"],
        "jquery.mobile": ["jquery"],
        "fullcalendar": ["jquery", "jquery-ui", "css!ace/styles/fullcalendar"],
        "jquery.gritter": ["jquery", "css!ace/styles/jquery.gritter"],
        "dropzone": ["jquery", "css!ace/styles/dropzone"],
        "jq.colorbox": ["jquery", "css!ace/colorbox/colorbox", "ace/colorbox/jquery.colorbox-min"],
        "bootbox": ["jquery", "aceJs"],
        "aceCheck": ["jquery", "jq.browsers"],
        "jquery.dataTables": ["jquery", "bootstrap"],
        "jquery.dataTables.bootstrap": ["jquery", "jquery.dataTables", "aceJs"],
        "jquery.jqGrid": ["jquery", "css!ace/styles/ui.jqgrid.css", "ace/jqGrid/jquery.jqGrid.src", "aceJs"],
        "jquery.ui.widget": ["jquery"],
        "plupload-2.1.1/plupload.full.min": ["jquery"],
        "jq.plupload": ["jquery", "plupload-2.1.1/plupload.full.min"],
        "jq.nestable": ["jquery"],
        "jq.headroom": ["jquery"],
        "bt.datepicker": ["jquery", "css!ace/styles/datepicker", "bootstrap", "ace/date-time/bootstrap-datepicker.min"],
        "jq.form": ["jquery"],
        "fuelux.tree": ["jquery"],
        "select2": ["jquery", "css!ace/styles/select2"],
        "bt.timepicker": ["jquery", "css!ace/styles/bootstrap-timepicker"],
        "jq.mousewheel": "/SitePages/QL.Resources/Scripts/jquery.mousewheel.min",
        "excanvas": "ace/excanvas.min",
        "jq.tmpl": ["jquery"],
        "moment": ["knockout/moment/moment.min"],
        "knockoutJs": ["jquery", "moment"],
        "magicodes": ["jquery", "aceJs", "knockoutJs"],
        "jq.validation": ["jquery", "jqPlus/jquery-validation/jquery.validate.min"],
        "kk.validate": ["jq.validation"],
        "jq.easy-pie-chart": ["jquery", "aceJs"],
        "jquery.signalR-2.1.2.min": ["jquery"],
        "jq.signalR": ["jquery", "jquery.signalR-2.1.2.min"],
        "bt.Wysiwyg": ["jquery", "aceJs", "ace/jquery.hotkeys.min"]
    },
    map: {
        '*': {
            'css': 'css'
        }
    }
});
'ontouchstart' in document.documentElement && require(["jquery.mobile", "jquery.ui.touch-punch"]);
//if ((rBrowser.browser.mozilla || rBrowser.browser.msie) && rBrowser.browser.version <= 9) {
//    require(["jquery"], function () {
//        $(function () {
//            $('body').prepend('<h2 style="color:yellow;padding:10px;">当前系统不支持此浏览器，请下载本系统专用安全浏览器。<br /><a href="/Content/chrome/Chrome_36.0.1985.143.exe">点此下载</a></h2>');
//        });
//    });
//}