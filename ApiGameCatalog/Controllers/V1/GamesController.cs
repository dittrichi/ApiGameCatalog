using ApiGameCatalog.Exceptions;
using ApiGameCatalog.InputModel;
using ApiGameCatalog.Services;
using ApiGameCatalog.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        /// <summary>
        /// Retrieve all games breaking by pages
        /// </summary>
        /// <remarks>
        /// It's not possible retrieve games without page breaks
        /// </remarks>
        /// <param name="page">Defines what page will be retrieved. Min. 1</param>
        /// <param name="amount">Defines maximum registers returned by page. Min. 1, Max. 50</param>
        /// <response code="200">Returns game list</response>
        /// <response code="204">When no game exists</response>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Retrieve([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {            
            var games = await _gameService.Retrieve(page, amount);
            if (games.Count() == 0)
                return NoContent();
            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Retrieve([FromRoute]Guid idGame)
        {
            var game = await _gameService.Retrieve(idGame);
            if (game == null)
                return NoContent();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody]GameInputModel gameInputModel)
        {
            try 
            {
                var game = await _gameService.Insert(gameInputModel);
                return Ok(game);
            }
            catch(GameAlreadyExistsException ex)
            {
                return UnprocessableEntity("A game with this name is already existent");
            }            
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute]Guid idGame, [FromBody]GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);
                return Ok();
            }
            catch(GameNotFoundException ex)
            {
                return NotFound("This game doesn't exists");
            }            
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute]Guid idGame, [FromRoute]double price)
        {
            try
            {
                await _gameService.Update(idGame, price);
                return Ok();
            }
            //catch(GameNotFoundException ex)
            catch (Exception ex)
            {
                return NotFound("This game doesn't exists");
            }            
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute]Guid idGame)
        {
            try
            {
                await _gameService.Remove(idGame);
                return Ok();
            }
            catch (GameNotFoundException ex)            
            {
                return NotFound("This game doesn't exists");
            }
        }
    }
}
