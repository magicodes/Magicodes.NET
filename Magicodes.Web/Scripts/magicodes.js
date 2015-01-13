setTimeout(function () {
    $('body').fadeIn();
}, 500);
window.magicodes = {
    isDebug: false,
    isMin: false,
    modules: [{ name: "login", module: "modules/login" }, { name: "navs", module: "modules/navs" }, { name: "odataGrid", module: "modules/odataGrid" }],
    getModule: function (name, func, winName) {
        var that = this;
        require(["jquery"], function () {
            if (winName) {
                var m = eval("window." + winName);
                if (typeof (m) != "undefined") {
                    $.isFunction(func) && func(m);
                    return;
                }
            }
            var moduleName = name;
            var modules = $.each(that.modules, function (i, v) {
                if (v.name == name) {
                    moduleName = that.isMin ? v.module + '.min' : v.module;
                    return false;
                }
            });
            require([moduleName], function (m) {
                if ($.isFunction(func)) {
                    if (winName) eval("window." + winName + "=m");
                    func(m);
                }
            });
        });
    }
};

window.magicodes.messager = {
    showMessage: function (title, message, className, funcs) {
        var setting = {
            title: title,
            text: message,
            class_name: className
        };
        if (typeof (funcs) !== "undefined") {
            if (typeof (funcs.before_close) !== "undefined")
                setting.before_close = funcs.before_close;
            if (typeof (funcs.after_open) !== "undefined")
                setting.after_open = funcs.after_open;
        }
        this._addGritter(setting);
    },
    showInfoMessage: function (title, message, funcs) {
        this.showMessage(title, message, 'gritter-info gritter-light', funcs);
    },
    showErrorMessage: function (title, message, funcs) {
        this.showMessage(title, message, 'gritter-error gritter-light', funcs);
    },
    showWarnMessage: function (title, message, funcs) {
        this.showMessage(title, message, 'gritter-warning gritter-light', funcs);
    },
    removeAll: function () {
        typeof ($.gritter) !== "undefined" && $.gritter.removeAll();
    },
    _addGritter: function (setting) {
        require(["jquery", "jquery.gritter"], function () {
            $.gritter.add(setting);
        });
    }
};
window.magicodes.dialog = function () {
    self = this;
    this.bootbox = {
        dialog: function (setting) {
            self._bootbox(function (bt) {
                self.bootbox.bootbox = bt.dialog(setting);
                $.isFunction(setting.func) && setting.func();
            });
        },
        confirm: function (setting) {
            self._bootbox(function (bt) {
                self.bootbox.bootbox = bt.confirm({
                    message: setting.message,
                    buttons: {
                        confirm: {
                            label: "确定",
                            className: "btn-warning btn-sm",
                        },
                        cancel: {
                            label: "取消",
                            className: "btn-sm",
                        }
                    },
                    callback: function (result) {
                        result && $.isFunction(setting.func) && setting.func();
                    }
                });
            });
        },
        hideAll: function () {
            self.__bootBox.hideAll();
        }
    },
    this._bootbox = function (func) {
        require(["bootbox"], function (bt) {
            self.__bootBox = bt;
            $.isFunction(func) && func(bt);
        });
    };
    this.colorbox = function (setting) {
        self._colorbox(function () {
            $.colorbox(setting);
        });
    },
    this._colorbox = function (func) {
        require(["jq.colorbox"], function () {
            $.isFunction(func) && func();
        });
    };
    return this;
};

