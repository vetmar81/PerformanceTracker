using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class PlayerDataHistoryDao : Dao
    {
        public PlayerDao PlayerDao { get; private set; }

        public double Weight { get; set; }
        public int Height { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string Remark { get; set; }

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
        }

        internal override void LoadMember(DomainObject obj, string propertyName, DbDataReader reader)
        {
            PlayerDataHistory dataHistory = obj as PlayerDataHistory;

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
