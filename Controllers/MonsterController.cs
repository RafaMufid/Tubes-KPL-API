using Microsoft.AspNetCore.Mvc;
using Tubes_API.Models;
using Tubes_API.Services;

namespace Tubes_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MonsterController : ControllerBase
{
    private readonly MonsterService _monsterService;
    public MonsterController(MonsterService monsterService)
    {
        _monsterService = monsterService;
    }


    [HttpGet]
    public ActionResult<List<Monster>> GetAll()
    {
        return _monsterService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Monster> GetById(int id)
    {
        var monster = _monsterService.GetById(id);
        if (monster == null)
            return NotFound();
        return monster;
    }

    [HttpGet("type/{race}")]
    public ActionResult<List<Monster>> GetByRace(string race)
    {
        var monsters = _monsterService.GetByRace(race);
        if (monsters == null || monsters.Count == 0)
            return NotFound();

        return monsters;
    }

    [HttpPost]
    public ActionResult Add(Monster monster)
    {
        _monsterService.Add(monster);
        return CreatedAtAction(nameof(GetById), new { id = monster.id }, monster);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Monster monster)
    {
        if (id != monster.id)
            return BadRequest();

        bool success = _monsterService.Update(monster);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        bool success = _monsterService.Delete(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
