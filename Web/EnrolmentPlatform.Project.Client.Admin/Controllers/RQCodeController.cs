using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Infrastructure.RQCode;

namespace EnrolmentPlatform.Project.Client.Admin.Controllers
{
    public class RQCodeController : Controller
    {
        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="qrInfo"></param>
        /// <returns></returns>
        public ActionResult GetRQCode(string content)
        {
            try
            {
                string fullName = Server.MapPath("~\\Content\\RQ") + "\\" + content + ".png";
                //检查是否存在该路径
                if (!Directory.Exists(Path.GetDirectoryName(fullName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fullName));
                }
                bool result = RQCodeHelper.CreateQRCode(content, "Byte", 3, 0, "M", fullName, false, null);
                if (result)
                {
                    return File(fullName, @"image/jpeg");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}