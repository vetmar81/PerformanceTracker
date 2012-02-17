using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:49
    /// Domain object representing a single <see cref="Measurement"/> of a certain performance feature.
    /// </summary>
    public class Measurement : DomainObject
    {
        private FeatureSubCategory subCategory;
        private PlayerReference playerReference;

        public double Value { get; internal set; }

        public MeasurementUnit Unit { get; internal set; }

        public DateTime TimeStamp { get; internal set; }

        public string Remark { get; internal set; }

        internal Measurement() : base()
        { 
        }

        internal Measurement(MeasurementDao dao)
            : base(dao)
        { }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}] Value: {2} {3}, TimeStamp: {4}, Remark: {5} PlayerReferenceId: {6}",
                                    GetType().Name, Id, Value, Enum.GetName(typeof(MeasurementUnit), Unit),
                                    TimeStamp.ToString(), string.IsNullOrEmpty(Remark) ? "None" : Remark, playerReference.Id);
        }
    }
}
