using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;

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
            List<T> resultList = new List<T>();

            //try
            //{
            //    database.OpenConnection();
            //    QueryBuilder builder = new QueryBuilder(QueryType.Select);
            //}
            //finally
            //{
            //    database.CloseConnection();
            //}

            return resultList;
        }
    }
}
