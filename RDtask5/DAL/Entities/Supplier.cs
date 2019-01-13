using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Supplier
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

    }
}
