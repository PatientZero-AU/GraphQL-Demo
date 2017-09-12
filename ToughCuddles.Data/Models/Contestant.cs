using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Contestant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public int DominantHand { get; set; }
        public decimal HeightCm { get; set; }
        public decimal WeightKg { get; set; }
        public decimal ReachCm { get; set; }
        public int StrikesMin { get; set; }
        public string ImageUrl { get; set; }

        public Team Team { get; set; }
    }
}
