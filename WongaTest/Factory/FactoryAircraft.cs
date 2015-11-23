using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;
using WongaTest.ConcreteEntities;

namespace WongaTest.Factory
{
    class FactoryAircraft
    {
        
        public FactoryAircraft()
        {
            
        }
        public AbstractAircraft GetAircraft()
        {
            return new Aircraft();
        }
    }
}
