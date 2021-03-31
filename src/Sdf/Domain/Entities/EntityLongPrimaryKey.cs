using Sdf.IdProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sdf.Domain.Entities
{
    public class EntityLongPrimaryKey : Entity<long>
    {
        private long id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public override bool Equals(Entity<long> obj)
        {
            return this.Id == obj.Id;
        }
    }
    public class EntityLongPrimaryKeyAuto : Entity<long>
    {
        private long? id = null;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id
        {
            get
            {
                if (!id.HasValue)
                {
                    using (var resolver = Bootstrapper.Instance.IocManager.GetResolver())
                    {
                        var longIdProvider = resolver.Resolve<ILongIdProvider>();
                        id = longIdProvider.CreateNew();
                    }
                }
                return id.Value;
            }
            set
            {
                id = value;
            }
        }
        public override bool Equals(Entity<long> obj)
        {
            return this.Id == obj.Id;
        }
    }
}
