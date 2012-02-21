using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    internal abstract class TemporalComparison<T> where T : DomainObject, ITemporal
    {
        internal abstract bool IsEqual(T previous, T current);
    }
}
