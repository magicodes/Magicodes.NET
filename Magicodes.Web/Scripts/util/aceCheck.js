define(["jquery", "jq.browsers"], function () {
    if (($.browser.mozilla || $.browser.msie) && $.browser.version <= 9)
        require(["css!ace/styles/ace-part2.min", "css!ace/styles/ace-ie.min"]);
    if (($.browser.mozilla || $.browser.msie) && $.browser.version <= 8)
        require(["html5shiv", "respond", "excanvas"]);
    //if ('ontouchstart' in document.documentElement) 
    //    require(["jquery.mobile", "jquery.ui.touch-punch"]);
});