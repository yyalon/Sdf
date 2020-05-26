using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.IdProvider.Default
{
    public class IdGenerator
    {
        private readonly static object lockObject = new object();
        private static IdGenerator instance;
        public static IdGenerator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new IdGenerator();
                        }
                    }
                }
                return instance;
            }
        }
        private IdWorker worker;
        private IdGenerator()
        {
            worker = new IdWorker(1, 1);
        }

        public long GetLongId()
        {
            lock (this)
            {
                return worker.NextId();
            }
        }
    }
}
