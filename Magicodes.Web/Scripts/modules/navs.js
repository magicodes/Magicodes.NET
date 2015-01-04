define(["jquery"], function () {
    if (window.location != window.parent.location) window.parent.location = window.location;
    $(window).on('resize.window', function () {
        resetFrameHeight();
    });

    function resetFrameHeight() {
        //alert($(document).height() - $('#navbar').height() - $('#breadcrumbs').height() - 40);
        $('#mainContent').height($(document).height() - $('#navbar').height() - $('#breadcrumbs').height() - 40);
    }
    require(["knockoutJs"], function (ko) {
        var ViewModel = function () {
            var self = this;
            this.loading = ko.observable(false);
            this.api = "/api/Menus";
            this.menus = ko.observableArray([]);
            this.nav = ko.observable({ Href: ko.observable('') });
            this.breadcrumbs = ko.observableArray([]);
            this.activeNavs = ko.observableArray([]);
            //订阅  加载状态
            self.loading.subscribe(function (newValue) {
                if (newValue)
                    magicodes.loader.show();
                else
                    magicodes.loader.close();
            });
            //订阅  当前url状态
            self.nav.subscribe(function (newValue) {
                $('#mainContent').attr('src', newValue.Href);
                resetFrameHeight();
            });
            this.load = function () {
                self.loading(true);
                magicodes.api.request("GET", {
                    url: self.api,
                    func: function (data) {
                        self.loading(false);
                        var menus = $.grep(data, function (i) {
                            return i.ParentId == null;
                        });
                        $.each(menus, function (i, v) {
                            v._active = ko.observable(false);
                            v._open = ko.observable(false);
                            self._appendChildrenMenus(data, v);
                        });
                        self.menus(menus);
                        //ace.handle_side_menu($);
                        $('#sidebar').ace_sidebar();
                        menus.length > 0 && $('.nav-list a:eq(0)').trigger('click');
                    }
                });
            };
            //添加菜单子项
            //源数据
            //当前项
            this._appendChildrenMenus = function (data, itemData) {
                var childrenNavs = $.grep(data, function (i) {
                    return i.ParentId == itemData.Id;
                });
                if (childrenNavs.length > 0) {
                    itemData.children = childrenNavs;
                    $.each(childrenNavs, function (i, v) {
                        v._active = ko.observable(false);
                        v._open = ko.observable(false);
                        self._appendChildrenMenus(data, v);
                    });
                }
            };
            this.open = function (item, event) {
                if (typeof (item.children) !== "undefined") return;
                self.breadcrumbs([]);
                self.nav(item);
                $.each(self.activeNavs(), function (i, v) {
                    v._active(false);
                    v._open(false);
                });
                //导航路径
                var navs = [$(event.target).closest('li')];
                if (navs[0].parents('li').length > 0)
                    navs.push(navs[0].parents('li'));
                if (navs.length > 1 && navs[1].parents('li').length > 0) {
                    navs.push(navs[1].parents('li'));
                }
                for (var i = navs.length - 1; i >= 0; i--) {
                    var nav = ko.dataFor(navs[i].get(0));
                    nav._active(true);
                    if (i == navs.length - 1 && navs.length > 1)
                        nav._open(true);
                    self.breadcrumbs.push(nav);
                    self.activeNavs.push(nav);
                    //ko.contextFor(navs[1].get(0)).$data
                    //ko.dataFor(navs[1].get(0))
                }
            };
            self.load();
        };

        $(function () {
            ko.applyBindings(new ViewModel());
            resetFrameHeight();
        });
    });

});