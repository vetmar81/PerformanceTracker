using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database;

namespace Vema.PerfTracker
{
    internal class ClientDatabase
    {
        private PgDb database;

        internal ClientDatabase(string configPath)
        {
            database = PgDb.Create(configPath);
        }

        internal void OpenConnection()
        {
        }
    }
}
