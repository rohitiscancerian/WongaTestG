using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WongaTest.Abstractions
{
    public abstract class AbstractRoute
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double TicketPrice { get; set; }
        public double CostPerPassenger { get; set; }
        public double MinTakeOffLoadPercent { get; set; }
    }
}
