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
        protected IDbContextFactory _dbContextFactory;

        public T_ExamService()
        {
            this.examRepository = DIContainer.Resolve<IT_ExamRepository>();
            this.examInfoRepository = DIContainer.Resolve<IT_ExamInfoRepository>();
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
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
            res.Data = this.examRepository.LoadPageEntitiesOrderByField(t => !t.IsDelete && (noName || t.Name.Contains(req.Name)),
               req.Field,
               req.Limit,
               req.Page,
               out int records,
               (req.Sort ?? "desc").ToLower().Equals("asc")
                ).Select(t => new ExamDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    CreatorTime = t.CreatorTime
                }).ToList();
            res.Count = records;
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
            var studentList = this.orderRepository.LoadEntities(t => !t.IsDelete && t.Status == (int)OrderStatusEnum.Join).ToList();
            var invalidList = new List<string>();
            dto.ExamList.ForEach(t =>
            {
                var student = studentList.FirstOrDefault(s => s.StudentName == t.StudentName && s.StudentNo == t.StudentNo);
                if (student != null)
                {
                    t.StudentId = student.Id;
                    t.ChannelId = student.FromChannelId ?? Guid.Empty;
                }
                else
                {
                    invalidList.Add(string.Format("姓名：{0}，学号：{1}", t.StudentName, t.StudentNo));
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
                    var exam = new T_Exam
                    {
                        Id = Guid.NewGuid(),
                        Name = dto.Name
                    };
                    resultMsg.IsSuccess = examRepository.AddEntity(exam, Domain.EFContext.E_DbClassify.Write, "新增考试", true, exam.Id.ToString()) > 0;

                    if (resultMsg.IsSuccess)
                    {
                        var examList = new List<T_ExamInfo>();
                        foreach (var item in dto.ExamList)
                        {
                            var examInfo = new T_ExamInfo
                            {
                                Id = Guid.NewGuid(),
                                ExamId = exam.Id,
                                StudentId = item.StudentId,
                                ChannelId = item.ChannelId,
                                StudentName = item.StudentName,
                                StudentNo = item.StudentNo,
                                BatchName = item.BatchName,
                                LevelName = item.LevelName,
                                MajorName = item.MajorName,
                                UserName = item.UserName,
                                ExamPlace = item.ExamPlace,
                                MailAddress = item.MailAddress,
                                ReturnAddress = item.ReturnAddress
                            };
                            examList.Add(examInfo);
                        }
                        resultMsg.IsSuccess = examInfoRepository.AddEntities(examList) > 0;
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
                    ReturnAddress = t.ReturnAddress
                }).ToList();
            res.Count = records;
            return res;
        }
    }
}
