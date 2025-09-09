using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Sword : Weapon
    {
        public Sword(int damage = 15) : base("Espada", damage) { }
        public override void Use(Character target)
        {
            Console.WriteLine("Usas la espada para un ataque especial!");
            target.TakeDamage(damage);
        }
    }
}
