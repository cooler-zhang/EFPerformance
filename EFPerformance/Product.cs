using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPerformance
{
    public class Product
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        //public virtual Category Category { get; set; }
    }
}
