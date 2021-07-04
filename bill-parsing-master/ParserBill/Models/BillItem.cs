using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Models
{
    public class BillItem
    {
        public short DepId { get; set; }
        public short BillNum { get; set; }
        public DateTime BillDate { get; set; }
        public int TermNum { get; set; }
        public string CardNum { get; set; }
        public string Client { get; set; }
        public decimal Sum { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsValid { get; set; } = true;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(DepId);
            stringBuilder.Append(';');
            stringBuilder.Append(BillNum);
            stringBuilder.Append(';');
            stringBuilder.Append(BillDate);
            stringBuilder.Append(';');
            stringBuilder.Append(TermNum);
            stringBuilder.Append(';');
            stringBuilder.Append(CardNum);
            stringBuilder.Append(';');
            stringBuilder.Append(Client);
            stringBuilder.Append(';');
            stringBuilder.Append(Sum);
            return stringBuilder.ToString();
        }

        public void CheckValid()
        {
            if (BillNum == 0)
            {
                IsValid = false;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var other = (BillItem)obj;
            return other.BillNum.Equals(BillNum);
        }

        public override int GetHashCode()
        {
            return BillNum.GetHashCode();
        }
    }
}
