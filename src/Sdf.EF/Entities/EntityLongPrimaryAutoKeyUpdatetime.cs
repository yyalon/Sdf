using Sdf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.EF.Entities
{
    public class EntityLongPrimaryAutoKeyUpdatetime : EntityLongPrimaryKeyAuto, IUpdateTimeField
    {
        public DateTime UpdateTime { get; set; }
    }
}
