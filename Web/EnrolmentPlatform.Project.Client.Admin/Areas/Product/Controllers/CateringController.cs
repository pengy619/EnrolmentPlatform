using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers; 
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Product;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Product.Controllers
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
        /// 套餐列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public async Task<string> List(SearchParamForCateringPackageDto param)
        {
            param.StatusForNo = (int)ProductStatusEnum.Draft;
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductForCateringPackage/GetProductForCateringPackageForList",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return data.Data.ToString();
        }

        /// <summary>
        /// 套餐上架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> OnSale(List<Guid> productIds, int Status, string Remark)
        {
            //10代表强制上架
            return await UpdateProductStatus(productIds, Status.Equals(1) ? (int)ProductStatusEnum.OnSale : Status.Equals(2) ? (int)ProductStatusEnum.CheckNo : 10, Remark); 
        }

        /// <summary>
        /// 套餐下架
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<ActionResult> OffSale(List<Guid> productIds)
        {
            return await UpdateProductStatus(productIds, (int)ProductStatusEnum.OffSale, ""); 
        }

        /// <summary>
        /// 操作更新数据
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="Status"></param> 
        /// <returns></returns>
        private async Task<ActionResult> UpdateProductStatus(List<Guid> productIds, int Status, string Remark)
        {
            var param = new OptionParamForSpecialtyDto
            {
                ProductIds = productIds,
                Remark = Remark,
                Status = Status,
                ModifyUserId = this.UserId,
                ProductClassify = (int)ProductClassifyEnum.Catering
            };
            var data = await WebApiHelper.PostAsync<HttpResponseMsg>(
                "/api/ProductBasic/UpdateProductStatus",
                JsonConvert.SerializeObject(param),
               ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(data, JsonRequestBehavior.AllowGet);
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


        /// <summary>
        /// 获取套餐详情
        /// </summary>
        /// <param name="cateringId"></param>
        /// <returns></returns>
        private ProductForCateringPackageAllDto GetCateringInfo(Guid cateringId)
        {
            ParamForSpecialtyForDetailDto paramForSpecialtyForDetailDto = new ParamForSpecialtyForDetailDto
            {
                ProductId = cateringId
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
    }
}