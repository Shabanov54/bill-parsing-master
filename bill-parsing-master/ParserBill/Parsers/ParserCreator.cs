using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserBill.Models;

namespace ParserBill.Parsers
{
    public class ParserCreator
    {
        ParserType _parserType;
        short _depId;
        public ParserCreator(ParserType parserType, short depId)
        {
            _parserType = parserType;
            _depId = depId;
        }
        internal List<BillItem> GetBillItem(string[] rows)
        {
            Parser parser;
            switch (_parserType)
            {
                case ParserType.A:
                    parser = new ParserA(_depId);
                    return parser.GetBillItems(rows);
                case ParserType.B:
                    parser = new ParserB(_depId);
                    return parser.GetBillItems(rows);
                case ParserType.C:
                    parser = new ParserC(_depId);
                    return parser.GetBillItems(rows);
                default:
                    break;
            }
            return null;
        }
    }
}
