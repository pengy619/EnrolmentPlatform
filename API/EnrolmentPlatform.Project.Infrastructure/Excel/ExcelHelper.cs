/*******************************************************************************
 * Author: SPF
 * Description: ExcelHelper
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public class ExcelHelper
    {
        HSSFWorkbook wb;//定义一个excel对象//创建HSSFWorkbook对象 （可理解为创建空excel工作簿）
        IWorkbook iwb;
        ISheet sheet;//定义一个工作页对象
        ICellStyle headerStyle; //定义一个单元格样式对象（表格单元格样式）
        int sheetCount = 0;   //记录创建的工作页个数（第0个是导出内容的工作页）

        /// <summary>
        /// 导出Excel公共类
        /// </summary>
        /// <param name="table">DataTable结果集</param>
        /// <param name="filename">设置导出的文件名和工作页的名称</param>
        /// <param name="dic">参数组List(列对象)</param>
        public void ExportToExcel(DataTable table, string filename, List<ExcelColumn> dic)
        {
            //导出后缀为xls的文件（最大65536）
            //MemoryStream ms = DataTableMS(table, filename, dic, contentCellStyle);
            //this.ResponseToExcel(ms, filename);
            //导出后缀为xlsx的文件（最大1048576）
            MemoryStream ms = DataTableMSx(table, filename, dic);
            this.ResponseToExcelx(ms, filename);
        }

        /// <summary>
        /// 导出Excel公共类, 在导入模板中添加填写说明
        /// </summary>
        /// <param name="table">DataTable结果集</param>
        /// <param name="filename">设置导出的文件名和工作页的名称</param>
        /// <param name="dic">参数组List(列对象)</param>
        /// <param name="explain">填表说明</param>
        public void ExportToExcel(DataTable table, string filename, List<ExcelColumn> dic, string explain, short explainHeight)
        {
            MemoryStream ms = DataTableMS(table, filename, dic, explain, explainHeight);
            this.ResponseToExcel(ms, filename);
        }

        private void ResponseToExcel(MemoryStream ms, string filename)
        {
            //设置流的输出名称
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\""
                + filename + ".xls\"; filename*=utf-8''"
                + HttpUtility.UrlEncode(filename, Encoding.UTF8) + ".xls");
            //设置流的输出格式
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentType = "application/x-xls";

            HttpContext.Current.Response.Charset = "UTF-8";
            //输出流

            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// xlsx
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="filename"></param>
        private void ResponseToExcelx(MemoryStream ms, string filename)
        {
            HttpResponse baseResponse = HttpContext.Current.Response;
            baseResponse.Clear();
            baseResponse.ClearHeaders();
            baseResponse.Charset = "UTF-8";
            baseResponse.ContentEncoding = System.Text.Encoding.UTF8;
            baseResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //设置导出文件名
            baseResponse.AppendHeader("Content-Disposition", "attachment;filename=\""
                + filename + ".xlsx\"; filename*=utf-8''"
                + HttpUtility.UrlEncode(filename, Encoding.UTF8) + ".xlsx");
            byte[] buffer = ms.ToArray();
            baseResponse.AddHeader("Content-Length", buffer.Length.ToString());
            baseResponse.BinaryWrite(buffer);
            baseResponse.Flush();
            baseResponse.End();
        }

        /// <summary>
        /// 将datatable数据转化为流（MemoryStream）
        /// </summary>
        /// <param name="table"></param>
        /// <param name="filename"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public MemoryStream DataTableMSx(DataTable table, string filename, List<ExcelColumn> dic)
        {
            iwb = new XSSFWorkbook();
            //创建HSSFSheet对象 （可理解为创建名为‘sheet1’的工作表）
            sheet = iwb.CreateSheet(filename);
            #region 表头样式
            //表头样式 (定义一个样式对象)
            headerStyle = iwb.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            headerStyle.Alignment = HorizontalAlignment.Center;//（居中）
            headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（左）
            headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（下）
            headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（右）
            headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（上）
            #endregion
            #region 字体样式
            //新建一个字体样式对象
            IFont headFont = iwb.CreateFont();
            //设置字体加粗样式
            headFont.Boldweight = short.MaxValue;
            //设置字体大小
            headFont.FontHeightInPoints = 10;
            #endregion
            headerStyle.SetFont(headFont);
            //添加表头行
            IRow headRow = sheet.CreateRow(0);
            int count = 0;//合并列
            //设置表头行的行高
            //dataRow1.Height = 30 * 15;
            try
            {
                #region  填充表头 也就是把列名填充到第0行
                for (int i = 0; i < dic.Count; i++)
                {
                    headRow.CreateCell(i).SetCellValue(dic[i].ExcelColumnName); //将dic里的列名根据dic里的排序写表头
                    Type columnType = table.Columns[dic[i].TbColumnName]?.DataType;
                    if (columnType == typeof(Decimal)
                               || columnType == typeof(Double)
                               || columnType == typeof(Int16)
                               || columnType == typeof(Int32)
                               || columnType == typeof(Int64)
                               || columnType == typeof(Single)
                               || columnType == typeof(UInt16)
                               || columnType == typeof(UInt32)
                               || columnType == typeof(UInt64))
                    {
                        sheet.SetColumnWidth(i, 5 * 256 * 2 + 256);
                    }
                    else if (columnType == typeof(DateTime))
                    {
                        sheet.SetColumnWidth(i, 9 * 256 * 2 + 256);
                    }
                    else
                    {
                        sheet.SetColumnWidth(i, 5 * 256 * 2 + 256);
                    }


                    EExcelHeadColer color = (EExcelHeadColer)dic[i].Color;//获取设置的颜色
                    ICellStyle newHeadStyle = iwb.CreateCellStyle();//创建新样式
                    newHeadStyle.CloneStyleFrom(headerStyle);//新样式继承原表头样式
                    switch (color)
                    {
                        case EExcelHeadColer.RED:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.BLUE:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.YELLOW:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.PINK:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        default:
                            //其他的用默认颜色（不做设置）
                            break;
                    }
                    headRow.GetCell(i).CellStyle = newHeadStyle; //设置表头样式（新的表头样式带背景颜色）
                    if (dic[i].DropBox != null) //判断当前列是否需要添加下拉框选项并设置不能自主输入（为空和null就不设置）
                    {
                        if (dic[i].DropBox.Count > 0 && dic[i].DropBox.Count <= 10)//下拉框的选项在10个或10个以内用直接写入的方式实现
                        {
                            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, i, i);//四个参数 1、从第几行；2、到excel的1048576行；3、第几列；4、到第几列（计数都是从0开始）
                            //DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(dic[i].DropBox.ToArray());//设置下拉框内容
                            //IDataValidation dataValidate = new HSSFDataValidation(regions, constraint);//定义单元格对象
                            XSSFDataValidationHelper dvHelper = new XSSFDataValidationHelper((XSSFSheet)sheet);
                            IDataValidationConstraint constraint = dvHelper.CreateExplicitListConstraint(dic[i].DropBox.ToArray()); //将容器转化为下拉框格式（根据容器名称）
                            XSSFDataValidation dataValidate = (XSSFDataValidation)dvHelper.CreateValidation(constraint, regions);
                            sheet.AddValidationData(dataValidate);//在第二页加入一个下拉框单元格
                        }
                        else if (dic[i].DropBox.Count > 10) //当下拉框的选项超过10个,则用引用的方式实现
                        {
                            sheetCount++;  //累计创建的用于引用的页面
                            List<string> dropBoxValue = dic[i].DropBox; //将下拉选项的值转存
                            ISheet sheet2 = iwb.CreateSheet("whsheet" + i); //创建新的工作页
                            for (int j = 0; j < dropBoxValue.Count; j++)//循环将下拉选项存储到工作页
                            {
                                sheet2.CreateRow(j).CreateCell(0).SetCellValue(dropBoxValue[j]); //存储的位置是第一列（从第0行开始）
                            }
                            IName range = iwb.CreateName();//创建引用容器
                            range.RefersToFormula = string.Format("{0}!$A$1:$A${1}", "whsheet" + i, dropBoxValue.Count); //引用的范围（参数1：工作页名称；参数2：到第多少行为止）
                            range.NameName = "whsheet" + i; //容器的名称
                            CellRangeAddressList regions = new CellRangeAddressList(1, 65536, i, i);//设置需要设置下拉框的范围

                            //IDataValidationConstraint constraint = DVConstraint.CreateFormulaListConstraint("whsheet" + i); //将容器转化为下拉框格式（根据容器名称）
                            //IDataValidation dataValidate = new XSSFDataValidation(regions, constraint);  //将下拉框和设置的范围相绑定

                            XSSFDataValidationHelper helper = new XSSFDataValidationHelper((XSSFSheet)sheet);//获得一个数据验证Helper
                            IDataValidationConstraint constraint = helper.CreateFormulaListConstraint("whsheet" + i);
                            IDataValidation dataValidate = helper.CreateValidation(constraint, regions);

                            sheet.AddValidationData(dataValidate);//将绑定好的数据设置到具体的工作页
                            iwb.SetSheetHidden(sheetCount, 0); //隐藏创建的引用页面
                        }
                    }

                }
                #endregion
                #region 填充内容 从第一行开始填充

                ICellStyle dateCellStyle = iwb.CreateCellStyle(); //日期样式
                IDataFormat format = iwb.CreateDataFormat();
                dateCellStyle.DataFormat = format.GetFormat("yyyy-MM-dd");

                ICellStyle dateTimeCellStyle = iwb.CreateCellStyle();  //日期时间样式
                dateTimeCellStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

                for (int i = 0; i < table.Rows.Count; i++) //根据table的记录条数来循环加载数据
                {

                    IRow newRow = sheet.CreateRow(i + 1);//添加行
                    for (int j = 0; j < dic.Count; j++)  //根据传入的参数，按参数的顺序将table里对应列的数据加载到单元格中
                    {
                        DataColumn column = table.Columns[dic[j].TbColumnName];
                        object cellValue = table.Rows[i][column];
                        if (cellValue == DBNull.Value)
                        {
                            newRow.CreateCell(j).SetCellValue("");
                        }
                        else
                        {
                            Type columnType = column.DataType;
                            if (columnType == typeof(Decimal)
                                || columnType == typeof(Double)
                                || columnType == typeof(Int16)
                                || columnType == typeof(Int32)
                                || columnType == typeof(Int64)
                                || columnType == typeof(Single)
                                || columnType == typeof(UInt16)
                                || columnType == typeof(UInt32)
                                || columnType == typeof(UInt64))
                            {
                                newRow.CreateCell(j).SetCellValue(Convert.ToDouble(cellValue));
                            }
                            else if (columnType == typeof(DateTime))
                            {
                                //newRow.CreateCell(j).CellStyle..SetCellValue((DateTime)cellValue);
                                ICell cell = newRow.CreateCell(j);
                                cell.SetCellValue((DateTime)cellValue);
                                if (((DateTime)cellValue).TimeOfDay.TotalSeconds > 0)
                                {
                                    cell.CellStyle = dateTimeCellStyle;
                                }
                                else
                                {
                                    cell.CellStyle = dateCellStyle;
                                }
                            }
                            else
                            {
                                var newCell = newRow.CreateCell(j);
                                newCell.SetCellValue(cellValue.ToString());
                            }
                        }
                        count = j;
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //创建内存流
            MemoryStream ms = new MemoryStream();
            iwb.Write(ms);//将准备好的HSSFWorkbook对象写入内存流中
            ms.Close();
            ms.Dispose();
            return ms;
        }

        /// <summary>
        /// 将datatable数据转化为流（MemoryStream）
        /// </summary>
        /// <param name="table">DataTable结果集</param>
        /// <param name="filename">设置导出的文件名和工作页的名称</param>
        /// <param name="dic">参数组List(列对象)</param>
        /// <param name="explain">导出说明</param>
        /// <param name="explainHeight">说明行的高度</param>
        /// <returns></returns>
        public MemoryStream DataTableMS(DataTable table, string filename, List<ExcelColumn> dic, string explain, short explainHeight)
        {
            wb = new HSSFWorkbook();
            //创建HSSFSheet对象 （可理解为创建名为‘sheet1’的工作表）
            sheet = wb.CreateSheet(filename);
            #region 表头样式
            //表头样式 (定义一个样式对象)
            headerStyle = wb.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            headerStyle.Alignment = HorizontalAlignment.Center;//（居中）
            headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（左）
            headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（下）
            headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（右）
            headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;//边框样式（上）
            #endregion
            #region 字体样式
            //新建一个字体样式对象
            IFont headFont = wb.CreateFont();
            //设置字体加粗样式
            headFont.Boldweight = short.MaxValue;
            //设置字体大小
            headFont.FontHeightInPoints = 10;
            #endregion

            #region 设置某一列为文本样式
            ICellStyle contentStyle = wb.CreateCellStyle();//创建一个需要格式化单元格
            IDataFormat formatString = wb.CreateDataFormat();//赋值的格式化对象
            contentStyle.DataFormat = formatString.GetFormat("@");//为单元格赋值
            #endregion




            //添加说明行
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dic.Count - 1));
            IRow explainRow = sheet.CreateRow(0);
            explainRow.Height = explainHeight;
            explainRow.CreateCell(0).CellStyle.WrapText = true;
            explainRow.CreateCell(0).SetCellValue(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(explain)));


            //添加表头行
            IRow headRow = sheet.CreateRow(1);

            //设置表头行的行高
            //dataRow1.Height = 30 * 15;           
            try
            {
                #region  填充表头 也就是把列名填充到第0行
                for (int i = 0; i < dic.Count; i++)
                {
                    ICell icell = headRow.CreateCell(i);//表头行
                    string excelColumnName = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(dic[i].ExcelColumnName));
                    icell.SetCellValue(excelColumnName); //将dic里的列名根据dic里的排序写表头

                    Type columnType = table.Columns[dic[i].TbColumnName].DataType;
                    if (columnType == typeof(Decimal)
                               || columnType == typeof(Double)
                               || columnType == typeof(Int16)
                               || columnType == typeof(Int32)
                               || columnType == typeof(Int64)
                               || columnType == typeof(Single)
                               || columnType == typeof(UInt16)
                               || columnType == typeof(UInt32)
                               || columnType == typeof(UInt64))
                    {
                        sheet.SetColumnWidth(i, 5 * 256 * 2 + 256);
                    }
                    else if (columnType == typeof(DateTime))
                    {
                        sheet.SetColumnWidth(i, 9 * 256 * 2 + 256);
                    }
                    else
                    {
                        sheet.SetColumnWidth(i, 5 * 256 * 2 + 256);
                    }

                    if (dic[i].TbColumnName == "身份证号")
                    {
                        sheet.SetDefaultColumnStyle(i, contentStyle);//处理身份证号这列为文本
                        sheet.SetColumnWidth(i, 9 * 256 * 2 + 256);
                    }


                    EExcelHeadColer color = (EExcelHeadColer)dic[i].Color;//获取设置的颜色
                    ICellStyle newHeadStyle = wb.CreateCellStyle();//创建新样式
                    newHeadStyle.CloneStyleFrom(headerStyle);//新样式继承原表头样式
                    switch (color)
                    {
                        case EExcelHeadColer.RED:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.BLUE:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.YELLOW:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        case EExcelHeadColer.PINK:
                            newHeadStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index;//指定背景色
                            newHeadStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;//指定背景图案
                            break;
                        default:
                            //其他的用默认颜色（不做设置）
                            break;
                    }
                    headRow.GetCell(i).CellStyle = newHeadStyle; //设置表头样式（新的表头样式带背景颜色）
                    if (dic[i].DropBox != null) //判断当前列是否需要添加下拉框选项并设置不能自主输入（为空和null就不设置）
                    {
                        if (dic[i].DropBox.Count > 0 && dic[i].DropBox.Count <= 10)//下拉框的选项在10个或10个以内用直接写入的方式实现
                        {
                            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, i, i);//四个参数 1、从第几行；2、到excel的65535行；3、第几列；4、到第几列（计数都是从0开始）
                            DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(dic[i].DropBox.ToArray());//设置下拉框内容
                            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);//定义单元格对象
                            if (dic[i].MultipleType == 1 || dic[i].MultipleType == 2 || dic[i].MultipleType == 3)
                            {
                                dataValidate.ShowErrorBox = false;
                            }
                            sheet.AddValidationData(dataValidate);//在第二页加入一个下拉框单元格
                        }
                        else if (dic[i].DropBox.Count > 10) //当下拉框的选项超过10个,则用引用的方式实现
                        {
                            sheetCount++;  //累计创建的用于引用的页面
                            List<string> dropBoxValue = dic[i].DropBox; //将下拉选项的值转存
                            ISheet sheet2 = wb.CreateSheet("whsheet" + i); //创建新的工作页
                            for (int j = 0; j < dropBoxValue.Count; j++)//循环将下拉选项存储到工作页
                            {
                                sheet2.CreateRow(j).CreateCell(0).SetCellValue(dropBoxValue[j]); //存储的位置是第一列（从第0行开始）
                            }
                            IName range = wb.CreateName();//创建引用容器
                            range.RefersToFormula = string.Format("{0}!$A$1:$A${1}", "whsheet" + i, dropBoxValue.Count); //引用的范围（参数1：工作页名称；参数2：到第多少行为止）
                            range.NameName = "whsheet" + i; //容器的名称
                            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, i, i);//设置需要设置下拉框的范围
                            DVConstraint constraint = DVConstraint.CreateFormulaListConstraint("whsheet" + i); //将容器转化为下拉框格式（根据容器名称）
                            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);  //将下拉框和设置的范围相绑定
                            if (dic[i].MultipleType == 1 || dic[i].MultipleType == 2 || dic[i].MultipleType == 3)
                            {
                                dataValidate.ShowErrorBox = false;
                            }
                            sheet.AddValidationData(dataValidate);//将绑定好的数据设置到具体的工作页
                            wb.SetSheetHidden(sheetCount, true); //隐藏创建的引用页面
                        }
                    }

                }
                #endregion
                #region 填充内容 从第一行开始填充

                ICellStyle dateCellStyle = wb.CreateCellStyle(); //日期样式
                IDataFormat format = wb.CreateDataFormat();
                dateCellStyle.DataFormat = format.GetFormat("yyyy-MM-dd");

                ICellStyle dateTimeCellStyle = wb.CreateCellStyle();  //日期时间样式
                dateTimeCellStyle.DataFormat = format.GetFormat("yyyy-MM-dd HH:mm:ss");

                for (int i = 0; i < table.Rows.Count; i++) //根据table的记录条数来循环加载数据
                {
                    var contentRow = 1;
                    if (!string.IsNullOrEmpty(explain))
                        contentRow++;
                    IRow newRow = sheet.CreateRow(i + contentRow);//添加行
                    for (int j = 0; j < dic.Count; j++)  //根据传入的参数，按参数的顺序将table里对应列的数据加载到单元格中
                    {
                        DataColumn column = table.Columns[dic[j].TbColumnName];
                        object cellValue = table.Rows[i][column];
                        if (cellValue == DBNull.Value)
                        {
                            newRow.CreateCell(j).SetCellValue("");
                        }
                        else
                        {
                            Type columnType = column.DataType;
                            if (columnType == typeof(Decimal)
                                || columnType == typeof(Double)
                                || columnType == typeof(Int16)
                                || columnType == typeof(Int32)
                                || columnType == typeof(Int64)
                                || columnType == typeof(Single)
                                || columnType == typeof(UInt16)
                                || columnType == typeof(UInt32)
                                || columnType == typeof(UInt64))
                            {
                                newRow.CreateCell(j).SetCellValue(Convert.ToDouble(cellValue));
                            }
                            else if (columnType == typeof(DateTime))
                            {
                                //newRow.CreateCell(j).CellStyle..SetCellValue((DateTime)cellValue);
                                ICell cell = newRow.CreateCell(j);
                                cell.SetCellValue((DateTime)cellValue);
                                if (((DateTime)cellValue).TimeOfDay.TotalSeconds > 0)
                                {
                                    cell.CellStyle = dateTimeCellStyle;
                                }
                                else
                                {
                                    cell.CellStyle = dateCellStyle;
                                }
                            }
                            else
                            {
                                newRow.CreateCell(j).SetCellValue(cellValue.ToString());
                            }
                        }
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //创建内存流
            MemoryStream ms = new MemoryStream();
            wb.Write(ms);//将准备好的HSSFWorkbook对象写入内存流中
            ms.Close();
            ms.Dispose();
            return ms;
        }

        /// <summary>
        /// 把excel文件转换成table
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="value">文件后缀名</param>
        /// <param name="headerRowNum">表头开始行数</param>
        /// <returns></returns>
        public DataTable ExcelValue(Stream fileStream, string value, int headerRowNum = 0)
        {
            IWorkbook workbook;
            try
            {
                if (value == "xls")
                {
                    workbook = new HSSFWorkbook(fileStream);
                }
                else if (value == "xlsx")
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataTable table = new DataTable();//创建存储数据的table
            try
            {
                ISheet sheet = workbook.GetSheetAt(0); //获取第一个工作页
                table.TableName = workbook.GetSheetName(0); //获取工作页名称
                IRow headerRow = sheet.GetRow(headerRowNum);//获取工作页第一行（读取表头）
                int cellCount = headerRow.LastCellNum; //获取第一行有多少列（表头列）

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)  //循环表头列
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).ToString()); //获取表头列名
                    table.Columns.Add(column); //将获取的表头列名设置为table的列名
                }
                int rowCount = sheet.LastRowNum;//获取记录行数
                int firstRowNum = sheet.FirstRowNum + 1 + headerRowNum;//取值开始行数

                for (int i = firstRowNum; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);//读取文件行第i行
                    DataRow dataRow = table.NewRow();//创建table行

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType == CellType.Numeric)
                            {
                                if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                {
                                    dataRow[j] = row.GetCell(j).DateCellValue;
                                }
                                else if (!row.GetCell(j).ToString().Contains("%"))//其他数字类型
                                {
                                    dataRow[j] = row.GetCell(j).NumericCellValue;
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j);//将获取的文件单元格内容赋值到table的单元格
                                }
                            }
                            else
                            {
                                dataRow[j] = row.GetCell(j);//将获取的文件单元格内容赋值到table的单元格
                            }
                        }
                        else
                        {
                            dataRow[j] = "";
                        }
                    }
                    table.Rows.Add(dataRow);//将行添加到table
                }
                #region 去掉DataTable里的空行
                List<DataRow> removelist = new List<DataRow>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    bool bol = true;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (!String.IsNullOrEmpty(table.Rows[i][j].ToString().Trim()))
                        {
                            bol = false;
                        }
                    }
                    if (bol)
                    {
                        removelist.Add(table.Rows[i]);
                    }

                }
                for (int i = 0; i < removelist.Count; i++)
                {
                    table.Rows.Remove(removelist[i]);
                }
                #endregion
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
