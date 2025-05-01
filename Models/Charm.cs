namespace Tubes_API.Models
{
    public class Charm
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string effect { get; set; }

        public Charm(int id, string name, int price, string effect)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.effect = effect;
        }
    }
}
