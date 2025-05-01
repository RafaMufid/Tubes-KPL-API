using System.Text.Json;
using Tubes_API.Models;

namespace Tubes_API.Services;
public class MonsterService
{
    private readonly string _filePath = "C:\\Users\\ASUS\\OneDrive\\Documents\\Rafa Mufid\\Kuliah\\Semester 4\\KPL\\Tubes-API\\bin\\Debug\\net8.0\\monsters.json";

    private static List<Monster> monsterList = new List<Monster>();

    public MonsterService()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            monsterList = JsonSerializer.Deserialize<List<Monster>>(json)??new List<Monster>();
        }
        else
        {
            monsterList = new List<Monster>();
        }
    }

    private void SaveData()
    {
        string json = JsonSerializer.Serialize(monsterList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<Monster> GetAll() => monsterList;

    public Monster? GetById(int id) => monsterList.FirstOrDefault(m => m.id == id);

    public List<Monster> GetByRace(string race)
    {
        List<Monster> result = new List<Monster>();

        foreach (var monster in monsterList)
        {
            if (string.Equals(monster.race, race, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(monster);
            }
        }

        return result;
    }

    public void Add(Monster monster)
    {
        monster.id = monsterList.Any() ? monsterList.Max(m => m.id) + 1 : 1;
        monsterList.Add(monster);
        SaveData();
    }

    public bool Update(Monster updatedMonster)
    {
        var existingMonster = GetById(updatedMonster.id);
        if (existingMonster == null)
            return false;

        existingMonster.id = updatedMonster.id;
        existingMonster.name = updatedMonster.name;
        existingMonster.health = updatedMonster.health;
        existingMonster.race = updatedMonster.race;
        existingMonster.damage = updatedMonster.damage;

        SaveData();
        return true;
    }

    public bool Delete(int id)
    {
        var monster = GetById(id);
        if (monster == null)
        {
            return false;
        }

        monsterList.Remove(monster);
        SaveData();
        return true;
    }
}
