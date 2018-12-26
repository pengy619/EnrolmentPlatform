using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.IBLL.Orders
{
    public interface IT_ExamService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        GridDataResponse GetPagedList(ExamSearchDto req);

        /// <summary>
        /// 新增考试
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg Add(AddExamDto dto);

        /// <summary>
        /// 获取考试名单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        GridDataResponse GetExamList(ExamListSearchDto req);

        /// <summary>
        /// 回填考试名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg Fill(FillExamInfoDto dto);
    }
}
