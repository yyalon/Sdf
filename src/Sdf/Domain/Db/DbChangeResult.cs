using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Db
{
    public class DbChangeResult
    {
        public int ChangeNum { get; private set; }
        public bool State { get; private set; }
        public IEnumerable<Exception> ErrorList { get; private set; }
        public DbChangeResult(bool state)
        {
            ChangeNum = 0;
            State = state;
        }
        public DbChangeResult(int changeNum, bool state)
        {
            ChangeNum = changeNum;
            State = state;
        }
        public DbChangeResult(int changeNum, bool state, IEnumerable<Exception> errorList)
        {
            ChangeNum = changeNum;
            State = state;
            ErrorList = errorList;
        }
    }
}
