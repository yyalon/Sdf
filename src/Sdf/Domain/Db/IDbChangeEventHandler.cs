using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Db
{
    public interface IDbChangeEventHandler
    {
        bool Disable { get; }
        void OnEntityAdded(object entity);
        void OnEntityDeleted(object entity);
        void OnEntityModified(object entity);
    }
}
