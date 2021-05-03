using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public abstract class Personne
    {
        protected string nom;
        protected string prenom;
        protected string adresse;
        protected string telephone;

        public Personne(string nom, string prenom, string adresse, string telephone)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.telephone = telephone;
        }

        public string Nom
        { get { return this.nom; } set { this.nom = value; } }
        public string Prenom
        { get { return this.prenom; } set { this.prenom = value; } }
        public string Adresse
        { get { return this.adresse; } set { this.adresse = value; } }
        public string Telephone
        { get { return this.telephone; } set { this.telephone = value; } }
        /// <summary>
        /// Permet d'avoir le détail d'une personne de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return this.nom + " " + this.prenom + " " + this.adresse + " " + this.telephone;
        }
    }
}
