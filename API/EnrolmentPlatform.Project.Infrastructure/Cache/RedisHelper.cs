using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public static class RedisHelper
    {

        private static readonly string _connectionStr = ConfigurationManager.AppSettings["RedisContext"];
        private static readonly string _redisAdmin = ConfigurationManager.AppSettings["RedisAdmin"];
        private static readonly int _pageSize = ConfigurationManager.AppSettings["RedisPageSize"].ToInt();
        private static object _locker = new Object();

        private static ConnectionMultiplexer _instance = null;
        /// <summary>
        /// 使用一个静态属性来返回已连接的实例，如下列中所示。这样，一旦 ConnectionMultiplexer 断开连接，便可以初始化新的连接实例。
        /// </summary>
        private static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = ConnectionMultiplexer.Connect(string.Format("{0},{1}", _connectionStr, _redisAdmin));
                        }
                    }
                }
                //注册如下事件
                _instance.ConnectionFailed += MuxerConnectionFailed;
                _instance.ConnectionRestored += MuxerConnectionRestored;
                _instance.ErrorMessage += MuxerErrorMessage;
                _instance.ConfigurationChanged += MuxerConfigurationChanged;
                _instance.HashSlotMoved += MuxerHashSlotMoved;
                _instance.InternalError += MuxerInternalError;
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDatabase GetDatabase()
        {
            return Instance.GetDatabase();
        }
        public static IServer GetServer()
        {
            return Instance.GetServer(_connectionStr);
        }
        /// <summary>
        /// 这里的 MergeKey 用来拼接 Key 的前缀，具体不同的业务模块使用不同的前缀。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string MergeKey(string key)
        {
            //BaseSystemInfo.SystemCode +
            return "EnrolmentPlatform_" + key;
        }
        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            key = MergeKey(key);
            return Deserialize<T>(GetDatabase().StringGet(key));
        }
        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            key = MergeKey(key);
            return Deserialize<object>(GetDatabase().StringGet(key));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            key = MergeKey(key);
            GetDatabase().StringSet(key, Serialize(value));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        public static void Set(string key, object value, DateTime expireTime)
        {
            key = MergeKey(key);
            TimeSpan span = DateTime.Now.AddMinutes(30) - DateTime.Now;//默认为30分钟
            if (expireTime > DateTime.Now)
            {
                span = expireTime - DateTime.Now;
            }
            TimeSpan sec = TimeSpan.Parse(span.Days + "." + span.Hours + ":" + span.Minutes);
            GetDatabase().StringSet(key, Serialize(value), sec);
        }


        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        public static void Set(string key, object value, TimeSpan expireTime)
        {
            key = MergeKey(key);
            GetDatabase().StringSet(key, Serialize(value), expireTime);
        }
        /// <summary>
        /// 判断在缓存中是否存在该key的缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            key = MergeKey(key);
            return GetDatabase().KeyExists(key);  //可直接调用
        }

        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            key = MergeKey(key);
            return GetDatabase().KeyDelete(key);
        }
        /// <summary>
        /// 批量移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Removes(RedisKey[] keys, CommandFlags commandFlags = CommandFlags.None)
        {
            return GetDatabase().KeyDelete(keys, commandFlags) > 0;
        }
        /// <summary>
        /// 批量移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Removes(string key)
        {
            RedisKey[] keys = GetRedisKeys(key, _pageSize).ToArray(); //模糊匹配 效率低
            return GetDatabase().KeyDelete(keys) > 0;
        }

        public static IEnumerable<RedisKey> GetRedisKeys(string key, int pageSize)
        {
            string pattern = string.Format("*{0}", key);
            return GetServer().Keys(GetDatabase().Database, pattern, pageSize, CommandFlags.None);
        }
        public static void FlushDatabase()
        {
            GetServer().FlushDatabase(GetDatabase().Database);
        }
        /// <summary>
        /// 异步设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static async Task SetAsync(string key, object value)
        {
            key = MergeKey(key);
            await GetDatabase().StringSetAsync(key, Serialize(value));
        }

        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<object> GetAsync(string key)
        {
            key = MergeKey(key);
            object value = await GetDatabase().StringGetAsync(key);
            return value;
        }

        /// <summary>
        /// 实现递增
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long Increment(string key)
        {
            key = MergeKey(key);
            //三种命令模式
            //Sync,同步模式会直接阻塞调用者，但是显然不会阻塞其他线程。
            //Async,异步模式直接走的是Task模型。
            //Fire - and - Forget,就是发送命令，然后完全不关心最终什么时候完成命令操作。
            //即发即弃：通过配置 CommandFlags 来实现即发即弃功能，在该实例中该方法会立即返回，如果是string则返回null 如果是int则返回0.这个操作将会继续在后台运行，一个典型的用法页面计数器的实现：
            return GetDatabase().StringIncrement(key, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 实现递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long Decrement(string key, string value)
        {
            key = MergeKey(key);
            return GetDatabase().HashDecrement(key, value, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {

        }
        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {

        }
        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {

        }
        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
        }
        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {

        }
        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {

        }

        //场景不一样，选择的模式便会不一样，大家可以按照自己系统架构情况合理选择长连接还是Lazy。
        //建立连接后，通过调用ConnectionMultiplexer.GetDatabase 方法返回对 Redis Cache 数据库的引用。从 GetDatabase 方法返回的对象是一个轻量级直通对象，不需要进行存储。

        /// <summary>
        /// 使用的是Lazy，在真正需要连接时创建连接。
        /// 延迟加载技术
        /// 微软azure中的配置 连接模板
        /// </summary>
        //private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    //var options = ConfigurationOptions.Parse(constr);
        //    ////options.ClientName = GetAppName(); // only known at runtime
        //    //options.AllowAdmin = true;
        //    //return ConnectionMultiplexer.Connect(options);
        //    ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(Coonstr);
        //    muxer.ConnectionFailed += MuxerConnectionFailed;
        //    muxer.ConnectionRestored += MuxerConnectionRestored;
        //    muxer.ErrorMessage += MuxerErrorMessage;
        //    muxer.ConfigurationChanged += MuxerConfigurationChanged;
        //    muxer.HashSlotMoved += MuxerHashSlotMoved;
        //    muxer.InternalError += MuxerInternalError;
        //    return muxer;
        //});


        #region  当作消息代理中间件使用 一般使用更专业的消息队列来处理这种业务场景
        /// <summary>
        /// 当作消息代理中间件使用
        /// 消息组建中,重要的概念便是生产者,消费者,消息中间件。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static long Publish(string channel, string message)
        {
            ISubscriber sub = Instance.GetSubscriber();
            //return sub.Publish("messages", "hello");
            return sub.Publish(channel, message);
        }

        /// <summary>
        /// 在消费者端得到该消息并输出
        /// </summary>
        /// <param name="channelFrom"></param>
        /// <returns></returns>
        public static void Subscribe(string channelFrom)
        {
            ISubscriber sub = Instance.GetSubscriber();
            sub.Subscribe(channelFrom, (channel, message) =>
            {
                Console.WriteLine((string)message);
            });
        }
        #endregion

        /// <summary>
        /// GetServer方法会接收一个EndPoint类或者一个唯一标识一台服务器的键值对
        /// 有时候需要为单个服务器指定特定的命令
        /// 使用IServer可以使用所有的shell命令，比如：
        /// DateTime lastSave = server.LastSave();
        /// ClientInfo[] clients = server.ClientList();
        /// 如果报错在连接字符串后加 ,allowAdmin=true;
        /// </summary>
        /// <returns></returns>
        public static IServer GetServer(string host, int port)
        {
            IServer server = Instance.GetServer(host, port);
            return server;
        }

        /// <summary>
        /// 获取全部终结点
        /// </summary>
        /// <returns></returns>
        public static EndPoint[] GetEndPoints()
        {
            EndPoint[] endpoints = Instance.GetEndPoints();
            return endpoints;
        }

    }
}