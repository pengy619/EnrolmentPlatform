﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Image;

namespace EnrolmentPlatform.Project.Client.FileStorage.Controllers
{
    public class UpLoadController : Controller
    {
        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="postFileKey">Key</param>
        /// <param name="fromType">图片来源</param>
        /// <param name="ZoomAuto">压缩类型</param>
        [HttpPost]
        public void Index(string postFileKey, int fromType, int imageClassify = 0)
        {
            ResultMsg _msg = new ResultMsg();
            if (postFileKey.Equals(ConfigurationManager.AppSettings["PostFileKey"]))
            {
                HttpPostedFileBase _file = Request.Files[0];
                if (_file.ContentLength <= 1024 * 1024 * 3)
                {
                    string _dir = "Admin";
                    switch (fromType)
                    {
                        case 1:
                            _dir = "Admin";
                            break;
                        case 2:
                            _dir = "Supplier";
                            break;
                        case 3:
                            _dir = "B2C";
                            break;
                        case 4:
                            _dir = "TouristServiceCenter";
                            break;
                        case 5:
                            _dir = "H5";
                            break;
                        default:
                            break;
                    }

                    //文件大小
                    long size = _file.ContentLength;
                    //文件类型
                    string type = _file.ContentType;
                    //文件名
                    string name = _file.FileName;
                    //文件格式
                    string fileExt = System.IO.Path.GetExtension(name);

                    if (fileExt.ToLower() == ".jpg" || fileExt.ToLower() == ".jpeg" || fileExt.ToLower() == ".gif" || fileExt.ToLower() == ".png")
                    {
                        string dirPath = "/Upload/" + _dir + "/";
                        String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                        dirPath += ymd + "/";
                        string resultUrl = string.Empty;
                        string _fileNameTime = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo);

                        if (!Directory.Exists(Server.MapPath("~/" + dirPath)))
                        {
                            DirectoryInfo _dirInfo = Directory.CreateDirectory(Server.MapPath("~/" + dirPath));
                        }
                        if (imageClassify == 0)//原图
                        {
                            String newFileName = _fileNameTime + fileExt;
                            resultUrl = dirPath + newFileName;
                            string filePath = Server.MapPath("~/" + resultUrl);
                            _file.SaveAs(filePath);
                        }
                        else if (imageClassify == 1) //图像压缩
                        {
                            String newthumbnailFileName = _fileNameTime + "_thumbnail" + fileExt;
                            resultUrl = dirPath + newthumbnailFileName;
                            String thumbnailFilePath = Server.MapPath("~/" + resultUrl);
                            //产品列表压缩图
                            ImageThumbnail.ZoomAuto(_file.InputStream, thumbnailFilePath, 400, 400, "", "");
                        }

                        _msg.Info = resultUrl;
                    }
                    else
                    {
                        _msg.IsSuccess = false;
                        _msg.Info = "图片格式有误,只支持jpg,jpeg,gif,png！";
                    }
                }
                else
                {
                    _msg.IsSuccess = false;
                    _msg.Info = "图片大小限制为3M以内！";
                }
            }
            else
            {
                _msg.IsSuccess = false;
                _msg.Info = "上传图片未经授权！";
            }
            Response.Write(_msg.ToJson());
        }


    }
}