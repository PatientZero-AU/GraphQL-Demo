using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Venues
    {
        public Venues()
        {
            Tickets = new HashSet<Tickets>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<Tickets> Tickets { get; set; }
    }
}
