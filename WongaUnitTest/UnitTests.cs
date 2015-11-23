using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WongaTest.Abstractions;
using WongaTest.Interfaces;
using WongaTest;
using System.IO;
namespace WongaUnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestFileData()
        {
            AbstractFlight objFlight = null;
            IFileData handleFileData = new FileData();

            System.IO.StreamReader file =
              new System.IO.StreamReader(Directory.GetCurrentDirectory() + "\\input1.txt");

            objFlight = handleFileData.ProcessFile(file);

            Assert.AreEqual<double>(objFlight.TotAdjRev, 750);
            Assert.AreEqual<double>(objFlight.TotalCostOfFlight, 600);
            Assert.AreEqual<double>(objFlight.lstPassengers.Count, 6);
            Assert.AreEqual<decimal>(objFlight.FlightAircraft.NoOfSeats, 12);
            Assert.AreEqual<decimal>(objFlight.MinTakeOffLoadPercent, 75);

            file = new System.IO.StreamReader(Directory.GetCurrentDirectory() + "\\input.txt");

            objFlight = handleFileData.ProcessFile(file);

            Assert.AreEqual<double>(objFlight.TotAdjRev, 1010);
            Assert.AreEqual<double>(objFlight.TotalCostOfFlight, 800);
            Assert.AreEqual<double>(objFlight.lstPassengers.Count, 8);
            Assert.AreEqual<decimal>(objFlight.FlightAircraft.NoOfSeats, 8);
            Assert.AreEqual<decimal>(objFlight.MinTakeOffLoadPercent, 75);


        }
    }
}
