using Sdf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.EF.Entities
{
    public class EntityLongPrimaryAutoKeyUpdatetime : EntityLongPrimaryKeyAuto, IUpdateTimeField
    {
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
