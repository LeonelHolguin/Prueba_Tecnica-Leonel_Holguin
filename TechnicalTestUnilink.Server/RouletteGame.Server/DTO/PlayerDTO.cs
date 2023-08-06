using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouletteGame.Server.DTO
{
    public class PlayerDTO
    {
        [Required]
        public string PlayerName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? PlayerBalance { get; set; }
    }
}