using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;
using WongaTest.Enums;
using WongaTest.ConcreteEntities;

namespace WongaTest.Factory
{
    class FactoryPassenger
    {
        private int passengerType;
        public FactoryPassenger(int type)
        {
            passengerType = type;
        }

        public AbstractPassenger GetPassenger()
        {
            AbstractPassenger objPassenger = null;

            switch (passengerType)
            {
                case (int)Enumerations.FlightAttributes.general:
                    objPassenger = new PassengerGeneral();
                    break;
                case (int)Enumerations.FlightAttributes.airline:
                    objPassenger = new PassengerAirline();
                    break;
                case (int)Enumerations.FlightAttributes.loyalty:
                    objPassenger = new PassengerLoyalty();
                    break;
                default:
                    break;
            }

            return objPassenger;
        }
    }
}
