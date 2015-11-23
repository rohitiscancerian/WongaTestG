using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;
using System.IO;

namespace WongaTest
{
    class Program
    {
        static void Main(string[] args)
        {
           FileData objFileData = new FileData();
           AbstractFlight objFlight;

            Console.WriteLine("Please enter the txt file name placed inside bin->debug folder");
           

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader( Directory.GetCurrentDirectory() + "\\" + Console.ReadLine().Split('.')[0] + ".txt");

            Console.WriteLine("Please enter the output file name");
            
            objFlight = objFileData.ProcessFile(file,Console.ReadLine());
            file.Close();
        }

       
    }
}
