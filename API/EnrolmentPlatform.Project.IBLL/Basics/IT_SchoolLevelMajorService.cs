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
        /// 添加配置
        /// </summary>
        /// <returns></returns>
        ResultMsg AddConfig(SchoolConfigDto dto);
    }
}