window.magicodes.loader = {
    show: function ($c) {
        var $container = typeof ($c) == "undefined" ? $('body') : $c;
        $loading = $('<div class="message-loading-overlay"><i class="fa-spin ace-icon fa fa-spinner orange2 bigger-180"></i></div>').css({ "z-index": 10000 });
        $container.append($loading);
        this.$loading = $loading;
        var loader = this;
        if (loader.onShow)
            loader.onShow();
    },
    close: function () {
        var loader = this;
        if (loader.onClose)
            loader.onClose();
        $('.message-loading-overlay').remove();
    }
}
window.magicodes.logger = {
    log: function () {
        var msg = '[magicodes]' + Array.prototype.join.call(arguments, '');
        if (window.console && window.console.log)
            window.console.log(msg);
        else if (window.opera && window.opera.postError)
            window.opera.postError(msg);
    }
};
window.magicodes.api = {
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
    //batchRequests: function (requestData) {
    //    require(["OData"], function (OData) {
    //        OData.request({
    //            requestUri: "/odata/$batch",
    //            method: "POST",
    //            data: requestData
    //        }, function (data) {
    //            for (var i = 0; i < data.__batchResponses.length; i++) {
    //                var batchResponse = data.__batchResponses[i];
    //                for (var j = 0; j < batchResponse.__changeResponses.length; j++) {
    //                    var changeResponse = batchResponse.__changeResponses[j];
    //                }
    //            }
    //            alert(window.JSON.stringify(data));
    //        }, function (error) {
    //            alert(error.message);
    //        }, OData.batchHandler);
    //    });
    //    //this._sendRequest({ url: setting.url, type: 'delete', data: setting.data, func: setting.func });
    //},
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
window.magicodes.validator = function (setting) {
    var self = this;
    var defaults = {
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: false,
        highlight: function (e) {
            $(e).closest('.input-control').removeClass('success-state').addClass('error-state');
            $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
        },
        success: function (e) {
            $(e).closest('.input-control').removeClass('error-state').addClass('success-state');
            $(e).closest('.form-group').removeClass('has-error').addClass('has-info');
            $(e).remove();
        },
        errorPlacement: function (error, element) {
            //error.prepend('<i class="icon-cancel red"></i>&nbsp;&nbsp;');
            if (element.is('input[type=checkbox]') || element.is('input[type=radio]')) {
                var controls = element.closest('div.CheckBoxListBorder');
                if (controls.find(':checkbox,:radio').length > 1) controls.append(error);
                else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
            }
            else if (element.is('.select2')) {
                error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
            }
            else if (element.is('.chosen-select')) {
                error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
            }
            else error.insertAfter(element.parent());
        },
        //submitHandler: function (form) {

        //},
        invalidHandler: function (e, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                validator.errorList[0].element.focus();
                magicodes.messager.showWarnMessage("警告", '您有 ' + errors + ' 个字段填写不符合要求，请根据下面的提示语进行修正。');
            }
        },
        $form: $('form'),
        initComplete: function () { }
    };
    self.options = $.extend(defaults, setting);
    require(["jq.validation"], function () {
        //为必填项添加*
        self.options.$form.find("textarea[required],input[required],select[required]").each(function () {
            var $input = $(this);
            var $lable = $input.closest('.form-group:not([hideRequired])').find('label:eq(0)');
            if (!$lable) $lable = $input.closest('.input-control').prev('label');
            if ($lable.find('.text-warning').length == 0) {
                $lable.append('<span class="text-warning">*</span>');
            }
        });
        //添加密码规则
        jQuery.validator.addMethod("password", function (value, element) {
            return this.optional(element) || value.length >= 6 && /\d/.test(value) && /[a-z]/i.test(value);
        }, "密码必须大于6个字符并且至少涵盖一个数字和字母！");

        self.options.$form.validate(self.options);

        $.isFunction(self.options.initComplete) && self.options.initComplete();
    });
};

