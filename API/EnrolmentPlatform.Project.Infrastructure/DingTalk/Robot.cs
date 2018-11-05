using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace EnrolmentPlatform.Project.Infrastructure.DingTalk
{
    public static class Robot
    {
        private static string url = "https://oapi.dingtalk.com/robot/send?access_token=6a472355c35af20bd3696c6f2d28158d77c23ddfadc8150991374cfbb4e17371";
        public static bool Send(DingMessage message)
        {
            string param = Json.ToJson(message);
            string resultStr = HttpMethods.HttpPost(url, param, "application/json");
            DingResult result = Json.ToObject<DingResult>(resultStr);
            return result.Errcode == 0;
        }

    }

    public class DingResult
    {
        [JsonProperty("errcode")]
        public int Errcode { get; set; }
        [JsonProperty("errmsg")]
        public string Errmsg { get; set; }
    }
    public enum MsgType
    {
        text = 1,
        link = 2,

    }

    public class DingMessage
    {
        [JsonProperty("msgtype")]
        public string MsgType { get; set; }
        [JsonProperty("at")]
        public AtMobiles At { get; set; }
        [JsonProperty("isAtAll")]
        public bool IsAtAll { get; set; }

    }

    public class AtMobiles
    {
        [JsonProperty("atMobiles")]
        public List<string> Lst { get; set; }
    }
    public class Content
    {
        [JsonProperty("content")]
        public string Description { get; set; }
    }
    public class TextMessage : DingMessage
    {
        [JsonProperty("text")]
        public Content Text { get; set; }
    }

    public class LinkMessage : DingMessage
    {
        [JsonProperty("link")]
        public Link Link { get; set; }
    }
    public class Link
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("picUrl")]
        public string PicUrl { get; set; }
        [JsonProperty("messageUrl")]
        public string MessageUrl { get; set; }
    }
}
