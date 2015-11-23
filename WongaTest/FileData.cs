using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;
using WongaTest.Enums;
using WongaTest.ConcreteEntities;
using WongaTest.Factory;
using WongaTest.Interfaces;
using System.IO;

namespace WongaTest
{
    public class FileData : IFileData
    {
        private string[]  comma = { "," };

        /// <summary>
        /// This method takes the input file as streamreader and populates business entities
        /// and then writes the output to txt file
        /// </summary>
        /// <param name="file"></param>
        public AbstractFlight ProcessFile(StreamReader file,string outputFilename)
        {
            int lineCounter = 1;
            string line;
            FactoryFlight objFactoryFlight = new FactoryFlight();
            FactoryAircraft objFactoryAircraft = new FactoryAircraft();
            AbstractFlight objFlight = objFactoryFlight.GetFlight();
            AbstractAircraft objAircraft = objFactoryAircraft.GetAircraft();
            objFlight.FlightAircraft = objAircraft;
            AbstractPassenger objPassenger;

            try
            {
                while ((line = file.ReadLine()) != null)
                {

                    string[] arrLine;

                    arrLine = line.Split(' ');

                    if (arrLine.Count() > 1)
                    {
                        Enumerations.FlightAttributes fltAttributeValue;

                        Enum.TryParse(arrLine[1].ToLower(), true, out fltAttributeValue);

                        switch (fltAttributeValue)
                        {
                            case Enumerations.FlightAttributes.aircraft:
                                int noOfSeats;
                                objAircraft.Title = arrLine[2];

                                if (int.TryParse(arrLine[3].ToString(), out noOfSeats))
                                {
                                    objAircraft.NoOfSeats = noOfSeats;
                                }
                                else
                                {
                                    Console.WriteLine("No of seats are not provided for the aircraft. Cannot proceed further");
                                }

                                break;
                            case Enumerations.FlightAttributes.route:
                                double costPerPassenger = 0;
                                double ticketPrice = 0;
                                decimal minTakeOffLoadPercent = 0;

                                objFlight.Origin = arrLine[2];
                                objFlight.Destination = arrLine[3];

                                double.TryParse(arrLine[4], out costPerPassenger);
                                objFlight.CostPerPassenger = costPerPassenger;

                                double.TryParse(arrLine[5], out ticketPrice);
                                objFlight.TicketPrice = ticketPrice;

                                decimal.TryParse(arrLine[6], out minTakeOffLoadPercent);
                                objFlight.MinTakeOffLoadPercent = minTakeOffLoadPercent;
                                break;
                            case Enumerations.FlightAttributes.general:
                            case Enumerations.FlightAttributes.airline:
                                objPassenger = new FactoryPassenger((int)fltAttributeValue).GetPassenger();
                                SetCommonPassengerProperties(objPassenger, arrLine, fltAttributeValue);
                                objFlight.addPassenger(objPassenger);
                                break;
                            case Enumerations.FlightAttributes.loyalty:
                                int loyaltyPts = 0;
                                bool usingLoyalPts = false;
                                bool usingExtraBag = false;

                                objPassenger = new FactoryPassenger((int)fltAttributeValue).GetPassenger();
                                SetCommonPassengerProperties(objPassenger, arrLine, fltAttributeValue);

                                if (int.TryParse(arrLine[4], out loyaltyPts))
                                {
                                    objPassenger.CurLoyaltyPts = loyaltyPts;
                                }
                                if (bool.TryParse(arrLine[5], out usingLoyalPts))
                                {
                                    objPassenger.UsingLoyaltyPts = usingLoyalPts;
                                }
                                if (bool.TryParse(arrLine[6], out usingExtraBag))
                                {
                                    objPassenger.UsingExtraBag = usingExtraBag;
                                }
                                objFlight.addPassenger(objPassenger);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(string.Format("The input line no {0} is not valid", lineCounter));
                    }

                    lineCounter++;
                }

                using (StreamWriter outFile = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + outputFilename + ".txt"))
                {
                    StringBuilder fileProcessOutput = objFlight.evaluateFlightParams(objAircraft);
                    string[] outputMessages = fileProcessOutput.ToString().Split(comma, StringSplitOptions.None);
                    foreach (string message in outputMessages)
                    {
                        outFile.WriteLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                Console.ReadLine();
            }
            return objFlight;
        }

       
        /// <summary>
        /// This method sets the common attributes of passenger object obtained from input file
        /// </summary>
        /// <param name="objPassenger"></param>
        /// <param name="arrLine"></param>
        /// <param name="fltAttribVal"></param>
        private void SetCommonPassengerProperties(AbstractPassenger objPassenger, string[] arrLine, Enumerations.FlightAttributes fltAttribVal)
        {
            int age = 0;

            objPassenger.Type = (int)fltAttribVal;
            objPassenger.Firstname = arrLine[2];
            if (int.TryParse(arrLine[3], out age))
            {
                objPassenger.Age = age;
            }
        }
    }
}
