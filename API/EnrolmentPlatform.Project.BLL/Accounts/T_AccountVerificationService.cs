using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IDAL.Accounts;

namespace EnrolmentPlatform.Project.BLL.Accounts
{
    public class T_AccountVerificationService: IT_AccountVerificationService
    {
        private IT_AccountVerificationRepository AccountVerificationRepository = null;

        public T_AccountVerificationService(IT_AccountVerificationRepository accountRepository)
        {
            this.AccountVerificationRepository = accountRepository;
        }

        /// <summary>
        /// 获得子用户资源
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<AccountVerificationDto> GetAccountVerification(Guid userId)
        {
            return this.AccountVerificationRepository.LoadEntities(a => a.AccountId == userId && a.IsDelete == false)
                .Select(a => new AccountVerificationDto()
                {
                    Classify = (VerificationTypeEnum)a.Classify,
                    Id = a.Id,
                    Remark = a.Remark,
                    ResourceAddress = a.ResourceAddress,
                    ResourceId = a.ResourceId,
                    ResourceName = a.ResourceName
                })
                .ToList();
        }
    }
}
