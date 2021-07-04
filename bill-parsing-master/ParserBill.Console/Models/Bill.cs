using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Console.Models
{
    [Table("bills")]
    public class Bill
    {
        [Column("bill_id")]
        public int BillId { get; set; }
        [Column("dep_id")]
        [ForeignKey("Pharmacy")]
        public short DepId { get; set; }
        public Pharmacy Pharmacy { get; set; }
        [Column("bill_num")]
        public short BillNum { get; set; }
        [Column("bill_date")]
        public DateTime BillDate { get; set; }
        [Column("term_num")]
        public int TermNum { get; set; }
        [Column("card_num")]
        public string CardNum { get; set; }
        [Column("client")]
        public string Client { get; set; }
        [Column("sum")]
        public decimal Sum { get; set; }
    }
}
