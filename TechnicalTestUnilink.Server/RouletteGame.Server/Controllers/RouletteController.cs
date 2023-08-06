using RouletteGame.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace RouletteGame.Server.Controllers
{
    public class RouletteController : Controller
    {
        // GET: Randomizer
        [HttpGet]
        public ActionResult GetNumberAndColor()
        {
            Random RandomizerNumber = new Random();

            string[] arrayColors = new string[2] { "red", "black" };

            int randomNumber = RandomizerNumber.Next(0, 36 + 1);
            string randomColor = arrayColors[RandomizerNumber.Next(0, 1 + 1)];

            return Json(new { 
                number = randomNumber,
                color = randomColor,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Prize(Bet userBet)
        {
            double prize = 0;
            if (!userBet.win)
                return Json(new { prize });


            switch(userBet.betType)
            {
                case "color":
                    prize = userBet.betAmount / 2;
                    break;
                case "evenOddForColor":
                    prize = userBet.betAmount;
                    break;
                case "numberAndColor":
                    prize = userBet.betAmount * 3;
                    break;
                default:
                    break;
            } 

            return Json(new { prize });
        }
    }
}