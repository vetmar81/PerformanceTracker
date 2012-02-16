using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class PlayerReferenceDao : Dao
    {
        public PlayerDao PlayerDao { get; private set; }
        public TeamDao TeamDao { get; private set; }

        public bool IsCurrent { get; private set; }

        #region Dao Members

        /// <summary>
        /// Saves this <see cref="IDao"/>.
        /// </summary>
        internal override void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads this <see cref="IDao"/>.
        /// </summary>
        internal override void Load(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        internal override void LoadProperty(DomainObject obj, string propertyName, DbDataReader reader)
        {
            PlayerReference reference = obj as PlayerReference;

            if (obj != null)
            {
                PropertyInfo info = GetType().GetProperty(propertyName,
                                                            BindingFlags.Public | BindingFlags.SetProperty
                                                            | BindingFlags.Instance);
                info.SetValue(this, reader[propertyName], null);
            }
        }

        /// <summary>
        /// Updates this <see cref="IDao"/>.
        /// </summary>
        internal override void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes this <see cref="IDao"/>.
        /// </summary>
        internal override void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
