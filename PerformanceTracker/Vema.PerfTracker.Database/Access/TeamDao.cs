﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class TeamDao : Dao
    {
        public List<PlayerReferenceDao> PlayerReferenceDaoList { get; private set; }

        public string Descriptor { get; private set; }
        public string AgeGroup { get; private set; }
        public bool IsDeleted { get; private set; }

        #region Dao Members

        internal override DomainObject CreateDomainObject()
        {
            return new Team(this);
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
            base.Load(reader);
        }

        internal override void LoadMember(DomainObject obj, string propertyName, DbDataReader reader)
        {
            Team team = obj as Team;

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
