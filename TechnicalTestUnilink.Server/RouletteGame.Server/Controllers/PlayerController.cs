using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RouletteGame.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Threading.Tasks;
using System.Net;
using RouletteGame.Server.DTO;
using System.Web.Http.Cors;

namespace RouletteGame.Server.Controllers
{
    public class PlayerController : Controller
    {
        private readonly TechnicalTestUnilink_DBContext _dbContext;

        public PlayerController(TechnicalTestUnilink_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Player
        [HttpGet]
        public async Task<ActionResult> Get(string id)
        {
            var playerDTO = new PlayerDTO();

            try
            {
                var playerDb = await _dbContext.Player.FindAsync(id);

                if (playerDb == null)
                    return HttpNotFound("Player not found");

                playerDTO.PlayerName = playerDb.PlayerName;
                playerDTO.PlayerBalance = playerDb.PlayerBalance;

            } catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }

            return Json(new {
                name = playerDTO.PlayerName,
                balance = playerDTO.PlayerBalance
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> Save(PlayerDTO player)
        {
            try
            {
                var playerExist = await _dbContext.Player.FindAsync(player.PlayerName);

                if (playerExist != null)
                {
                    playerExist.PlayerBalance = player.PlayerBalance;
                    await _dbContext.SaveChangesAsync();

                    return new HttpStatusCodeResult(HttpStatusCode.NoContent);
                }
                else
                {
                    var playerDb = new Player
                    {
                        PlayerName = player.PlayerName,
                        PlayerBalance = player.PlayerBalance
                    };

                    _dbContext.Player.Add(playerDb);
                    await _dbContext.SaveChangesAsync();

                    return Json(new
                    {
                        name = playerDb.PlayerName,
                        balance = playerDb.PlayerBalance
                    });
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}