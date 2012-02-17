using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:46
    /// Domain object for additional temporally relevant player data.
    /// </summary>
    public class PlayerDataHistory : DomainObject, ITemporal
    {
        private Player player;

        /// <summary>
        /// Gets the weight of this entry.
        /// </summary>
        public double Weight { get; internal set; }

        /// <summary>
        /// Gets the height of this entry.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        /// Gets the valid from date.
        /// </summary>
        public DateTime ValidFrom { get; internal set; }

        /// <summary>
        /// Gets the valid from date.
        /// </summary>
        public DateTime ValidTo { get; internal set; }

        /// <summary>
        /// Gets the remark linked to this entry.
        /// </summary>
        public string Remark { get; internal set; }

        internal PlayerDataHistory()
        { 
        }

        internal PlayerDataHistory(PlayerDataHistoryDao dao)
            : base(dao)
        {
            Height = dao.Height;
            Weight = dao.Weight;
            ValidFrom = dao.ValidFrom;
            ValidTo = dao.ValidTo;
            Remark = dao.Remark;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], PlayerId: {2}, Height: {3} cm, Weight: {4} kg, ValidFrom: '{5}', ValidTo: '{6}', Remark {7}",
                                GetType().Name, Id, player.Id, Height, Weight,
                                ValidFrom.ToString(), ValidTo.ToString(), string.IsNullOrEmpty(Remark) ? "None" : Remark);
        }
    }
}
