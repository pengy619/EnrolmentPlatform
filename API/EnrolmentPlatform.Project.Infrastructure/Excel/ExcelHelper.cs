/*******************************************************************************
 * Author: SPF
 * Description: ExcelHelper
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace EnrolmentPlatform.Project.Infrastructure
{

    public class ExcelHelper
    {
        //private ExcelHelper()
        //{
        //}

        //public static ExcelHelper Instance = new ExcelHelper();

        public void CreateExcel(string fileName, string sheetName, string[] title, int[] columnWidths, List<ArrayList> dataList)
        {
            IWorkbook workBook = new HSSFWorkbook();
            ISheet sheet = workBook.CreateSheet(sheetName);
            CreateTitleRow(workBook, sheet, title, columnWidths);
            CreateDataRows(workBook, sheet, dataList);
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"/" + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workBook.Write(fs);
            fs.Flush();
            fs.Close();
        }


        private void CreateTitleRow(IWorkbook workbook, ISheet sheet, string[] titles, int[] columnWidths)
        {
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            font.FontName = "宋体";
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.WrapText = true;
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.SetFont(font);
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.TopBorderColor = IndexedColors.Black.Index;
            cellStyle.RightBorderColor = IndexedColors.Black.Index;
            cellStyle.BottomBorderColor = IndexedColors.Black.Index;
            cellStyle.LeftBorderColor = IndexedColors.Black.Index;
            //cellStyle.FillBackgroundColor = IndexedColors.Grey25Percent.Index;
            //cellStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.SetFont(font);
            IRow titleRow = sheet.CreateRow(GetNewRowNum(sheet));
            titleRow.HeightInPoints = 30;
            for (int i = 0; i < titles.Length; i++)
            {
                ICell cell = titleRow.CreateCell(i);
                cell.SetCellValue(titles[i]);
                cell.CellStyle = cellStyle;
            }
            for (int i = 0; i < columnWidths.Length; i++)
            {
                sheet.SetColumnWidth(i, columnWidths[i] * 256);
            }
        }


        private void CreateDataRows(IWorkbook workbook, ISheet sheet, List<ArrayList> dataList)
        {
            IFont font = workbook.CreateFont();
            font.FontName = "宋体";
            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.TopBorderColor = IndexedColors.Black.Index;
            cellStyle.RightBorderColor = IndexedColors.Black.Index;
            cellStyle.BottomBorderColor = IndexedColors.Black.Index;
            cellStyle.LeftBorderColor = IndexedColors.Black.Index;
            cellStyle.WrapText = true;
            cellStyle.SetFont(font);
            foreach (ArrayList arry in dataList)
            {
                IRow row = sheet.CreateRow(GetNewRowNum(sheet));
                row.HeightInPoints = 20;
                for (int j = 0; j < arry.Count; j++)
                {
                    CreateCell(row, cellStyle, arry[j].ToString(), j);
                }
            }
        }
        private int GetNewRowNum(ISheet sheet)
        {
            return sheet.PhysicalNumberOfRows;
        }




        //创建列CreatCell
        private void CreateCell(IRow row, ICellStyle cellSytle, string cellValue, int columnIndex)
        {
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(cellValue);
            cell.CellStyle = cellSytle;
        }
    }
}
