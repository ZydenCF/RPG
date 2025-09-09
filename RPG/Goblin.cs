using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Goblin : Enemy
    {
        public Goblin(int health, int attack, int defense)
            : base("Goblin", health, attack, defense) { }

        public override void Attack(Character target)
        {
            Console.WriteLine(name + " hace un ataque rápido!");
            base.Attack(target);
        }
    }
}
