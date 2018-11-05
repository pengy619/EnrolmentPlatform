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
        //初始化登录账号的下拉操作
        layui.use(['jquery'], function () {
            var $ = layui.$;
            $(document).click(function (e) {
                if ($(e.target).hasClass('account')) {
                    $(e.target).toggleClass('active');
                } else {
                    $('.user-tools .account').removeClass('active');
                }
            });
            ////初始化counter组件
            //$(document).on('click', '.ui-counter .add', function () {
            //    var value = Number($(this).prev('input').val());
            //    var id = $(this).parent('.ui-counter').attr('data-id');
            //    if (!isNaN(value)) {
            //        value++;
            //        $(this).prev('input').val(value);
            //        if (window.counter) {
            //            var obj = {
            //                id: id,
            //                value: value
            //            };
            //            window.counter(obj);
            //        }
            //    }
            //});
            //$(document).on('click', '.ui-counter .reduce', function () {
            //    var value = Number($(this).next('input').val());
            //    var id = $(this).parent('.ui-counter').attr('data-id');
            //    if (!isNaN(value)) {
            //        value--;
            //        $(this).next('input').val(value);
            //        if (window.counter) {
            //            var obj = {
            //                id: id,
            //                value: value
            //            };
            //            window.counter(obj);
            //        }
            //    }
            //});
        });
    }
};
common.importModule();
common.collapseLeft();
common.checkBrowser();
common.initGlobalEvent();