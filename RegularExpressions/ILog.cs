using System.IO;

namespace RegularExpressions
{
    interface ILog
    {
        void WriteFileInfo(FileInfo fileInf);

        void WriteSymbolsInfo(string infoAboutSymbols);

        void WriteScanResult(int countSymbolsIsReplaced, int countSymbolsNotReplaced);
    }
}
