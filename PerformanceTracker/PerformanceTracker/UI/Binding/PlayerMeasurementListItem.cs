using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Binding
{
    internal class PlayerMeasurementListItem
    {
        internal long Id { get; private set; }

        internal Measurement Measurement { get; private set; }

        internal double Value { get; set; }
        internal MeasurementUnit Unit { get; set; }
        internal DateTime Timestamp { get; set; }
        internal string Category { get; set; }
        internal string SubCategory { get; set; }
        internal string Remark { get; set; }

        internal PlayerMeasurementListItem()
        {
            Id = -1;
        }

        internal PlayerMeasurementListItem(Measurement measurement)
        {
            Id = measurement.Id;
            Measurement = measurement;

            Value = measurement.Value;
            Unit = measurement.Unit;
            Timestamp = measurement.Timestamp;
            Category = measurement.CategoryDesc;
            SubCategory = measurement.SubCategoryDesc;
            Remark = measurement.Remark;
        }

        internal string[] ToArray()
        {
            return new string[] { Value.ToString(), Measurement.GetUnitAsString(Unit),
                                    TimestampAsString(), Category, SubCategory, Remark};
        }

        private string TimestampAsString()
        {
            return string.Format("{0} {1}", Timestamp.ToShortDateString(), Timestamp.ToLongTimeString());
        }
    }
}
