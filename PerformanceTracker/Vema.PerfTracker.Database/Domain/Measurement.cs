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
    }
}
