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
    /// <summary>
    /// Markus Vetsch, 17.02.2012 18:23
    /// Abstract generic base class for all services accessing the database for
    /// data manipulations. The services offer methods to for straightforward
    /// loading / saving of object-relational data.
    /// </summary>
    /// <typeparam name="T">Defines the type of object, to be used in the service interaction.</typeparam>
    public abstract class BaseService<T> where T : DomainObject
    {
        private static BaseService<T> instance;

        protected readonly Db database;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="database">The underlying database implementation.</param>
        protected BaseService(Db database)
        {
            this.database = database;
        }

        /// <summary>
        /// Loads all objects of specified type <typeparamref name="T"/> from database.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> LoadAll()
        {
            return database.LoadAll<T>();
        }

        /// <summary>
        /// Loads the object of type <typeparamref name="T"/> specified by the database ID.
        /// </summary>
        /// <param name="id">The database ID of the object to be loaded.</param>
        /// <returns>The loaded object of type <typeparamref name="T"/>.</returns>
        public virtual T LoadById(long id)
        {
            return database.LoadById<T>(id);
        }

        /// <summary>
        /// Loads all currently valid objects for any kind of <see cref="ITemporal"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be respected; must be an <see cref="ITemporal"/>.</typeparam>
        /// <returns></returns>
        public virtual List<T> LoadAllCurrent<T>() where T : DomainObject, ITemporal
        {
            return database.LoadAllCurrent<T>();
        }

        public virtual void SaveAll(IEnumerable<T> tList)
        {
            foreach (T t in tList)
            {
                Save(t);
            }
        }

        public virtual void Save(T t)
        {
            database.SaveObject<T>(t);
        }

        public virtual void Update(T t)
        {
            database.UpdateObject<T>(t);
        }

        public virtual void Delete(T t)
        {
            database.DeleteObject<T>(t);
        }
    }
}
