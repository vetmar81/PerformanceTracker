using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    internal class PlayerDataHistoryComparison : TemporalComparison<PlayerDataHistory>
    {
        internal override bool IsEqual(PlayerDataHistory previous, PlayerDataHistory current)
        {
            bool test = (previous.Height == current.Height);
            test &= (previous.Weight == current.Weight);
            test &= (previous.Remark == current.Remark);

            return test;
        }
    }
}
