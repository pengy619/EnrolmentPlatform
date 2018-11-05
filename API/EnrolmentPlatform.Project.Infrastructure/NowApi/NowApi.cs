using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Infrastructure.NowApi
{
    public class NowApi
    {
        public static string Appkey = "28158";
        public static string Sign = "10664dd484f68d6540652b908f049b9e";
        public static string Format = "json";
        public static NowApiResponse<IpResponse> IpCheck(string ip)
        {
            string url = string.Format("http://api.k780.com/?app=ip.get&ip={0}&appkey={1}&sign={2}&&format={3}", ip, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<IpResponse>>();
            return ret;
        }
        public static NowApiResponse<PhoneResponse> PhoneCheck(string phone)
        {
            string url = string.Format("http://api.k780.com/?app=phone.get&phone={0}&appkey={1}&sign={2}&format={3}", phone, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<PhoneResponse>>();
            return ret;
        }
        public static NowApiResponse<IdCardResponse> IdCardCheck(string idCard)
        {
            string url = string.Format("http://api.k780.com/?app=idcard.get&idcard={0}&appkey={1}&sign={2}&format={3}", idCard, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<IdCardResponse>>();
            return ret;
        }
        public static NowApiResponse<List<WeatherResponse>> WeatherCheck(string city)
        {

            string url = string.Format("http://api.k780.com/?app=weather.future&weaid={0}&appkey={1}&sign={2}&format={3}", city, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<List<WeatherResponse>>>();
            return ret;
        }
        public static NowApiResponse<Dictionary<string, BikeResponse>> BikeCheck(string city)
        {
            string url = string.Format("http://api.k780.com/?app=pbike.state&city={0}&appkey={1}&sign={2}&format={3}", city, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            return jsonStr.ToObject<NowApiResponse<Dictionary<string, BikeResponse>>>();
        }
        public static NowApiResponse<ShortUrlResponse> ShortUrl(string sourceUrl, string fixedUrl = "")
        {
            string url = string.Format("http://api.k780.com/?app=shorturl.set&url={0}&fixed={1}&appkey={2}&sign={3}&format={4}", sourceUrl, fixedUrl, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<ShortUrlResponse>>();
            return ret;
        }
        public static NowApiResponse<PM25Response> PM25Check(string city)
        {

            string url = string.Format("http://api.k780.com/?app=weather.pm25&weaid={0}&appkey={1}&sign={2}&format={3}", city, Appkey, Sign, Format);
            string jsonStr = HttpMethods.HttpGet(url);
            var ret = jsonStr.ToObject<NowApiResponse<PM25Response>>();
            return ret;
        }
    }

    public class NowApiResponse<T>
    {
        public string success { get; set; }

        public T result { get; set; }
    }
    public class IpResponse
    {
        [JsonProperty("status")]
        /// <summary>
        /// 查询状态,值为OK时信息有效
        /// </summary>
        public string Status { get; set; }

        [JsonProperty("ip")]
        /// <summary>
        /// 查询Ip
        /// </summary>
        public string Ip { get; set; }
        [JsonProperty("ip_str")]
        public string IpStr { get; set; }
        [JsonProperty("ip_end")]
        public string IpEnd { get; set; }
        [JsonProperty("inet_str")]
        public string InetStr { get; set; }
        [JsonProperty("inet_end")]
        public string InetEnd { get; set; }
        [JsonProperty("operators")]
        /// <summary>
        /// 运营商
        /// </summary>
        public string Operators { get; set; }
        [JsonProperty("att")]
        /// <summary>
        /// 归属地
        /// </summary>
        public string Att { get; set; }
        [JsonProperty("detailed")]
        /// <summary>
        /// 归属详情
        /// </summary>
        public string Detailed { get; set; }
        [JsonProperty("area_style_simcall")]
        /// <summary>
        /// 地区1
        /// </summary>
        public string Area_style_simcall { get; set; }
        [JsonProperty("area_style_areanm")]
        /// <summary>
        /// 地区2
        /// </summary>
        public string Area_style_areanm { get; set; }
        [JsonProperty("msg")]
        /// <summary>
        /// 错误提示（status不为OK时有值）
        /// </summary>
        public string Msg{ get; set; }
    }
    public class PhoneResponse
    {
        [JsonProperty("status")]
        /// <summary>
        /// 查询状态,值为ALREADY_ATT时信息有效
        /// </summary>
        public string Status { get; set; }
        [JsonProperty("phone")]
        /// <summary>
        /// 查询号码
        /// </summary>
        public string Phone { get; set; }
        [JsonProperty("area")]
        /// <summary>
        /// 城市区号
        /// </summary>
        public string Area { get; set; }
        [JsonProperty("postno")]
        /// <summary>
        /// 城市邮编
        /// </summary>
        public string Postno { get; set; }
        [JsonProperty("att")]
        /// <summary>
        /// 归属地
        /// </summary>
        public string Att { get; set; }
        [JsonProperty("ctype")]
        /// <summary>
        /// 电话卡类型
        /// </summary>
        public string Ctype { get; set; }
        [JsonProperty("par")]
        /// <summary>
        /// 前缀
        /// </summary>
        public string Par { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("operators")]
        /// <summary>
        /// 运营商
        /// </summary>
        public string Operators { get; set; }
        [JsonProperty("style_simcall")]
        /// <summary>
        /// 地区1
        /// </summary>
        public string Style_simcall { get; set; }
        [JsonProperty("style_citynm")]
        /// <summary>
        /// 地区2
        /// </summary>
        public string Style_citynm { get; set; }
        [JsonProperty("msg")]
        /// <summary>
        /// 错误提示（status不为ALREADY_ATT时有值）
        /// </summary>
        public string Msg { get; set; }
    }
    public class IdCardResponse
    {
        [JsonProperty("status")]
        /// <summary>
        /// 查询状态,值为ALREADY_ATT时信息有效
        /// </summary>
        public string Status { get; set; }
        [JsonProperty("par")]
        /// <summary>
        /// 前缀
        /// </summary>
        public string Par { get; set; }
        [JsonProperty("idcard")]
        /// <summary>
        /// 查询的身份证号码
        /// </summary>
        public string Idcard { get; set; }
        [JsonProperty("born")]
        /// <summary>
        /// 出生年月日
        /// </summary>
        public string Born { get; set; }
        [JsonProperty("sex")]
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        [JsonProperty("att")]
        /// <summary>
        /// 归属地
        /// </summary>
        public string Att { get; set; }
        [JsonProperty("postno")]
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostNo { get; set; }
        [JsonProperty("areano")]
        /// <summary>
        /// 区号
        /// </summary>
        public string AreaNo { get; set; }
        [JsonProperty("style_simcall")]
        /// <summary>
        /// 地区1
        /// </summary>
        public string Style_simcall { get; set; }
        [JsonProperty("style_citynm")]
        /// <summary>
        /// 地区2
        /// </summary>
        public string Style_citynm { get; set; }
        [JsonProperty("msg")]
        /// <summary>
        /// 错误提示（status不为ALREADY_ATT时有值）
        /// </summary>
        public string Msg { get; set; }
    }
    public class WeatherResponse
    {
        [JsonProperty("weaid")]
        /// <summary>
        /// 查询城市编号
        /// </summary>
        public string Weaid { get; set; }
        [JsonProperty("days")]
        /// <summary>
        /// 日期
        /// </summary>
        public string Days { get; set; }
        [JsonProperty("week")]
        /// <summary>
        /// 星期
        /// </summary>
        public string Week { get; set; }
        [JsonProperty("cityno")]
        /// <summary>
        /// 城市全拼
        /// </summary>
        public string CityNo { get; set; }
        [JsonProperty("citynm")]
        /// <summary>
        /// 城市
        /// </summary>
        public string CityNm { get; set; }
        [JsonProperty("cityid")]
        /// <summary>
        /// 气象局城市编号
        /// </summary>
        public string CityId { get; set; }
        [JsonProperty("temperature")]
        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature { get; set; }

        [JsonProperty("temperature_curr")]
        public string Temperature_Curr { get; set; }
        [JsonProperty("weather")]
        /// <summary>
        /// 天气
        /// </summary>
        public string Weather { get; set; }
        [JsonProperty("weather_icon")]
        /// <summary>
        /// 气象局图标
        /// </summary>
        public string Weather_icon { get; set; }
        [JsonProperty("weather_icon1")]
        /// <summary>
        /// 气象局图标1
        /// </summary>
        public string Weather_icon1 { get; set; }
        [JsonProperty("wind")]
        /// <summary>
        /// 风向
        /// </summary>
        public string Wind { get; set; }
        [JsonProperty("winp")]
        /// <summary>
        /// 风力
        /// </summary>
        public string Winp { get; set; }
        [JsonProperty("temp_high")]
        /// <summary>
        /// 最高温度
        /// </summary>
        public string Temp_high { get; set; }
        [JsonProperty("temp_low")]
        /// <summary>
        /// 最低温度
        /// </summary>
        public string Temp_low { get; set; }
        [JsonProperty("humi_high")]
        /// <summary>
        /// 最高湿度
        /// </summary>
        public string Humi_high { get; set; }
        [JsonProperty("humi_low")]
        /// <summary>
        /// 最低湿度
        /// </summary>
        public string Humi_low { get; set; }
        [JsonProperty("weatid")]
        /// <summary>
        /// 白天天气ID
        /// </summary>
        public string Weatid { get; set; }
        [JsonProperty("weatid1")]
        /// <summary>
        /// 夜间天气ID
        /// </summary>
        public string Weatid1 { get; set; }
        [JsonProperty("windid")]
        /// <summary>
        /// 风向ID
        /// </summary>
        public string Windid { get; set; }
        [JsonProperty("winpid")]
        /// <summary>
        /// 风力ID
        /// </summary>
        public string Winpid { get; set; }
    }
    public class BikeResponse
    {
        [JsonProperty("cityid")]
        /// <summary>
        /// 城市Id
        /// </summary>
        public string Cityid { get; set; }
        [JsonProperty("townid")]
        /// <summary>
        /// 城镇Id
        /// </summary>
        public string Townid { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("oid")]
        public string Oid { get; set; }
        [JsonProperty("pname")]
        /// <summary>
        /// 地点
        /// </summary>
        public string Pname { get; set; }
        [JsonProperty("lat")]
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }
        [JsonProperty("lng")]
        /// <summary>
        /// 径度
        /// </summary>
        public string Lng { get; set; }
        [JsonProperty("capacity")]
        /// <summary>
        /// 车位总数
        /// </summary>
        public string Capacity { get; set; }
        [JsonProperty("canget")]
        /// <summary>
        /// 停放车辆数
        /// </summary>
        public string Canget { get; set; }
        [JsonProperty("canstop")]
        /// <summary>
        /// 离桩车辆数
        /// </summary>
        public string Canstop { get; set; }
        [JsonProperty("address")]
        /// <summary>
        /// 位置
        /// </summary>
        public string Address { get; set; }
        
    }
    public class ShortUrlResponse
    {
        [JsonProperty("keyid")]
        /// <summary>
        /// 短网址Key
        /// </summary>
        public string Keyid { get; set; }
        [JsonProperty("short_url")]
        /// <summary>
        /// 生成的短网址
        /// </summary>
        public string Short_url { get; set; }
        [JsonProperty("source_url")]
        /// <summary>
        /// 源网址
        /// </summary>
        public string Source_url { get; set; }
        [JsonProperty("exits")]
        public string Exits { get; set; }
        [JsonProperty("msgid")]
        /// <summary>
        /// 错误消息Id
        /// </summary>
        public string Msgid { get; set; }
        [JsonProperty("msg")]
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { get; set; }
    }

    public class PM25Response
    {
        [JsonProperty("weaid")]
        /// <summary>
        /// 查询城市编号
        /// </summary>
        public string Weaid { get; set; }
        [JsonProperty("cityno")]
        /// <summary>
        /// 城市全拼
        /// </summary>
        public string CityNo { get; set; }
        [JsonProperty("citynm")]
        /// <summary>
        /// 城市
        /// </summary>
        public string CityNm { get; set; }
        [JsonProperty("cityid")]
        /// <summary>
        /// 气象局城市编号
        /// </summary>
        public string CityId { get; set; }
        [JsonProperty("aqi")]
        /// <summary>
        /// AQI
        /// </summary>
        public string AQI { get; set; }
        [JsonProperty("aqi_scope")]
        /// <summary>
        /// 指数
        /// </summary>
        public string AQI_Scope { get; set; }
        [JsonProperty("aqi_levid")]
        /// <summary>
        /// 等级
        /// </summary>
        public string AQI_Levid { get; set; }
        [JsonProperty("aqi_levnm")]
        /// <summary>
        /// 空气质量
        /// </summary>
        public string AQI_Levnm { get; set; }
        [JsonProperty("aqi_remark")]
        /// <summary>
        /// 注意事项
        /// </summary>
        public string AQI_Remark { get; set; }      
    }
}
