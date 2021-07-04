using ParserBill.Console.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserBill.Console.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BillContext:DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

        public BillContext():base("conn")
        {

        }
    }
}
