﻿using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Umpires
    {
        public Umpires()
        {
            Matches = new HashSet<Match>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalMatchStops { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
