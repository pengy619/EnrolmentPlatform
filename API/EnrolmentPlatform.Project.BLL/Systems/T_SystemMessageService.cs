using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    public class T_SystemMessageService : BaseService<T_SystemMessage>, IT_SystemMessageService, IInterceptorLogic
    {
        private IT_SystemMessageRepository IT_SystemMessageRepository;

        public override bool SetCurrentRepository()
        {
            base.CurrentRepository = DIContainer.Resolve<IT_SystemMessageRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            this.IT_SystemMessageRepository = DIContainer.Resolve<IT_SystemMessageRepository>();
            base.AddDisposableObject(IT_SystemMessageRepository);
            return true;
        }


        /// <summary>
        /// 供应商根据条件分页查询系统消息列表
        /// </summary>
        /// <param name="searchParamForSpecialty"></param>
        /// <returns></returns>
       
        public GridDataResponse GetSystemMessageForSupplierForList(ParamForSystemMessageDto searchParamForSpecialty)
        {
            return IT_SystemMessageRepository.GetSystemMessageForSupplierForList(searchParamForSpecialty);
        }

        /// <summary>
        /// 系统消息列表标记已读
        /// </summary>
        /// <param name="messageIds"></param>
        /// <returns></returns>
        public bool MessageOnReadForSupplier(List<Guid> messageIds)
        {
            bool result = false;
            var messageList = this.LoadEntities(it => messageIds.Contains(it.Id)).ToList();
            if (messageList != null && messageList.Any())
            {
                messageList.ForEach(it =>
                {
                    it.Status = (int)MessageStatusEnum.Read;
                });
            }
            result = this.UpdateEntities(messageList) > 0;
            return result;
        }

        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public HomeInfoForSupplierDto GetHomeInfoForSupplierId(Guid supplierId)
        {
            return IT_SystemMessageRepository.GetHomeInfoForSupplierId(supplierId);
        }
        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="enterpriseIds">发送对象Id</param>
        /// <param name="systemMessageEnum">消息模板</param>
        /// <param name="parmas">参数值</param>
        /// <returns></returns>
        public ResultMsg SendMessage(Guid[] enterpriseIds, SystemMessageEnum systemMessageEnum, string[] parmas)
        {
            ResultMsg _resultMsg = new ResultMsg();
            try
            {

                if (enterpriseIds != null && enterpriseIds.Count() > 0)
                {
                    string content, businessName = string.Empty;
                    content = string.Format(EnumDescriptionHelper.GetDescription(systemMessageEnum), parmas);
                    switch (systemMessageEnum)
                    {
                        case SystemMessageEnum.ChangePrice:
                            businessName = "订单改价";
                            break;
                        case SystemMessageEnum.DeliverGoods:
                            businessName = "发货";
                            break;
                        case SystemMessageEnum.RefundRecordHandle:
                            businessName = "退换货处理";
                            break;
                        case SystemMessageEnum.ChangeLoginPassWord:
                            businessName = "登录密码修改";
                            break;
                        case SystemMessageEnum.Settlement:
                            businessName = "结算单生成";
                            break;
                        default:
                            break;
                    }
                    List<T_SystemMessage> _systemMessageLst = new List<T_SystemMessage>();
                    foreach (var item in enterpriseIds)
                    {
                        T_SystemMessage _systemMessage = new T_SystemMessage
                        {
                            Id = Guid.NewGuid(),
                            BusinessName = businessName,
                            Content = content,
                            Title = content,
                            Status = 1,
                            EnterpriseId = item,
                            CreatorAccount = "系统"
                        };
                        _systemMessageLst.Add(_systemMessage);
                    }
                    base.AddEntities(_systemMessageLst);
                }

            }
            catch (Exception)
            {

            }
            return _resultMsg;
        }

        /// <summary>
        /// admin查询首页信息
        /// </summary> 
        /// <returns></returns>
        public HomeInfoForSupplierDto GetHomeInfoForAdmin()
        {
            return IT_SystemMessageRepository.GetHomeInfoForAdmin();
        }

        /// <summary>
        /// ADMIN查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public HomeInfoForAdminDto GetHomeInfoForAdminDtoByTime(string startTime, string endTime)
        {
            return IT_SystemMessageRepository.GetHomeInfoForAdminDtoByTime(startTime, endTime);
        }

        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public HomeInfoForAdminDto GetHomeInfoForSupplierByTime(string startTime, string endTime, Guid supplierId)
        {
            return IT_SystemMessageRepository.GetHomeInfoForSupplierByTime(startTime, endTime, supplierId);
        }
    }
}
