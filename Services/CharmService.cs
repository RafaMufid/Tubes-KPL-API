using System.Text.Json;
using System.Xml.Linq;
using Tubes_API.Models;

namespace Tubes_API.Services;

public class CharmService
{
    private readonly string _filePath = "C:\\Users\\ASUS\\OneDrive\\Documents\\Rafa Mufid\\Kuliah\\Semester 4\\KPL\\Tubes-API\\bin\\Debug\\net8.0\\charm.json";

    private static List<Charm> charmList = new List<Charm>();

    public CharmService()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            charmList = JsonSerializer.Deserialize<List<Charm>>(json) ?? new List<Charm>();
        }
        else
        {
            charmList = new List<Charm>();
        }
    }

    private void SaveData()
    {
        string json = JsonSerializer.Serialize(charmList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<Charm> GetAll() => charmList;

    public Charm? GetById(int id) => charmList.FirstOrDefault(c => c.id == id);

    public void Add(Charm charm)
    {
        charm.id = charmList.Any() ? charmList.Max(w => w.id) + 1 : 1;
        charmList.Add(charm);
        SaveData();
    }

    public bool Update(Charm updatedCharm)
    {
        var existingCharm = GetById(updatedCharm.id);
        if (existingCharm == null)
            return false;

        existingCharm.id = updatedCharm.id;
        existingCharm.name = updatedCharm.name;
        existingCharm.price = updatedCharm.price;
        existingCharm.effect = updatedCharm.effect;

        SaveData();
        return true;
    }

    public bool Delete(int id)
    {
        var charm = GetById(id);
        if (charm == null)
        {
            return false;
        }

        charmList.Remove(charm);
        SaveData();
        return true;
    }
}
