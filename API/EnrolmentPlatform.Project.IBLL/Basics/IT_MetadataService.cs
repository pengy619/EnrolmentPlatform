using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Basics
{
    public interface IT_MetadataService
    {
        /// <summary>
        /// 添加基础数据
        /// </summary>
        /// <returns></returns>
        ResultMsg Add(MetadataDto dto);

        /// <summary>
        /// 修改基础数据
        /// </summary>
        /// <returns></returns>
        ResultMsg Update(MetadataDto dto);

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        ResultMsg Delete(List<Guid> idList);

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        List<MetadataDto> GetList(MetadataTypeEnum type);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        GridDataResponse GetPagedList(MetadataSearchDto req);
    }
}