window.magicodes.grid = {
    //{url:'/odata/Members',pageSize:10}
    init: function (setting) {
        require(["knockoutJs"], function (ko) {
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

            var PagedGridModel = function () {
                var self = this;
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
                this.gridViewModel = {
                    pages: ko.observable([]),
                    dataRows: ko.observableArray([]),
                    pageSize: ko.observable(typeof (setting.pageSize) == "undefined" ? 10 : setting.pageSize),
                    totalCount: ko.observable(0),
                    currentPageIndex: ko.observable(1),
                    pageOptions: ko.observableArray([10, 25, 50, 100]),
                };
                this.$filter = null;
                this.$orderby = null;
                this.$select = null;
                this.filter = function () {
                    if (typeof (setting.filterTemplate) !== "undefined" && setting.filterTemplate.length > 0) {
                        var tpl = setting.filterTemplate;
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
                this.loadData = function () {
                    var paramsData = { $count: true, $top: this.gridViewModel.pageSize(), $skip: (this.gridViewModel.currentPageIndex() - 1) * this.gridViewModel.pageSize() };
                    if (self.$filter != null) paramsData.$filter = self.$filter;
                    if (self.$orderby != null) paramsData.$orderby = self.$orderby;
                    magicodes.loader.show($(setting.gridId));
                    window.magicodes.api.request("GET", {
                        url: setting.url,
                        data: paramsData,
                        func: function (data) {
                            $.each(data.value, function (i, v) {
                                v._selected = ko.observable(false);
                            });
                            self.gridViewModel.dataRows(data.value);
                            self.gridViewModel.totalCount(data["@odata.count"]);
                            self.gridViewModel.pages(self.getPagesArr());
                            magicodes.loader.close();
                        }
                    });
                };
                this.init = function () {
                    if (setting.$orderby) self.$orderby = setting.$orderby;
                    if (setting.$select) self.$select = setting.$select;
                    this.loadData();
                    $.isFunction(setting.inited) && setting.inited(setting, ko);
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
                this.edit = function (id, row) {
                    var dialoger = new magicodes.dialog();
                    window.magicodes.api.request("GET", {
                        url: setting.url + '(' + id + ')',
                        data: {},
                        func: function (data) {
                            var _eidt = typeof (setting.eidtModel) == "undefined" ? {} : setting.eidtModel;
                            // 克隆一个
                            self.eidtModel = ko.observable($.extend(data, _eidt));
                            dialoger.bootbox.dialog({
                                title: '编辑',
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
                                             window.magicodes.api.request("PUT", {
                                                 url: setting.url,
                                                 data: self.eidtModel(),
                                                 func: function (data, statusCode) {
                                                     magicodes.messager.showInfoMessage('温馨提示', '保存成功！');
                                                     dialoger.bootbox.hideAll();
                                                     self.loadData();
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
                                            //Example.show("uh oh, look out!");
                                        }
                                    }
                                },
                                show: false,
                                func: function () {
                                    dialoger.bootbox.bootbox.on("shown.bs.modal", function () {
                                        //bootbox - body
                                        //data-bind="template: {name: \"addTemplate\", replaceChildren: addModel}"
                                        ko.renderTemplate(
                                            'editTemplate',
                                            self.eidtModel,
                                            {
                                                afterRender: function (renderedElement) {
                                                    self.validator = setting.editFormId ? new magicodes.validator({ $form: $('#' + setting.editFormId) }) : new magicodes.validator();
                                                    $.isFunction(setting.afterEditDialogShow) && setting.afterEditDialogShow();
                                                }
                                            },
                                            $('.bootbox-body:eq(0)').get(0), "replaceChildren");
                                    });
                                    dialoger.bootbox.bootbox.modal('show');
                                }
                            });
                        }
                    });
                };
                //移除选择行
                this.removeSelectedRows = function () {
                    var selectedRows = self.getSelectedRows();
                    if (selectedRows.length == 0) {
                        magicodes.messager.showWarnMessage('警告', '请先选择需要删除的项！');
                        return;
                    }
                    var ids = '';
                    $.each(selectedRows, function (i, v) { ids += (eval('v.' + setting.keyName)) + '-'; });
                    ids = ids.substr(0, ids.length - 1);
                    new magicodes.dialog().bootbox.confirm({
                        message: "确定要删除所选项么？", func: function () {
                            var count = selectedRows.length;
                            $.each(selectedRows, function (i, v) {
                                var url = setting.url + '(' + (eval('v.' + setting.keyName)) + ')';
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
                }
                this.add = function () {
                    var dialoger = new magicodes.dialog();
                    var _add = typeof (setting.addModel) == "undefined" ? {} : setting.addModel;
                    // 克隆一个
                    this.addModel = ko.observable($.extend({}, _add));
                    dialoger.bootbox.dialog({
                        title: '新增',
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
                    		         window.magicodes.api.request("POST", {
                    		             url: setting.url,
                    		             data: self.addModel(),
                    		             func: function (data) {
                    		                 magicodes.messager.showInfoMessage('温馨提示', '创建成功！');
                    		                 dialoger.bootbox.hideAll();
                    		                 self.loadData();
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
                    		        //Example.show("uh oh, look out!");
                    		    }
                    		}
                    	},
                        show: false,
                        func: function () {
                            dialoger.bootbox.bootbox.on("shown.bs.modal", function () {
                                //$('.bootbox-body:eq(0)').html($('#addTemplate').html());
                                //self.validator = setting.editFormId ? new magicodes.validator({ $form: $('#' + setting.editFormId) }) : new magicodes.validator();
                                //$.isFunction(setting.afterEditDialogShow) && setting.afterEditDialogShow();
                                //ko.applyBindings(self.addModel(), $('.bootbox-body:eq(0)').get(0));

                                //bootbox - body
                                //data-bind="template: {name: \"addTemplate\", replaceChildren: addModel}"
                                ko.renderTemplate(
                                    'addTemplate',
                                    self.addModel,
                                    {
                                        afterRender: function (renderedElement) {
                                            self.validator = setting.editFormId ? new magicodes.validator({ $form: $('#' + setting.editFormId) }) : new magicodes.validator();
                                            $.isFunction(setting.afterEditDialogShow) && setting.afterEditDialogShow();
                                        }
                                    },
                                    $('.bootbox-body:eq(0)').get(0), "replaceChildren");
                            });
                            dialoger.bootbox.bootbox.modal('show');
                        }
                    });
                };
            };
            var pageView = new PagedGridModel();

            ko.applyBindings(pageView);
            $(function () {
                pageView.init();
            });
        });
    }
};
window.magicodes.form = function (setting) {
    var defaults = {
        //保存后是否重新加载
        reloadAfterSave: false,
        //接口路径（仅支持WebAPI和OData协议）
        api: '',
        //加载完成事件
        onloaded: function () { },
        //点击Enter时，是否触发保存事件
        enterSave: true,
        //是否加载数据
        loadData: true,
        //当前绑定的元素
        bindElement: null,
        //保存Web方式
        saveMethod: 'PUT',
        //保存完成事件
        onsaved: null,
        //保存前事件，返回false表示取消
        onsave: null,
        //视图模型创建完成事件，在此可以修改视图模型
        onViewModelCreated: null,
        //保存出错事件，返回错误JSON以及状态码，如果返回true，表示替代默认的错误处理逻辑
        onsaveError: null
    };
    this.options = $.extend(defaults, setting);
    var that = this;
    this.ViewModel = function () {
        var self = this;
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
        this.load = function () {
            self.loading(true);
            magicodes.api.request("GET", {
                url: that.options.api,
                func: function (data) {
                    self.loading(false);
                    self.formData(data);
                    if (self.firstLoad) {
                        self.firstLoad = false;
                        $.isFunction(that.options.onloaded) && that.options.onloaded(data);
                    }
                },
                error: function (data, statusCode) {
                    self.loading(false);
                }
            });

        };
        this.save = function () {
            if (self.validator && !self.validator.options.$form.valid())
                return false;

            if (that.options.onsave) {
                if (!that.options.onsave()) return;
            }
            self.loading(true);
            magicodes.api.request(that.options.saveMethod, {
                url: that.options.api,
                data: self.formData(),
                func: function (data, statusCode) {
                    self.loading(false);
                    if (that.options.onsaved)
                        that.options.onsaved(data, statusCode, self);
                    else
                        magicodes.messager.showInfoMessage('温馨提示', '保存成功！');
                    that.options.reloadAfterSave && self.formData(data);
                },
                error: function (data, statusCode) {
                    self.loading(false);
                    if (that.options.onsaveError)
                        if (that.options.onsaveError(data, statusCode)) return true;
                    return false;
                }
            });
        };
        that.options.onViewModelCreated && that.options.onViewModelCreated(self);
        //创建验证
        self.validator = new magicodes.validator();
        that.options.loadData && self.load();
        if (that.options.enterSave) {
            $(bindElement).keypress(function (event) {
                var keyCode = (event.which ? event.which : event.keyCode);
                if (keyCode === 13) {
                    $(this).find(':focus').trigger('blur');
                    self.save();
                    return false;
                }
                return true;
            });
        }
    };
    this.bindingModel = function () {
        magicodes.getModule('knockoutJs', function (ko) {
            if (that.options.bindElement == null)
                ko.applyBindings(new that.ViewModel());
            else
                ko.applyBindings(new that.ViewModel(), that.options.bindElement);
        }, 'ko');
    };
};
