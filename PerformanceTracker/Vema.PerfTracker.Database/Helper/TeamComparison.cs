using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    internal class TeamComparison : TemporalComparison<Team>
    {
        internal override bool IsEqual(Team previous, Team current)
        {
            bool test = (previous.Descriptor == current.Descriptor);
            test &= (previous.AgeGroup == current.AgeGroup);

            if (previous.References.Count == current.References.Count)
            {
                List<PlayerReference> previousList = previous.References.ToList();
                List<PlayerReference> currentList = current.References.ToList();
                previousList.Sort(Compare);
                currentList.Sort(Compare);

                for (int i = 0; i < previousList.Count; i++)
                {
                    PlayerReference previousRef = previousList[i];
                    PlayerReference currentRef = currentList[i];

                    PlayerReferenceComparison comparison = new PlayerReferenceComparison();
                    test &= comparison.IsEqual(previousRef, currentRef);
                }

                return test;
            }

            return true;
        }

        private int Compare(PlayerReference either, PlayerReference other)
        {
            return either.Id.CompareTo(other.Id);
        }
    }
}
