window.magicodes.odataGrid = function (setting) {
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
        //绑定完毕
        onBound: null,
        //是否启用Html绑定（使用htmlValue）
        htmlValueBind: false,
        //添加数据模型
        addModel: {},
        //编辑模型初始化完毕事件
        editAfterRender: null
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
                magicodes.loader.show($(bindElement));
            }
            else
                magicodes.loader.close();
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
            if (typeof (that.options.filterTemplate) !== "undefined" && that.options.filterTemplate.length > 0) {
                var tpl = that.options.filterTemplate;
                var tplInfo = tpl.replace(/\{[^\}]+\}/ig, function (s, value) {
                    var $input = $(s.substr(1, s.length - 2));
                    return '\'' + ($input.length > 0 ? $input.val() : '') + '\'';
                });
                this.$filter = tplInfo;
                self.loadData();
            }
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
            var paramsData = { $count: true, $top: this.gridViewModel.pageSize(), $skip: (this.gridViewModel.currentPageIndex() - 1) * this.gridViewModel.pageSize() };
            if (self.$filter != null) paramsData.$filter = self.$filter;
            if (self.$orderby != null) paramsData.$orderby = self.$orderby;
            self.loading(true);
            window.magicodes.api.request("GET", {
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
            $.isFunction(that.options.inited) && that.options.inited(that.options, ko);
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
        this.form = function (title, model, apiType, templateId, formId) {
            var dialoger = new magicodes.dialog();
            dialoger.bootbox.dialog({
                title: title,
                message: '<div></div>',
                buttons:
                {
                    "ok":
                     {
                         "label": "<i class='ace-icon fa fa-check'></i> 确定",
                         "className": "btn-sm btn-success",
                         "callback": function () {
                             if (self.validator && !self.validator.options.$form.valid())
                                 return false;
                             self.loading(true);
                             window.magicodes.api.request(apiType, {
                                 url: that.options.url,
                                 data: ko.mapping.toJS(model),
                                 func: function (data, statusCode) {
                                     magicodes.messager.showInfoMessage('温馨提示', '保存成功！');
                                     dialoger.bootbox.hideAll();
                                     self.loading(false);
                                     self.loadData();
                                 },
                                 error: function (data, statusCode) {
                                     self.loading(false);
                                 }
                             });
                             return false;
                         }
                     },
                    "cancel":
                    {
                        "label": "取消",
                        "className": "btn-sm btn-danger",
                        "callback": function () {
                        }
                    }
                },
                show: false,
                func: function () {
                    dialoger.bootbox.bootbox.on("shown.bs.modal", function () {
                        var $render = $('.bootbox-body:eq(0)');
                        //bootbox - body
                        //data-bind="template: {name: \"addTemplate\", replaceChildren: addModel}"
                        ko.renderTemplate(
                            templateId,
                            model,
                            {
                                afterRender: function (renderedElement) {
                                    self.validator = formId ? new magicodes.validator({ $form: $('#' + formId) }) : new magicodes.validator();
                                    $.isFunction(that.options.editAfterRender) && that.options.editAfterRender($render);
                                }
                            },
                            $render.get(0), "replaceChildren");
                    });
                    dialoger.bootbox.bootbox.modal('show');
                }
            });
        };
        this.edit = function (id, row) {
            self.loading(true);
            window.magicodes.api.request("GET", {
                url: self.getGetUrl(id),
                data: {},
                func: function (data) {
                    self.loading(false);
                    var _eidt = typeof (that.options.eidtModel) == "undefined" ? {} : that.options.eidtModel;
                    // 克隆一个
                    self.eidtModel = ko.mapping.fromJS($.extend(data, _eidt));
                    self.form('编辑', self.eidtModel, 'PUT', 'editTemplate', that.options.editFormId);
                },
                error: function (data, statusCode) {
                    self.loading(false);
                }
            });
        };
        //添加
        this.add = function () {
            var _add = typeof (that.options.addModel) == "undefined" ? {} : that.options.addModel;
            // 克隆一个
            self.addModel = ko.mapping.fromJS(_add);
            self.form('新增', self.addModel, 'POST', 'addTemplate', that.options.addFormId);
        };
        this.loadData();
        that.options.onViewModelCreated && that.options.onViewModelCreated(self, ko);
    };
    this.bindingModel = function () {
        magicodes.getModule('knockoutJs', function (ko) {
            //启用HtmlValue绑定
            that.options.htmlValueBind && htmlValueBind(ko);
            require(["kk.mapping"], function (map) {
                ko.mapping = map;
                window.ko = ko;
                var vm = new that.ViewModel();
                if (that.options.bindElement == null)
                    ko.applyBindings(vm);
                else
                    ko.applyBindings(vm, that.options.bindElement);
                that.options.onBound && that.options.onBound(vm, ko);
            });

        });
    };
    this.bindingModel();
    //设置HtmlValue绑定
    function htmlValueBind(ko) {
        console.debug('HtmlValue');
        //HTML绑定
        ko.bindingHandlers.htmlValue = {
            init: function (element, valueAccessor, allBindingsAccessor) {
                var updateHandler = function () {
                    var modelValue = valueAccessor(),
                        elementValue = element.innerHTML;
                    //update the value on keyup
                    modelValue(elementValue);
                };

                ko.utils.registerEventHandler(element, "keyup", updateHandler);
                ko.utils.registerEventHandler(element, "input", updateHandler);
            },
            update: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor()) || "",
                    current = element.innerHTML;

                if (value !== current) {
                    element.innerHTML = value;
                }
            }
        };
    }
};