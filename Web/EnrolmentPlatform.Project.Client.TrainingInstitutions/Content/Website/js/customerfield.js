//获得学校的自定义字段列表
function filedList(schoolId, beforeContains) {
    $.ajax({
        type: "POST",
        contentType: "application/json;utf-8",
        dataType: "json",
        async: false,
        data: "{schooldId:" + schoolId+"}",
        url: '/Order/Manager/GetCustomerFieldList',
        success: function (result) {
            alert(JSON.stringify(result));
        }
    });
}