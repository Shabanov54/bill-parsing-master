using ParserBill;
using ParsingBill.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBill
{
    class Program
    {
        static void Main(string[] args)
        {
            ParserManager parserManager = new ParserManager();
            var items = parserManager.GetBillItems();
            BillRepository repository = new BillRepository();
            repository.AddBills(items);
        }
    }
}
