using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_ExamService : IT_ExamService, IInterceptorLogic
    {
        private IT_ExamRepository examRepository;
        private IT_ExamInfoRepository examInfoRepository;
        private IT_OrderRepository orderRepository;
        private IT_EnterpriseRepository enterpriseRepository;
        protected IDbContextFactory _dbContextFactory;

        public T_ExamService()
        {
            this.examRepository = DIContainer.Resolve<IT_ExamRepository>();
            this.examInfoRepository = DIContainer.Resolve<IT_ExamInfoRepository>();
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
            this.enterpriseRepository = DIContainer.Resolve<IT_EnterpriseRepository>();
            this._dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public GridDataResponse GetPagedList(ExamSearchDto req)
        {
            var noName = string.IsNullOrWhiteSpace(req.Name);
            GridDataResponse res = new GridDataResponse();
            var query = from a in examRepository.LoadEntities(o => !o.IsDelete)
                        join b in enterpriseRepository.LoadEntities(o => !o.IsDelete) on a.LearningCenterId equals b.Id
                        where (noName || a.Name.Contains(req.Name))
                        && (req.LearningCenterId.HasValue ? a.LearningCenterId == req.LearningCenterId.Value : true)
                        select new ExamDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            CreatorTime = a.CreatorTime,
                            LearningCenter = b.EnterpriseName
                        };
            res.Count = query.Count();
            res.Data = query.OrderByDescending(o => o.CreatorTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
            return res;
        }

        /// <summary>
        /// 新增考试
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg Add(AddExamDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            //根据学生姓名和学号验证数据有效性（订单表存在且已录取）
            int reCount = 0;
            var studentList = this.orderRepository.GetStudentList(new OrderListReqDto
            {
                Page = 1,
                Limit = int.MaxValue,
                Status = OrderStatusEnum.Join
            }, ref reCount);
            var invalidList = new List<string>();
            var exam = new T_Exam
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LearningCenterId = dto.LearningCenterId
            };
            var examInfoList = new List<T_ExamInfo>();
            dto.ExamList.ForEach(t =>
            {
                var student = studentList.FirstOrDefault(s => s.StudentName == t.StudentName && s.XueHao == t.StudentNo
                && s.BatchName == t.BatchName && s.LevelName == t.LevelName && s.MajorName == t.MajorName);
                if (student != null)
                {
                    var examInfo = new T_ExamInfo
                    {
                        Id = Guid.NewGuid(),
                        ExamId = exam.Id,
                        StudentId = student.OrderId,
                        ChannelId = student.FromChannelId ?? Guid.Empty,
                        StudentName = t.StudentName,
                        StudentNo = t.StudentNo,
                        BatchName = t.BatchName,
                        LevelName = t.LevelName,
                        MajorName = t.MajorName,
                        UserName = t.UserName,
                        ExamPlace = t.ExamPlace,
                        MailAddress = t.MailAddress,
                        ReturnAddress = t.ReturnAddress,
                        IDCardNo = t.IDCardNo,
                        ExamSubject = t.ExamSubject,
                        Remark = t.Remark
                    };
                    examInfoList.Add(examInfo);
                }
                else
                {
                    invalidList.Add(string.Format("姓名：{0}，身份证号：{1}", t.StudentName, t.IDCardNo));
                }
            });
            if (invalidList != null && invalidList.Any())
            {
                resultMsg.IsSuccess = false;
                resultMsg.Info = "存在无法匹配的记录，请确认以下学生数据正确且已录取：<br>" + string.Join("<br>", invalidList);
                return resultMsg;
            }

            DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection;
            conn.Open();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    resultMsg.IsSuccess = examRepository.AddEntity(exam, Domain.EFContext.E_DbClassify.Write, "新增考试", true, exam.Id.ToString()) > 0;

                    if (resultMsg.IsSuccess)
                    {
                        resultMsg.IsSuccess = examInfoRepository.AddEntities(examInfoList) > 0;
                    }

                    if (resultMsg.IsSuccess)
                    {
                        tran.Commit();
                        resultMsg.Data = exam.Id;
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    resultMsg.IsSuccess = false;
                    resultMsg.Info = ex.Message;
                }
            }
            return resultMsg;
        }

        /// <summary>
        /// 获取考试名单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public GridDataResponse GetExamList(ExamListSearchDto req)
        {
            GridDataResponse res = new GridDataResponse();
            res.Data = this.examInfoRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete && t.ExamId == req.ExamId && (!req.ChannelId.HasValue || t.ChannelId == req.ChannelId),
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new ExamInfoDto
                {
                    StudentName = t.StudentName,
                    StudentNo = t.StudentNo,
                    BatchName = t.BatchName,
                    LevelName = t.LevelName,
                    MajorName = t.MajorName,
                    UserName = t.UserName,
                    ExamPlace = t.ExamPlace,
                    MailAddress = t.MailAddress,
                    ReturnAddress = t.ReturnAddress,
                    IDCardNo = t.IDCardNo,
                    ExamSubject = t.ExamSubject,
                    Remark = t.Remark
                }).ToList();
            res.Count = records;
            return res;
        }

        /// <summary>
        /// 回填考试名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ResultMsg Fill(FillExamInfoDto dto)
        {
            ResultMsg resultMsg = new ResultMsg();
            //校验数据有效性（考试名单表中存在）
            var examList = this.examInfoRepository.LoadEntities(t => !t.IsDelete && t.ExamId == dto.ExamId && t.ChannelId == dto.ChannelId).ToList();
            var invalidList = new List<string>();
            var updateList = new List<T_ExamInfo>();
            dto.ExamList.ForEach(t =>
            {
                var item = examList.FirstOrDefault(s => s.StudentName == t.StudentName && s.StudentNo == t.StudentNo);
                if (item != null)
                {
                    item.MailAddress = t.MailAddress;
                    item.ReturnAddress = t.ReturnAddress;
                    item.LastModifyTime = DateTime.Now;
                    item.LastModifyUserId = dto.UserId;
                    updateList.Add(item);
                }
                else
                {
                    invalidList.Add(string.Format("姓名：{0}，身份证号：{1}", t.StudentName, t.IDCardNo));
                }
            });
            if (invalidList != null && invalidList.Any())
            {
                resultMsg.IsSuccess = false;
                resultMsg.Info = "存在无法匹配的记录，请确认以下学生在本次考试名单中：<br>" + string.Join("<br>", invalidList);
                return resultMsg;
            }

            DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection;
            conn.Open();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    resultMsg.IsSuccess = examInfoRepository.UpdateEntities(updateList) > 0;

                    if (resultMsg.IsSuccess)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    resultMsg.IsSuccess = false;
                    resultMsg.Info = ex.Message;
                }
            }
            return resultMsg;
        }
    }
}
