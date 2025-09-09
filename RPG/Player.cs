using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Player : Character
    {
        public Player(string name, int health, int attack, int defense)
            : base(name, health, attack, defense) { }

        public override void DisplayStats()
        {
            Console.WriteLine("Jugador" + name);
            Console.WriteLine("Vida: " + health + "/" + maxHealth);
            Console.WriteLine("Ataque: " + attack);
            Console.WriteLine("Defensa: " + defense);
            Console.WriteLine("Items: " + inventory.Count);
        }

        public void UseItem(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < inventory.Count)
            {
                IUsable item = inventory[itemIndex];
                item.Use(this);
                inventory.RemoveAt(itemIndex);
            }
        }

        public void ListInventory()
        {
            Console.WriteLine("Inventario");
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].GetName());
            }
        }
    }
}
