using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Infrastructure.Extend
{
    public static  class Dictionary
    {
        public class EnumComparer<T> : IEqualityComparer<T> where T : struct
        {
            public bool Equals(T first, T second)
            {
                var firstParam = Expression.Parameter(typeof(T), "first");
                var secondParam = Expression.Parameter(typeof(T), "second");
                var equalExpression = Expression.Equal(firstParam, secondParam);

                return Expression.Lambda<Func<T, T, bool>>
                    (equalExpression, new[] { firstParam, secondParam }).
                    Compile().Invoke(first, second);
            }

            public int GetHashCode(T instance)
            {
                var parameter = Expression.Parameter(typeof(T), "instance");
                var convertExpression = Expression.Convert(parameter, typeof(int));

                return Expression.Lambda<Func<T, int>>
                    (convertExpression, new[]{parameter}).
                    Compile().Invoke(instance);
            }
        }
    }
}
