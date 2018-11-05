using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;

namespace EnrolmentPlatform.Project.Infrastructure
{
    public static class AutoMapperExt
    {
        #region 由AutoMapper创建目标对象
        /// <summary>
        ///  类型映射-由AutoMapper创建目标对象
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this object obj)
        {
            if (obj == null) return default(TDestination);
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<TDestination>(obj);
        }

        /// <summary>
        /// 集合列表类型映射-由AutoMapper创建目标对象
        /// </summary>
        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IEnumerable source)
        {
            if (source == null) return new List<TDestination>();
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<IEnumerable<TDestination>>(source);
        }
        #endregion

        #region 把源对象中的属性值合并/覆盖到目标对象
        /// <summary>
        /// 类型映射-把源对象中的属性值合并/覆盖到目标对象
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// 集合列表类型映射-把源对象中的属性值合并/覆盖到目标对象
        /// </summary>
        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source, IEnumerable<TDestination> destination)
        {
            return Mapper.Map(source, destination);
        }
        #endregion

        #region DataReader映射
        /// <summary>
        /// DataReader映射
        /// </summary>
        public static IEnumerable<T> DataReaderMapTo<T>(this System.Data.IDataReader reader)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<System.Data.IDataReader, IEnumerable<T>>());
            return Mapper.Map<System.Data.IDataReader, IEnumerable<T>>(reader);
        }
        #endregion
    }
}
