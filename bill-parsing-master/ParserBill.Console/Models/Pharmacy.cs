using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Console.Models
{
    [Table("pharmacies")]
    public class Pharmacy
    {
        [Key]
        [Column("dep_id")]
        public short DepId { get; set; }
        [Column("ul")]
        [MaxLength(3)]
        public string Ul { get; set; }
        [Column("address")]
        [MaxLength(40)]
        public string Address { get; set; }

        public ICollection<Bill>Bills { get; set; }
    }
}
