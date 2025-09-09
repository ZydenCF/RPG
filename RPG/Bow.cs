using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Bow : Weapon
    {
        public Bow(int damage = 12) : base("Arco", damage) { }
        public override void Use(Character target)
        {
            Console.WriteLine("Disparas una flecha certera!");
            target.TakeDamage(damage);
        }
    }
}
