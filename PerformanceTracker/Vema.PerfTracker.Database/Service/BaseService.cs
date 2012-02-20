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
    /// manipulation of object-relational data persistently stored on the database.
    /// </summary>
    /// <typeparam name="T">Defines the type of object, to be used in the service interaction.</typeparam>
    public abstract class BaseService<T> where T : DomainObject
    {
        protected readonly Db database;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="database">The underlying database implementation.</param>
        protected BaseService(Db database)
        {
            this.database = database;
        }

        #region Manipulation Methods - Default Implementation

        /// <summary>
        /// Default implementation to loads all objects of specified type <typeparamref name="T"/> from database.
        /// </summary>
        /// <returns>All the objects of specified type <typeparamref name="T"/> from th</returns>
        public virtual List<T> LoadAll()
        {
            return database.LoadAll<T>();
        }

        /// <summary>
        /// Default implementation to load the object of type <typeparamref name="T"/> specified by the database ID.
        /// </summary>
        /// <param name="id">The database ID of the object to be loaded.</param>
        /// <returns>The loaded object of type <typeparamref name="T"/>.</returns>
        public virtual T LoadById(long id)
        {
            return database.LoadById<T>(id);
        }

        /// <summary>
        /// Default implementation to load all currently valid objects for any kind of <see cref="ITemporal"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be respected; must be an <see cref="ITemporal"/>.</typeparam>
        /// <returns>All currently valid objects for specified kind of <see cref="ITemporal"/>.</returns>
        public virtual List<T> LoadAllCurrent<T>() where T : DomainObject, ITemporal
        {
            return database.LoadAllCurrent<T>();
        }

        /// <summary>
        /// Default implementation to save a set of objects of <typeparamref name="T"/> to the database.
        /// </summary>
        /// <param name="tList">The set of objects to be saved.</param>
        public virtual void SaveAll(IEnumerable<T> tList)
        {
            database.BulkSaveObject(tList);
        }

        /// <summary>
        /// Default implementation to save a single object of type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <param name="t">The single object to be saved.</param>
        public virtual void Save(T t)
        {
            database.SaveObject(t);
        }

        /// <summary>
        /// Default implementation to update a set of objects of <typeparamref name="T"/> on the database.
        /// </summary>
        /// <param name="tList">The set of objects to be updated.</param>
        public virtual void UpdateAll(IEnumerable<T> tList)
        {
            database.BulkUpdateObject(tList);
        }

        /// <summary>
        /// Default implementation to update the single object of type <typeparamref name="T"/> on the database.
        /// </summary>
        /// <param name="t">The single object to be updated.</param>
        public virtual void Update(T t)
        {
            database.UpdateObject<T>(t);
        }

        /// <summary>
        /// Default implementation to delete a set of objects of <typeparamref name="T"/> from the database.
        /// </summary>
        /// <param name="tList">The set of objects to be deleted.</param>
        public virtual void DeleteAll(IEnumerable<T> tList)
        {
            database.BulkDeleteObject(tList);
        }

        /// <summary>
        /// Default implementation to delete the single object of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <param name="t">The single object to be deleted.</param>
        public virtual void Delete(T t)
        {
            database.DeleteObject(t);
        }

        #endregion
    }
}
