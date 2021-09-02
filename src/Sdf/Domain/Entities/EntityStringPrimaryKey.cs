using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Entities
{
    public class EntityStringPrimaryKey : Entity<string>
    {
        public override string Id { get; set; }
        public override bool Equals(Entity<string> obj)
        {
            return this.Id == obj.Id;
        }
    }
}
