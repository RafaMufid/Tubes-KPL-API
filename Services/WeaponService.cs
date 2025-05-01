using System.Text.Json;
using Tubes_API.Models;

namespace Tubes_API.Services;

public class WeaponService
{
    private readonly string _filePath = "C:\\Users\\ASUS\\OneDrive\\Documents\\Rafa Mufid\\Kuliah\\Semester 4\\KPL\\Tubes-API\\bin\\Debug\\net8.0\\weapons.json";
    
    private static List<Weapon> weaponList = new List<Weapon>();

    public WeaponService()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            weaponList = JsonSerializer.Deserialize<List<Weapon>>(json) ?? new List<Weapon>();
        }
        else
        {
            weaponList = new List<Weapon>();
        }
    }

    private void SaveData()
    {
        string json = JsonSerializer.Serialize(weaponList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public List<Weapon> GetAll() => weaponList;

    public Weapon? GetById(int id) => weaponList.FirstOrDefault(w => w.id == id);

    public List<Weapon> GetByType(string type)
    {
        List<Weapon> result = new List<Weapon>();

        foreach (var weapon in weaponList)
        {
            if (string.Equals(weapon.type, type, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(weapon);
            }
        }

        return result;
    }

    public void Add(Weapon weapon)
    {
        weapon.id = weaponList.Any() ? weaponList.Max(w => w.id) + 1 : 1;
        weaponList.Add(weapon);
        SaveData();
    }

    public bool Update(Weapon updatedWeapon)
    {
        var existingWeapon = GetById(updatedWeapon.id);
        if (existingWeapon == null)
            return false;

        existingWeapon.id = updatedWeapon.id;
        existingWeapon.name = updatedWeapon.name;
        existingWeapon.type = updatedWeapon.type;
        existingWeapon.price = updatedWeapon.price;
        existingWeapon.baseDamage = updatedWeapon.baseDamage;

        SaveData();
        return true;
    }

    public bool Delete(int id)
    {
        var weapon = GetById(id);
        if (weapon == null)
        {
            return false;
        }

        weaponList.Remove(weapon);
        SaveData(); 
        return true;
    }
}
