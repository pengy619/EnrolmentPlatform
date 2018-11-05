using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.Client.LearningCenter.Controllers
{
    public class AddressController : Controller
    {
        public ActionResult Index(Guid? addressId)
        {
            List<AddressDto> list = new List<AddressDto>();
            if (addressId.HasValue && !addressId.IsEmpty())
            {
                Dictionary<string, string> parames = new Dictionary<string, string>();
                parames.Add("parentId", addressId.ToString());
                Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
                var msg = WebApiHelper.Get<HttpResponseMsg>("/api/Address/FindParentAddressId", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
                list = msg.Data.ToString().ToObject<List<AddressDto>>();
            }
            ViewBag.AddressList = list;

            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetChinaAllProvince()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("countryName", "中国");
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Address/GetAllProvinceByCountryName", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            msg.Data = JsonConvert.DeserializeObject<List<AddressDto>>(msg.Data.ToString());
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> GetAddressByParentId(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("Id", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Address/FindSubAddressId", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            msg.Data = JsonConvert.DeserializeObject<List<AddressDto>>(msg.Data.ToString());
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}