using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IDAL.Systems
{
    public interface IT_SystemLoginLogRepository : IBaseRepository<T_SystemLoginLog>
    {
        IList<T_SystemLoginLog> GetEnterpriseLoginLog(LoginLogDto param, out int records);
    }
}
