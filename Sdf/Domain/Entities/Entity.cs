using Sdf.Fundamentals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sdf.Domain.Entities
{
    [Serializable]
    public abstract class Entity<TPrimaryKey> : Mapper.IUseMapper
    {
        public Entity()
        {
            createTime = DateTime.Now;
        }
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public virtual TPrimaryKey Id { get; set; }

        public abstract bool Equals(Entity<TPrimaryKey> obj);

        public virtual void SetId(TPrimaryKey id)
        {
            this.Id = id;
        }

        private DateTime createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime
        {
            get
            {
                return createTime;
            }
            protected set
            {
                createTime = new DateTime(value.Ticks);
            }
        }
        public virtual void SetCreateTime(DateTime dateTime)
        {
            this.CreateTime = dateTime;
        }
        
        //public IDateTimeProvider DateTimeProvider
        //{
        //    get
        //    {
        //        using (var resolver = Bootstrapper.Instance.IocManager.GetResolver())
        //        {
        //            var dateTimeProvider = resolver.Resolve<IDateTimeProvider>();
        //            return dateTimeProvider;
        //        }
        //    }
        //}
    }
}
