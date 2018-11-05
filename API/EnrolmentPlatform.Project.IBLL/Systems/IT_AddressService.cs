using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    public interface IT_AddressService : IBaseService<T_Address>
    {
        /// <summary>
        /// 根绝id向上查找
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        IList<AddressDto> FindParentAddress(Guid parentId, IList<AddressDto> addressLst = null);
        /// <summary>
        /// 查找子类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IList<AddressDto> FindSubAddressId(Guid Id);
        /// <summary>
        ///根据类型查找
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IList<AddressDto> FindAddressByClassify(int classify);

        /// <summary>
        /// 获取国家所有省份
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        IList<AddressDto> GetAllProvinceByCountryName(string countryName);
        IList<AddressDto> FindAddressByClassifyAndParentId(int classify, Guid parentId);

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        ResultMsg GetList();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultMsg Add(T_Address entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultMsg Update(T_Address entity);

        /// <summary>
        /// 根据ID获取地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultMsg GetEntityById(Guid id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultMsg DeleteById(Guid id);
    }
}
