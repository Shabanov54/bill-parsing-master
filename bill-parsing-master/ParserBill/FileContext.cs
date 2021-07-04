using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill
{
    public class FileContext : IFileContext
    {
        private readonly string _workDir = @"D:\Projects\Bills";
        private readonly string _patternFileName = "*paycard*";
        public string WorkDir { get; }
        public string PatternFileName { get; }

        public FileContext()
        {
            WorkDir = _workDir;
            PatternFileName = _patternFileName;
        }

        public string[] GetFileList(string workDir, string patternFileName)
        {
            return Directory.GetFiles(workDir, patternFileName);
        }

        public string[] GetBillRows(string fileName)
        {
            return File.ReadAllLines(fileName,Encoding.Default);
        }
    }
}
