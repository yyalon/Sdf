using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Sdf.Domain.Uow
{
    public class UnitOfWorkOption
    {
        public TransactionScopeOption? Scope { get; set; }
    }
}
