using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Boisson
    {
        string taille;
        string type;
        double prix;

        public Boisson(string taille, string type, double prix)
        {
            this.taille = taille;
            this.type = type;
            this.prix = prix;
        }

        public string Type
        { get { return this.type; } }
        public string Taille
        { get { return this.taille; } }
        public double Prix
        { get { return this.prix; } }
        /// <summary>
        /// Permet d'afficher le détail d'une boisson
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return this.type + " " + this.taille + " " + this.prix + "E";
        }
    }
}
