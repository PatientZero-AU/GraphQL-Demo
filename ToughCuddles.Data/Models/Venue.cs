using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Venue
    {
        public Venue()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
