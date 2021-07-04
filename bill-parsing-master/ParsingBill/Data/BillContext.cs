using ParsingBill.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingBill.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class BillContext:DbContext
    {
        public DbSet<Bill> Bills { get; set; }

        public BillContext():base("conn")
        {

        }
    }
}
