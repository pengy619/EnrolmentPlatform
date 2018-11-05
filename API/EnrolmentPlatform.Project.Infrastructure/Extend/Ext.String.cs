using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Extend
{
    public static partial class Ext
    {
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="sText">原始字符串</param>
        /// <param name="iLength">截取长度</param>
        /// <returns>新的字符串</returns>
        public static string CutString(this string sText, int iLength)
        {
            if (sText == null || sText == "") return sText;
            if (iLength < 1) return sText;
            byte[] b = System.Text.Encoding.Default.GetBytes(sText);
            double n = 0.0;
            int m = 0;
            bool l0 = false, l1 = false, l2 = false;
            for (int i = 0; i < b.Length; i++)
            {
                l0 = ((int)b[i] > 128);
                if (l0) i++;
                n += (l0 ? 1.0 : 0.5);
                if (n > iLength)
                {
                    string strOut = (l2 ? sText.Substring(0, m - 1) : sText.Substring(0, m - 2));
                    if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(strOut) + 2 > iLength * 2)
                        strOut = strOut.Substring(0, strOut.Length - 1);
                    return strOut + "…";
                }
                m++;
                l2 = l1;
                l1 = l0;
            }
            return sText;
        }
        /// <summary>
        /// 过滤特殊字符
        /// 如果字符串为空，直接返回。
        /// </summary>
        /// <param name="str">需要过滤的字符串</param>
        /// <returns>过滤好的字符串</returns>
        public static string FilterSpecial(this string str)
        {
            if (str == "")
            {
                return str;
            }
            else
            {
                str = str.Replace("'", "");
                str = str.Replace("<", "");
                str = str.Replace(">", "");
                str = str.Replace("%", "");
                str = str.Replace("'delete", "");
                str = str.Replace("''", "");
                str = str.Replace("\"\"", "");
                str = str.Replace(",", "");
                str = str.Replace(".", "");
                str = str.Replace(">=", "");
                str = str.Replace("=<", "");
                str = str.Replace("-", "");
                str = str.Replace("_", "");
                str = str.Replace(";", "");
                str = str.Replace("||", "");
                str = str.Replace("[", "");
                str = str.Replace("]", "");
                str = str.Replace("&", "");
                str = str.Replace("#", "");
                str = str.Replace("/", "");
                str = str.Replace("-", "");
                str = str.Replace("|", "");
                str = str.Replace("?", "");
                str = str.Replace(">?", "");
                str = str.Replace("?<", "");
                str = str.Replace(" ", "");
                str = str.Replace("[", "【");
                str = str.Replace("]", "】");
                return str;
            }
        }

        /// <summary>
        /// 转换成html格式
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ReplaceHTML(this string Input)
        {

            if (!string.IsNullOrEmpty(Input))
            {
                string input = Input;
                input = Regex.Replace(input, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"-->", "", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"<!--.*", "", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                input = Regex.Replace(input, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                input.Replace("<", "");
                input.Replace(">", "");
                input.Replace("\r\n", "");
                input = Regex.Replace(input.Trim(), "\\s+", "");
                return input;
            }
            return Input;
        }

        /// <summary>
        /// 取得HTML中所有图片。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(this string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Value;

            return sUrlList;
        }
        /// <summary>
        /// 得到图片路径
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <param name="isThumbnail">去压缩图</param>
        /// <param name="pictrueThumbnail">压缩类型1：产品 资源压缩图 2：Banner压缩图 3：详情压缩图 </param>
        /// <returns></returns>
        public static string GetPictruePath(this string path, bool isThumbnail = true, int pictrueThumbnail = 3)
        {
            string _pathResult = path;

            if (!string.IsNullOrEmpty(path) && !path.Contains("http"))
            {
                _pathResult = ConfigurationManager.AppSettings["AdminDoMain"] + path;
                if (isThumbnail)
                {
                    string _thumbnail = "_thumbnail";
                    switch (pictrueThumbnail)
                    {
                        case 1:
                            _thumbnail = "_thumbnail"; //列表压缩图
                            break;
                        case 2:
                            _thumbnail = "_bannerthumbnail"; //banner压缩图
                            break;
                        case 3:
                            _thumbnail = "_detailthumbnail"; //详情压缩图
                            break;
                        case 4:
                            _thumbnail = "_avtivethumbnail"; //首页活动压缩图
                            break;
                    }
                    try
                    {
                        string _route = path.Substring(0, path.LastIndexOf('.'));
                        string _ext = path.Substring(path.LastIndexOf('.'));
                        _pathResult = ConfigurationManager.AppSettings["AdminDoMain"] + _route + _thumbnail + _ext;
                    }
                    catch (Exception)
                    {
                        _pathResult = ConfigurationManager.AppSettings["AdminDoMain"] + path;
                    }

                }
                else
                {
                    _pathResult =ConfigurationManager.AppSettings["AdminDoMain"] + path;
                }
            }
            return _pathResult;
        }

        public static string ReplaceImgSrc(this string html)
        {
            if (html != null)
            {
                string _oldpath = "/Content/website/js/kindeditor-4.1.10/attached/image/";
                string _newpath =ConfigurationManager.AppSettings["AdminDoMain"] + "/Content/website/js/kindeditor-4.1.10/attached/image/";
                return html.Replace(_oldpath, _newpath);
            }
            else
            {
                return string.Empty;
            }

        }
        public static bool IsOnline(this string localhost)
        {
            bool _result = false;
            if (!string.IsNullOrEmpty(localhost))
            {
                if (localhost.ToLower().Contains(ConfigurationManager.AppSettings["Online"]))
                {
                    _result = true;
                }

            }
            return _result;
        }

        public static string PhoneChangeStar(this string phone)
        {
            string _result = phone;
            if (!string.IsNullOrEmpty(phone))
            {
                _result = phone.Substring(0, 4) + "*****" + phone.Substring(phone.Length - 3);
            }
            return _result;
        }


        public static string IDCardChangeStar(this string idcard)
        {
            string _result = idcard;
            if (!string.IsNullOrEmpty(idcard))
            {
                if (idcard.Length > 7)
                {
                    _result = idcard.Substring(0, 4) + "*****" + idcard.Substring(idcard.Length - 2);
                }
                else
                {
                    _result = idcard.Substring(0, 1) + "*****" + idcard.Substring(idcard.Length - 1);
                }
            }
            return _result;
        }
        public static string EmailChangeStar(this string email)
        {
            string _result = email;
            if (!string.IsNullOrEmpty(email))
            {
                if (email.IndexOf("@") > 0)
                {
                    string _subStr = email.Substring(0, email.IndexOf("@"));
                    if (_subStr.Length >= 3)
                    {
                        _subStr = _subStr.Substring(0, 3);
                    }
                    _result = _subStr + "*****" + email.Substring(email.IndexOf("@"));
                }
                else
                {
                    _result = "*****" + email;
                }
            }
            return _result;
        }
        public static string GuidOutChar(this Guid value)
        {
            string _result = value.ToString().Replace("-", "");
            return _result;
        }
        public static string GuidAddChar(this string value)
        {
            string _result =
                value.ToString().Substring(0, 8) + "-" +
                value.ToString().Substring(8, 4) + "-" +
                value.ToString().Substring(12, 4) + "-" +
                 value.ToString().Substring(16, 4) + "-" +
                value.ToString().Substring(20);
            return _result;
        }
        private static int rep = 0;

        /// 生成随机数字字符串
        /// 
        /// 待生成的位数
        /// 生成的数字字符串
        public static string GenerateCheckCodeNum(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }
        /// 生成随机字母字符串(数字字母混和)
        /// 
        /// 待生成的位数
        /// 生成的字母字符串
        public static string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks +rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }


    }
}
