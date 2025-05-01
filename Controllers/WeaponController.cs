using Microsoft.AspNetCore.Mvc;
using Tubes_API.Models;
using Tubes_API.Services;

namespace Tubes_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeaponController : ControllerBase
{
    private readonly WeaponService _weaponService;
    public WeaponController(WeaponService weaponService)
    {
        _weaponService = weaponService;
    }

    [HttpGet]
    public ActionResult<List<Weapon>> GetAll()
    {
        return _weaponService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Weapon> GetById(int id)
    {
        var weapon = _weaponService.GetById(id);
        if (weapon == null)
            return NotFound();
        return weapon;
    }

    [HttpGet("type/{type}")]
    public ActionResult<List<Weapon>> GetByRace(string type)
    {
        var weapons = _weaponService.GetByType(type);
        if (weapons == null || weapons.Count == 0)
            return NotFound();

        return weapons;
    }

    [HttpPost]
    public ActionResult Add(Weapon weapon)
    {
        _weaponService.Add(weapon);
        return CreatedAtAction(nameof(GetById), new { id = weapon.id }, weapon);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Weapon weapon)
    {
        if (id != weapon.id)
            return BadRequest();

        bool success = _weaponService.Update(weapon);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        bool success = _weaponService.Delete(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
