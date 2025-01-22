using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Entities
{
    /// <summary>
    /// 软删除接口
    /// </summary>
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        void Delete();
    }
}
