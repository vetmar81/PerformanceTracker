using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Config;

namespace Vema.PerfTracker.Database.Service
{
    public abstract class BaseService<T> where T : DomainObject
    {
        protected readonly Db database;

        protected BaseService(Db database)
        {
            this.database = database;
        }

        public List<T> SelectAll()
        {
            return database.LoadAll<T>();
        }

        public T SelectById(long id)
        {
            return database.LoadById<T>(id);
        }
    }
}
