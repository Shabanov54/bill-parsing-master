using ParserBill.Models;
using ParserBill.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Console.Data
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

        internal void RemoveBills(DateTime startDate, DateTime endDate)
        {
            using (var context = new BillContext())
            {
                var oldBills = context.Bills
                    .Where(b => b.BillDate >= startDate && b.BillDate <= endDate).ToList();
                context.Bills.RemoveRange(oldBills);
                context.SaveChanges();
            }
        }

        internal List<Bill> GetBills()
        {
            using (var context = new BillContext())
            {
                return context.Bills.ToList();
            }
        }
    }
}
