using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.PagePosition
{
    public class PagingHtml
    {  
        #region UI模板
        /* UI模板
          <nav class="text-right pc_paging">
              <ul class="pagination marbottom10 martop10">
                  <li><a href="#">&laquo;</a></li>
                  <li class="active"><a href="#">1</a></li>
                  <li><a href="#">2</a></li>
                  <li><a href="#">3</a></li>
                  <li><a href="#">4</a></li>
                  <li><a href="#">5</a></li>
                  <li><a href="#">&raquo;</a></li>
              </ul>
          </nav>
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
            sb.AppendFormat("<nav class=\"text-right pc_paging\"><ul class=\"pagination marbottom10 martop10\">"); 
            //首页
            sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{0}\">&laquo;</a></li>", ajaxMethod + "(1);");
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
                    sb.AppendFormat("<li><a   class=\"paging_a\" href=\"javascript:void(0);\">{0}</a></li>", i);
                }
                else
                {
                    //如果不是当前页码
                    sb.AppendFormat("<li><a onclick=\"{0};\" href=\"javascript:void(0);\">{1}</a></li>", ajaxMethod + "(" + i.ToString() + ")", i);
                }
            } 
            //最后一页
            sb.AppendFormat("<li><a href=\"javascript:void(0);\" onclick=\"{0};\">&raquo;</a></li>", ajaxMethod + "(" + totalPage.ToString() + ")"); 
            return sb.ToString();
        }
    }
}
