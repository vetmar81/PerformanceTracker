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
    public class PlayerDao : Dao
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        public PlayerDataHistoryDao DataHistoryDao { get; set; }

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
            base.Load(reader);

            //TODO: Load chlid instance
        }

        internal override void LoadProperty(DomainObject obj, string propertyName, DbDataReader reader)
        {
            Player player = obj as Player;

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
