using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RPG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                RPGGame game = new RPGGame();
                game.StartGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error crítico: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Presiona cualquier tecla para salir");
                Console.ReadKey();
            }
        }
    }
}
