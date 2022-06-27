/*!
 * Cropper v3.0.0
 */

layui.config({
    base: '/Content/Website/cropper/' //layui自定义layui组件目录
}).define(['jquery', 'layer', 'cropper'], function (exports) {
    var $ = layui.jquery
        , layer = layui.layer;
    var html = "<link rel=\"stylesheet\" href=\"/Content/Website/cropper/cropper.css\">\n" +
        "<div class=\"layui-fluid showImgEdit\">\n" +
        "    <div class=\"layui-form-item\">\n" +
        "        <div class=\"layui-input-inline layui-btn-container\" style=\"width: auto;\padding-top:15px;\">\n" +
        "            <label for=\"cropper_avatarImgUpload\" class=\"layui-btn layui-btn-primary\">\n" +
        "                <i class=\"layui-icon\">&#xe67c;</i>选择图片\n" +
        "            </label>\n" +
        "            <input class=\"layui-upload-file\" id=\"cropper_avatarImgUpload\" type=\"file\" value=\"选择图片\" name=\"file\">\n" +
        "        </div>\n" +
        "        <div class=\"layui-form-mid layui-word-aux\"></div>\n" +
        "    </div>\n" +
        "    <div class=\"layui-row layui-col-space15\">\n" +
        "        <div class=\"layui-col-xs9\">\n" +
        "            <div class=\"readyimg\" style=\"height:450px;background-color: rgb(247, 247, 247);\">\n" +
        "                <img src=\"{pic}\" >\n" +
        "            </div>\n" +
        "        </div>\n" +
        "        <div class=\"layui-col-xs3\">\n" +
        "            <div class=\"img-preview\" style=\"width:200px;height:200px;overflow:hidden\">\n" +
        "            </div>\n" +
        "        </div>\n" +
        "    </div>\n" +
        "    <div class=\"layui-row layui-col-space15\">\n" +
        "        <div class=\"layui-col-xs9\">\n" +
        "            <div class=\"layui-row\">\n" +
        "                <div class=\"layui-col-xs6\">\n" +
        "                    <button type=\"button\" class=\"layui-btn layui-icon layui-icon-left\" cropper-event=\"rotate\" data-option=\"-15\" title=\"Rotate -90 degrees\"> 向左旋转</button>\n" +
        "                    <button type=\"button\" class=\"layui-btn layui-icon layui-icon-right\" cropper-event=\"rotate\" data-option=\"15\" title=\"Rotate 90 degrees\"> 向右旋转</button>\n" +
        "                    <button type=\"button\" class=\"layui-btn layui-icon layui-icon-refresh\" cropper-event=\"reset\" title=\"重置图片\"></button>\n" +
        "                </div>\n" +
        "            </div>\n" +
        "        </div>\n" +
        "    </div>\n" +
        "\n" +
        "</div>";
    var obj = {
        render: function (e) {
            var self = this,
                elem = e.elem,
                saveW = e.saveW,
                saveH = e.saveH,
                mark = e.mark,
                area = e.area,
                url = e.url,
                forId = e.forId,
                imgType = e.imgType,
                pic = e.pic,
                done = e.done;
            var image;
            $(elem).on('click', function () {
                pic = e.pic
                if (typeof (pic) != 'undefined' && pic.indexOf("nopic.png") > -1) {
                    pic = ''
                }
                var content = typeof (pic) == 'undefined' ? html.replace("{pic}", "") : html.replace('{pic}', pic);
                layer.open({
                    type: 1
                    , content: content
                    , area: area
                    , btn: ['确认', '取消']
                    , btn1: function (index, layero) {
                        var toImage = image.cropper("getCroppedCanvas");
                        if (toImage == null) {
                            layer.alert("请选择一张图片", { icon: 2 });
                            return
                        }
                        //保存图片
                        toImage.toBlob(function (blob) {
                            var formData = new FormData();
                            formData.append('file', blob, 'imgFile.jpg');
                            formData.append("orderId", forId);
                            formData.append("type", imgType);
                            $.ajax({
                                method: "post",
                                url: url, //用于文件上传的服务器端请求地址
                                data: formData,
                                processData: false,
                                contentType: false,
                                success: function (result) {
                                    layer.closeAll('loading');
                                    if (result.ret == true) {
                                        layer.msg(result.msg, { icon: 1 });
                                        //layer.closeAll('page');
                                        e.pic = result.url
                                        layer.close(index);
                                        return done(result.url);
                                    } else {
                                        layer.alert(result.msg, { icon: 2 });
                                    }
                                },
                                beforeSend: function () {
                                    layer.load({
                                        shade: true,
                                        time: 60 * 1000
                                    });
                                },
                                error: function () {
                                    layer.closeAll('loading');
                                    layer.msg("服务器繁忙,请稍后重试", { icon: 5 });
                                }
                            });
                        }, "image/jpeg", 0.6);
                    }
                    , success: function (layero, index) {
                        image = layero.find(".readyimg img")
                            , preview = layero.find(".img-preview")
                            , file = layero.find("input[name='file']")
                            , options = {
                                preview: preview
                                , viewMode: 1
                                , autoCropArea: 1
                            };
                        //文件选择
                        file.change(function () {
                            var r = new FileReader();
                            var f = this.files[0];
                            r.readAsDataURL(f);
                            r.onload = function (e) {
                                image.cropper('destroy').attr('src', this.result).cropper(options);
                            };
                        });
                        layero.on('click', '.layui-btn', function () {
                            var event = $(this).attr("cropper-event");
                            //监听旋转
                            if (event === 'rotate') {
                                var option = $(this).attr('data-option');
                                image.cropper('rotate', option);
                                //重设图片
                            } else if (event === 'reset') {
                                image.cropper('reset');
                            }
                        })
                        image.cropper(options);
                    }
                    , cancel: function (index, layero) {
                        var image = layero.find(".readyimg img")
                        layer.close(index);
                        image.cropper('destroy');
                    }
                });
            });
        }

    };
    exports('croppers', obj);
});