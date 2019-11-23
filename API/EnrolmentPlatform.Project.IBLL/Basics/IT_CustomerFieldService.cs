using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Basics
{
    /// <summary>
    /// 自定义字段服务
    /// </summary>
    public interface IT_CustomerFieldService
    {
        /// <summary>
        /// 获得一个学校所有自定义字段列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        List<CustomerFieldDto> GetFullList();

        /// <summary>
        /// 获得一个学校所有自定义字段列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        List<CustomerFieldDto> GetAllList(GetAllListSearchDto dto);

        /// <summary>
        /// 获得一个学校所有自定义字段列表
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        GridDataResponse GetList(GetAllListSearchDto dto);

        /// <summary>
        /// 新增自定义字段
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        ResultMsg Add(CustomerFieldDto dto);

        /// <summary>
        /// 修改自定义字段
        /// </summary>
        /// <param name="">dto</param>
        /// <returns></returns>
        ResultMsg Update(CustomerFieldDto dto);

        /// <summary>
        /// 查找自定义字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        CustomerFieldDto Find(Guid id);

        /// <summary>
        /// 删除自定义字段
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        ResultMsg Delete(Guid id);
    }
}
