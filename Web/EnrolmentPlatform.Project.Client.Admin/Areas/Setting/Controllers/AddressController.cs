using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EnrolmentPlatform.Project.Client.Admin.Controllers;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Extend;

namespace EnrolmentPlatform.Project.Client.Admin.Areas.Setting.Controllers
{
    public class AddressController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetList()
        {
            List<JSTree> listtree = new List<JSTree>();
            Dictionary<string, string> parames = new Dictionary<string, string>();
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var msg = await WebApiHelper.GetAsync<HttpResponseMsg>( "/api/Address/GetList", null, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            if (msg.StatusCode == 200 && msg.IsSuccess)
            {
                List<AddressOptionDTO> list = msg.Data == null ? null : msg.Data.ToString().ToObject<List<AddressOptionDTO>>();
                InitJsTree(list, ref listtree);
            }
            return Json(listtree, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Add(Guid parentId, string chinaRoute, string pinyinRoute, int classify = 1)
        {
            ViewBag.ParentId = parentId;
            ViewBag.ChinaRoute = chinaRoute;
            ViewBag.Classify = classify;
            ViewBag.PinyinRoute = pinyinRoute;
            return View("_Add");
        }
        [HttpGet]
        public async Task<ActionResult> Detail(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var tag = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Address/GetEntityById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            return View("_Detail", tag.Data.ToString().ToObject<AddressOptionDTO>());
        }
        [HttpPost]
        public async Task<ActionResult> Update(AddressOptionDTO entity)
        {
            HttpResponseMsg msg = new HttpResponseMsg();
            msg = await WebApiHelper.PostAsync<HttpResponseMsg>( "/api/Address/Update", JsonConvert.SerializeObject(entity), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        [HttpPost]
        public async Task<ActionResult> AddAddress(AddressOptionDTO entity)
        {
            HttpResponseMsg msg = new HttpResponseMsg();

            entity.Pinyin = entity.ChinaName.ConvertToAllSpell();
            entity.EnglishName = (entity.EnglishName ?? "") + "-" + entity.EnglishName;
            entity.ShortPinyin = entity.ChinaName.ConvertToFirstSpell();
            entity.ChinaRoute = (entity.ChinaRoute ?? "") + "-" + entity.ChinaName;
            entity.PinyinRoute = (entity.PinyinRoute ?? "") + "-" + entity.ShortPinyin;
            msg = await WebApiHelper.PostAsync<HttpResponseMsg>( "/api/Address/Add", JsonConvert.SerializeObject(entity), ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(msg);
        }
        [HttpGet]
        public async Task<JsonResult> Delete(Guid id)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", id.ToString());
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var tag = await WebApiHelper.GetAsync<HttpResponseMsg>("/api/Address/DeleteById", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
            return Json(tag, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public async Task<JsonResult> GetAddressByClass(int Classify)
        //{
        //    Dictionary<string, string> parames = new Dictionary<string, string>();
        //    parames.Add("Classify", Classify.ToString());
        //    Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
        //    var tag = await WebApiHelper.GetAsync<HttpResponseMsg>( "/api/Address/GetAddressByClass", parameters.Item1, parameters.Item2, ConfigurationManager.AppSettings["StaffId"].ToInt());
        //    List<T_Address> list = JsonConvert.DeserializeObject<List<T_Address>>(tag.Data.ToString());
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        #region 递归的得到树
        public void InitJsTree(List<AddressOptionDTO> list, ref List<JSTree> listtree)
        {
            //增加根
            JSTree jstree = new JSTree();
            jstree.id = Guid.Empty;
            jstree.text = "地理位置";
            jstree.li_attr = new LiAttr() { level = 0 };
            jstree.state = new State() { opened = true };
            listtree.Add(jstree);
            //加载第一级模块数据
            if (list != null)
            {
                List<AddressOptionDTO> level1 = list.Where(t => t.ParentId.Equals(Guid.Empty)).ToList();
                if (level1.Any())
                {
                    List<JSTree> listchild = new List<JSTree>();
                    foreach (var item in level1)
                    {
                        JSTree jstreeitem = new JSTree();
                        jstreeitem.id = item.Id;
                        jstreeitem.text = item.ChinaName;
                        jstreeitem.state = new State() { opened = true };
                        jstreeitem.li_attr = new LiAttr() { parentId = item.ParentId, level = item.Classify, ShortPinyin = item.ShortPinyin };
                        listchild.Add(jstreeitem);
                    }
                    GetListTree(list, ref listchild);
                    listtree[0].children = listchild;
                }
            }
        }
        public void GetListTree(List<AddressOptionDTO> list, ref List<JSTree> listtree)
        {
            foreach (var itemtree in listtree)
            {
                List<AddressOptionDTO> listp = list.Where(t => t.ParentId == itemtree.id).ToList();
                List<JSTree> listchild = new List<JSTree>();
                foreach (var itemp in listp)
                {
                    JSTree jstreeitem = new JSTree();
                    jstreeitem.id = itemp.Id;
                    jstreeitem.text = itemp.ChinaName;
                    jstreeitem.icon = itemp.Classify == (int)E_AddressClassify.Street ? "none" : "";
                    jstreeitem.state = new State() { opened = (itemp.Classify > 2 ? false : true) };
                    jstreeitem.li_attr = new LiAttr() { parentId = itemp.ParentId, level = itemp.Classify, ShortPinyin = itemp.ShortPinyin };
                    listchild.Add(jstreeitem);
                }
                itemtree.children = listchild.OrderBy(t => t.text).ToList();
                GetListTree(list, ref listchild);
            }
        }
        #endregion
    }
}