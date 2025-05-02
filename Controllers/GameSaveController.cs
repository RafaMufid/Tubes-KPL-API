using Microsoft.AspNetCore.Mvc;
using Tubes_API.Models;
using Tubes_API.Services;

namespace Tubes_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameSaveController : ControllerBase
    {
        private readonly GameSaveService _service = new();

        [HttpGet("{slot}")]
        public ActionResult<GameState> LoadGame(int slot)
        {
            var state = _service.LoadGame(slot);
            if (state == null)
                return NotFound($"No save found for slot {slot}");
            return Ok(state);
        }

        [HttpPost("{slot}")]
        public IActionResult SaveGame(int slot, [FromBody] GameState state)
        {
            _service.SaveGame(state, slot);
            return Ok("Game saved successfully");
        }

        [HttpDelete("{slot}")]
        public IActionResult DeleteGame(int slot)
        {
            bool deleted = _service.DeleteSave(slot);
            if (!deleted)
                return NotFound($"No save file found in slot {slot} to delete.");

            return Ok($"Save slot {slot} deleted.");
        }

    }
}
