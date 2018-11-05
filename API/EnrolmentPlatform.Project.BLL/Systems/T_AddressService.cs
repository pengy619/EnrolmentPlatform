using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    public class T_AddressService : BaseService<T_Address>, IT_AddressService, IInterceptorLogic
    {
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_AddressRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }
        /// <summary>
        /// 根据id查Parent
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>

        public IList<AddressDto> FindParentAddress(Guid parentId, IList<AddressDto> addressLst = null)
        {
            T_Address _address = base.LoadEntities(it => it.Id == parentId).FirstOrDefault();

            if (_address != null)
            {
                if (addressLst == null)
                {
                    addressLst = new List<AddressDto>();
                }
                addressLst.Add(new AddressDto { Id = _address.Id, ChinaName = _address.ChinaName, Classify = _address.Classify });
                return FindParentAddress(_address.ParentId, addressLst);
            }
            else
            {
                return addressLst;
            }
        }
        /// <summary>
        /// 根据id查子项
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public IList<AddressDto> FindSubAddressId(Guid Id)
        {
            return base.LoadEntities(it => it.ParentId == Id).OrderBy(it => it.ChinaName).Select(t => new AddressDto { Id = t.Id, ChinaName = t.ChinaName }).ToList();
        }

        /// <summary>
        /// 根据类型查找地址
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        public IList<AddressDto> FindAddressByClassify(int classify)
        {
            return base.LoadEntities(it => it.Classify == classify).OrderBy(it => it.ChinaName).Select(t => new AddressDto { Id = t.Id, ChinaName = t.ChinaName }).ToList();
        }

        /// <summary>
        /// 获取国家所有省份
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>

        public IList<AddressDto> GetAllProvinceByCountryName(string countryName)
        {
            var list = new List<AddressDto>();
            T_Address address = base.LoadEntities(t => t.Classify == 2 && t.ChinaName == countryName).FirstOrDefault();
            if (address != null)
            {
                list = base.LoadEntities(t => t.Classify == 3 && t.ParentId == address.Id).OrderBy(it => it.ChinaName).Select(t => new AddressDto { Id = t.Id, ChinaName = t.ChinaName }).ToList();
            }
            return list;
        }
        /// <summary>
        /// 根据类型和父类Id查找
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>

        public IList<AddressDto> FindAddressByClassifyAndParentId(int classify, Guid parentId)
        {
            return base.LoadEntities(it => it.Classify == classify && it.ParentId == parentId).OrderBy(it => it.ChinaName).Select(t => new AddressDto { Id = t.Id, ChinaName = t.ChinaName }).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMsg DeleteById(Guid id)
        {
            ResultMsg msg = new ResultMsg();
            string strSql = "EXEC SP_DeleteAddress @Id";
            SqlParameter[] Paras = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };
            int n = this.ExecSql(strSql, E_DbClassify.Write, Paras);
            msg.IsSuccess = n > 0 ? true : false; ;
            return msg;
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>

        public ResultMsg GetList()
        {
            ResultMsg _resultMsg = new ResultMsg();
            List<T_Address> list = this.LoadEntities(t => t.IsDelete == false).ToList();
            _resultMsg.Data = list;
            return _resultMsg;

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMsg Add(T_Address entity)
        {

            ResultMsg _resultMsg = new ResultMsg();
            entity.Id = Guid.NewGuid();
            entity.CreatorTime = DateTime.Now;
            entity.CreatorAccount = entity.CreatorAccount ?? "";
            string _businessName = string.Format("{0}添加行政区", entity.CreatorAccount);
            _resultMsg.IsSuccess = (this.AddEntity(entity, E_DbClassify.Write, _businessName) > 0 ? true : false);
            return _resultMsg;


        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMsg Update(T_Address entity)
        {

            ResultMsg _resultMsg = new ResultMsg();
            T_Address dbentity = this.FindEntityByIdToDB(entity.Id);
            dbentity.ChinaName = entity.ChinaName;
            dbentity.EnglishName = entity.EnglishName;
            dbentity.Pinyin = entity.Pinyin;
            dbentity.ShortPinyin = entity.ShortPinyin;
            dbentity.Classify = entity.Classify;
            dbentity.ChinaRoute = entity.ChinaRoute;
            dbentity.PinyinRoute = entity.PinyinRoute;
            dbentity.IsMunicipality = entity.IsMunicipality;
            dbentity.ZipCode = entity.ZipCode;
            dbentity.AreaCode = entity.AreaCode;
            dbentity.LastModifyTime = DateTime.Now;
            dbentity.LastModifyUserId = entity.LastModifyUserId;
            string _businessName = string.Format("{0}修改行政区", entity.CreatorAccount);
            _resultMsg.IsSuccess = (this.UpdateEntity(dbentity, E_DbClassify.Write, _businessName) > 0 ? true : false);

            return _resultMsg;


        }

        /// <summary>
        /// 根据ID获取地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMsg GetEntityById(Guid id)
        {
            ResultMsg _resultMsg = new ResultMsg();
            T_Address dbentity = this.FindEntityById(id);
            _resultMsg.Data = dbentity;
            return _resultMsg;

        }
    }
}
