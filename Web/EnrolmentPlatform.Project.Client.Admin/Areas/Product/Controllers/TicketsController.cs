using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
{
    public class TicketsController : BaseController
    {
        // GET: Product/Tickets 
        /// <summary>
        /// 门票 管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //门票类别
            ViewBag.TypeForTicket = EnumDescriptionHelper.GetItemValueList<TypeForTicketEnum, int>().ToList();

            //门票状态
            ViewBag.ProductStatus = EnumDescriptionHelper.GetItemValueList<ProductStatusEnum, int>().ToList();

            //供应商类型
            ViewBag.SupplierType = EnumDescriptionHelper.GetItemValueList<SupplierTypeEnum, int>().ToList();

            //票种
            ViewBag.ProductCate = GetProductCategories();

            //票务主题
            ViewBag.TikcetParamTheme = GetTikcetParam((int)TikcetParamClassifyEnum.Theme);
            return View();
        }

        /// <summary>
        /// 门票详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid ticketId)
        {
            var dto = GetTicketInfo(ticketId);
            return View(dto);
        }

        /// <summary>
        /// 门票列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> TicketList(SearchParamForTicketMngDto param)
        {
            param.StatusForNo = (int)ProductStatusEnum.Draft;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
             "/api/TicketForAdmin/GetProductForTicketForList",
             JsonConvert.SerializeObject(param),
            ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 门票审核上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> TicketOnSale(List<Guid> ticketIds, int Status, string Remark)
        {
            //10代表强制上架
            return await UpdateProductStatus(ticketIds, Status.Equals(1) ? (int)ProductStatusEnum.OnSale : Status.Equals(2) ? (int)ProductStatusEnum.CheckNo : 10, Remark);
        }

        /// <summary>
        /// 门票下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> TicketOffSale(List<Guid> ticketIds)
        {
            return await UpdateProductStatus(ticketIds, (int)ProductStatusEnum.OffSale, "");
        }

        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="Status"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        private async Task<ActionResult> UpdateProductStatus(List<Guid> ticketIds, int Status, string Remark)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = ticketIds,
                Remark = Remark,
                Status = Status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Ticket
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取门票详情
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        private OptionParamForTicketForDataDto GetTicketInfo(Guid ticketId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = ticketId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/TicketForAdmin/GetProductForTicketByProductId",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForTicketForDataDto data = new OptionParamForTicketForDataDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForTicketForDataDto>();
            }
            return data;
        }

        /// <summary>
        /// 获取门票票种
        /// </summary>
        /// <returns></returns>
        private List<ProductCategoriesDto> GetProductCategories()
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("cateClassify", ((int)ProductClassifyEnum.Ticket).ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/ProductCategories/GetProductCategoriesForAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductCategoriesDto>();
            }
            return list;
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns></returns>
        private List<ProductCategoriesDto> GetTikcetParam(int classify)
        {
            List<ProductCategoriesDto> list = new List<ProductCategoriesDto>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("classify", classify.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var ret = WebApiHelper.Get<HttpResponseMsg>(
                "/api/TicketParam/GetTikcetParamForAll", parameters.Item1, parameters.Item2,
                ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (ret.IsSuccess)
            {
                list = ret.Data.ToString().ToList<ProductCategoriesDto>();
            }
            return list;
        }
    }
}