using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IDAL.Finance;

namespace EnrolmentPlatform.Project.DAL.Finance
{
    public class T_AccountDetailInfoRepository : BaseRepository<T_AccountDetailInfo>, IT_AccountDetailInfoRepository
    {
    }
}
