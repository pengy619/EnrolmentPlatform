using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    public interface IT_SystemLoginLogService : IBaseService<T_SystemLoginLog>
    {
        IList<T_SystemLoginLog> GetLoginLog(LoginLogDto param, out int records);
        IList<T_SystemLoginLog> GetEnterpriseLoginLog(LoginLogDto param, out int records);
    }
}
