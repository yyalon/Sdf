using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Db
{
    public class DefaultDbChangeEventHandler : IDbChangeEventHandler
    {
        public bool Disable
        {
            get
            {
                return false;
            }
        }

        public void OnEntityAdded(object entity)
        {
            
        }

        public void OnEntityDeleted(object entity)
        {
            
        }

        public void OnEntityModified(object entity)
        {
           
        }
    }
}
