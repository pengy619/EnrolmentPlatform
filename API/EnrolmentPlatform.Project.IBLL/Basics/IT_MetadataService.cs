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
        /// 添加元数据
        /// </summary>
        /// <returns></returns>
        ResultMsg Add(MetadataDto dto);

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <returns></returns>
        ResultMsg Update(MetadataDto dto);

        /// <summary>
        /// 删除元数据
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
        /// 获取可用的列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<MetadataDto> GetEnableList(MetadataTypeEnum type);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        GridDataResponse GetPagedList(MetadataSearchDto req);

        /// <summary>
        /// 启用/禁用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        ResultMsg EnableOrDisable(Guid id, bool isEnable);

        /// <summary>
        /// 根据学习形式获取学校列表
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        List<MetadataDto> GetSchoolListByTags(string tags, Guid? enterpriseId);

        /// <summary>
        /// 获取学校必须上传的证件
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        List<int> GetSchoolImageTypes(Guid schoolId);

        /// <summary>
        /// 获取学校必须上传的证件
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="isZsb"></param>
        /// <returns></returns>
        List<int> GetSchoolImageTypes(Guid schoolId, bool isZsb = false);

        /// <summary>
        /// 保存学校证件配置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg SaveSchoolImageConfig(SchoolImageConfigDto dto);
    }
}
