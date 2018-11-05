using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    public interface IT_FileService : IBaseService<T_File>
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Id">文件Id</param> 
        /// <returns></returns>
        bool DeleteFileById(Guid Id);
    }
}
