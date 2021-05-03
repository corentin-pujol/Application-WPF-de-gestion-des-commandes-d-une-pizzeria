using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Pizza
    {
        string taille;
        string type;
        double prix;

        public Pizza(string taille, string type, double prix)
        {
            this.taille = taille;
            this.type = type;
            this.prix = prix;
        }

        public string Type
        { get { return this.type; } }
        public string Taille
        { get { return this.taille; } set { this.taille = value; } }
        public double Prix
        { get { return this.prix; } set { this.prix = value; } }
        /// <summary>
        /// Permet d'avoir le détail d'une pizza de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return this.type + " " + this.taille + " " + this.prix + "E";
        }
    }
}
