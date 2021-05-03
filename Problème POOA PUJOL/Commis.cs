using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Commis : Personne, IComparable<Commis>
    {
        string etat;
        DateTime embauche;
        int nombreCommande;

        public Commis(string nom, string prenom, string adresse, string telephone, string etat, DateTime embauche) : base(nom, prenom, adresse, telephone)
        {
            this.etat = etat;
            this.embauche = embauche;
            this.nombreCommande = 0;
        }

        public string Etat
        { get { return this.etat; } set { this.etat = value; } }
        public DateTime Embauche
        { get { return this.embauche; } }
        public int NombreCommande
        { get { return this.nombreCommande; } set { this.nombreCommande = value; } }
        /// <summary>
        /// Permet d'avoir le détail d'un commis de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return base.ToString() + " " + this.etat + " " + this.embauche.ToShortDateString();
        }
        /// <summary>
        /// Implémentation de l'interface IComparable pour le tri des commis
        /// </summary>
        /// <param name="c"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareTo(Commis c)
        { return this.nom.CompareTo(c.Nom); }
        /// <summary>
        /// Méthode de comparaison par ville pour utiliser un Sort avec invocation pour le tri d'une liste de commis par ville
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareToVille(Commis c1, Commis c2)
        {
            string villec1 = "";
            string villec2 = "";
            for (int i = c1.Adresse.Length - 1; i >= 0; i--) //La ville est le dernier mot de l'adresse dont chaque element est separé par un espace
            {
                if (c1.Adresse[i].Equals(' ') == false)
                {
                    villec1 = c1.Adresse[i] + villec1;
                }
                else
                {
                    i = 0;
                }
            }

            for (int i = c2.Adresse.Length - 1; i >= 0; i--)
            {
                if (c2.Adresse[i].Equals(' ') == false)
                {
                    villec2 = c2.Adresse[i] + villec2;
                }
                else
                {
                    i = 0;
                }
            }
            return villec1.CompareTo(villec2);
        }
        /// <summary>
        /// Même chose que les fonctions précédentes mais par le nombre de commande
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareToNbCommande(Commis c1, Commis c2)
        {
            return c1.NombreCommande.CompareTo(c2.NombreCommande);
        }
    }
}
