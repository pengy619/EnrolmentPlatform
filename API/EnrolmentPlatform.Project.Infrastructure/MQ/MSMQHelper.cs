using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.MQ
{
    public class MSMQHelper
    {
        /// <summary>
        /// 通过Create方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static bool Createqueue(string queuePath, string lable = "msmq")
        {
            bool result = true;
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    //MessageQueue mq = MessageQueue.Create(queuePath);
                    using (MessageQueue mq = MessageQueue.Create(queuePath, true))
                    {
                        mq.Label = lable;
                    }
                }
            }
            catch (MessageQueueException)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        public static bool SendMessage<T>(string queuePath, T body, MessagePriority priority = MessagePriority.Normal)
        {
            bool result = true;
            try
            {
                //连接到本地的队列
                MessageQueue myQueue = new MessageQueue(queuePath);

                Message myMessage = new Message();
                myMessage.Body = body;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                myMessage.Priority = priority;


                MessageQueueTransaction transcation = new MessageQueueTransaction();

                transcation.Begin();

                //发送消息到队列中
                myQueue.Send(myMessage, transcation);

                transcation.Commit();


            }
            catch (ArgumentException )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 连接消息队列并从队列中接收消息
        /// </summary>
        public static T ReceiveMessage<T>(string queuePath)
        {
            T result = default(T);
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(queuePath);
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            try
            {
                MessageQueueTransaction myTransaction = new MessageQueueTransaction();
                //启动事务
                myTransaction.Begin();
                //从队列中接收消息
                Message myMessage = myQueue.Receive();
                result = (T)myMessage.Body; //获取消息的内容
                myTransaction.Commit();
            }
            catch (MessageQueueException)
            {

            }
            catch (InvalidCastException)
            {

            }
            return result;
        }

        /// <summary>
        /// 清空指定队列的消息
        /// </summary>
        public static void ClearMessage(string queuePath)
        {
            MessageQueue myQueue = new MessageQueue(queuePath);
            myQueue.Purge();
        }
        /// <summary>
        ///删除队列
        /// </summary>
        public static void DeleteMessage(string queuePath)
        {
            MessageQueue.Delete(queuePath);
        }


        /// <summary>
        /// 连接队列并获取队列的全部消息
        /// </summary>
        public static List<T> GetAllMessage<T>(string queuePath)
        {
            List<T> lst = new List<T>();
            //连接到本地队列
            MessageQueue myQueue = new MessageQueue(queuePath);
            Message[] message = myQueue.GetAllMessages();
            XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            for (int i = 0; i < message.Length; i++)
            {
                message[i].Formatter = formatter;
                lst.Add((T)message[i].Body);
            }
            return lst;
        }


    }
}
