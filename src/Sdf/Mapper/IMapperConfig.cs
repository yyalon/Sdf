using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sdf.Mapper
{
    public interface IMapperConfig<TSource, TTarget>
    {
        void Bind(Expression<Func<TSource, object>> source, Expression<Func<TTarget, object>> target);
        void Bind(Expression<Func<TTarget, object>> target, Type targetType);
        void Ignore(Expression<Func<TSource, object>> expression);
        //void BindFilter(Action<object, object> filter);
        void BindFilter(Action<TSource, TTarget> filter);
        void UseConstruct(Func<TSource, TTarget> action);
    }
}
