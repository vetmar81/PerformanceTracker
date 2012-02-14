﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Access
{
    internal class TeamDao : IDao
    {
        internal List<PlayerReferenceDao> PlayerReferenceDaoList { get; private set; }

        internal string Descriptor { get; private set; }
        internal string AgeGroup { get; private set; }
        internal bool IsDeleted { get; private set; }

        #region IDao Members

        /// <summary>
        /// Gets or sets the Id of this <see cref="IDao"/>.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        /// .
        public long Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Saves this <see cref="IDao"/>.
        /// </summary>
        public void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads this <see cref="IDao"/>.
        /// </summary>
        public void Load()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates this <see cref="IDao"/>.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes this <see cref="IDao"/>.
        /// </summary>
        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
