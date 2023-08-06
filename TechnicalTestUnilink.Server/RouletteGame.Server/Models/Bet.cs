using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouletteGame.Server.Models
{
    public class Bet
    {
        public bool win { get; set; }
        public string betType { get; set; }
        public double betAmount { get; set; }
    }
}