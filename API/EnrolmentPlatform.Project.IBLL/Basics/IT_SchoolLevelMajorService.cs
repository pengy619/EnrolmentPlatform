﻿using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Basics
{
    public interface IT_SchoolLevelMajorService
    {
        /// <summary>
        /// 查找子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<SchoolItemDto> FindSubItemById(Guid id);

        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns></returns>
        IList<SchoolItemDto> GetAllList();

        /// <summary>
        /// 获取学校配置
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        List<KeyValuePair<Guid, Guid>> GetSchoolConfigList(Guid schoolId);

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        ResultMsg SaveConfig(SchoolConfigDto dto);

        /// <summary>
        /// 根据层次、专业查询学校列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        List<SchoolItemListDto> GetSchoolItemList(SchoolItemListReqDto req, out int reCount);
    }
}
