using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public abstract class Weapon : IUsable
    {
        protected int damage;
        protected string weaponName;
        public Weapon(string name, int damage) { this.weaponName = name; this.damage = damage; }
        public string GetName() { return weaponName; }
        public abstract void Use(Character target);
    }
}
