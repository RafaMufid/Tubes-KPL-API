using System.Text.Json;
using Tubes_API.Models;

namespace Tubes_API.Services
{
    public class GameSaveService
    {
        private string GetSaveFilePath(int slot)
        {
            return $"SaveFiles/savegame_slot{slot}.json";
        }

        public GameState? LoadGame(int slot)
        {
            var path = GetSaveFilePath(slot);
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<GameState>(json);
        }

        public void SaveGame(GameState state, int slot)
        {
            Directory.CreateDirectory("SaveFiles");
            var json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetSaveFilePath(slot), json);
        }

        public bool DeleteSave(int slot)
        {
            var path = GetSaveFilePath(slot);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
