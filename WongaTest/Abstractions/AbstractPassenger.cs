using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Enums;

namespace WongaTest.Abstractions
{
    public abstract class AbstractPassenger
    {
        public int Type { get; set; }
        public string Firstname { get; set; }
        public double Age { get; set; }
        public int CurLoyaltyPts { get; set; }
        public bool UsingLoyaltyPts { get; set; }
        public bool UsingExtraBag { get; set; }
    }
}
