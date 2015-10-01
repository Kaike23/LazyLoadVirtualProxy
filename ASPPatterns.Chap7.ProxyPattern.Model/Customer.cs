using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPPatterns.Chap7.ProxyPattern.Model
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
