using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public abstract class Dao
    {
        internal DbTableMap Map { get; private set; }

        /// <summary>
        /// Gets or sets the Id of this <see cref="Dao"/>.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Saves this <see cref="Dao"/>.
        /// </summary>
        internal abstract void Save();

        /// <summary>
        /// Loads this <see cref="Dao"/>.
        /// </summary>
        internal virtual void Load(DbDataReader reader)
        {
            foreach (var member in Map.Members)
            {
                if (member.IsInitiallyLoaded && !member.IsReferencedType)
                {
                    PropertyInfo info = GetType().GetProperty(member.Name,
                                                            BindingFlags.Public | BindingFlags.SetProperty 
                                                            | BindingFlags.Instance);
                    info.SetValue(this, reader[member.Column], null);
                }
            }
        }

        internal abstract void LoadMember(DomainObject obj, string propertyName, DbDataReader reader);

        /// <summary>
        /// Updates this <see cref="Dao"/>.
        /// </summary>
        internal abstract void Update();

        /// <summary>
        /// Deletes this <see cref="Dao"/>.
        /// </summary>
        internal abstract void Delete();

        /// <summary>
        /// Initializes a new instance of the <see cref="Dao"/> class.
        /// </summary>
        protected Dao()
        {
            Id = -1;
        }

        internal void AssignPersistenceInfo(DbTableMap mapping)
        {
            Map = mapping;
        }
    }
}
