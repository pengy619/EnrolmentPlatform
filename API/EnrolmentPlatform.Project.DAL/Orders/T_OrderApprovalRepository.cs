using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_OrderApprovalRepository : BaseRepository<T_OrderApproval>, IT_OrderApprovalRepository
    {
    }
}
