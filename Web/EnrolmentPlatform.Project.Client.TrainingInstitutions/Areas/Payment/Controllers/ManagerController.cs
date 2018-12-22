using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Payment.Controllers
{
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 缴费单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountIndex()
        {
            return View();
        }

        /// <summary>
        /// 用户缴费记录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPaymentRecord()
        {
            return View();
        }

        /// <summary>
        /// 缴费添加
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentAdd()
        {
            return View();
        }

        /// <summary>
        /// 缴费查看
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentDetail()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Search(OrderListReqDto param)
        {
            int reCount = 0;
            param.FromChannelId = this.EnterpriseId;
            List<OrderPaymentListDto> list = OrderService.GetStudentPaymentList(param, ref reCount);
            if (list == null)
            {
                list = new List<OrderPaymentListDto>();
            }
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return grid.ToJson();
        }
    }
}