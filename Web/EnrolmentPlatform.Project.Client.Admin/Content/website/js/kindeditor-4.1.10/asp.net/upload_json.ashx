<%@ WebHandler Language="C#" Class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using EnrolmentPlatform.Project.Infrastructure.Image;

public class Upload : IHttpHandler
{
    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

        //文件保存目录路径
        String savePath = "../attached/";

        //文件保存目录URL
        String saveUrl = aspxUrl + "../attached/";

        //定义允许上传的文件扩展名
        Hashtable extTable = new Hashtable();
        extTable.Add("image", "gif,jpg,jpeg,png,bmp");
        extTable.Add("excel", "xls,xlsx");
        //extTable.Add("flash", "swf,flv");
        //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
        //extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

        //最大文件大小
        int maxSize = 2097152;
        this.context = context;
        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError("请选择文件。");
        }

        String dirPath = context.Server.MapPath(savePath);
        if (!Directory.Exists(dirPath))
        {
            showError("上传目录不存在。");
        }

        String dirName = context.Request.QueryString["dir"];
        if (String.IsNullOrEmpty(dirName))
        {
            dirName = "image";
        }
        if (!extTable.ContainsKey(dirName))
        {
            showError("目录名不正确。");
        }

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
        {
            showError("上传文件大小不能超过2M");
        }

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
        {
            showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
        }

        //创建文件夹
        dirPath += dirName + "/";
        saveUrl += dirName + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
        dirPath += ymd + "/";
        saveUrl += ymd + "/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string _fileNameTime = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo);
        String newFileName = _fileNameTime + fileExt;
        
        String filePath = dirPath + newFileName;

        imgFile.SaveAs(filePath);

        var result = UploadImg(filePath);
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        //hash["url"] = saveUrl + "/" + newFileName;
        hash["url"] =System.Configuration.ConfigurationManager.AppSettings["FileDoMain"]+result.Info; 
        hash["urlInfo"] = result.Info; 
        hash["title"] = fileName;
        hash["border"] = fileExt;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    protected EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg UploadImg(string dirPath)
    {
        EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg _saveResult = new EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg();
        if (System.IO.File.Exists(dirPath))
        {
            System.Collections.Generic.Dictionary<object, object> parames = new System.Collections.Generic.Dictionary<object, object>();
            parames.Add("fromType", System.Configuration.ConfigurationManager.AppSettings["FileFrom"]);
            parames.Add("postFileKey", System.Configuration.ConfigurationManager.AppSettings["PostFileKey"]);
            var _saveRet = EnrolmentPlatform.Project.Infrastructure.HttpMethods.HttpPost(System.Configuration.ConfigurationManager.AppSettings["FileDoMain"] + "/UpLoad/Index", parames, dirPath.Replace(@"\\", @"\"));
            _saveResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg>(_saveRet);
        }
        else
        {
            _saveResult.IsSuccess = false;
            _saveResult.Data = "文件不存在";
        }
        return _saveResult;
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
