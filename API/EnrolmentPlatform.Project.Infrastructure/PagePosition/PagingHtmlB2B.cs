using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.PagePosition
{
    public class PagingHtmlB2B
    {
        #region UI模板
        /* UI模板
        <div>
				<nav aria-label="Page navigation">
					<ul class="pagination">
					    <li>
					        <a href="#" aria-label="Previous">
					            <span aria-hidden="true">&laquo;</span>
					        </a>
					    </li>
					    <li><a href="#">1</a></li>
					    <li><a href="#">2</a></li>
					    <li><a href="#">3</a></li>
					    <li><a href="#">4</a></li>
					    <li><a href="#">5</a></li>
					    <li>
					        <a href="#" aria-label="Next">
					            <span aria-hidden="true">&raquo;</span>
					        </a>
					    </li>
					</ul>
				</nav>
			</div>
          */
        #endregion
        public static string Render(int recordCount, int pageIndex, string ajaxMethod, int pageSize = 10)
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
            sb.AppendFormat("<div><nav aria-label=\"Page navigation\"><ul class=\"pagination\">");
            ////首页
            //sb.AppendFormat("<li class=\"page_first\" name=\"first\"><a href=\"javascript:void(0);\" onclick=\"{0}\">首页</a></li>", ajaxMethod + "(1);");
            //上一页
            if (pageIndex != 1)
            {
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" aria-label=\"Previous\" onclick=\"{0}\"><span aria-hidden=\"true\">&laquo;</span></a></li>", ajaxMethod + "(" + (pageIndex - 1).ToString() + ");");
            }
            int startPageNum, endPageNum;
            //让当前页居中显示
            if (pageIndex <= 3)
            {
                startPageNum = 1;
                endPageNum = totalPage <= 5 ? totalPage : 5;
            }
            else if (pageIndex > totalPage - 4)
            {
                startPageNum = totalPage - 4;
                if (startPageNum == 0) startPageNum = 1;
                endPageNum = totalPage;
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
                    sb.AppendFormat("<li><a class=\"active\" href=\"javascript:void(0);\">{1}</a></li>", i, i);
                }
                else
                {
                    //如果不是当前页码
                    sb.AppendFormat("<li><a onclick=\"{1};\" href=\"javascript:void(0);\">{0}</a></li>", i.ToString(), ajaxMethod + "(" + i.ToString() + ")");
                }
            }
            //下一页
            if (pageIndex != endPageNum)
            {
                sb.AppendFormat("<li><a href=\"javascript:void(0);\" aria-label=\"Next\" onclick=\"{0}\"><span aria-hidden=\"true\">&raquo;</span></a></li>", ajaxMethod + "(" + (pageIndex + 1).ToString() + ");");
            }
            //最后一页
            //sb.AppendFormat("<li class=\"page_last\" name=\"last\"><a href=\"javascript:void(0);\" onclick=\"{0};\">尾页</a></li>", ajaxMethod + "(" + totalPage.ToString() + ")");
            sb.Append("</ul></div>");
            return sb.ToString();
        }

        public static string RenderForList(int recordCount, int pageIndex, string ajaxMethod, int pageSize = 5)
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
            sb.AppendFormat("<ul class=\"pagination pagination-lg pull-right\">");
            ////首页
            sb.AppendFormat("<li class=\"first-child\"><a href=\"javascript:;\" onclick=\"{0};\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>", ajaxMethod + "(1);");
            int startPageNum, endPageNum;
            //让当前页居中显示
            if (pageIndex <= 3)
            {
                startPageNum = 1;
                endPageNum = totalPage <= 5 ? totalPage : 5;
            }
            else if (pageIndex > totalPage - 4)
            {
                startPageNum = totalPage - 4;
                if (startPageNum == 0) startPageNum = 1;
                endPageNum = totalPage;
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
                    sb.AppendFormat("<li><a href=\"javascript:;\" class=\"active\">{0}</a></li>", i.ToString());
                }
                else
                {
                    //如果不是当前页码
                    sb.AppendFormat("<li><a href=\"javascript:;\" onclick=\"{0};\">{1}</a></li>", ajaxMethod + "(" + i.ToString() + ")", i.ToString());
                }
            } 
            //最后一页
            sb.AppendFormat("<li class=\"last-child\"><a href=\"javascript:;\" onclick=\"{0};\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>", ajaxMethod + "(" + totalPage.ToString() + ")");
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}
