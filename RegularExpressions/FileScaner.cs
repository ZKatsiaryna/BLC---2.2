using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RegularExpressions
{
    class FileScaner
    {
        public int countSymbolsIsReplaced { get; private set; }
        public int countSymbolsNotReplaced { get; private set; }
        public string ScanFilePath { get; private set; }
        public string MoveFilePath { get; private set; }

        private string StringWithMultipleSpaces { get; set; }
        private List<string> listChangeText { get; set; }
        private bool isReplaceable = false;
        private FileInfo FileInf { get; set; }
        ILog ConsoleLog { get; set; }

        public FileScaner(string scanFilePath, string moveFilePath, ILog consoleLog)
        {
            ScanFilePath = scanFilePath;
            MoveFilePath = moveFilePath;
            FileInf = new FileInfo(scanFilePath);
            ConsoleLog = consoleLog;
        }

        public void PrintFileInfo()
        {
            ConsoleLog.WriteFileInfo(FileInf);
        }

        public bool IsExistOtherSymbols()
        {
            ReadFile();
            var listText = StringExtensional.RemoveNewlines(StringWithMultipleSpaces);

            string pattern = "[А-Яа-яЁё]+";
            if (Regex.IsMatch(StringWithMultipleSpaces, pattern))
            {
                for (int i = 0; i < listText.Count; i++)
                {
                    foreach (Match match in Regex.Matches(listText[i], pattern))
                    {
                        string infoAboutSymbols = $" lN ={i + 1}, Start index= {match.Index}, Text = {match.Value}";
                        var strToChange = ReplaceSymbols(match.Value);
                        listText[i] = listText[i].Replace(match.Value, strToChange);
                        if (isReplaceable)
                        {
                            ConsoleLog.WriteSymbolsInfo(infoAboutSymbols + " - the value can be replaced");
                        }
                        else { ConsoleLog.WriteSymbolsInfo(infoAboutSymbols); }
                    }
                }
            }
            ConsoleLog.WriteScanResult(countSymbolsIsReplaced, countSymbolsNotReplaced);

            if (countSymbolsIsReplaced > 0)
            {
                listChangeText = listText;
                return true;
            }
            return false;
        }

        private string ReplaceSymbols(string strToReplace)
        {
            Translit symbolsDictionary = new Translit();

            StringBuilder replaceString = new StringBuilder();
            foreach (var symbol in strToReplace)
            {
                var value = "";

                if (symbolsDictionary.russianToEnglishsymbols.TryGetValue(symbol.ToString(), out value))
                {
                    countSymbolsIsReplaced++;
                    replaceString.Append(value);
                    isReplaceable = true;
                }
                else
                {
                    countSymbolsNotReplaced++;
                    replaceString.Append(symbol);
                    isReplaceable = false;
                }
            }

            return replaceString.ToString();
        }

        public void MoveFile()
        {
            File.Move(ScanFilePath, Path.Combine(MoveFilePath, FileInf.Name));
        }

        public void SaveChanges()
        {
            using (StreamWriter sw = new StreamWriter(ScanFilePath, false, Encoding.Default))
            {
                var result = String.Join(Environment.NewLine,
                         listChangeText.Select(a => String.Join(", ", a)));
                sw.Write(result);
            }
        }

        private string ReadFile()
        {
            using (StreamReader streamReader = new StreamReader(ScanFilePath, Encoding.Default))
            {
                StringWithMultipleSpaces = streamReader.ReadToEnd();
            };
            return StringWithMultipleSpaces;
        }
    }
}
