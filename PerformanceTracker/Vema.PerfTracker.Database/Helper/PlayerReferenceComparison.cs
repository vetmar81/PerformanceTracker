using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    internal class PlayerReferenceComparison : TemporalComparison<PlayerReference>
    {
        internal override bool IsEqual(PlayerReference previous, PlayerReference current)
        {
            bool test = (previous.Player.Id == current.Player.Id);
            test &= (previous.Team.Id == current.Team.Id);

            return test;
        }
    }
}
