using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.Client.LearningCenter.Filter;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Product.Controllers
{
    public class TicketController : BaseController
    {
        // GET: Product/Ticket 
        public ActionResult Index()
        {
            GetBasicInfo();
            return View();
        }

        /// <summary>
        /// 基础数据资料
        /// </summary>
        private void GetBasicInfo()
        {
            //门票类别
            ViewBag.TypeForTicket = EnumDescriptionHelper.GetItemValueList<TypeForTicketEnum, int>().ToList();
            //门票状态
            ViewBag.ProductStatus = EnumDescriptionHelper.GetItemValueList<ProductStatusEnum, int>().ToList();
            //票种
            ViewBag.ProductCate = GetProductCategories();
            //票务主题
            ViewBag.TikcetParamTheme = GetTikcetParam((int)TikcetParamClassifyEnum.Theme);
            //退款规则
            ViewBag.RefundPrice = EnumDescriptionHelper.GetItemValueList<RefundPriceEnum, int>().ToList();
        }

        /// <summary>
        /// 门票编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid ticketId)
        {
            GetBasicInfo();
            var dto = GetTicketInfo(ticketId);
            return View(dto);
        }

        /// <summary>
        /// 基本信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OptionBasic(Guid? ticketId)
        {
            GetBasicInfo();
            var _id = ticketId ?? Guid.Empty;
            OptionParamForTicketForDataForBasicDto dto = GetTicketInfoForBasic(_id);
            return View(dto);
        }

        /// <summary>
        /// 详细资料
        /// </summary>
        /// <returns></returns>
        public ActionResult OptionXiang(Guid ticketId)
        {
            var dto = GetTicketInfoForXiang(ticketId);
            return View(dto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid ticketId)
        {
            var dto = GetTicketInfo(ticketId);
            return View(dto);
        }

        #region 方法
        /// <summary>
        /// 门票列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> TicketList(SearchParamForTicketMngDto param)
        {
            param.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForSupplier/GetProductForTicketForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 门票上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> TicketOnSale(List<Guid> ticketIds)
        {
            return await UpdateProductStatus(ticketIds, (int)ProductStatusEnum.Checking);
        }

        /// <summary>
        /// 门票下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> TicketOffSale(List<Guid> ticketIds)
        {
            return await UpdateProductStatus(ticketIds, (int)ProductStatusEnum.OffSale);
        }

        /// <summary>
        /// 门票草稿
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> TicketOnDraft(List<Guid> ticketIds)
        {
            return await UpdateProductStatus(ticketIds, (int)ProductStatusEnum.Draft);
        }

        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="Status"></param> 
        /// <returns></returns>
        private async Task<ActionResult> UpdateProductStatus(List<Guid> ticketIds, int Status)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = ticketIds,
                Status = Status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Ticket,
                SupplierId = this.SupplierId
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除门票
        /// </summary>
        /// <param name="ticketIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteTicket(List<Guid> ticketIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForSupplier/DelProductForTicket",
                JsonConvert.SerializeObject(ticketIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 复制门票
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> CopyTicket(Guid TicketId)
        {
            var param = new ProductForCopyDto
            {
                ProductId = TicketId,
                SupplierId = this.SupplierId,
                Classify = (int)ProductClassifyEnum.Ticket,
                CreatorUserId = this.UserId,
                CreatorAccount = this.UserAccount
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/CpoyProductById",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 基础信息页面下一步
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> TicketForBasicNext(OptionParamForTicketForDataForBasicDto optionParamForTicketForDataForBasicDto)
        {
            optionParamForTicketForDataForBasicDto.Status = (int)ProductStatusEnum.Draft;
            if (optionParamForTicketForDataForBasicDto.Id.Equals(Guid.Empty))
            {
                return await OptionTicketForBasic(optionParamForTicketForDataForBasicDto);
            }
            else
            {
                return await ModifyTicketForBasic(optionParamForTicketForDataForBasicDto);
            }
        }

        /// <summary>
        /// 基础信息页面发布
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> TicketForBasicSubmit(OptionParamForTicketForDataForBasicDto optionParamForTicketForDataForBasicDto)
        {
            optionParamForTicketForDataForBasicDto.ApplyOnSaleTime = DateTime.Now;
            optionParamForTicketForDataForBasicDto.Status = (int)ProductStatusEnum.Checking;
            return await ModifyTicketForBasic(optionParamForTicketForDataForBasicDto);
        }

        /// <summary>
        /// 详细信息页面下一步
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> TicketForXiangNext(OptionParamForTicketForDataForXiangDto optionParamForTicketForDataForXiangDto)
        {
            optionParamForTicketForDataForXiangDto.Status = (int)ProductStatusEnum.Draft;
            return await ModifyTicketForXiang(optionParamForTicketForDataForXiangDto);
        }

        /// <summary>
        /// 详细信息页面发布
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> TicketForXiangSubmit(OptionParamForTicketForDataForXiangDto optionParamForTicketForDataForXiangDto)
        {
            //optionParamForTicketForDataForBasicDto.ApplyOnSaleTime = DateTime.Now;
            optionParamForTicketForDataForXiangDto.Status = (int)ProductStatusEnum.Checking;
            return await ModifyTicketForXiang(optionParamForTicketForDataForXiangDto);
        }

        /// <summary>
        /// 基础信息页面做添加
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> OptionTicketForBasic(OptionParamForTicketForDataForBasicDto optionParamForTicketForDataForBasicDto)
        {
            optionParamForTicketForDataForBasicDto.SupplierId = this.SupplierId;
            optionParamForTicketForDataForBasicDto.CreatorAccount = this.UserAccount;
            optionParamForTicketForDataForBasicDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForSupplier/OptionProductForTicketForBasicForSupplier",
                JsonConvert.SerializeObject(optionParamForTicketForDataForBasicDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 基础信息页面做修改
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> ModifyTicketForBasic(OptionParamForTicketForDataForBasicDto optionParamForTicketForDataForBasicDto)
        {
            optionParamForTicketForDataForBasicDto.SupplierId = this.SupplierId;
            optionParamForTicketForDataForBasicDto.CreatorAccount = this.UserAccount;
            optionParamForTicketForDataForBasicDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForSupplier/ModifyProductForTicketForBasicForSupplier",
                JsonConvert.SerializeObject(optionParamForTicketForDataForBasicDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 详细信息页面做修改
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> ModifyTicketForXiang(OptionParamForTicketForDataForXiangDto optionParamForTicketForDataForXiangDto)
        {
            optionParamForTicketForDataForXiangDto.SupplierId = this.SupplierId;
            optionParamForTicketForDataForXiangDto.CreatorAccount = this.UserAccount;
            optionParamForTicketForDataForXiangDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/TicketForSupplier/ModifyProductForTicketForXiangForSupplier",
                JsonConvert.SerializeObject(optionParamForTicketForDataForXiangDto),
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
                ProductId = ticketId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/TicketForSupplier/GetProductForTicketByProductId",
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
        /// 获取门票详情-基础数据
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        private OptionParamForTicketForDataForBasicDto GetTicketInfoForBasic(Guid ticketId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = ticketId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/TicketForSupplier/GetOptionParamForTicketForDataForBasicDtoByProductId",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForTicketForDataForBasicDto data = new OptionParamForTicketForDataForBasicDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForTicketForDataForBasicDto>();
            }
            return data;
        }

        /// <summary>
        /// 获取门票详情-详细数据
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        private OptionParamForTicketForDataForXiangDto GetTicketInfoForXiang(Guid ticketId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = ticketId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/TicketForSupplier/GetOptionParamForTicketForDataForXiangDtoByProductId",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            OptionParamForTicketForDataForXiangDto data = new OptionParamForTicketForDataForXiangDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<OptionParamForTicketForDataForXiangDto>();
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
        #endregion


    }
}