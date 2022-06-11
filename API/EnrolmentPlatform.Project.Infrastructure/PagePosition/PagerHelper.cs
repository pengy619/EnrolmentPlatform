using System;

namespace EnrolmentPlatform.Project.Infrastructure
{
    /// <summary>
    /// 用于SQL分页处理的辅助类
    /// </summary>
    public class PagerHelper
    {
        /// <summary>
        /// 根据总记录数和页大小计算总页数
        /// </summary>
        public static int GetPageCount(int recordCount, int pageSize)
        {
            if (pageSize <= 0) return 0;
            return (int)Math.Ceiling(recordCount * 1.0M / pageSize);
        }

        /// <summary>
        /// 包装获取指定页数据的sql（注意：sql的select列表中必须已包含cRowNumber列）
        /// </summary>
        /// <param name="sql">带cRowNumber字段的sql语句</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页记录数</param>
        public static string GetSqlOfPage(string sql, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) pageIndex = 1;
            return string.Format("select * from (" + sql + ") T where cRowNumber between {0} and {1}"
                , (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
        }

        /// <summary>
        /// 包装获取指定页数据的sql（自动添加cRowNumber列）
        /// </summary>
        /// <param name="sql">不带cRowNumber字段的sql语句</param>
        /// <param name="orderby">不带order by前缀的排序字段列表（不能是数字序号，且不要加表别名前缀）</param>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        public static string GetSqlOfPage(string sql, string orderby, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) pageIndex = 1;
            return string.Format(@"
select * from (
    select T.*, ROW_NUMBER() OVER(order by {2}) as cRowNumber
    from (" + sql + ") T" + @"
) T2 where cRowNumber between {0} and {1}"
                , (pageIndex - 1) * pageSize + 1, pageIndex * pageSize
                , orderby);

        }

        /// <summary>
        /// 包装获取结果集行数的sql（注意：sql中不能包含order by子句）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string GetSqlOfRecordCount(string sql)
        {
            return "select count(*) recc from (" + sql + ") T ";
        }
    }
}
