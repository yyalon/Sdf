using Dapper.Contrib.Extensions;
using Sdf.Domain.Entities;

namespace Sdf.Dapper.Entities
{
    public class DapperEntityLongKey: EntityLongPrimaryKey
    {
        [ExplicitKey]
        public override long Id { get => base.Id;protected set => base.Id = value; }
    }
}
