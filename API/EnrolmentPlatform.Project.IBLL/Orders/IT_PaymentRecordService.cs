using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Orders;

namespace EnrolmentPlatform.Project.IBLL.Orders
{
    public interface IT_PaymentRecordService
    {
        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <returns></returns>
        string AddPaymentRecord(PaymentRecordDto dto);
    }
}
