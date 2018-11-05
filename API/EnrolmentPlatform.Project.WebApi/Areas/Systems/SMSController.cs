using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using EnrolmentPlatform.Project.Infrastructure.SMS;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    public class SMSController : ApiBaseController
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendVerificationCode(SendVeriyCodeDto veriyCodedto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                if (veriyCodedto.Phone.IsEmpty())
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "请提供手机号";
                }
                else
                {
                    Random ran = new Random();
                    string phoneCode = string.Empty;
                    for (int i = 0; i < 6; i++)
                    {
                        phoneCode += ran.Next(0, 9).ToString();
                    }
                    string content = string.Format(EnumDescriptionHelper.GetDescription(SMSTemplateEnum.VerificationCode), ConfigurationManager.AppSettings["MD_Sign"], phoneCode);
                    if (MiaodiHelper.SendSMS(veriyCodedto.Phone, content))
                    {
                        _resultMsg.Data = phoneCode;
                    }
                    else
                    {
                        _resultMsg.IsSuccess = false;
                        _resultMsg.Info = "发送失败，请再次操作";
                    }
                }
                return _resultMsg.ResponseMessage();
            });

        }
    }
}
