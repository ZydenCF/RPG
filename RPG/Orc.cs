using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Orc : Enemy
    {
        public Orc(int health, int attack, int defense)
            : base("Orc", health, attack, defense) { }

        public override void Attack(Character target)
        {
            Console.WriteLine(name + " hace un ataque poderoso!");
            int damage = Math.Max(1, (attack + 2) - target.GetDefense());
            target.TakeDamage(damage);
            Console.WriteLine(name + " ataca a " + target.GetName() + " por " + damage + " de daño!");
        }
    }
}
