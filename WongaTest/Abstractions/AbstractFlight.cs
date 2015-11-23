using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.ConcreteEntities;
using WongaTest.Enums;

namespace WongaTest.Abstractions
{
    public class AbstractFlight
    {

        public string Origin { get; set; }
        public string Destination { get; set; }
        public double TicketPrice { get; set; }
        public double CostPerPassenger { get; set; }
        public decimal MinTakeOffLoadPercent { get; set; }
        public int TotNoAirlinePass { get; set; }
        public int TotLoyaltyPointsUsed { get; set; }
        public double TotalCostOfFlight { get; set; }
        public double TotAdjRev { get; set; }
        public string EvaluationMessage { get; set; }
        public bool CanFlightProceed { get; set; }

        private const char space = ' ';
        private const char comma = ',';

        public List<AbstractPassenger> lstPassengers { get; set; }

        public AbstractAircraft FlightAircraft { get; set; }

        /// <summary>
        /// Adds passenger to flight
        /// </summary>
        /// <param name="passenger"></param>
        public void addPassenger(AbstractPassenger passenger)
        {
            lstPassengers.Add(passenger);
        }

        /// <summary>
        /// Gets count of general passenger 
        /// </summary>
        /// <returns></returns>
        public int getGenPassCount()
        {
            var genPass = from pass in lstPassengers
                          where pass.Type == (int)Enumerations.FlightAttributes.general
                          select pass;

            return genPass.Count();
        }

        /// <summary>
        /// Gets count of Airline passenger
        /// </summary>
        /// <returns></returns>
        public int getAirPassCount()
        {
            var airPass = from pass in lstPassengers
                          where pass.Type == (int)Enumerations.FlightAttributes.airline
                          select pass;
            TotNoAirlinePass = airPass.Count();
            return TotNoAirlinePass;
        }

        /// <summary>
        /// Gets count of Loyalty passenger
        /// </summary>
        /// <returns></returns>
        public int getLoyalPassCount()
        {
            var loyalPass = from pass in lstPassengers
                          where pass.Type == (int)Enumerations.FlightAttributes.loyalty
                          select pass;

            return loyalPass.Count();
        }

        /// <summary>
        /// Gets count of total number of bags in flight
        /// </summary>
        /// <returns></returns>
        public int getTotalNoOfBags()
        {
            var passWithExtraBag = from pass in lstPassengers
                              where pass.UsingExtraBag == true
                              select pass;

            return passWithExtraBag.Count() + lstPassengers.Count();
        }

        /// <summary>
        /// Gets total loyal points redeemed by Loyalty passengers
        /// </summary>
        /// <returns></returns>
        public int getTotLoyalPtsRedeem()
        {
            var totLoyalPoints = from pass in lstPassengers
                                 where pass.UsingLoyaltyPts == true
                                 select pass.CurLoyaltyPts ;

            TotLoyaltyPointsUsed = (int)totLoyalPoints.Sum();

            return TotLoyaltyPointsUsed;
        }

        /// <summary>
        /// Gets total cost of flight
        /// </summary>
        /// <param name="aircraft"></param>
        /// <returns></returns>
        public double getTotalCostOfFlight(AbstractAircraft aircraft)
        {
            TotalCostOfFlight = CostPerPassenger * lstPassengers.Count();
            return TotalCostOfFlight;
        }

        /// <summary>
        /// Gets total unadjusted ticket revenue
        /// </summary>
        /// <returns></returns>
        public double getTotUnadjTktRev()
        {
            return TicketPrice * lstPassengers.Count;
        }

        /// <summary>
        /// Gets total adjusted revenue
        /// </summary>
        /// <returns></returns>
        public double getTotAdjstRev()
        {
            TotAdjRev = (TicketPrice * lstPassengers.Count) - (TotNoAirlinePass * TicketPrice) - TotLoyaltyPointsUsed;
            return TotAdjRev;
        }

        public bool canFlightProceed(AbstractAircraft aircraft)
        {
            CanFlightProceed = true;
            decimal totalPassengers = 0;
            totalPassengers = lstPassengers.Count ;
            if  (TotAdjRev <= TotalCostOfFlight)
            {
                EvaluationMessage = ",total adjusted revenue is less than total flight cost";
                CanFlightProceed = false;
            }
            if (totalPassengers > aircraft.NoOfSeats)
            {
                CanFlightProceed = false;
                EvaluationMessage += ",total no. of booked tickets is more than total no. of seats";
            }
            if (decimal.Divide(totalPassengers , aircraft.NoOfSeats) * 100 <= MinTakeOffLoadPercent)
            {
                CanFlightProceed = false;
                EvaluationMessage += string.Format(", it is less than {0}% full", MinTakeOffLoadPercent.ToString());
            }
            return CanFlightProceed;
        }

        public StringBuilder evaluateFlightParams(AbstractAircraft objAircraft)
        {
            StringBuilder messageArray = new StringBuilder();
            string outputLine = lstPassengers.Count.ToString() + space + getGenPassCount().ToString() + space + 
                                    getAirPassCount().ToString() + space + getLoyalPassCount().ToString() + space +
                                    getTotalNoOfBags().ToString() + space + getTotLoyalPtsRedeem().ToString() + space +
                                    getTotalCostOfFlight(objAircraft).ToString() + space + getTotUnadjTktRev().ToString() + space +
                                    getTotAdjstRev().ToString() + space + canFlightProceed(objAircraft).ToString();

            messageArray.Append(outputLine);
            messageArray.Append(comma);
            EvaluationMessage = (CanFlightProceed == true) ? "The Flight can proceed" + EvaluationMessage : "The Flight cannot proceed" + EvaluationMessage;
            messageArray.Append(EvaluationMessage);

            return messageArray;
        }

    }
}
