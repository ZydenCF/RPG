using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class StrengthPotion : IUsable
    {
        private int strengthBoost;
        public StrengthPotion(int strengthBoost = 5) { this.strengthBoost = strengthBoost; }
        public string GetName() { return "Poción de Fuerza"; }
        public void Use(Character target)
        {
            var field = typeof(Character).GetField("attack",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                int currentAttack = (int)field.GetValue(target);
                field.SetValue(target, currentAttack + strengthBoost);
                Console.WriteLine(target.GetName() + " incrementa su ataque en " + strengthBoost + "!");
            }
        }
    }
}
