using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IBLL.Accounts
{
    public interface IT_AccountVerificationService
    {
        /// <summary>
        /// 获得子用户资源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<AccountVerificationDto> GetAccountVerification(Guid userId);
    }
}
