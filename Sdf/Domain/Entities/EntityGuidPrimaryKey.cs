using Sdf.IdProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Entities
{
    public abstract class EntityGuidPrimaryKey : Entity<Guid>
    {
        public override Guid Id { get;  set; }
        public override bool Equals(Entity<Guid> obj)
        {
            return this.Id == obj.Id;
        }
    }
    public abstract class EntityGuidPrimaryKeyAuto : Entity<Guid>
    {
        private Guid? id = null;
        public override Guid Id
        {
            get
            {
                if (!id.HasValue)
                {
                    using (var resolver = Bootstrapper.Instance.IocManager.GetResolver())
                    {
                        var guidIdProvider = resolver.Resolve<IGuidIdProvider>();
                        id = guidIdProvider.CreateNew();
                    }
                }
                return id.Value;
            }
            set
            {
                id = value;
            }
        }
        public override bool Equals(Entity<Guid> obj)
        {
            return this.Id == obj.Id;
        }
    }
}
