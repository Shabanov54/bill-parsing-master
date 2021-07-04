using ParserBill.Models;
using System.Text.RegularExpressions;

namespace ParserBill.Parsers
{
    public class ParserB:Parser
    {
        public ParserB(short depId):base(depId)
        {

        }

        public override void ParseBillRow(string row, ref BillItem billItem)
        {
            
            if (Regex.IsMatch(row,DatePattern))
            {
                billItem.BillDate = GetBillDate(row);
                return;
            }
            if (Regex.IsMatch(row,BillNumPattern))
            {
                billItem.BillNum = GetBillNum(row);
                return;
            }
            if (Regex.IsMatch(row,TermPattern))
            {
                billItem.TermNum = GetTermNum(row);
                return;
            }
            if (Regex.IsMatch(row,CardPattern))
            {
                billItem.CardNum = GetCardNum(row);
                return;
            }
            if (row.Contains(ClientPattern))
            {
                billItem.Client = GetClient(row);
                return;
            }
            if (Regex.IsMatch(row,SumPattern))
            {
                billItem.Sum = GetSumBill(row);
                billItem.IsCompleted = true;
                return;
            }
        }
    }
}
