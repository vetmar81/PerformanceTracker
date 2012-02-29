using System;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Binding
{
    internal class PlayerListItem
    {
        internal Player Player { get; private set; }

        internal long Id { get; private set; }

        internal string FirstName { get; set; }
        internal string LastName { get; set; }
        internal DateTime Birthday { get; set; }
        internal string Country { get; set; }
        internal int? Height { get; set; }
        internal double? Weight { get; set; }
        internal string Remark { get; set; }

        internal PlayerListItem()
        {
            Id = -1;
            Player = null;
        }

        internal PlayerListItem(Player player)
        {
            Player = player;
            Id = player.Id;

            FirstName = player.FirstName;
            LastName = player.LastName;
            Birthday = player.Birthday;
            Country = player.Country;
            Height = player.Height;
            Weight = player.Weight;
            Remark = player.Remark;
        }

        internal string[] ToArray()
        {
            string heightValue = Height.HasValue ? Height.Value.ToString() : null;
            string weightValue = Weight.HasValue ? Weight.Value.ToString() : null;

            return new [] { FirstName, LastName, Birthday.ToShortDateString(), Country,
                            heightValue, weightValue, Remark};
        }
    }
}
