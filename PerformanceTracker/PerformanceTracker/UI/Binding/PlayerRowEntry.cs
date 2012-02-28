using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Windows.Forms;

namespace Vema.PerformanceTracker.UI.Binding
{
    internal class PlayerRowEntry
    {
        public string LastName { get { return Player.LastName; } }
        public string FirstName { get { return Player.FirstName; } }
        public DateTime Birthday { get { return Player.Birthday; } }
        public int Age { get { return CalculateAge(); } }
        public string Country { get { return Player.Country; } }

        internal Player Player { get; private set; }

        internal PlayerRowEntry(Player player)
        {
            Player = player;
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            DateTime birthday = Player.Birthday;
            int age = today.Year - birthday.Year;

            if (birthday > today.AddYears(-age)) { age--; } 

            return age;
        }
    }
}
