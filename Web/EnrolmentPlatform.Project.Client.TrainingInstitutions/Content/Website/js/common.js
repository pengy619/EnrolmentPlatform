"use strict";

;var common = {
    //判断ie版本
    IEVersion: function IEVersion() {
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
        var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
        var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
        var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
        if (isIE) {
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if (fIEVersion == 7) {
                return 7;
            } else if (fIEVersion == 8) {
                return 8;
            } else if (fIEVersion == 9) {
                return 9;
            } else if (fIEVersion == 10) {
                return 10;
            } else {
                return 6; //IE版本<=7
            }
        } else if (isEdge) {
            return 'edge'; //edge
        } else if (isIE11) {
            return 11; //IE11  
        } else {
            return -1; //不是ie浏览器
        }
        console.log(isIE);
    },
    //低版本浏览器提示
    checkBrowser: function checkBrowser() {
        var browser = common.IEVersion();
        layui.use(['jquery'], function () {
            var $ = layui.$;
            var minH = document.body.offsetHeight - $('.top-wrap').height();
            $('.body-wrap').css('min-height', minH + 'px');
            if (!isNaN(browser) && browser > 4 && browser <= 9) {
                var tip = $('<div class="browser-tip">您当前的浏览器版本过低，为了更好的体验，请<a target="_blank" href="http://rj.baidu.com/soft/detail/14744.html?ald">下载我们推荐的浏览器</a>。<i class="layui-icon">&#x1007;</i></div>');
                tip.find('i').click(function () {
                    tip.remove();
                });
                $('body').prepend(tip);
            }
        });
    },
    //控制左侧菜单收起展开
    collapseLeft: function collapseLeft() {
        layui.use(['jquery', 'layer'], function () {
            var $ = layui.$;
            var collapseButton = $('#collapse-button');
            var leftWrap = $('.left-wrap');
            collapseButton.click(function () {
                if (leftWrap.hasClass('shrink')) {
                    leftWrap.removeClass('shrink');
                    collapseButton.find('i').html('&#xe65a;');
                } else {
                    leftWrap.addClass('shrink');
                    collapseButton.find('i').html('&#xe65b;');
                }
            });

            var layer = layui.layer;
            $('.left-wrap .layui-nav-item').hover(function () {
                if (leftWrap.hasClass('shrink')) {
                    var text = $(this).text();
                    layer.tips(text, this);
                }
            }, function () {
                layer.close(layer.index);
            });
        });
    },
    //预先引入常用模块
    importModule: function importModule() {
        var modules = ['jquery', 'element', 'form'];
        layui.use(modules, function () {});
    },
    //初始化全局基础事件
    initGlobalEvent: function initGlobalEvent() {
        layui.use(['jquery'], function () {
            var $ = layui.$;
            //初始化登录账号的下拉操作
            $(document).click(function (e) {
                if ($(e.target).hasClass('account')) {
                    $(e.target).toggleClass('active');
                } else {
                    $('.user-tools .account').removeClass('active');
                }
            });
            //初始化隐藏checkbox展开
            $('.ui-form .more').click(function (e) {
                var parent = $(this).parents('.layui-form-item');
                if (parent.css('max-height') == '44px') {
                    $(this).find('span').text('收起');
                    $(this).find('i').css('transform', 'rotate(180deg)');
                    parent.css('max-height', '9999px');
                } else {
                    $(this).find('span').text('更多');
                    $(this).find('i').css('transform', 'rotate(0deg)');
                    parent.css('max-height', '44px');
                }
            });
            //初始化counter组件
            $(document).on('click', '.ui-counter .add', function () {
                var value = Number($(this).prev('input').val());
                var id = $(this).parent('.ui-counter').attr('data-id');
                if (!isNaN(value)) {
                    value++;
                    $(this).prev('input').val(value);
                    if (window.counter) {
                        var obj = {
                            id: id,
                            value: value
                        };
                        window.counter(obj);
                    }
                }
            });
            $(document).on('click', '.ui-counter .reduce', function () {
                var value = Number($(this).next('input').val());
                var id = $(this).parent('.ui-counter').attr('data-id');
                if (!isNaN(value)) {
                    value++;
                    $(this).next('input').val(value);
                    if (window.counter) {
                        var obj = {
                            id: id,
                            value: value
                        };
                        window.counter(obj);
                    }
                }
            });
        });
    },
    //自定义日历选择器
    uiCalendar: function uiCalendar() {
        var _this = this;

        this.nowDay = new Date();
        this.data = [];
        this.calendar = null;
        this.calendarB = null;
        this.calendarH = null;
        this.modal = null;
        this.option = {
            id: null,
            currentDate: new Date(),
            httpUrl: false,
            loading: false,
            objectName: 'data',
            dateField: 'date',
            priceField: 'price',
            stockField: 'stock',
            onSelect: function onSelect() {}
        };
        this.init = function (opt) {
            _this.option = Object.assign(_this.option, opt);
            if (opt.currentDate && opt.currentDate instanceof Date) {
                _this.option.currentDate = opt.currentDate;
                _this.nowDay = opt.currentDate;
            } else if (opt.currentDate && new Date(opt.currentDate) instanceof Date) {
                _this.option.currentDate = new Date(opt.currentDate);
                _this.nowDay = new Date(opt.currentDate);
            }
            _this.renderHtml();
        };
        this.renderHtml = function (date) {
            _this.calendar = document.createElement('div');
            _this.calendar.className = 'ui-calendar';

            _this.calendarH = document.createElement('div');
            _this.calendarH.className = 'calendar-header';
            _this.calendarH.innerHTML = "<span class=\"calendar-btn\" id=\"prveMonth\"><i class=\"layui-icon\">&#xe603;</i></span>\n            <span class=\"calendar-title\">" + _this.option.currentDate.getFullYear() + "\u5E74" + (_this.option.currentDate.getMonth() + 1) + "\u6708</span>\n            <span class=\"calendar-btn\" id=\"nextMonth\"><i class=\"layui-icon\">&#xe602;</i></span>";
            _this.calendarH.querySelector('#nextMonth').onclick = function () {
                _this.setMonth(true);
            };
            _this.calendarH.querySelector('#prveMonth').onclick = function () {
                _this.setMonth(false);
            };

            var calendarW = document.createElement('div');
            calendarW.className = 'calendar-week';
            calendarW.innerHTML = "<span>\u65E5</span>\n            <span>\u4E00</span>\n            <span>\u4E8C</span>\n            <span>\u4E09</span>\n            <span>\u56DB</span>\n            <span>\u4E94</span>\n            <span>\u516D</span>";

            _this.calendarB = document.createElement('div');
            _this.calendarB.className = 'calendar-body';

            _this.loading = document.createElement('div');
            _this.loading.className = "body-loading";
            _this.loading.innerHTML = "<i class=\"layui-icon layui-anim layui-anim-rotate layui-anim-loop\">&#xe63e;</i>  " + _this.option.loading;

            _this.option.httpUrl && _this.getData(function () {
                _this.renderDate(_this.option.currentDate);
            });
            _this.renderDate(_this.option.currentDate, _this.option.httpUrl && _this.option.loading);

            _this.calendar.appendChild(_this.calendarH);
            _this.calendar.appendChild(calendarW);
            _this.calendar.appendChild(_this.calendarB);

            document.body.appendChild(_this.calendar);
            _this.modal = layer.open({
                type: 1,
                shade: 0.3,
                title: false, //不显示标题
                scrollbar: false, //屏蔽滚动
                area: '604px',
                content: $('.ui-calendar'),
                cancel: function cancel() {
                    _this.calendar.remove();
                }
            });
        };
        this.renderDate = function (date) {
            var loading = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : false;

            _this.calendarH.querySelector('.calendar-title').innerHTML = date.getFullYear() + "\u5E74" + (date.getMonth() + 1) + "\u6708";
            var dateCols = [],
                dates = _this.createMonthDay(date);
            for (var i = 0; i < dates.length; i++) {
                var active = _this.equal(dates[i].date, _this.nowDay);
                var data = _this.findDataItem(dates[i].date);
                var item = "<div date=\"" + dates[i].fullDate + "\" class=\"date-col " + (dates[i].isCurrent ? '' : 'not-current-month') + " " + (active ? 'active' : '') + "\">\n                    <div class=\"row-1\">\n                        <span>" + dates[i].day + "</span>\n                        " + (dates[i].isCurrent && data ? "<span class=\"blue\">\u4F59" + data[_this.option.stockField] + "</span>" : '') + "\n                    </div>\n                    " + (dates[i].isCurrent && data ? "<div class=\"row-2\">\n                            <span>\uFFE5" + data[_this.option.priceField] + "</span>\n                        </div>" : '') + "\n                </div>";
                dateCols.push(item);
            }
            _this.calendarB.innerHTML = dateCols.join('');
            var cols = _this.calendar.querySelectorAll('.date-col');
            cols.forEach(function (item) {
                if (item.children.length > 1) {
                    item.onclick = function (e) {
                        _this.select(e);
                    };
                }
            });

            if (loading) {
                _this.calendarB.appendChild(_this.loading);
            }
        };
        this.createMonthDay = function (d) {
            var date = new Date();
            if (d && d instanceof Date) {
                date = d;
            } else if (d && new Date(d) instanceof Date) {
                date = new Date(d);
            }
            var daysOfMonth = [];
            var fullYear = date.getFullYear();
            var month = date.getMonth() + 1;
            _this.option.currentDate = new Date(fullYear, date.getMonth(), 1);
            month = month > 10 ? month : '0' + month;
            var lastDayOfMonth = new Date(fullYear, month, 0).getDate();
            for (var i = 1; i <= lastDayOfMonth; i++) {
                i = i >= 10 ? i : '0' + i;
                var item = {
                    fullDate: fullYear + '-' + month + '-' + i,
                    date: new Date(fullYear + '-' + month + '-' + i),
                    day: Number(i),
                    isCurrent: true
                };
                daysOfMonth.push(item);
            };

            var maxD = date.setDate(0);
            maxD = date.getDate();
            for (var i = 0; i < _this.option.currentDate.getDay(); i++) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                m = m > 10 ? m : '0' + m;
                var d = maxD - i;
                d = d > 10 ? d : '0' + d;
                var item = {
                    fullDate: y + '-' + m + '-' + d,
                    date: new Date(y + '-' + m + '-' + d),
                    day: Number(d),
                    isCurrent: false
                };
                daysOfMonth.unshift(item);
            }

            var len = daysOfMonth.length;
            var nextDate = date.setMonth(Number(month));
            nextDate = new Date(nextDate);
            for (var i = 1; i <= 42 - len; i++) {
                nextDate.setDate(i);
                var y = nextDate.getFullYear();
                var m = nextDate.getMonth() + 1;
                m = m > 10 ? m : '0' + m;
                var d = nextDate.getDate();
                d = d > 10 ? d : '0' + d;
                var item = {
                    fullDate: y + '-' + m + '-' + d,
                    date: new Date(y + '-' + m + '-' + d),
                    day: Number(d),
                    isCurrent: false
                };
                daysOfMonth.push(item);
            }
            return daysOfMonth;
        };
        this.setMonth = function (add) {
            if (add) {
                new Date(_this.option.currentDate.setMonth(_this.option.currentDate.getMonth() + 1)).getMonth();
                _this.renderDate(_this.option.currentDate, _this.option.httpUrl && _this.option.loading);
                _this.option.httpUrl && _this.getData(function () {
                    _this.renderDate(_this.option.currentDate);
                });
            } else {
                new Date(_this.option.currentDate.setMonth(_this.option.currentDate.getMonth() - 1)).getMonth();
                _this.renderDate(_this.option.currentDate, _this.option.httpUrl && _this.option.loading);
                _this.option.httpUrl && _this.getData(function () {
                    _this.renderDate(_this.option.currentDate);
                });
            }
        };
        this.equal = function (d1, d2) {
            var str1 = d1.getFullYear() + '-' + d1.getMonth() + '-' + d1.getDate();
            var str2 = d2.getFullYear() + '-' + d2.getMonth() + '-' + d2.getDate();
            return str1 == str2;
        };
        this.findDataItem = function (d) {
            var res = null;
            if (_this.data.length !== 0) {
                _this.data.forEach(function (item) {
                    if (_this.equal(new Date(item[_this.option.dateField]), d)) {
                        res = item;
                    }
                });
            }
            return res;
        };
        this.getData = function (cb) {
            //"https://www.easy-mock.com/mock/5a015fca36a23b429ea956a0/ceshi/getPrice"
            var d = _this.option.currentDate.getFullYear() + '-' + (_this.option.currentDate.getMonth() + 1) + '-' + _this.option.currentDate.getDate();
            var url = _this.option.httpUrl.replace('{d}', d);
            console.log(url);
            $.get(url, function (result) {
                _this.data = result[_this.option.objectName];
                cb && cb();
            });
        };
        this.select = function (e) {
            var d = new Date(e.currentTarget.getAttribute('date'));
            var item = _this.findDataItem(d);
            var res = {
                id: _this.option.id,
                fullDate: e.currentTarget.getAttribute('date'),
                date: d,
                price: item ? item[_this.option.priceField] : null,
                stock: item ? item[_this.option.stockField] : null
            };
            _this.option.onSelect(res);
            layer.close(_this.modal);
            _this.calendar.remove();
        };
    }
};
common.importModule();
common.collapseLeft();
common.checkBrowser();
common.initGlobalEvent();