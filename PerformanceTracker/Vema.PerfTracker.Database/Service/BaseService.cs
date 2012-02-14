using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Service
{
    public abstract class BaseService<T> where T : DomainObject
    {
    }
}
