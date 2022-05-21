using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public enum EExcelHeadColer
    {
        /// <summary>
        /// 白色(默认)
        /// </summary>
        WHITE = 0,
        /// <summary>
        /// 红色
        /// </summary>
        RED = 1,
        /// <summary>
        /// 蓝色
        /// </summary>
        BLUE = 2,
        /// <summary>
        /// 黄色
        /// </summary>
        YELLOW = 3,
        /// <summary>
        /// 粉红色
        /// </summary>
        PINK = 4,
    }

    /// <summary>
    /// 导出Excel功能的列对象
    /// </summary>
    public class ExcelColumn
    {
        /// <summary>
        /// 列对象
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        /// <param name="excelColumnName">Excel表头列名</param>
        /// <param name="Colors">背景颜色</param>
        /// <param name="DropBox">下拉框选项</param>
        public ExcelColumn(string sourceFieldName, string excelColumnName, EExcelHeadColer Colors, List<string> DropBox)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = excelColumnName;
            this._Color = Colors;
            this._DropBox = DropBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        /// <param name="excelColumnName">Excel表头列名</param>
        /// <param name="Colors">背景颜色</param>
        /// <param name="dropBoxDictionary">下拉框选项</param>
        public ExcelColumn(string sourceFieldName, string excelColumnName, EExcelHeadColer Colors, List<KeyValuePair<Guid, string>> dropBoxDictionary)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = excelColumnName;
            this._Color = Colors;
            this.DropBox = dropBoxDictionary.Select(m => m.Value).Distinct().ToList();
            this.DropBoxDictionary = new Dictionary<string, string>();
            foreach (var item in dropBoxDictionary)
            {
                DropBoxDictionary[item.Value] = item.Key.ToString();
            }
            DropBoxDictionary[""] = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        /// <param name="excelColumnName">Excel表头列名</param>
        /// <param name="Colors">背景颜色</param>
        /// <param name="dropBoxDictionary">下拉框选项</param>
        public ExcelColumn(string sourceFieldName, string excelColumnName, EExcelHeadColer Colors, Dictionary<string, string> dropBoxDictionary)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = excelColumnName;
            this._Color = Colors;
            this.DropBox = dropBoxDictionary.Select(m => m.Value).Distinct().ToList();
            this.DropBoxDictionary = dropBoxDictionary.ToDictionary(m => m.Value, m => m.Key);
            this.DropBoxDictionary[""] = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        /// <param name="excelColumnName">Excel表头列名</param>
        /// <param name="Colors">背景颜色</param>
        public ExcelColumn(string sourceFieldName, string excelColumnName, EExcelHeadColer Colors)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = excelColumnName;
            this._Color = Colors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        /// <param name="excelColumnName">Excel表头列名</param>
        /// <param name="Colors">背景颜色</param>
        public ExcelColumn(string sourceFieldName, string excelColumnName)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = excelColumnName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFieldName">DataTable的列名</param>
        public ExcelColumn(string sourceFieldName)
        {
            this._TbColumnName = sourceFieldName;
            this._ExcelColumnName = sourceFieldName;
        }

        string _TbColumnName;
        /// <summary>
        /// DataTable列名
        /// </summary>
        public string TbColumnName
        {
            get { return _TbColumnName; }
            set { _TbColumnName = value; }
        }
        string _ExcelColumnName;
        /// <summary>
        /// 将要导出的Excel列表头名
        /// </summary>
        public string ExcelColumnName
        {

            get { if (_ExcelColumnName == "") { return _TbColumnName; } else { return _ExcelColumnName; } }
            set { _ExcelColumnName = value; }
        }
        EExcelHeadColer _Color;
        /// <summary>
        /// Excel列表头的背景颜色 RED:红色;BLUE:蓝色;YELLOW：黄色;PINK:粉红色；
        /// </summary>
        public EExcelHeadColer Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        List<string> _DropBox = null;
        /// <summary>
        /// 列内容的下拉框选项内容
        /// </summary>
        public List<string> DropBox
        {
            get { return _DropBox; }
            set { _DropBox = value; }
        }

        public Type Type { get; set; }

        //public List<IRule> Rules { get; set; }
        /// <summary>
        /// 1，多选
        /// 2，校区/价格 类似这种 多选
        /// 3，单选 但不限制必须从下拉框选
        /// </summary>
        public int MultipleType { get; set; }
        public Dictionary<string, string> DropBoxDictionary { get; set; }
    }
}
