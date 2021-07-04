using ParserBill.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParserBill.Parsers
{
    public abstract class Parser
    {
        public string DepPattern { get; set; }
        public string BillNumPattern { get; set; }
        public string DatePattern { get; set; }
        public string TermPattern { get; set; }
        public string CardPattern { get; set; }
        public string ClientPattern { get; set; }
        public string SumPattern { get; set; }
        public short DepId { get; set; }

        public List<BillItem> BillItems { get; set; }
        public Parser(short depId)
        {
            DatePattern = @"^\d{2}[.]\d{2}[.]\d{2}\s{2}";
            BillNumPattern = @"[^\d\*]0\d{3}$";
            TermPattern = @"[0-9]{8}\W";
            CardPattern = @"[*]{12}\d{4}";
            ClientPattern = "Клиент";
            SumPattern = @"[0-9]{1,}[.][0-9]{2}$";
            DepId = depId;
        }

        public List<BillItem> GetBillItems(string[] rows)
        {
            List<BillItem> items = ParseBillRows(rows);
            return items;
        }

        public virtual List<BillItem> ParseBillRows(string[] rows)
        {
            List<BillItem> items = new List<BillItem>();
            BillItem billItem = null;

            foreach (var row in rows)
            {
                if (billItem == null)
                {
                    billItem = new BillItem();
                }

                ParseBillRow(row, ref billItem);

                if (billItem.IsCompleted)
                {
                    billItem.CheckValid();
                    if (billItem.IsValid)
                    {
                        billItem.DepId = DepId;
                        items.Add(billItem);
                    }
                    billItem = null;
                }
            }
            return items.Distinct().ToList();
        }

        public virtual void ParseBillRow(string row, ref BillItem billItem)
        {
            if (Regex.IsMatch(row,DatePattern))
            {
                try
                {
                    billItem.BillDate = GetBillDate(row);
                    billItem.BillNum = GetBillNum(row);
                }
                catch (Exception)
                {
                    billItem.IsValid = false;
                }
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

        public virtual string GetClient(string row)
        {
            return row.Substring(10).Trim();
        }

        public virtual string GetCardNum(string row)
        {
            return row.Substring(10).Trim();
        }

        public virtual int GetTermNum(string row)
        {
            return Convert.ToInt32(Regex.Match(row,TermPattern).ToString().Trim());
        }

        public virtual short GetBillNum(string row)
        {
            var billStr = Regex.Match(row, BillNumPattern).ToString().Trim();
            return Convert.ToInt16(billStr);
        }

        public virtual DateTime GetBillDate(string row)
        {
            var stringBuider = new StringBuilder();
            stringBuider.Append(Regex.Match(row,DatePattern).ToString().Trim());
            stringBuider.Append(' ');
            stringBuider.Append(Regex.Match(row,"[0-9]{2}[:][0-9]{2}").ToString().Trim());
            var dateStr =  stringBuider.ToString();
            return DateTime.ParseExact(dateStr, "dd.MM.yy HH:mm", null);
        }

        public virtual string GetDepName(string row)
        {
            return row.Trim();
        }

        public virtual decimal GetSumBill(string row)
        {
            var sumStr = Regex.Match(row, SumPattern).ToString();
            return Convert.ToDecimal(sumStr, new CultureInfo("en-US"));
        }
    }
}
