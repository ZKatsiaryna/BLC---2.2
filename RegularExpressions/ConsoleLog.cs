using System;
using System.IO;

namespace RegularExpressions
{
    class ConsoleLog: ILog
    {
        public void WriteFileInfo (FileInfo fileInf)
        {
            Console.WriteLine($"Name = {fileInf.Name}, file extensional = {fileInf.Extension}, size = {fileInf.Length} ");
        }

        public void WriteScanResult(int countSymbolsIsReplaced, int countSymbolsNotReplaced)
        {
            Console.WriteLine($"Number of Russian sumbols = {countSymbolsIsReplaced + countSymbolsNotReplaced}, the {countSymbolsIsReplaced} symbols can be reaplaced");
        }

        public void WriteSymbolsInfo(string infoAboutSymbols)
        {
            Console.WriteLine(infoAboutSymbols);
        }


    }
}
