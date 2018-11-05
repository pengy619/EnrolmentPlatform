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
        /// 删除文件
        /// </summary>
        /// <param name="Id">文件Id</param> 
        /// <returns></returns>  
        public bool DeleteFileById(Guid Id)
        {
            #region 删除农产品图片  
            T_File t_File = this.FindEntityById(Id);
            return this.PhysicsDeleteEntity(t_File, E_DbClassify.Write, "删除农产品图片", true, t_File.ForeignKeyId.ToString()) > 0;
            #endregion
        }

    }
}
