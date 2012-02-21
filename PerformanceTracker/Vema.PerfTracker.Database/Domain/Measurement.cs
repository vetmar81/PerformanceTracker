using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:49
    /// Domain object representing a single <see cref="Measurement"/> of a certain performance feature.
    /// </summary>
    public class Measurement : DomainObject
    {
        public MeasurementDao Dao
        {
            get { return dao as MeasurementDao; }
        }

        public double Value { get; internal set; }

        public MeasurementUnit Unit { get; internal set; }

        public DateTime TimeStamp { get; internal set; }

        public string Remark { get; internal set; }

        public string CategoryDesc
        {
            get { return (SubCategory.ParentCategory == null) ? "N/A" : SubCategory.ParentCategory.NiceName; }
        }

        public string SubCategoryDesc
        {
            get { return (SubCategory == null) ? "N/A" : SubCategory.NiceName; }
        }

        public Team Team
        {
            get { return (Reference == null) ? null : Reference.Team; }
        }

        public Player Player
        {
            get { return (Reference == null) ? null : Reference.Player; }
        }

        internal FeatureSubCategory SubCategory { get; set; }

        internal PlayerReference Reference { get; set; }

        internal Measurement() : base()
        { 
        }

        internal Measurement(MeasurementDao dao)
            : base(dao)
        {
            Value = dao.Value;
            Unit = dao.Unit;
            TimeStamp = dao.TimeStamp;
            Remark = dao.Remark;

            if (dao.SubCategoryDao != null)
            {
                SubCategory = (FeatureSubCategory) dao.SubCategoryDao.CreateDomainObject();
            }
            if (dao.PlayerReferenceDao != null)
            {
                Reference = (PlayerReference) dao.PlayerReferenceDao.CreateDomainObject();

                if (Reference.Dao.PlayerDao != null)
                {
                    Reference.Player = (Player) Reference.Dao.PlayerDao.CreateDomainObject();
                }
                if (Reference.Dao.TeamDao != null)
                {
                    Reference.Team = (Team) Reference.Dao.TeamDao.CreateDomainObject();
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
            return string.Format("[{0} - Id: {1}] Value: {2} {3}, TimeStamp: {4}, Remark: {5} PlayerReferenceId: {6}",
                                    GetType().Name, Id, Value, Enum.GetName(typeof(MeasurementUnit), Unit),
                                    TimeStamp.ToString(), string.IsNullOrEmpty(Remark) ? "None" : Remark, Reference.Id);
        }
    }
}
