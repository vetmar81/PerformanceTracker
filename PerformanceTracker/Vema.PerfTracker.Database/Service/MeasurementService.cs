using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Service
{
    public class MeasurementService : BaseService<Measurement>
    {
        private static MeasurementService instance;

        private MeasurementService(Db database)
            : base(database)
        { 
        }

        public static MeasurementService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new MeasurementService(database);
            }

            return instance;
        }
    }
}
