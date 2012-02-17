using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class FeatureCategoryDao : Dao
    {
        public string NiceName { get; private set; }

        #region Dao Members

        /// <summary>
        /// Creates the corresponding <see cref="DomainObject"/>.
        /// </summary>
        /// <returns>
        /// the corresponding <see cref="DomainObject"/>.
        /// </returns>
        internal override DomainObject CreateDomainObject()
        {
            return new FeatureCategory(this);
        }

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

        internal override void LoadMember(DomainObject obj, string propertyName, DbDataReader reader)
        {
            FeatureCategory category = obj as FeatureCategory;

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
