using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.LearningCenter.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Areas.Product.Controllers
{
    public class CateringController : BaseController
    {
        /// <summary>
        /// 套餐管理
        /// </summary>
        /// <returns></returns>
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
            //状态
            ViewBag.ProductStatus = EnumDescriptionHelper.GetItemValueList<ProductStatusEnum, int>().ToList();
        }

        /// <summary>
        /// 编辑套餐管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Option(Guid? cateringId)
        {
            ViewBag.UsePeopleEnum = EnumDescriptionHelper.GetItemValueList<UsePeopleEnum, int>().ToList();
            ViewBag.RefundRuleEnum = EnumDescriptionHelper.GetItemValueList<RefundRuleEnum, int>().ToList();
            ProductForCateringPackageDataDto productForCateringPackageDataDto = new ProductForCateringPackageDataDto();
            var cid = cateringId ?? cateringId.GetValueOrDefault();
            if (!cid.Equals(Guid.Empty))
            {
                productForCateringPackageDataDto = GetCateringForBasic(cid);
            }
            return View(productForCateringPackageDataDto);
        }

        /// <summary>
        /// 编辑套餐详情
        /// </summary>
        /// <returns></returns>
        public ActionResult OptionDetail(Guid? cateringId)
        {
            ProductForCateringPackageDetailDataDto productForCateringPackageDetailDataDto = new ProductForCateringPackageDetailDataDto();
            var cid = cateringId ?? cateringId.GetValueOrDefault();
            if (!cid.Equals(Guid.Empty))
            {
                productForCateringPackageDetailDataDto = GetCateringForDetail(cid);
            }
            return View(productForCateringPackageDetailDataDto);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid? cateringId)
        {
            ProductForCateringPackageAllDto productForCateringPackageAllDto = new ProductForCateringPackageAllDto();
            var cid = cateringId ?? cateringId.GetValueOrDefault();
            if (!cid.Equals(Guid.Empty))
            {
                productForCateringPackageAllDto = GetCateringInfo(cid);
            }
            return View(productForCateringPackageAllDto);
        }

        #region 方法
        /// <summary>
        /// 套餐列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> List(SearchParamForCateringPackageDto param)
        {
            param.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/GetProductForCateringPackageForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }
        /// <summary>
        /// 套餐列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> MenuList(SearchParamForFoodDto param)
        {
            param.SupplierId = this.SupplierId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/GetFoodForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 套餐上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> OnSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.Checking);
        }

        /// <summary>
        /// 套餐下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> OffSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.OffSale);
        }
         
        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="Status"></param> 
        /// <returns></returns>
        private async Task<ActionResult> UpdateProductStatus(List<Guid> productIds, int Status)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = productIds,
                Status = Status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Catering,
                SupplierId = this.SupplierId
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(List<Guid> productIds)
        {
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/DelProductForCateringPackage",
                JsonConvert.SerializeObject(productIds),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 复制套餐
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ActionResult> Copy(Guid productId)
        {
            var param = new ProductForCopyDto
            {
                ProductId = productId,
                SupplierId = this.SupplierId,
                Classify = (int)ProductClassifyEnum.Catering,
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
        public async Task<ActionResult> CateringForNext(ProductForCateringPackageDataDto productForCateringPackageDataDto)
        {
            productForCateringPackageDataDto.Status = (int)ProductStatusEnum.Draft;
            if (productForCateringPackageDataDto.ProductId.Equals(Guid.Empty))
            {
                return await OptionCateringForBasic(productForCateringPackageDataDto);
            }
            else
            {
                return await ModifyCateringForBasic(productForCateringPackageDataDto);
            }
        }

        /// <summary>
        /// 基础信息页面发布
        /// </summary>
        /// <param name="optionParamForTicketForDataForBasicDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> CateringForSubmit(ProductForCateringPackageDataDto productForCateringPackageDataDto)
        {
            productForCateringPackageDataDto.ApplyOnSaleTime = DateTime.Now;
            productForCateringPackageDataDto.Status = (int)ProductStatusEnum.Checking;
            return await ModifyCateringForBasic(productForCateringPackageDataDto);
        }

        /// <summary>
        /// 详细信息页面发布
        /// </summary> 
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> CateringForDetailSubmit(ProductForCateringPackageDetailDataDto productForCateringPackageDetailDataDto)
        {
            productForCateringPackageDetailDataDto.Status = (int)ProductStatusEnum.Checking;
            return await ModifyCateringForDetail(productForCateringPackageDetailDataDto);
        }

        /// <summary>
        /// 详细信息页面草稿
        /// </summary> 
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<ActionResult> CateringForDetailDraft(ProductForCateringPackageDetailDataDto productForCateringPackageDetailDataDto)
        {
            productForCateringPackageDetailDataDto.Status = (int)ProductStatusEnum.Draft;
            return await ModifyCateringForDetail(productForCateringPackageDetailDataDto);
        }

        /// <summary>
        /// 基础信息页面做添加
        /// </summary>
        /// <param name="productForCateringPackageDataDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> OptionCateringForBasic(ProductForCateringPackageDataDto productForCateringPackageDataDto)
        {
            productForCateringPackageDataDto.SupplierId = this.SupplierId;
            productForCateringPackageDataDto.CreatorAccount = this.UserAccount;
            productForCateringPackageDataDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/OptionCateringForBasic",
                JsonConvert.SerializeObject(productForCateringPackageDataDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 基础信息页面做修改
        /// </summary>
        /// <param name="productForCateringPackageDataDto"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> ModifyCateringForBasic(ProductForCateringPackageDataDto productForCateringPackageDataDto)
        {
            productForCateringPackageDataDto.SupplierId = this.SupplierId;
            productForCateringPackageDataDto.CreatorAccount = this.UserAccount;
            productForCateringPackageDataDto.CreatorUserId = this.UserId;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/ModifyCateringForBasic",
                JsonConvert.SerializeObject(productForCateringPackageDataDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 详细信息页面做修改
        /// </summary> 
        /// <returns></returns>
        [ValidateInput(false)]
        private async Task<ActionResult> ModifyCateringForDetail(ProductForCateringPackageDetailDataDto productForCateringPackageDetailDataDto)
        {
            productForCateringPackageDetailDataDto.SupplierId = this.SupplierId;
            productForCateringPackageDetailDataDto.CreatorAccount = this.UserAccount;
            productForCateringPackageDetailDataDto.CreatorUserId = this.UserId;
            productForCateringPackageDetailDataDto.CreatorTime = DateTime.Now;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/ModifyCateringForDetail",
                JsonConvert.SerializeObject(productForCateringPackageDetailDataDto),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取套餐详情
        /// </summary>
        /// <param name="cateringId"></param>
        /// <returns></returns>
        private ProductForCateringPackageAllDto GetCateringInfo(Guid cateringId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = cateringId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/ProductForCateringPackage/GetCateringInfo",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            ProductForCateringPackageAllDto data = new ProductForCateringPackageAllDto();
            if (ret.IsSuccess)
            {
                data = ret.Data.ToString().ToObject<ProductForCateringPackageAllDto>();
            }
            return data;
        }

        /// <summary>
        /// 获取套餐详情-基础数据
        /// </summary>
        /// <param name="cateringId"></param>
        /// <returns></returns>
        private ProductForCateringPackageDataDto GetCateringForBasic(Guid cateringId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = cateringId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/ProductForCateringPackage/GetCateringForBasic",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            ProductForCateringPackageDataDto data = new ProductForCateringPackageDataDto();
            if (ret.IsSuccess && ret.Data != null)
            {
                data = ret.Data.ToString().ToObject<ProductForCateringPackageDataDto>();
            }
            return data;
        }

        /// <summary>
        /// 获取套餐详情-详细数据
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        private ProductForCateringPackageDetailDataDto GetCateringForDetail(Guid cateringId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = cateringId,
                SupplierId = this.SupplierId
            };
            var ret = WebApiHelper.Post<HttpResponseMsg>(
                    "/api/ProductForCateringPackage/GetCateringForDetail",
                    JsonConvert.SerializeObject(paramForSpecialtyForDetailDto),
                   ConfigurationManager.AppSettings["StaffId"].ToInt());
            ProductForCateringPackageDetailDataDto data = new ProductForCateringPackageDetailDataDto();
            if (ret.IsSuccess && ret.Data != null)
            {
                data = ret.Data.ToString().ToObject<ProductForCateringPackageDetailDataDto>();
            }
            return data;
        }

        #endregion
    }
}