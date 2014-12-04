(function() {
    // Write your code below.
    
})(window.Zepto);

+function ($) {
    var f, pH, wH;
    f = $(".footer-fixed");
    pH = document.body.clientHeight;
    wH = document.documentElement.clientHeight;
    if (pH < wH) {
        f.css("position", "fixed");
        f.css("bottom", "0");
    } else if (pH >= wH) {
        f.css("position", "static");
    }
}(jQuery);