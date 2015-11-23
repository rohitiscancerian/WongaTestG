using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WongaTest.Abstractions;
using System.IO;

namespace WongaTest.Interfaces
{
    public interface IFileData
    {
        AbstractFlight ProcessFile(StreamReader file, string outputFilename);
    }
}
