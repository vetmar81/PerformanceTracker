using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Vema.PerfTracker;
using Vema.PerfTracker.Database.Domain;

namespace FootballPerformanceTracker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string configPath = Path.Combine(Application.StartupPath, "PgDbConfig.xml");
            ClientDatabase database = new ClientDatabase(configPath);
            Player player = database.SelectPlayerById(1);
            List<Player> players1 = database.SelectByBirthdateOlder(new DateTime(1975, 12, 31));
            List<Player> players2 = database.SelectByBirthdateYounger(new DateTime(1976, 1, 1));
            List<Player> players3 = database.SelectPlayerByLastNamePart("M%");
            List<Player> players4 = database.SelectPlayerByFirstNamePart("Ha%");
            List<Player> all = database.SelectAllPlayers();
            Team team = database.SelectTeamById(3);
            Team team1 = database.SelectTeamById(2);
            List<Player> teamPlayers = database.SelectAllPlayerForTeam(3);
            List<Player> teamPlayers2 = database.SelectAllPlayerForTeam(2);
            //Team team1 = database.SelectCurrentTeamByDescriptor("U-19");
            //List<Team> teams = database.SelectAllCurrentTeams();
            List<Team> allTeams = database.SelectAllTeams();
        }
    }
}
