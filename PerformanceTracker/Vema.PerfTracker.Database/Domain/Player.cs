using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:45
    /// Domain object representing a single <see cref="Player"/>.
    /// </summary>
    public class Player : DomainObject
    {
        internal PlayerDataHistory DataHistory { get; set; }

        public PlayerDao Dao
        {
            get { return dao as PlayerDao; }
        }

        public PlayerReference Reference { get; internal set; }
        
        /// <summary>
        /// Gets the first name of the <see cref="Player"/>.
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        /// Gets the last name of the <see cref="Player"/>.
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        /// Gets the country of the <see cref="Player"/>.
        /// </summary>
        public string Country { get; internal set; }

        /// <summary>
        /// Gets the date of birth of the <see cref="Player"/>.
        /// </summary>
        public DateTime Birthday { get; internal set; }

        /// <summary>
        /// Gets the weight of the <see cref="Player"/> in kg.
        /// </summary>
        public double? Weight 
        {
            get 
            {
                if (DataHistory == null)
                {
                    return null;
                }

                return DataHistory.Weight;
            }
        }

        /// <summary>
        /// Gets the height of the <see cref="Player"/> in cm.
        /// </summary>
        public int? Height
        {
            get 
            {
                if (DataHistory == null)
                {
                    return null;
                }

                return DataHistory.Height;
            }
        }

        /// <summary>
        /// Gets the remark associated.
        /// </summary>
        public string Remark
        {
            get
            {
                if (DataHistory == null)
                {
                    return null;
                }

                return DataHistory.Remark;
            }
        }


        /// <summary>
        /// Gets the team this <see cref="Player"/> is currently associated to.
        /// </summary>
        public Team Team
        {
            get 
            {
                if (DataHistory == null)
                {
                    throw new ArgumentNullException("Property Player.Reference is not loaded!");
                }

                return Reference.Team; 
            }
        }

        internal Player()
            : base()
        { }

        internal Player(PlayerDao dao)
            : base(dao)
        {
            FirstName = dao.FirstName;
            LastName = dao.LastName;
            Country = dao.Country;
            Birthday = dao.Birthday;

            if (dao.DataHistoryDao != null)
            {
                DataHistory = (PlayerDataHistory) dao.DataHistoryDao.CreateDomainObject();
                DataHistory.Player = this;
            }

            if (dao.ReferenceDao != null)
            {
                Reference = (PlayerReference) dao.ReferenceDao.CreateDomainObject();
                Reference.Player = this;

                if (dao.ReferenceDao.TeamDao != null)
                {
                    Team team = (Team) dao.ReferenceDao.TeamDao.CreateDomainObject();
                    Reference.Team = team;
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], Name: '{2} {3}', Birthday: '{4}', Country: '{5}'",
                                    GetType().Name, Id, FirstName, LastName, Birthday.ToString("yyyy-MM-dd"), Country);
        }
    }
}
