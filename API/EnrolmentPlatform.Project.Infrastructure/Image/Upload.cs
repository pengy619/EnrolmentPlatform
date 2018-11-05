using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EnrolmentPlatform.Project.Infrastructure.Image
{
    public class Upload
    {
        /// <summary>
        /// 保存上传图片
        /// </summary>
        /// <param name="uploadType">值为0时保存第一张图片，为1时保存最后一张图，为2时全部保存</param>
        /// <returns></returns>

        public static List<ResultModel> UpLoadImg(string filePath, int uploadType = 0)
        {
            List<ResultModel> ret = new List<ResultModel>();
            System.Web.HttpFileCollection _file = System.Web.HttpContext.Current.Request.Files;
            if (_file.Count > 0)
            {
                try
                {
                    if (uploadType == 1)
                    {
                        List<ResultModel> uploadList = new List<ResultModel>();
                        var imgIndex = _file.Count - 1;
                        var saveRet = SaveImg(imgIndex, filePath, _file);
                        uploadList.Add(saveRet);
                        ret = uploadList;
                    }
                    else if (uploadType == 2)
                    {
                        List<ResultModel> uploadList = new List<ResultModel>();
                        for (var i = 0; i < _file.Count; i++)
                        {
                            var saveRet = SaveImg(i, filePath, _file);
                            uploadList.Add(saveRet);
                        }
                        ret = uploadList;
                    }
                    else
                    {
                        List<ResultModel> uploadList = new List<ResultModel>();
                        var saveRet = SaveImg(0,filePath, _file);
                        uploadList.Add(saveRet);
                        ret = uploadList;
                    }
                }
                catch (Exception ex)
                {
                    ret.Add(new ResultModel() { IsSuccess = false, Message = ex.Message });
                    return ret;
                }
            }
            return ret;
        }

        private static ResultModel SaveImg(int index,string filePath ,System.Web.HttpFileCollection files)
        {
            ResultModel ret = new ResultModel();
            //文件大小
            long size = files[index].ContentLength;
            //文件类型
            string type = files[index].ContentType;
            //文件名
            string name = files[index].FileName;
            //文件格式
            string _tp = System.IO.Path.GetExtension(name);

            string saveName = name;

            if (size > 1024*1024*3)
            {
                ret = new ResultModel() { IsSuccess = false, Message = "文件大小不能超过3M" };
                return ret;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                ret = new ResultModel() { IsSuccess = false, Message = "请上传图片" };
                return ret;
            }
            if (_tp.ToLower() == ".jpg" || _tp.ToLower() == ".jpeg" || _tp.ToLower() == ".gif" || _tp.ToLower() == ".png")
            {
                //获取文件流
                System.IO.Stream stream = files[index].InputStream;
                //保存文件
                saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + _tp;
                //string path = HttpContext.Current.Server.MapPath("/upload/img/" + saveName);
                string path = filePath + "/"+saveName;
                files[index].SaveAs(path);
                ret = new ResultModel() { IsSuccess = true, Message = saveName };
            }
            else
            {
                ret = new ResultModel() { IsSuccess = false, Message = "请上传正确格式的图片" };
            }
            return ret;
        }
    }
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
