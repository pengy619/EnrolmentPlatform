//绑定学校的自定义字段列表
function filedList(schoolId, data, beforeContains) {
    if (schoolId) {
        var jsonData = null;
        if (data) {
            jsonData = JSON.parse(data);
        }
        $.ajax({
            type: "POST",
            contentType: "application/json;utf-8",
            dataType: "json",
            async: false,
            data: "{schooldId:'" + schoolId + "'}",
            url: '/Order/Manager/GetCustomerFieldList',
            success: function (result) {
                if (result) {
                    var html = "";
                    for (var i = 0; i < result.length; i++) {
                        var cur = result[i];
                        //段落开头
                        if (i == 0 || i % 3 == 0) {
                            html = html + "<div class=\"layui-form-item required\">";
                        }

                        //字段信息
                        html = html + "<label class=\"layui-form-label\">" + cur.Name + "</label>";
                        html = html + "<div class=\"layui-input-inline\">";
                        if (cur.CustomerFieldType == 1) {
                            //文本框
                            var curValue = "";
                            if (jsonData && jsonData[cur.Name]) {
                                curValue = jsonData[cur.Name];
                            }
                            html = html + "<input type=\"text\" class=\"layui-input customerField\" name='" + cur.Name + "' maxlength=\"50\" lay-verify=\"required\" value='" + curValue + "'>";
                        }
                        else if (cur.CustomerFieldType == 2) {
                            //下拉框
                            html = html + "<select lay-verify=\"required\" class=\"customerField\" name=\"" + cur.Name + "\">";
                            html += "<option value=\"\"></option>";
                            var items = cur.SelectItems.split('|');
                            var curValue = "";
                            if (jsonData && jsonData[cur.Name]) {
                                curValue = jsonData[cur.Name];
                            }
                            for (var j = 0; j < items.length; j++) {
                                if (curValue == items[j]) {
                                    html += "<option value=\"" + items[j] + "\" selected>" + items[j] + "</option>";
                                }
                                else {
                                    html += "<option value=\"" + items[j] + "\">" + items[j] + "</option>";
                                }
                            }
                            html = html + "</select>";
                        }
                        html = html + "</div>";

                        //段落结尾
                        if ((i + 1) % 3 == 0) {
                            html = html + "</div>";
                        }
                    }
                    $("#" + beforeContains).parent().append(html);
                }
            }
        });
    }
}

//获得学校的自定义字段列表
function getFieldList() {
    if ($(".customerField").size() > 0) {
        var customerField = {};
        $(".customerField").each(function () {
            var name = $(this).attr("name");
            customerField[name] = $(this).val();
        });
        return JSON.stringify(customerField);
    }
    return null;
}