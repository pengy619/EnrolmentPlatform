using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.BLL.Orders;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Filter;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IBLL.Articles;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using NPOI.HSSF.UserModel;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers
{
    [AuthorityAttribute]
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        private UserLoginDto _loginInfo = null;
        private UserLoginDto LoginInfo
        {
            get
            {
                if (this._loginInfo == null)
                {
                    this._loginInfo = LoginInfoHandle.GetLoginInfo();
                }
                return this._loginInfo;
            }
        }

        /// <summary>
        /// 当前为招生机构ID
        /// </summary>
        public Guid EnterpriseId
        {
            get
            {
                return this.LoginInfo.EnterpriseId;
            }
        }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string EnterpriseName
        {
            get
            {
                var enterprise = DIContainer.Resolve<IT_EnterpriseService>().GetSupplierInfo(this.EnterpriseId);
                return enterprise?.EnterpriseName;
            }
        }

        /// <summary>
        /// 账号ID（目前用户ID）
        /// </summary>
        public Guid UserId
        {
            get
            {
                return this.LoginInfo.UserId;
            }
        }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId
        {
            get
            {
                return this.LoginInfo.RoleId;
            }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.LoginInfo.UserName;
            }
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount
        {
            get
            {
                return this.LoginInfo.UserAccount;
            }
        }


        /// <summary>
        /// 用户手机
        /// </summary>
        public string Phone
        {
            get
            {
                return this.LoginInfo.Phone;
            }
        }

        /// <summary>
        /// 是否是主账号
        /// </summary>
        public bool IsMaster
        {
            get
            {
                return this.LoginInfo.IsMaster;
            }
        }

        #region Service

        /// <summary>
        /// 订单接口
        /// </summary>
        public static IT_OrderService OrderService = DIContainer.Resolve<IT_OrderService>();

        /// <summary>
        /// 缴费接口
        /// </summary>
        public static IT_PaymentRecordService PaymentRecordService = DIContainer.Resolve<IT_PaymentRecordService>();

        /// <summary>
        /// 基础数据接口
        /// </summary>
        public static IT_MetadataService MetadataService = DIContainer.Resolve<IT_MetadataService>();

        /// <summary>
        /// 层级接口
        /// </summary>
        public static IT_SchoolLevelMajorService LevelService = DIContainer.Resolve<IT_SchoolLevelMajorService>();

        /// <summary>
        /// 考试接口
        /// </summary>
        public static IT_ExamService ExamService = DIContainer.Resolve<IT_ExamService>();

        /// <summary>
        /// 文件接口
        /// </summary>
        public static IT_FileService FileService = DIContainer.Resolve<IT_FileService>();

        /// <summary>
        /// 文章栏目接口
        /// </summary>
        public static IT_ArticleCategoryService ArticleCategoryService = DIContainer.Resolve<IT_ArticleCategoryService>();

        /// <summary>
        /// 文章接口
        /// </summary>
        public static IT_ArticleService ArticleService = DIContainer.Resolve<IT_ArticleService>();

        /// <summary>
        /// 订单审批接口
        /// </summary>
        public static IT_OrderApprovalService OrderApprovalService = DIContainer.Resolve<IT_OrderApprovalService>();

        /// <summary>
        /// 指标设置接口
        /// </summary>
        public static IT_StockSettingService StockSettingService = DIContainer.Resolve<IT_StockSettingService>();

        /// <summary>
        /// 自定义接口
        /// </summary>
        public static IT_CustomerFieldService CustomerFieldService = DIContainer.Resolve<IT_CustomerFieldService>();

        #endregion

        #region NPOI模板导出

        /// <summary>
        /// NPOI组件导出Excel共用方法
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="hssfworkbook"></param>
        /// <param name="sheets"></param>
        protected void NPOIExport(string fileName, NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook, List<HSSFSheet> sheets)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                hssfworkbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheets.ForEach(a =>
                {
                    a = null;
                });
                hssfworkbook = null;

                byte[] data = ms.ToArray();

                //客户端保存
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";

                string encodeFileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).Replace("+", " ");
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + encodeFileName));// Server.UrlEncode(fileName)
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
            }
        }

        #endregion

        #region 图片上传

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file">图片文件</param>
        /// <returns></returns>
        protected string ImageUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }

            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            string fileServerUrl = System.Configuration.ConfigurationManager.AppSettings["FileDoMain"];
            string fileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1].ToString();
            System.Collections.Generic.Dictionary<object, object> parames = new System.Collections.Generic.Dictionary<object, object>();
            parames.Add("fromType", System.Configuration.ConfigurationManager.AppSettings["FileFrom"]);
            parames.Add("postFileKey", System.Configuration.ConfigurationManager.AppSettings["PostFileKey"]);
            var _saveRet = EnrolmentPlatform.Project.Infrastructure.HttpMethods.HttpPost(fileServerUrl + "/UpLoad/Index", parames, fileName, data);
            EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg _saveResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrolmentPlatform.Project.Infrastructure.HttpResponseMsg>(_saveRet);
            if (_saveResult.IsSuccess == false)
            {
                return "";
            }

            //图片完整地址
            return fileServerUrl + "/" + _saveResult.Info;
        }

        #endregion
    }
}