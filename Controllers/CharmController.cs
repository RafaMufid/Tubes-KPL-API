using Microsoft.AspNetCore.Mvc;
using Tubes_API.Models;
using Tubes_API.Services;

namespace Tubes_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharmController : ControllerBase
{
    private readonly CharmService _charmService;
    public CharmController(CharmService charmService)
    {
        _charmService = charmService;
    }

    [HttpGet]
    public ActionResult<List<Charm>> GetAll()
    {
        return _charmService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Charm> GetById(int id)
    {
        var charm = _charmService.GetById(id);
        if (charm == null)
            return NotFound();
        return charm;
    }

    [HttpPost]
    public ActionResult Add(Charm charm)
    {
        _charmService.Add(charm);
        return CreatedAtAction(nameof(GetById), new { id = charm.id }, charm);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Charm charm)
    {
        if (id != charm.id)
            return BadRequest();

        bool success = _charmService.Update(charm);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        bool success = _charmService.Delete(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
