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
    public interface IT_FileService : IBaseService<T_File>
    {
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="foreignKeyId"></param>
        /// <returns></returns>
        List<FileDto> GetFileList(Guid foreignKeyId);

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        ResultMsg AddFile(FileDto dto);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件Id</param> 
        /// <returns></returns>
        bool DeleteFileById(Guid id);
    }
}
