using Nelibur.ObjectMapper.Bindings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sdf.Mapper
{
    public class Bind1Model<TSource, TTarget>
    {
        public Expression<Func<TSource, object>> Source { get; set; }
        public Expression<Func<TTarget, object>> Target { get; set; }
    }
    public class Bind2Model<TSource, TTarget>
    {
        public Type TargetType { get; set; }
        public Expression<Func<TTarget, object>> Target { get; set; }
    }
    public class Ignore<TSource, TTarget>
    {
        public Expression<Func<TSource, object>> Expression { get; set; }
    }
    public class MapperConfig<TSource, TTarget> : IMapperConfig<TSource, TTarget>
    {
        //private Dictionary<>
        public Action<IBindingConfig<TSource, TTarget>> CreateTinyMapperConfig()
        {
            Action<IBindingConfig<TSource, TTarget>> configAction = config =>
            {
                foreach (var item in bind1List)
                {
                    config.Bind(item.Source, item.Target);
                }
                foreach (var item in bind2List)
                {
                    config.Bind(item.Target, item.TargetType);
                }
                foreach (var item in ignoreList)
                {
                    config.Ignore(item.Expression);
                }
            };
            return configAction;
        }
        public Action<TSource, TTarget> GetFilter()
        {
            return filter;
        }
        public Func<TSource, TTarget> ConstructUsing()
        {
            return constructUsing;
        }
        private List<Bind1Model<TSource, TTarget>> bind1List = new List<Bind1Model<TSource, TTarget>>();
        private List<Bind2Model<TSource, TTarget>> bind2List = new List<Bind2Model<TSource, TTarget>>();
        private List<Ignore<TSource, TTarget>> ignoreList = new List<Ignore<TSource, TTarget>>();
        //private Action<object, object> filter;
        private Action<TSource, TTarget> filter;
        private Func<TSource, TTarget> constructUsing;
        public void Bind(Expression<Func<TSource, object>> source, Expression<Func<TTarget, object>> target)
        {
            bind1List.Add(new Bind1Model<TSource, TTarget>() { Source = source, Target = target });
        }

        public void Bind(Expression<Func<TTarget, object>> target, Type targetType)
        {
            bind2List.Add(new Bind2Model<TSource, TTarget>() { Target = target, TargetType = targetType });
        }
        //public void BindFilter(Action<object,object> filter)
        //{
        //    this.filter=filter;
        //}
        public void BindFilter(Action<TSource, TTarget> filter)
        {
            this.filter = filter;
        }
        public void UseConstruct(Func<TSource, TTarget> action)
        {
            this.constructUsing = action;
        }
        public void Ignore(Expression<Func<TSource, object>> expression)
        {
            ignoreList.Add(new Ignore<TSource, TTarget>() { Expression = expression });
        }
    }
}
