using ParserBill.Models;
using ParsingBill.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBill.Data
{
    public class BillRepository
    {
        public void AddBills(List<BillItem> billItems)
        {
            var bills = new List<Bill>();
            foreach (var item in billItems)
            {
                var bill = new Bill
                {
                    BillDate = item.BillDate,
                    BillNum = item.BillNum,
                    CardNum = item.CardNum,
                    DepId = item.DepId,
                    TermNum = item.TermNum,
                    Client = item.Client,
                    Sum = item.Sum
                };
                bills.Add(bill);
            }
            using (var context = new BillContext())
            {
                context.Bills.AddRange(bills);
                context.SaveChanges();
            }
        }
    }
}
