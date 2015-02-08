var magicodes = magicodes || {};
magicodes.api = {
    request: function (action, setting) {
        switch (action.toUpperCase()) {
            case "GET":
            case "DELETE":
                this._sendRequest({ url: setting.url, type: action, data: setting.data, func: setting.func, error: setting.error });
                break;
            case "POST":
            case "PUT":
                this._sendRequest({ url: setting.url, type: action, data: JSON.stringify(setting.data), func: setting.func, error: setting.error });
                break;
            default:
                this._sendRequest({ url: setting.url, type: action, data: setting.data, func: setting.func, error: setting.error });
                break;
        }
    },
    _sendRequest: function (setting) {
        $.ajax({
            url: setting.url,
            type: setting.type,
            data: setting.data,
            cache: false,
            accepts: "application/json",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            statusCode: {
                200/*成功响应*/: function (data) {
                    $.isFunction(setting.func) && setting.func(data, 200);
                },
                201 /*创建成功*/: function (data) {
                    $.isFunction(setting.func) && setting.func(data, 201);
                },
                401/*需要验证*/: function (jqXHR, textStatus, errorThrown) {
                    window.location.href = '/Login';
                },
                204/*成功响应，无内容返回*/: function (data) {
                    $.isFunction(setting.func) && setting.func(data, 204);
                },
                400 /*请求失败*/: function (jqxhr) {
                    var result = $.parseJSON(jqxhr.responseText);
                    if ($.isFunction(setting.error)) {
                        if (setting.error(result, 400)) {
                            return;
                        }
                    }
                    if (result.error) {
                        var message = result.error.innererror && result.error.innererror.message ? result.error.innererror.message : result.error.message;
                        magicodes.messager.showErrorMessage('错误', message.replace(/\r\n/gi, '<br />'));
                    } else if (result.ModelState) {
                        var message = result.Message;
                        var detail = '<br />';
                        for (var p in result.ModelState) {
                            //var arr = p.split('.');
                            //var name = arr[arr.length - 1];
                            detail += result.ModelState[p] + '<br />';
                        }
                        magicodes.messager.showErrorMessage('错误', message.replace(/\r\n/gi, '<br />') + detail);
                    }
                },
                500/*服务器错误*/: function (jqxhr) {
                    var result = $.parseJSON(jqxhr.responseText);
                    if ($.isFunction(setting.error)) {
                        if (setting.error(result, 400)) {
                            return;
                        }
                    }
                    if (result.error) {
                        var message = result.error.innererror && result.error.innererror.message ? result.error.innererror.message : result.error.message;
                        magicodes.messager.showErrorMessage('操作失败', message.replace(/\r\n/gi, '<br />'));
                    } else if (result.Message) {
                        var message = result.Message;
                        magicodes.messager.showErrorMessage('操作失败', message.replace(/\r\n/gi, '<br />'));
                    } else if (result.message) {
                        var message = result.message;
                        magicodes.messager.showErrorMessage('操作失败', message.replace(/\r\n/gi, '<br />'));
                    }
                }
            }
        }).fail(function (jqxhr, textStatus) {
            var result = $.parseJSON(jqxhr.responseText);
            console.error(result);
        });
    }
};
magicodes.odataGrid = function (setting) {
    var defaults = {
        //接口路径（仅支持WebAPI和OData协议）
        url: '',
        //加载完成事件
        onloaded: function () { },
        //当前绑定的元素
        bindElement: null,
        //视图模型创建完成事件，在此可以修改视图模型
        onViewModelCreated: null,
        //主键字段
        keyName: "Id",
        //主键类型
        keyType: "string",
        //筛选模板：Demo  contains(Content,{#txtSearch})
        filterTemplate: null,
        //排序
        $orderby: null,
        //分页数
        pageSize: 10,
        //绑定完毕事件
        onBound: null,
        //绑定前事件
        onBinding: null
    };
    this.options = $.extend(defaults, setting);
    var that = this;
    this.ViewModel = function () {
        var self = this;
        this.gridViewModel = {
            pages: ko.observable([]),
            dataRows: ko.observableArray([]),
            pageSize: ko.observable(typeof (that.options.pageSize) == "undefined" ? 10 : that.options.pageSize),
            totalCount: ko.observable(0),
            currentPageIndex: ko.observable(1),
            pageOptions: ko.observableArray([10, 25, 50, 100]),
        };
        this.$filter = null;
        this.$orderby = null;
        this.$select = null;
        //加载状态
        this.loading = ko.observable(false);
        //表单数据
        this.formData = ko.observable(null);
        this.firstLoad = true;
        var bindElement = that.options.bindElement || document.body;
        //订阅  加载状态
        self.loading.subscribe(function (newValue) {
            if (newValue) {
                //magicodes.loader.show($(bindElement));
            }
            //else
            //magicodes.loader.close();
        });
        this.addItem = function (rowData) {
            this.items.push(rowData);
        };
        this.nextPage = function () {
            this.gridViewModel.currentPageIndex() < this.getTotalPages() && this.gridViewModel.currentPageIndex(this.gridViewModel.currentPageIndex() + 1);
        };
        this.previousPage = function () {
            this.gridViewModel.currentPageIndex() > 1 && this.gridViewModel.currentPageIndex(this.gridViewModel.currentPageIndex() - 1)
        };
        this.getTotalPages = function () {
            var i = this.gridViewModel.totalCount() / this.gridViewModel.pageSize();
            return Math.floor(i) + (i > Math.floor(i) ? 1 : 0);
        };
        this.getPagesArr = function () {
            var totalPages = this.getTotalPages();
            var limitCount = totalPages > 10 ? 10 : totalPages;
            var currentPageIndex = this.gridViewModel.currentPageIndex();
            var min = 1, max = limitCount;
            if (currentPageIndex > 5) {
                if (totalPages - currentPageIndex > 5) {
                    min = currentPageIndex - 4; max = currentPageIndex + 5;
                }
                else {
                    min = currentPageIndex - 9; max = totalPages;
                }
            }
            if (min < 1) min = 1;
            return ko.utils.range(min, max);
        };
        this.filter = function () {
            if (that.options.filterTemplate != null && typeof (that.options.filterTemplate) !== "undefined" && that.options.filterTemplate.length > 0) {
                var tpl = that.options.filterTemplate;
                var tplInfo = tpl.replace(/\{[^\}]+\}/ig, function (s, value) {
                    var $input = $(s.substr(1, s.length - 2));
                    return '\'' + ($input.length > 0 ? $input.val() : '') + '\'';
                });
                this.$filter = tplInfo;
            }
        };
        this.search = function () {
            self.filter();
            self.loadData();
        };
        //订阅  总记录数
        self.gridViewModel.totalCount.subscribe(function (newValue) {
            //self.gridViewModel.currentPageIndex(1);
        });
        //订阅  当前页
        self.gridViewModel.currentPageIndex.subscribe(function (newValue) {
            self.loadData();
        });
        //订阅  分页数
        self.gridViewModel.pageSize.subscribe(function (newValue) {
            var t = self.getTotalPages();
            self.gridViewModel.pages(self.getPagesArr());
            if (self.gridViewModel.currentPageIndex() > t)
                self.gridViewModel.currentPageIndex(1);
            self.loadData();
        });
        //加载数据
        this.loadData = function () {
            self.filter();
            var paramsData = { $count: true, $top: this.gridViewModel.pageSize(), $skip: (this.gridViewModel.currentPageIndex() - 1) * this.gridViewModel.pageSize() };
            if (self.$filter != null) paramsData.$filter = self.$filter;
            if (self.$orderby != null) paramsData.$orderby = self.$orderby;
            self.loading(true);
            magicodes.api.request("GET", {
                url: that.options.url,
                data: paramsData,
                func: function (data) {
                    $.each(data.value, function (i, v) {
                        v._selected = ko.observable(false);
                    });
                    self.gridViewModel.dataRows(data.value);
                    self.gridViewModel.totalCount(data["@odata.count"]);
                    self.gridViewModel.pages(self.getPagesArr());
                    self.loading(false);
                }
            });
        };
        //初始化
        this.init = function () {
            if (that.options.$orderby) self.$orderby = that.options.$orderby;
            if (that.options.$select) self.$select = that.options.$select;
            this.loadData();
        };
        //选择所有
        this.isAllSelected = ko.computed({
            read: function () {
                var item = ko.utils.arrayFirst(self.gridViewModel.dataRows(), function (item) {
                    return !item._selected();
                });
                return item == null;
            },
            write: function (value) {
                ko.utils.arrayForEach(self.gridViewModel.dataRows(), function (item) {
                    item._selected(value);
                });
            }
        });
        //移除选择行
        this.removeSelectedRows = function () {
            var selectedRows = self.getSelectedRows();
            if (selectedRows.length == 0) {
                magicodes.messager.showWarnMessage('警告', '请先选择需要删除的项！');
                return;
            }
            var ids = '';
            $.each(selectedRows, function (i, v) { ids += (eval('v.' + that.options.keyName)) + '-'; });
            ids = ids.substr(0, ids.length - 1);
            new magicodes.dialog().bootbox.confirm({
                message: "确定要删除所选项么？", func: function () {
                    var count = selectedRows.length;
                    $.each(selectedRows, function (i, v) {
                        var url = self.getGetUrl((eval('v.' + that.options.keyName)));
                        window.magicodes.api.request("DELETE", {
                            url: url,
                            data: {},
                            func: function (data) {
                                count--;
                                if (count == 0) {
                                    self.gridViewModel.currentPageIndex(1);
                                    self.loadData();
                                    magicodes.messager.showInfoMessage('温馨提示', '操作成功！');
                                }
                            }
                        });
                    });
                }
            });

        };
        //获取选中行
        this.getSelectedRows = function () {
            return $.grep(self.gridViewModel.dataRows(), function (a) { return a._selected() });
        };
        this.getQuotes = function () {
            return that.options.keyType == "string" ? "'" : "";
        };
        this.getGetUrl = function (id) {
            return that.options.url + '(' + self.getQuotes() + id + self.getQuotes() + ')';
        };
        this.loadData();
    };
    this.bindingModel = function () {
        var vm = new that.ViewModel();
        that.options.onBinding && that.options.onBinding(vm);
        if (that.options.bindElement == null)
            ko.applyBindings(vm);
        else
            ko.applyBindings(vm, that.options.bindElement);
        that.options.onBound && that.options.onBound(vm);
        return vm;
    };
    return this.bindingModel();
};