using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class HealthPotion : IUsable
    {
        private int healAmount;
        public HealthPotion(int healAmount = 30) { this.healAmount = healAmount; }
        public string GetName() { return "Poción de Vida"; }
        public void Use(Character target) { target.Heal(healAmount); }
    }
}
