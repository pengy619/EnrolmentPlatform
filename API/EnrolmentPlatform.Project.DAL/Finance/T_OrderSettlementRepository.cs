using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Finance;
using EnrolmentPlatform.Project.IDAL.Finance;

namespace EnrolmentPlatform.Project.DAL.Finance
{
    public class T_OrderSettlementRepository : BaseRepository<T_OrderSettlement>, IT_OrderSettlementRepository
    {

    }
}
