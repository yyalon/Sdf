using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Sdf.IdProvider.Default
{
    public class IdGenerator
    {
        private const string WORKER_ID = "WorkerId";
        private const string DATACENTER_ID = "DatacenterId";
        private const string SEQUENCE = "Sequence";

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
            var snowflakeModel = GetSnowflakeModel();
            worker = new IdWorker(snowflakeModel.WorkerId, snowflakeModel.DatacenterId, snowflakeModel.Sequence);
        }

        public long GetLongId()
        {
            lock (this)
            {
                return worker.NextId();
            }
        }
        
       
        private SnowflakeModel GetSnowflakeModel()
        {
            long workerId = 1L;
            long datacenterId = 1L;
            long sequence = 0L;

            string xml = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.xml");
            if (File.Exists(xml))
            {
                try
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Snowflake.xml"));
                    var childNodes = xmldoc.DocumentElement.ChildNodes;

                    foreach (XmlNode item in childNodes)
                    {
                        if (item.Name == WORKER_ID)
                        {
                            workerId = Convert.ToInt64(item.InnerText.Trim());
                        }
                        if (item.Name == DATACENTER_ID)
                        {
                            datacenterId = Convert.ToInt64(item.InnerText.Trim());
                        }
                        if (item.Name == SEQUENCE)
                        {
                            sequence = Convert.ToInt64(item.InnerText.Trim());
                        }
                    }
                }
                catch (Exception){ }
            }
           
            SnowflakeModel snowflakeModel = new SnowflakeModel()
            {
                DatacenterId = datacenterId,
                Sequence = sequence,
                WorkerId = workerId
            };
            return snowflakeModel;
        }
        class SnowflakeModel
        { 
            public long WorkerId { get; set; }
            public long DatacenterId { get; set; }
            public long Sequence { get; set; }
        }
    }
}
