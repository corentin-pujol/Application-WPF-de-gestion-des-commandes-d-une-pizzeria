using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizzeria ChezBrice = new Pizzeria("Commis.csv", "Livreur.csv", "Clients.csv", "Commandes.csv", "Pizzas.csv", "Boissons.csv");
            Console.WriteLine(ChezBrice.ToString());
            Console.ReadKey();
        }
    }
}
