using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EnrolmentPlatform.Project.Infrastructure.EnumHelper
{
    /// <summary>
    /// 枚举泛型操作
    /// </summary>
    public static class EnumDescriptionHelper
    {
        /// <summary>
        /// 枚举Descripition缓存
        /// </summary>
        public static ConcurrentDictionary<string, string> EnumDescriptionCacheData = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 得到Flags特性的枚举的集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<Enum> GetEnumValuesFromFlagsEnum(Enum value)
        {
            List<Enum> values = Enum.GetValues(value.GetType()).Cast<Enum>().ToList();
            List<Enum> res = new List<Enum>();
            foreach (var itemValue in values)
            {
                if ((value.GetHashCode() & itemValue.GetHashCode()) != 0)
                    res.Add(itemValue);
            }
            return res;
        }


        /// <summary>  
        /// 获取枚举变量值的 Description 属性  
        /// </summary>  
        /// <param name="obj">枚举变量</param>  
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>  
        public static string GetDescription(Enum obj)
        {
            string description = string.Empty;
            string key = string.Format("EnumDescription_{0}_{1}", obj.GetType(), Convert.ToInt64(obj));

            if (EnumDescriptionCacheData.ContainsKey(key))
            {
                return EnumDescriptionCacheData[key];
            }


            Type _enumType = obj.GetType();
            DescriptionAttribute dna = null;
            FieldInfo fi = null;
            var fields = _enumType.GetCustomAttributesData();

            if (!fields.Where(i => i.Constructor.DeclaringType.Name == "FlagsAttribute").Any())
            {
                string name = Enum.GetName(_enumType, obj);
                if (name==null)
                {
                    return null;
                }
                fi = _enumType.GetField(name);
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
                return null;
            }

            GetEnumValuesFromFlagsEnum(obj).ToList().ForEach(i =>
            {
                fi = _enumType.GetField(Enum.GetName(_enumType, i));
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    description += dna.Description + ",";
            });

            var result = description.EndsWith(",")
                ? description.Remove(description.LastIndexOf(','))
                : description;
            EnumDescriptionCacheData.TryAdd(key, result);
            return result;

        }


        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        public static string GetFieldText<T>(object enumValue) where T : struct
        {
            try
            {
                if (enumValue == null)
                    return "";
                T typ = (T)Enum.Parse(typeof(T), enumValue.ToString(), true);
                FieldInfo field = typ.GetType().GetField(typ.ToString());
                DescriptionAttribute[] attributes =
                      (DescriptionAttribute[])field.GetCustomAttributes(
                      typeof(DescriptionAttribute), false);
                if (attributes == null)
                {
                    return "";
                }
                return (attributes.Length > 0) ? attributes[0].Description : typ.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把枚举转换为键值对集合
        /// </summary>
        public static Dictionary<string, string> EnumToDictionary<T>() where T : struct
        {
            var enumType = typeof(T);
            Dictionary<string, string> enumDic = new Dictionary<string, string>();
            Array enumValues = Enum.GetValues(enumType);
            foreach (Enum enumValue in enumValues)
            {
                string key = enumValue.ToString();
                string value = GetFieldText<T>(enumValue);
                enumDic.Add(key, value);
            }
            return enumDic;
        }

        /// <summary>
        /// 获取枚举Description属性
        /// </summary>
        public static Dictionary<T, DescriptionAttribute> GetDescriptionAttributeList<T>(Enum language = null) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("参数必须是枚举！");
            }
            Dictionary<T, DescriptionAttribute> ret = new Dictionary<T, DescriptionAttribute>();

            Array array = typeof(T).GetEnumValues();
            foreach (object t in array)
            {
                DescriptionAttribute att = GetDescriptionAttribute(t as Enum);
                if (att != null)
                    ret.Add((T)t, att);
            }

            return ret;
        }

        /// <summary>
        /// 获取枚举的DescripitionArribute
        /// </summary>
        public static DescriptionAttribute GetDescriptionAttribute(Enum e)
        {
            if (e == null)
            {
                return null;
            }
            DescriptionAttribute ret = null;
            FieldInfo fi = e.GetType().GetField(e.ToString().Trim());
            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0) ret = attributes[0];
            }
            return ret;
        }

        /// <summary>
        /// 获取枚举所有项的标题,其文本是由应用在枚举值上的DescriptionAttribute设定
        /// </summary>
        public static Dictionary<TKey, string> GetItemValueList<T, TKey>(bool isAll = false) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("参数必须是枚举！");
            }
            Dictionary<TKey, string> ret = new Dictionary<TKey, string>();

            var titles = GetDescriptionAttributeList<T>();
            foreach (var t in titles)
            {
                if (!isAll && (t.Key.ToString() == "None"))
                    continue;

                if (t.Key.ToString() == "None" && isAll)
                {
                    ret.Add((TKey)(object)t.Key, string.IsNullOrWhiteSpace(t.Value.Description) ? "全部" : t.Value.Description);
                }
                else
                {
                    if (!string.IsNullOrEmpty(t.Value.Description))
                        ret.Add((TKey)(object)t.Key, t.Value.Description);
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取枚举所有项的标题（包含NONE）,其文本是由应用在枚举值上的DescriptionAttribute设定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Dictionary<TKey, string> GetAllItemValueList<T, TKey>(Enum language = null) where T : struct
        {
            return GetItemValueList<T, TKey>(true);
        }

        /// <summary>
        /// 获取枚举值集合
        /// </summary>
        public static List<T> GetItemKeyList<T>(Enum language = null) where T : struct
        {
            List<T> list = new List<T>();
            Array array = typeof(T).GetEnumValues();
            foreach (object t in array)
            {
                list.Add((T)t);
            }
            return list;
        }

        /// <summary>
        /// 根据枚举项的描述信息返回。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="desc">枚举标书。</param>
        /// <returns>枚举项的描述信息。</returns>
        public static Enum GetValueByDesc(Type type, string desc)
        {
            Enum value = null;
            if (type.IsEnum)
            {
                foreach (Enum t in Enum.GetValues(type))
                {
                    if (desc.Equals(GetDescription(t)) == true)
                    {
                        //value = (Convert.ToInt32(t)).ToString();
                        value = t;
                    }
                }
            }
            return value;
        }

    }
}
