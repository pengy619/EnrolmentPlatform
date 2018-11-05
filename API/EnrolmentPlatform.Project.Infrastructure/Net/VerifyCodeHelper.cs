using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public class VerifyCodeHelper
    {
        private static ValidateCodeType[] validateCodeStyles;
        public static byte[] GenerateVerifyCode(out string code)
        {
            if (validateCodeStyles == null)
                validateCodeStyles = new ValidateCodeType[]{
                    new ValidateCode_Style1(),
                    new ValidateCode_Style2()
                    //new ValidateCode_Style3(),
                    //new ValidateCode_Style4(),
                    //new ValidateCode_Style5(),
                    //new ValidateCode_Style6(),
                    //new ValidateCode_Style7(),
                    //new ValidateCode_Style8(),
                    //new ValidateCode_Style9(),
                    //new ValidateCode_Style10()
                    };

            var randomValidateCodeStyle = validateCodeStyles[DateTime.Now.Second % validateCodeStyles.Length];
            return randomValidateCodeStyle.CreateImage(out code);
        }
    }

    public class VerifyCodeBag
    {
        public string VerifyCode { get; set; }
        public string Guid { get; set; }
        public byte[] Bytes { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class QuickVerifyCodeHelper
    {
        static QuickVerifyCodeHelper()
        {
            preProducePool = new ConcurrentDictionary<string, VerifyCodeBag>();
            verifyCodePool = new ConcurrentDictionary<string, string>();
            preProduceTimer = new Timer();
            preProduceTimer.Interval = preProduceIntervalMinitus * 1000 * 60;
            preProduceTimer.Elapsed += (s, e) =>
            {
                //删除池子里过期的验证码
                ClearExpiredVerifyCode();
                //预生成验证码
                ProduceVerifyCodes();

            };

            preProduceTimer.Start();
        }

        private static void ClearExpiredVerifyCode()
        {
            VerifyCodeBag preVerifyCode = null;
            preProducePool.Values
            .Where(v => v.CreateTime.AddMinutes(verifyCodeExpiredMinitus) < DateTime.Now)
            .Select(v => v.Guid).ToList()
            .ForEach(v => preProducePool.TryRemove(v, out preVerifyCode));

            verifyCodePool.Clear();
        }

        private static VerifyCodeBag ProduceVerifyCode(bool fillPreProducePool = true)
        {
            var bytes = VerifyCodeHelper.GenerateVerifyCode(out string code);
            var guid = Guid.NewGuid().ToString();
            var preVerifyCode = new VerifyCodeBag()
            {
                Guid = guid,
                VerifyCode = code,
                Bytes = bytes,
                CreateTime = DateTime.Now
            };

            if (fillPreProducePool)
                preProducePool.TryAdd(guid, preVerifyCode);

            return preVerifyCode;
        }

        /// <summary>
        /// 初始化静态函数里的定时器
        /// </summary>
        public static void Initialize()
        {
            //不做任何事，将初始化静态函数里的定时器
        }

        /// <summary>
        /// 主动清空验证码池和Memcached里的所有缓存住的数据
        /// </summary>
        public static void Clear()
        {
            preProducePool.Clear();
            verifyCodePool.Clear();
        }

        /// <summary>
        /// 预生成验证码
        /// </summary>
        /// <param name="amount">数量，默认为500个</param>
        public static void ProduceVerifyCodes(int amount = preProduceAmount)
        {
            //池子里还有1/3的可用，就不生成了
            if (preProducePool.Count > amount / 3)
                return;

            for (int i = 0; i < amount; i++)
                ProduceVerifyCode();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static VerifyCodeBag GetVerifyCode()
        {
            VerifyCodeBag verifyCodeBag;

            if (preProducePool.Count == 0)
            {
                //如果池子里没有了，就不走预生成机制了，直接生成
                verifyCodeBag = ProduceVerifyCode(false);
            }
            else
            {
                //池子里还有，取池子里最后一个出来
                var guid = preProducePool.Keys.Last();
                //取出来后删除
                preProducePool.TryRemove(guid, out verifyCodeBag);
            }

            //放到验证码池子里，待用户过来验证
            verifyCodePool.TryAdd(verifyCodeBag.Guid, verifyCodeBag.VerifyCode);

            return verifyCodeBag;
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        public static bool CheckVerifyCode(string verifyCode, string guid)
        {
            //如果本地待验证池子里存在
            if (verifyCodePool.ContainsKey(guid))
            {
                verifyCodePool.TryRemove(guid, out string vc);
                return string.Equals(vc, verifyCode, StringComparison.OrdinalIgnoreCase);
            }
            return false;

        }

        private static int preProduceIntervalMinitus = 5;
        private const int preProduceAmount = 500;
        private static Timer preProduceTimer;
        private static int verifyCodeExpiredMinitus = 60;
        private static ConcurrentDictionary<string, VerifyCodeBag> preProducePool;
        private static ConcurrentDictionary<string, string> verifyCodePool;

    }
}
