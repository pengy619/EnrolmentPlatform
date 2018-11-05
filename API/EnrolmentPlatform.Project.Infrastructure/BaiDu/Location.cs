using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.BaiDu
{

    public static class Location
    {
        public const string AK = "OKCHaigXg0MLLVZrmwTgsrAWNYfV1ooc";
        public static string Url = "http://api.map.baidu.com/";
        /// <summary>
        /// 得到ip定位
        /// </summary>
        /// <param name="ip">不传为当前请求地址</param>
        /// <param name="coor">设置返回位置信息中，经纬度的坐标类型，分别如下 coor不出现、或为空：百度墨卡托坐标，即百度米制坐标； coor = bd09ll：百度经纬度坐标，在国测局坐标基础之上二次加密而来；coor = gcj02：国测局02坐标，在原始GPS坐标基础上，按照国家测绘行业统一要求，加密后的坐标；注：百度地图的坐标类型为bd09ll，如果结合百度地图使用，请注意坐标选择。</param>
        /// <returns></returns>
        public static LocationModel GetLocationByIp(string ip, string coor = "bd09ll")
        {
            try
            {
                string FormatUrl = Url + "location/ip?ip={0}&ak={1}&coor={2}";
                string urlStr = string.Format(FormatUrl, ip, AK, coor);
                string result = HttpMethods.HttpGet(urlStr);
                LocationModel model = Json.ToObject<LocationModel>(result);
                return model;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 得到静态图片地址
        /// </summary>
        /// <param name="mcode">安全码。若为Android/IOS SDK的ak, 该参数必需。</param>
        /// <param name="center">地图中心点位置，参数可以为经纬度坐标或名称。坐标格式：lng<经度>，lat<纬度>，例如116.43213,38.76623。</param>
        /// <param name="width">图片宽度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。</param>
        /// <param name="height">图片高度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。</param>
        /// <param name="zoom">地图级别。高清图范围[3, 18]；低清图范围[3,19]</param>
        /// <returns></returns>
        public static string GetStaticimage(string mcode, string center, string width, string height, string zoom)
        {
            string FormatUrl = Url + "/staticimage/v2?ak={0}&mcode={1}&center={2}&width={3}&height={4}&zoom={5}";
            string result = string.Format(FormatUrl,AK, mcode, center, width, height, zoom);
            return result;
        }
        /// <summary>
        /// 得到全景静态图片地址
        /// </summary>
        /// <param name="mcode">安全码。若为Android/IOS SDK的ak, 该参数必需。</param>
        /// <param name="location">地图中心点位置，参数可以为经纬度坐标或名称。坐标格式：lng<经度>，lat<纬度>，例如116.43213,38.76623。</param>
        /// <param name="width">图片宽度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。</param>
        /// <param name="height">图片高度。取值范围：(0, 1024]。Scale=2,取值范围：(0, 512]。</param>
        /// <param name="fov">水平方向范围，范围[10,360]，fov=360即可显示整幅全景图</param>
        /// <returns></returns>
        public static string GetPanoramaStaticimage(string mcode, string location, string width, string height, string fov)
        {
            string FormatUrl = Url + "/panorama/v2?ak={0}&mcode={1}&location={2}&width={3}&height={4}&fov={5}";
            string result = string.Format(FormatUrl, AK, mcode, location, width, height, fov);
            return result;
        }

        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
    }
    public class LocationModel
    {
        public string address { get; set; }
        public Content content { get; set; }
        public int status { get; set; }
    }

    public class Content
    {
        public string address { get; set; }
        public Address_Detail address_detail { get; set; }
        public Point point { get; set; }

    }
    public class Address_Detail
    {
        public string city { get; set; }
        public string city_code { get; set; }
        public string district { get; set; }
        public string province { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
    }

    public class Point
    {
        /// <summary>
        /// 当前城市中心点经度
        /// </summary>
        public string x { get; set; }
        /// <summary>
        /// 当前城市中心点纬度
        /// </summary>
        public string y { get; set; }
    }
}
