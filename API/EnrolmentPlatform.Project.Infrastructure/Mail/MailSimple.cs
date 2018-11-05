using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Mail
{
    public class MailSimple
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get; set; }
        /// <summary>
        /// 发送者昵称如：土驴网
        /// </summary>
        public string mailFromNick { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string mailTo { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }
        /// <summary>
        /// 正文文件读取
        /// </summary>
        public string mailBodyFilePath { get; set; }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailFromPwd { get; set; } 
        public void Send()
        {
            //简单邮件传输协议类
            SmtpClient client = new SmtpClient();
            client.Host = "smtp." + mailFrom.Split('@')[1].ToString();//邮件服务器
            client.Port = 25;//smtp主机上的端口号,默认是25.
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//邮件发送方式:通过网络发送到SMTP服务器
            client.Credentials = new NetworkCredential(mailFrom, mailFromPwd);//凭证,发件人登录邮箱的用户名和密码

            //电子邮件信息类
            MailAddress fromAddress = new MailAddress(mailFrom, mailFromNick);
            MailAddress toAddress = new MailAddress(mailTo, mailTo.Split('@')[0].ToString());
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);//创建一个电子邮件类
            mailMessage.Subject = mailSubject;
            if (!string.IsNullOrWhiteSpace(mailBodyFilePath))
            {
                //string mailBodyFilePath = Server.MapPath("/mailJihuo.html");//邮件的内容可以是一个html文本.
                StreamReader read = new StreamReader(mailBodyFilePath, Encoding.GetEncoding("GB2312"));
                string mailBody = read.ReadToEnd();
                read.Close();
                mailMessage.Body = mailBody;//可为html格式文本
                mailMessage.IsBodyHtml = true;//邮件内容是否为html格式
            }
            else
            {
                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = false;//邮件内容是否为html格式
            }
            mailMessage.SubjectEncoding = Encoding.UTF8;//邮件主题编码
            mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");//邮件内容编码
            mailMessage.Priority = MailPriority.High; 
            try
            {
                client.Send(mailMessage);//发送邮件
                //client.SendAsync(mailMessage, "ojb");异步方法发送邮件,不会阻塞线程.
            }
            catch (Exception)
            {
            }
        }
    }
}
