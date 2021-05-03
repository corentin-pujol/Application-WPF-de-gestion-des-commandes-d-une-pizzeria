using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problème_POOA_PUJOL
{
    public class Carte
    {
        SortedList<string, Pizza> pizzas;
        SortedList<string, Boisson> boissons;
        public Carte(string filename1,string filename2) //On définit la carte avec les prix des pizzas en fonction de leur taille et de leur type (idem avec les boissons).
        {
            SortedList<string, Pizza> pizzas_pizzeria = new SortedList<string, Pizza>();

            StreamReader fichLect1 = new StreamReader(filename1);
            char[] sep = new char[1] { ';' };
            string ligne = "";
            string[] datas_p = new string[3];
            while (fichLect1.Peek() > 0)
            {
                ligne = fichLect1.ReadLine(); //Lecture d'une ligne
                datas_p = ligne.Split(sep);
                string type = datas_p[0];
                string taille = datas_p[1];
                double prix = Convert.ToDouble(datas_p[2]);
                Pizza p = new Pizza(taille, type, prix);
                pizzas_pizzeria.Add(p.Type + " Small",p);
                Pizza m = new Pizza("Moyenne", type, prix + 1);
                pizzas_pizzeria.Add(m.Type + " Medium", m);
                Pizza g = new Pizza("Grande", type, prix + 2);
                pizzas_pizzeria.Add(g.Type + " Large", g);
                
            }
            fichLect1.Close();
            this.pizzas = pizzas_pizzeria;

            SortedList<string, Boisson> boissons_pizzeria = new SortedList<string, Boisson>();

            StreamReader fichLect2 = new StreamReader(filename2);
            string[] datas_b = new string[3];
            while (fichLect2.Peek() > 0)
            {
                ligne = fichLect2.ReadLine(); //Lecture d'une ligne
                datas_b = ligne.Split(sep);
                string type = datas_b[0];
                string taille = datas_b[1];
                double prix = Convert.ToDouble(datas_b[2]);
                Boisson b = new Boisson(taille, type, prix);
                boissons_pizzeria.Add(b.Type, b);
                Boisson B = new Boisson("70 cL", type, prix + 2);
                boissons_pizzeria.Add(B.Type + " XL", B);
            }
            fichLect2.Close();
            this.boissons = boissons_pizzeria;
        }

        public SortedList<string, Pizza> Pizzas
        { get { return this.pizzas; } }
        public SortedList<string, Boisson> Boissons
        { get { return this.boissons; } }
        /// <summary>
        /// Permet d'avoir le détail de la Carte de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            string result = "";
            Pizza pizzaRef = this.pizzas.First().Value;
            Boisson boissonRef = this.boissons.First().Value;
            foreach (KeyValuePair<string, Pizza> p in this.pizzas)
            {
                if(p.Value.Type == pizzaRef.Type)
                {
                    result += p.Value.ToString() + "\n";
                }
                else
                {
                    pizzaRef = p.Value;
                    result += "\n" + p.Value.ToString() + "\n";
                }
            }
            result += "\n\n";
            foreach (KeyValuePair<string, Boisson> b in this.boissons)
            {
                if (b.Value.Type == boissonRef.Type)
                {
                    result += b.Value.ToString() + "\n";
                }
                else
                {
                    boissonRef = b.Value;
                    result += "\n" + b.Value.ToString() + "\n";
                }
            }
            return result;
        }
    }
}
