using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Basics;
using EnrolmentPlatform.Project.IBLL.Basics;
using EnrolmentPlatform.Project.IDAL.Basics;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Basics
{
    public class T_ChargeStrategyService : IT_ChargeStrategyService, IInterceptorLogic
    {
        private IT_ChargeStrategyRepository chargeStrategyRepository;

        public T_ChargeStrategyService()
        {
            this.chargeStrategyRepository = DIContainer.Resolve<IT_ChargeStrategyRepository>();
        }

        /// <summary>
        /// 添加收费策略
        /// </summary>
        /// <returns></returns>
        public ResultMsg Add(ChargeStrategyDto dto)
        {
            ResultMsg _resultMsg = new ResultMsg();
            var exist = this.chargeStrategyRepository.Count(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId && a.MajorId == dto.MajorId
            && ((a.StartDate <= dto.StartDate && a.EndDate >= dto.StartDate) || (a.StartDate <= dto.EndDate && a.EndDate >= dto.EndDate))) > 0;
            if (exist == true)
            {
                _resultMsg.IsSuccess = false;
                _resultMsg.Info = "时间段不允许重叠";
                return _resultMsg;
            }

            var entity = new T_ChargeStrategy
            {
                Id = Guid.NewGuid(),
                SchoolId = dto.SchoolId,
                LevelId = dto.LevelId,
                MajorId = dto.MajorId,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                InstitutionCharge = dto.InstitutionCharge,
                CenterCharge = dto.CenterCharge,
                CreatorUserId = dto.CreatorUserId,
                CreatorAccount = dto.CreatorAccount
            };
            _resultMsg.IsSuccess = this.chargeStrategyRepository.AddEntity(entity) > 0;
            return _resultMsg;
        }

        /// <summary>
        /// 删除收费策略
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            return this.chargeStrategyRepository.PhysicsDeleteBy(t => t.Id == id) > 0;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public GridDataResponse GetPagedList(ChargeStrategySearchDto req)
        {
            GridDataResponse res = new GridDataResponse();
            res.Data = this.chargeStrategyRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete
            && t.SchoolId == req.SchoolId && t.LevelId == req.LevelId && t.MajorId == req.MajorId,
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new ChargeStrategyDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    InstitutionCharge = t.InstitutionCharge,
                    CenterCharge = t.CenterCharge
                }).ToList();
            res.Count = records;
            return res;
        }
    }
}
