using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.IDAL.Orders
{
    public interface IT_PaymentRecordRepository : IBaseRepository<T_PaymentRecord>
    {
        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <returns></returns>
        string AddPaymentRecord(PaymentRecordDto dto);
    }
}
