using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public interface ICombatant
    {
        void Attack(Character target);
        bool GetIsAlive();
        string GetName();
    }
}
