namespace Tubes_API.Models
{
    public class Monster
    {
        public int id { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public string race { get; set; }
        public int damage { get; set; }

        public Monster(int id, string name, int health, string race, int damage)
        {
            this.id = id;
            this.name = name;
            this.health = health;
            this.race = race;
            this.damage = damage;
        }
    }
}
