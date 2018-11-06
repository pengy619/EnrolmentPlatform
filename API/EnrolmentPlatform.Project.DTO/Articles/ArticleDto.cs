﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Article;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Articles
{
    /// <summary>
    /// 文章Dto
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public Guid ClassifyId { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public string ClassifyName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布对象
        /// </summary>
        public string PublicObject { get; set; }

        public string PublicObjectStr
        {
            get
            {
                var list = new List<string>();
                if (!string.IsNullOrEmpty(PublicObject))
                {
                    var objectArr = PublicObject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in objectArr)
                    {
                        PublicObjectEnum objectEnum = (PublicObjectEnum)Enum.Parse(typeof(PublicObjectEnum), item);
                        list.Add(EnumDescriptionHelper.GetDescription(objectEnum));
                    }
                }
                return string.Join(",", list);
            }
        }

        /// <summary>
        /// 发布者Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 发布者账号
        /// </summary>
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((ArticleStatusEnum)Status);
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }

    }

    /// <summary>
    /// B2C 获取资讯 详情 Dto
    /// </summary>
    public class B2CActicleDetailDto
    {
        /// <summary>
        /// 当前
        /// </summary>
        public ArticleDto Article { get; set; }

        /// <summary>
        /// 上一篇
        /// </summary>
        public ArticleDto PreviousArticle { get; set; }
        /// <summary>
        /// 下一篇
        /// </summary>
        public ArticleDto NextArticle { get; set; }
    }

    /// <summary>
    /// 文章查询DTO
    /// </summary>
    public class ArticleSearchDto : GridDataRequest
    {
        /// <summary>
        /// 栏目
        /// </summary>
        public Guid? ClassifyId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public DateTime? StartDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DateStr))
                {
                    return (DateStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0]).ToDate();
                }
                return null;
            }
        }
        public DateTime? EndDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DateStr))
                {
                    return (DateStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1]).ToDate().AddDays(1);
                }
                return null;
            }
        }
        public string DateStr { get; set; }
    }
}