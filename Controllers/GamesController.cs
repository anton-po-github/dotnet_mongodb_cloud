using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly GamesService _gameService;

    public GamesController(GamesService gamesService)
    {
        _gameService = gamesService;
    }

    [HttpGet]
    public ActionResult<List<Game>> Get() =>
        _gameService.Get();

    [HttpGet("{id:length(24)}", Name = "GetGame")]
    public ActionResult<Game> Get(string id)
    {
        var game = _gameService.Get(id);

        if (game == null)
        {
            return NotFound();
        }

        return game;
    }

    [HttpPost]
    public ActionResult<Game> Create(Game game)
    {
        _gameService.Create(game);

        return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
    }

    [HttpPut("{id:length(24)}")]
    public IActionResult Update(string id, Game newGame)
    {
        var oldGame = _gameService.Get(id);

        if (oldGame == null)
        {
            return NotFound();
        }

        _gameService.Update(oldGame, newGame);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var game = _gameService.Get(id);

        if (game == null)
        {
            return NotFound();
        }

        _gameService.Delete(game.Id);

        return NoContent();
    }
}
