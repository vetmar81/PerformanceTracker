using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Binding
{
    /// <summary>
    /// Markus Vetsch, 28.02.2012 14:45
    /// Helper class for binding to data grid view
    /// </summary>
    internal class PlayerDataRowEntry
    {
        internal long PlayerId { get; private set; }
        internal PlayerDataHistory DataHistory { get; private set; }

        public int? Height { get { return DataHistory.Height; } }
        public double? Weight { get { return DataHistory.Weight; } }
        public string Remark { get { return DataHistory.Remark; } }
        public DateTime Date { get { return DataHistory.ValidFrom; } }

        internal PlayerDataRowEntry(PlayerDataHistory dataHistory, long playerId)
        {
            DataHistory = dataHistory;
            PlayerId = playerId;
        }
    }
}
