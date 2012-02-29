using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Binding
{
    /// <summary>
    /// Markus Vetsch, 28.02.2012 14:46
    /// Helper class for binding to data grid view.
    /// </summary>
    internal class PlayerMeasurementRowEntry
    {
        internal long PlayerId { get; private set; }
        internal Measurement Measurement { get; private set; }

        public double Value { get { return Measurement.Value; } }
        public string Unit { get { return Measurement.UnitNiceName; } }
        public string Category { get { return Measurement.CategoryDesc; } }
        public string SubCategory { get { return Measurement.SubCategoryDesc; } }
        public string Remark { get { return Measurement.Remark; } }
        public DateTime Timestamp { get { return Measurement.Timestamp; } }

        internal PlayerMeasurementRowEntry(Measurement measurement, long playerId)
        {
            Measurement = measurement;
            PlayerId = playerId;
        }
    }
}
