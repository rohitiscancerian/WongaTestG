using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;

namespace WongaTest.ConcreteEntities
{
    class Flight : AbstractFlight
    {
        public Flight()
        {
            lstPassengers = new List<AbstractPassenger>();
        }
    }
}
