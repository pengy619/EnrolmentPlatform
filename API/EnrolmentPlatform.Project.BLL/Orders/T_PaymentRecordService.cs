using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_PaymentRecordService: IT_PaymentRecordService
    {
        private IT_PaymentRecordRepository paymentRecordRepository;

        public T_PaymentRecordService()
        {
            this.paymentRecordRepository = DIContainer.Resolve<IT_PaymentRecordRepository>();
        }

        /// <summary>
        /// 新增付款单
        /// </summary>
        /// <returns></returns>
        public string AddPaymentRecord(PaymentRecordDto dto)
        {
            return this.paymentRecordRepository.AddPaymentRecord(dto);
        }

        //获得缴费登记列表

        //获得缴费登记明细

        //缴费登记审核（拒绝和通过）

        //缴费登记删除

        //查看个人缴费记录
    }
}
