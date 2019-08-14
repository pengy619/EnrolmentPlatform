using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.File;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    public class T_FileService : BaseService<T_File>, IT_FileService, IInterceptorLogic
    {
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_FileRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            return true;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="foreignKeyId"></param>
        /// <returns></returns>
        public List<FileDto> GetFileList(Guid foreignKeyId)
        {
            var query = from a in this.LoadEntities(t => !t.IsDelete && t.ForeignKeyId == foreignKeyId)
                        select new FileDto
                        {
                            Id = a.Id,
                            FilePath = a.FilePath,
                            FileName = a.FileName
                        };
            return query.ToList();
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg AddFile(FileDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            if (this.Count(t => t.ForeignKeyId == dto.ForeignKeyId && t.FileName == dto.FileName) > 0)
            {
                resultMsg.IsSuccess = false;
                resultMsg.Info = "该文件已存在，请勿重复添加";
                return resultMsg;
            }
            var file = new T_File
            {
                Id = Guid.NewGuid(),
                ForeignKeyId = dto.ForeignKeyId,
                FilePath = dto.FilePath,
                FileName = dto.FileName,
                FileClassify = (int)FileClassifyEnum.File,
                CreatorUserId = dto.CreatorUserId,
                CreatorAccount = dto.CreatorAccount
            };
            resultMsg.IsSuccess = this.AddEntity(file) > 0;
            return resultMsg;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件Id</param> 
        /// <returns></returns>  
        public bool DeleteFileById(Guid id)
        {
            return this.PhysicsDeleteBy(t => t.Id == id) > 0;
        }

    }
}
