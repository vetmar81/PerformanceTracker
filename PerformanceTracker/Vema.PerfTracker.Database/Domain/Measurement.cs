﻿using System;
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

        public string UnitNiceName
        {
            get { return GetUnitAsString(Unit); }
        }

        public DateTime Timestamp { get; internal set; }

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
            Timestamp = dao.Timestamp;
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
                                    GetType().Name, Id, Value, GetUnitAsString(Unit),
                                    Timestamp.ToString(), string.IsNullOrEmpty(Remark) ? "None" : Remark, Reference.Id);
        }

        /// <summary>
        /// Gets the unit as <see cref="string"/>.
        /// </summary>
        /// <returns>The unit as <see cref="string"/>.</returns>
        public static string GetUnitAsString(MeasurementUnit unit)
        {
            switch (unit)
            {
                case MeasurementUnit.Meters:
                    return "Meter";
                case MeasurementUnit.Kilograms:
                    return "Kilogramm";
                case MeasurementUnit.MetersPerSecond:
                    return "Meter / Sekunde";
                case MeasurementUnit.Seconds:
                    return "Sekunden";
                case MeasurementUnit.Unspecified:
                default:
                    return "N/A";
            }
        }

        /// <summary>
        /// Parses the specified unit <see cref="string"/> and returns corresponding <see cref="MeasurementUnit"/>.
        /// </summary>
        /// <param name="unit">The unit as <see cref="string"/>.</param>
        /// <returns>The converted <see cref="MeasurementUnit"/>.</returns>
        public static MeasurementUnit Parse(string unit)
        {
            string normalizedUnit = unit.ToLower();

            switch (normalizedUnit)
            {
                case "meter":
                case "meters":
                    return MeasurementUnit.Meters;
                case "meter / sekunde":
                case "meterspersecond":
                    return MeasurementUnit.MetersPerSecond;
                case "sekunden":
                case "seconds":
                    return MeasurementUnit.Seconds;
                case "kilogramm":
                case "kilograms":
                    return MeasurementUnit.Kilograms;
                default:
                    return MeasurementUnit.Unspecified;
            }
        }
    }
}
