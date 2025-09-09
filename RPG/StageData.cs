using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public struct StageData
    {
        public int stageNumber;
        public List<Enemy> enemies;
        public string description;
        public StageData(int number, List<Enemy> enemies, string description)
        {
            this.stageNumber = number;
            this.enemies = enemies;
            this.description = description;
        }
    }
}
