using System;
using System.Collections.Generic;
using System.IO;

namespace OrganizationPanel
{
    public class CsvFile
    {
        public string SourceCsvPath { get; }
        public string TargetCsvPath { get; }
        private char _divider;
        public CsvFile(string readFromPath, string writeToPath, char divider)
        {
            SourceCsvPath = readFromPath;
            TargetCsvPath = writeToPath;
            _divider = divider;
        }
        
        public List<string[]> ReadAllLines()
        {
            return ReadAllLines(SourceCsvPath);
        }

        public List<string[]> ReadAllLines(string pathToFile)
        {
            using (var fr = new FileStream(pathToFile, FileMode.Open, FileAccess.Read, FileShare.Read, 1024))
            {
                var linesOfCsv = new List<string[]>();
                using (var sr = new StreamReader(fr))
                {
                    while (!sr.EndOfStream)
                    {
                        var lineOfCsv = sr.ReadLine();
                        var fieldsOfLine = lineOfCsv?.Split(_divider);
                        linesOfCsv.Add(fieldsOfLine);
                    }
                }

                return linesOfCsv;
            }
        }

        public void WriteLine(string[] fieldsOfLine, char divider)
        {
            using (var fr = new FileStream(TargetCsvPath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                using (var sw = new StreamWriter(fr))
                {
                    sw.WriteLine(String.Join(divider, fieldsOfLine));
                }
            }
        }
    }
}