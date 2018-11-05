
using System.ComponentModel;

namespace EnrolmentPlatform.Project.DTO.Enums
{
    public enum StatusCodeForApiEnum
    {
        [Description("请求(或处理)成功")]
        Success = 200, //请求(或处理)成功

        [Description("内部请求出错")]
        Error = 500, //内部请求出错

        [Description("未授权标识")]
        Unauthorized = 401,//未授权标识

        [Description("请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        [Description("请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效

        [Description("HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        [Description("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        [Description("该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法
    }

    public enum E_StatusCodeForSQL
    {
        [Description("您有未下架产品")]
        Error0 = 6000,
        [Description("未完善景区简介")]
        Error1 = 6001,
        [Description("未设置封面图")]
        Error2 = 6002,
        [Description("未设置轮播图")]
        Error3 = 6003,
        [Description("未完善费用说明")]
        Error4 = 6004,
        [Description("未完善预定说明")]
        Error5 = 6005,
        [Description("未完善退改规则")]
        Error6 = 6006,
        [Description("未完善发票说明")]
        Error7 = 6007,
        [Description("未完善重要条款")]
        Error8 = 6008,
        [Description("未完善产品班期")]
        Error9 = 6009,
        [Description("未添加票种")]
        Error10 = 6010,
        [Description("未添加产品")]
        Error11 = 6011,
        [Description("未完善酒店简介")]
        Error12 = 6012,
        [Description("未设置有效房型")]
        Error13 = 6013, 
        [Description("该房型有在售产品，不能下架")]
        Error14 = 6014,
        [Description("该产品库存类型未设置，无法上架")]
        Error15 = 6015,
        [Description("未完善线路行程信息")]
        Error16 = 6016,
        [Description("该资源下有在售产品，不能删除")]
        Error17 = 6017,
        [Description("该账号已禁用")]
        Error18 = 6018,
        [Description("当前登录供应商类型与账号所属供应商不符合")]
        Error19 = 6019,
        [Description("该员工不属于该公司")]
        Error20 = 6020,
        [Description("该产品包含商品，不能删除")]
        Error21 = 6021,
        [Description("包含子菜单，不能删除")]
        Error22 = 6022
    }
}
