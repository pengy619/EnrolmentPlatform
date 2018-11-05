using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.PagePosition
{
    public class PagingHtmlB2C
    {
        #region UI模板
        /* UI模板
         <div class=\"layui-box layui-laypage layui-laypage-default\">
                        <span class=\"layui-laypage-count\">共 100 条</span>
                        <span class=\"layui-laypage-count\">当前第1/100页</span>
                        <a href=\"javascript:;\" class=\"layui-laypage-prev layui-disabled\">上一页</a>
                        <span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>1</em></span>
                        <a href="javascript:;" data-page="2">2</a>
                        <a href="javascript:;" data-page="3">3</a>
                        <a href="javascript:;" data-page="4">4</a>
                        <a href="javascript:;" data-page="5">5</a>
                        <span class=\"layui-laypage-spr\">…</span>
                        <a href="javascript:;" class="layui-laypage-last" title="尾页" data-page="7">7</a>
                        <a href=\"javascript:;\" class=\"layui-laypage-next\">下一页</a>
                        <a href=\"javascript:;\" class=\"layui-laypage-next layui-disabled\">下一页</a>
                    </div>
          */
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordCount">总记录数</param>
        /// <param name="pageIndex">当前页页数</param>
        /// <param name="ajaxMethod">ajax方法</param>
        /// <param name="pageSize">每页数据个数</param>
        /// <returns></returns>
        public static string Render(int recordCount, int pageIndex, string ajaxMethod, int pageSize = 20)
        {
            if (pageSize < 1)
                pageSize = 1;
            if (pageIndex < 1)
                pageIndex = 1;
            //总页码
            int totalPage = (recordCount % pageSize) == 0 ? recordCount / pageSize : recordCount / pageSize + 1;
            //如果总页码小于1
            if (totalPage <= 1)
                return "";
            //如果当前页码大于总页数  赋值等于总页数
            if (pageIndex > totalPage)
                pageIndex = totalPage;
            //开始拼接分页字符串
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"layui-box layui-laypage layui-laypage-default\">");
            sb.AppendFormat("<span class=\"layui-laypage-count\">共 {0} 条</span>", recordCount);
            sb.AppendFormat("<span class=\"layui-laypage-count\">当前第{0}/{1}页</span>", pageIndex, totalPage);
            //上一页
            if (pageIndex != 1)
            {
                sb.AppendFormat("<a href=\"javascript:;\" class=\"layui-laypage-prev\" onclick=\"{0}\">上一页</a>", ajaxMethod + "(" + (pageIndex - 1).ToString() + ");");
            }
            else
            {
                sb.AppendFormat("<a href=\"javascript:;\" class=\"layui-laypage-prev layui-disabled\">上一页</a>");
            }
            //首页
            if (pageIndex == 1)
            {
                sb.AppendFormat("<span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>1</em></span>");
            }
            else
            {
                sb.AppendFormat("<a href=\"javascript:;\" onclick=\"{0}\">1</a>", ajaxMethod + "(1);");
            }
            //如果当前页码大于5，并且总页码大于7 追加前省略号
            if (pageIndex >= 5 && totalPage > 7)
            {
                sb.AppendFormat("<span class=\"layui-laypage-spr\">…</span>");
            }

            //如果页数大于2
            int startPageNum = 0, endPageNum = 0;
            if (totalPage > 2)
            {
                //让当前页居中显示
                if (pageIndex <= 4)
                {
                    startPageNum = 2;
                    endPageNum = (totalPage - 1) < 7 ? (totalPage - 1) : 6;
                }
                else if (pageIndex > totalPage - 4)
                {
                    startPageNum = totalPage - 5; 
                    startPageNum = startPageNum < 2 ? 2 : startPageNum;
                    endPageNum = totalPage - 1;
                }
                else
                {
                    startPageNum = pageIndex - 2;
                    endPageNum = startPageNum + 4;
                }
                for (int i = startPageNum; i <= endPageNum; i++)
                {
                    if (i == pageIndex)
                    {
                        //如果循环到当前页 
                        sb.AppendFormat("<span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>{0}</em></span>", i);
                    }
                    else
                    {
                        //如果不是当前页码 
                        sb.AppendFormat("<a href=\"javascript:;\" onclick=\"{0};\">{1}</a>", ajaxMethod + "(" + i.ToString() + ")", i.ToString());
                    }
                }
            }

            //追加后省略号
            if (totalPage > 7 && pageIndex < totalPage - 3)
            {
                sb.AppendFormat("<span class=\"layui-laypage-spr\">…</span>");
            }
            //最后一页
            if (pageIndex != totalPage)
            {
                sb.AppendFormat("<a href=\"javascript:;\" onclick=\"{0};\">{1}</a>", ajaxMethod + "(" + totalPage.ToString() + ")", totalPage.ToString());
            }
            else
            {
                sb.AppendFormat("<span class=\"layui-laypage-curr\"><em class=\"layui-laypage-em\"></em><em>{0}</em></span>", totalPage);
            }
            //下一页 
            if (pageIndex != totalPage)
            {
                sb.AppendFormat("<a href=\"javascript:;\" class=\"layui-laypage-next\" onclick=\"{0}\">下一页</a>", ajaxMethod + "(" + (pageIndex + 1).ToString() + ");");
            }
            else
            {
                sb.AppendFormat("<a href=\"javascript:;\" class=\"layui-laypage-next layui-disabled\">下一页</a>");
            }
            sb.Append("</div>");
            return sb.ToString();
        }

    }
}
