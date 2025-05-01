namespace Tubes_API.Models
{
    public class Weapon
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public int baseDamage { get; set; }

        public Weapon(int id, string name, string type, int price, int baseDamage)
        {
            this.id = id;
            this.name = name;
            this.type = type ;
            this.price = price;
            this.baseDamage = baseDamage;
        }
    }
}
