using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Ticket
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public decimal Price { get; set; }
        public string Seat { get; set; }
        public Guid VenueId { get; set; }
        public DateTime DateSold { get; set; }

        public Match Match { get; set; }
        public Venue Venue { get; set; }
    }
}
