using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class RPGGame
    {
        private Player player;
        private List<StageData> stages;
        private Dictionary<string, Func<int, int, int, Enemy>> enemyFactory;
        private Dictionary<string, Func<IUsable>> itemFactory;
        private Random random;

        public RPGGame()
        {
            stages = new List<StageData>();
            random = new Random();
            InitializeFactories();
        }

        private void InitializeFactories()
        {
            enemyFactory = new Dictionary<string, Func<int, int, int, Enemy>>
        {
            { "goblin", (h, a, d) => new Goblin(h, a, d) },
            { "orc", (h, a, d) => new Orc(h, a, d) }
        };

            itemFactory = new Dictionary<string, Func<IUsable>>
        {
            { "health", () => new HealthPotion() },
            { "strength", () => new StrengthPotion() },
            { "sword", () => new Sword() },
            { "bow", () => new Bow() }
        };
        }

        public void StartGame()
        {
            try
            {
                Console.WriteLine("STAR ===");
                SetupEnemiesAndItems();
                SetupStages();
                CreatePlayer();
                PlayGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el juego: " + ex.Message);
            }
        }

        private void SetupEnemiesAndItems()
        {
            Console.WriteLine("CONFIGURACIÓN DE ENEMIGOS");
            Console.WriteLine("Tipos disponibles: goblin, orc");
            Console.WriteLine("Items disponibles: health, strength, sword, bow");
        }

        private void SetupStages()
        {
            Console.WriteLine("¿Cuántos stages quieres crear?");
            int numStages;
            if (!int.TryParse(Console.ReadLine(), out numStages) || numStages < 1)
            {
                numStages = 3;
                Console.WriteLine("Usando 3 stages por defecto.");
            }

            for (int i = 0; i < numStages; i++)
            {
                Console.WriteLine("CONFIGURANDO STAGE:" + (i + 1));
                List<Enemy> enemies = new List<Enemy>();

                Console.WriteLine("¿Cuántos enemigos en este stage?");
                int numEnemies;
                if (!int.TryParse(Console.ReadLine(), out numEnemies) || numEnemies < 1) numEnemies = 1;

                for (int j = 0; j < numEnemies; j++)
                {
                    Console.WriteLine("Enemigo " + (j + 1) + " - Tipo (goblin/orc):");
                    string enemyType = Console.ReadLine().ToLower();
                    if (!enemyFactory.ContainsKey(enemyType)) enemyType = "goblin";

                    Console.WriteLine("Vida:");
                    int health;
                    int.TryParse(Console.ReadLine(), out health);
                    if (health <= 0) health = 50;

                    Console.WriteLine("Ataque:");
                    int attack;
                    int.TryParse(Console.ReadLine(), out attack);
                    if (attack <= 0) attack = 10;

                    Console.WriteLine("Defensa:");
                    int defense;
                    int.TryParse(Console.ReadLine(), out defense);
                    if (defense < 0) defense = 2;

                    Enemy enemy = enemyFactory[enemyType](health, attack, defense);

                    Console.WriteLine("¿Qué item dropeará? (health/strength/sword/bow):");
                    string itemType = Console.ReadLine().ToLower();
                    if (itemFactory.ContainsKey(itemType)) enemy.AddDropItem(itemFactory[itemType]());

                    enemies.Add(enemy);
                }

                stages.Add(new StageData(i + 1, enemies, "Stage " + (i + 1)));
            }
        }

        private void CreatePlayer()
        {
            Console.WriteLine(" CREACIÓN DEL JUGADOR");
            Console.WriteLine("Nombre del jugador:");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "Héroe";

            int health = GetValidatedStat("Vida", 50, 200);
            int attack = GetValidatedStat("Ataque", 10, 50);
            int defense = GetValidatedStat("Defensa", 1, 20);

            player = new Player(name, health, attack, defense);
            Console.WriteLine("¡Jugador creado exitosamente!");
            player.DisplayStats();
        }

        private int GetValidatedStat(string statName, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(statName + " (" + min + "-" + max + "):");
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    return value;
                Console.WriteLine("Valor inválido. Debe estar entre " + min + " y " + max + ".");
            }
        }

        private void PlayGame()
        {
            Console.WriteLine(" INICIANDO AVENTURA ");
       

            foreach (StageData stage in stages)
            {
                Console.WriteLine(stage.description);
                if (!PlayStage(stage))
                {

                    Console.WriteLine("El jugador ha muerto. ¡Mejor suerte la próxima vez!");
                    return;
                }

                if (stage.stageNumber < stages.Count)
                {
                    Console.WriteLine("Stage completado! Presiona Enter para continuar...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("¡Has completado todos los stages! ¡Eres un verdadero héroe!");
        }

        private bool PlayStage(StageData stage)
        {
            List<Enemy> currentEnemies = new List<Enemy>(stage.enemies);

            while (currentEnemies.Any(e => e.GetIsAlive()) && player.GetIsAlive())
            {
                if (!PlayerTurn(currentEnemies)) return false;

                List<Enemy> deadEnemies = currentEnemies.Where(e => !e.GetIsAlive()).ToList();
                foreach (Enemy deadEnemy in deadEnemies)
                {
                    foreach (IUsable item in deadEnemy.GetDropItems()) player.AddToInventory(item);
                    currentEnemies.Remove(deadEnemy);
                }

                EnemyTurn(currentEnemies);
            }

            return player.GetIsAlive();
        }

        private bool PlayerTurn(List<Enemy> enemies)
        {
            if (!player.GetIsAlive()) return false;

            Console.WriteLine("TU TURNO");
            player.DisplayStats();

            Enemy[] aliveEnemies = enemies.Where(e => e.GetIsAlive()).ToArray();
            if (aliveEnemies.Length == 0) return true;

            Console.WriteLine("\nEnemigos vivos:");
            for (int i = 0; i < aliveEnemies.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + aliveEnemies[i].GetName() + " (Vida: " + aliveEnemies[i].GetHealth() + ")");
            }

            Console.WriteLine("¿Qué quieres hacer?");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Usar item");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1: return HandlePlayerAttack(aliveEnemies);
                    case 2: return HandlePlayerUseItem();
                    default:
                        Console.WriteLine("Opción inválida. Perdiste tu turno.");
                        return true;
                }
            }

            Console.WriteLine("Opción inválida. Perdiste tu turno.");
            return true;
        }

        private bool HandlePlayerAttack(Enemy[] aliveEnemies)
        {
            Console.WriteLine("¿A quién atacar?");
            for (int i = 0; i < aliveEnemies.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + aliveEnemies[i].GetName());
            }

            int targetIndex;
            if (int.TryParse(Console.ReadLine(), out targetIndex) &&
                targetIndex > 0 && targetIndex <= aliveEnemies.Length)
            {
                Enemy target = aliveEnemies[targetIndex - 1];
                player.Attack(target);
            }
            else
            {
                Console.WriteLine("Objetivo inválido. Perdiste tu turno.");
            }

            return player.GetIsAlive();
        }

        private bool HandlePlayerUseItem()
        {
            if (player.GetInventory().Count == 0)
            {
                Console.WriteLine("No tienes items para usar.");
                return true;
            }

            player.ListInventory();
            Console.WriteLine("¿Qué item usar? (número):");

            int itemIndex;
            if (int.TryParse(Console.ReadLine(), out itemIndex) &&
                itemIndex > 0 && itemIndex <= player.GetInventory().Count)
            {
                player.UseItem(itemIndex - 1);
            }
            else
            {
                Console.WriteLine("Item inválido. Perdiste tu turno.");
            }

            return player.GetIsAlive();
        }

        private void EnemyTurn(List<Enemy> enemies)
        {
            if (!player.GetIsAlive()) return;

            Console.WriteLine(" TURNO DE LOS ENEMIGOS");

            List<Enemy> aliveEnemies = enemies.Where(e => e.GetIsAlive()).ToList();

            foreach (Enemy enemy in aliveEnemies)
            {
                if (!player.GetIsAlive()) break;
                enemy.Attack(player);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
