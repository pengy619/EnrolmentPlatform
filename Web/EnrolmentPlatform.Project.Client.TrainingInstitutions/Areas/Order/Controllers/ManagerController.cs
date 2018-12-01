using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.TrainingInstitutions.Controllers;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.Client.TrainingInstitutions.Areas.Order.Controllers
{
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 列表界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult Search(OrderListReqDto param)
        {
            int reCount = 0;
            List<OrderListDto> list = orderService.GetStudentList(param, ref reCount);
            GridDataResponse grid = new GridDataResponse
            {
                Count = reCount,
                Data = list
            };
            return Json(grid.Data.ToString());
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>1：成功，2：错误</returns>
        [HttpPost]
        public JsonResult Delete(Guid[] ids)
        {
            var ret = orderService.Delete(ids);
            if (ret == true)
            {
                return Json(new { ret = 1 });
            }
            else
            {
                return Json(new { ret = 0, msg = "删除失败。" });
            }
        }
    }
}