using ParserBill.Models;
using ParserBill.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill
{
    public enum ParserType
    {
        A,B,C,D,E
    }
    public class ParserManager
    {
        private readonly IFileContext _fileContext;
        private List<BillItem> _billItems;
        private List<short> _deps;
        public ParserManager()
        {
            _fileContext = new FileContext();
            _billItems = new List<BillItem>();
            GetMockDeps();
        }

        private void GetMockDeps()
        {
            _deps = new List<short>();
            for (short i = 1; i < 75; i++)
            {
                _deps.Add(i);
            }

        }

        public List<BillItem> GetBillItems()
        {
            var workDir = _fileContext.WorkDir;
            var pattern = _fileContext.PatternFileName;

            var files = _fileContext.GetFileList(workDir, pattern);
            GetBillItems(files);
            return _billItems;
        }

        private void GetBillItems(string[] files)
        {
            foreach (var file in files)
            {
                List<BillItem> billItemsRange = GetBillItemsRange(file);
                _billItems.AddRange(billItemsRange);
            }
        }

        private List<BillItem> GetBillItemsRange(string file)
        {
            var rows = _fileContext.GetBillRows(file);
            ParserType parserType = GetPareserType(rows);
            short depId = GetDepIdMock();
            ParserCreator parserCreator = new ParserCreator(parserType, depId);
            List<BillItem> items = parserCreator.GetBillItem(rows);
            return items;
        }

        private short GetDepIdMock()
        {
            Random random = new Random();
            var i = random.Next(0, _deps.Count);
            var depId = _deps[i];
            _deps.RemoveAt(i);
            return depId;
        }

        private ParserType GetPareserType(string[] rows)
        {
            int count = 0;
            foreach (var row in rows)
            {
                if (row.Contains("ЧЕК"))
                {
                    

                    foreach (char c in row)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            count++;
                        }
                    }
                    if (count == 3)
                    {
                        return ParserType.B;
                    }
                    else
                    {
                        return ParserType.A;
                    }
                }
            }

            throw new ArgumentNullException();
        }
    }
}
