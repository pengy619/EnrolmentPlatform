/*******************************************************************************
 * Author: SPF
 * Description: Md5加密
*********************************************************************************/
using System.Text;
using System.Security.Cryptography;
namespace EnrolmentPlatform.Project.Infrastructure
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5
    {
        public static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
