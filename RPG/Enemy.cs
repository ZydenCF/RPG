using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Enemy : Character
    {
        protected List<IUsable> dropItems;

        public Enemy(string name, int health, int attack, int defense)
            : base(name, health, attack, defense)
        {
            dropItems = new List<IUsable>();
        }

        public List<IUsable> GetDropItems()
        {
            return dropItems;
        }

        public void AddDropItem(IUsable item)
        {
            dropItems.Add(item);
        }

        public override void DisplayStats()
        {
            Console.WriteLine("Enmigo" + name);
            Console.WriteLine("Vida: " + health + "/" + maxHealth);
            Console.WriteLine("Ataque: " + attack);
            Console.WriteLine("Defensa: " + defense);
        }
    }
}
