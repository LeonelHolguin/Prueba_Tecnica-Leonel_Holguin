using System;
using System.Collections.Generic;

namespace RouletteGame.Server.Models
{
    public partial class Player
    {
        public string PlayerName { get; set; }
        public decimal? PlayerBalance { get; set; }
    }
}
