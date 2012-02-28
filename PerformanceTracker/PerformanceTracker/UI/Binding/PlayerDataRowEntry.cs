using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Binding
{
    internal class PlayerDataRowEntry
    {
        internal PlayerDataHistory DataHistory { get; private set; }

        public int Height { get { return DataHistory.Height; } }
        public double Weight { get { return DataHistory.Weight; } }
        public string Remark { get { return DataHistory.Remark; } }
        public DateTime Date { get { return DataHistory.ValidFrom; } }

        internal PlayerDataRowEntry(PlayerDataHistory dataHistory)
        {
            DataHistory = dataHistory;
        }
    }
}
