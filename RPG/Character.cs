using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Character : ICombatant
    {
        protected string name;
        protected int health;
        protected int maxHealth;
        protected int attack;
        protected int defense;
        protected List<IUsable> inventory;

        public Character(string name, int health, int attack, int defense)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = health;
            this.attack = attack;
            this.defense = defense;
            this.inventory = new List<IUsable>();
        }

        public string GetName()
        {
            return name;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        public int GetAttack()
        {
            return attack;
        }

        public int GetDefense()
        {
            return defense;
        }

        public bool GetIsAlive()
        {
            return health > 0;
        }

        public List<IUsable> GetInventory()
        {
            return inventory;
        }

        public virtual void Attack(Character target)
        {
            if (!GetIsAlive() || !target.GetIsAlive()) return;

            int damage = Math.Max(1, attack - target.defense);
            target.TakeDamage(damage);
            Console.WriteLine(name + " ataca a " + target.name + " por " + damage + " de daño");
        }

        public virtual void TakeDamage(int damage)
        {
            health = Math.Max(0, health - damage);
            if (health == 0)
            {
                Console.WriteLine("Haz muerto!");
            }
        }

        public virtual void Heal(int amount)
        {
            health = Math.Min(maxHealth, health + amount);
            Console.WriteLine("Se curo " + amount + " puntos de vida!");
        }

        public void AddToInventory(IUsable item)
        {
            inventory.Add(item);
            Console.WriteLine("Obtuvo: " + item.GetName());
        }

        public abstract void DisplayStats();
    }
}
