using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public static class NumberGenerator
    {
        //private static long _ticks = DateTime.Now.Ticks;
        //private static object _object = new object();
        /// <summary>
        /// 账号 交易流水号 唯一
        /// </summary>
        /// <returns></returns>
        public static string GenerateTranscationNo()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 生成结算单 唯一
        /// </summary>
        /// <returns></returns>
        public static string GenerateSettlementNo()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 生成合拼单据  唯一
        /// </summary>
        /// <returns></returns>
        public static string GenerateBillMergeNo()
        {
            return DateTime.Now.ToString("yymmddhhmmsss") + GenerateRandomCode(3);
        }
        /// <summary>
        /// 生成单据 唯一
        /// </summary>
        /// <returns></returns>
        public static string GenerateBillNo()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 生成退换货单号 唯一
        /// </summary>
        /// <returns></returns>
        public static string GenerateRefundGoodNo()
        {
            return DateTime.Now.ToString("HHmmssfff") + GenerateRandomCode(3);
        }


        /// <summary>
        /// 生成订单编号 唯一
        /// </summary>
        /// <returns>订单编号</returns>
        public static string GenerateOrderNo(int productClassify)
        {

            string _orderNo = string.Empty;
            Random ran = new Random();
            switch (productClassify)
            {
                case 1://门票
                    _orderNo = DateTime.Now.ToString("HHmmssfff") + GenerateRandomCode(3);
                    break;
                case 2://农产品
                    _orderNo = DateTime.Now.ToString("HHmmssfff") + GenerateRandomCode(3);
                    break;
                case 3://餐饮
                    _orderNo = DateTime.Now.ToString("HHmmssfff") + GenerateRandomCode(3);
                    break;
                default:
                    break;
            }
            return _orderNo;
        }
        /// <summary>
        /// 生成统一取票码
        /// </summary>
        /// <returns></returns>
        public static string GenerateCheckCode()
        {
            return GetUniqueKey();
        }
        /// <summary>
        ///生成子取票码 唯一
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateTicketToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        ///生成项目核销编码 唯一
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateVerificationNo()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
    
        private static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

    }
}
