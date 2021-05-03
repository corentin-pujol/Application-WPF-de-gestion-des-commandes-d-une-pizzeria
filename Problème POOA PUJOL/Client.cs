using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Client : Personne, IComparable<Client>
    {
        DateTime premiere_commande;

        public Client(string nom, string prenom, string adresse, string telephone, DateTime premiere_commande) : base(nom, prenom, adresse, telephone)
        {
            this.premiere_commande = premiere_commande;
        }

        public DateTime Premiere_commande
        { get { return this.premiere_commande; } }
        /// <summary>
        /// Permet d'avoir le détail d'un client de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return base.ToString() + " " + premiere_commande.ToShortDateString();
        }
        /// <summary>
        /// Méthode CompareTo implémentée pour l'utilisation de la fonction Sort() pour le tri d'une liste de clients
        /// </summary>
        /// <param name="c"></param>
        /// <returns>entier -1, 0 ou 1</returns>
        public int CompareTo(Client c)
        {
            return this.nom.CompareTo(c.Nom);
        }
        /// <summary>
        /// Méthode de comparaison par ville pour utiliser un Sort avec invocation pour le tri d'une liste de clients par adresse
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareToVille(Client c1, Client c2)
        {
            string villec1 = "";
            string villec2 = "";
            for(int i = c1.Adresse.Length - 1; i>=0;i--) //La ville est le dernier mot de l'adresse dont chaque element est separé par un espace
            {
                if(c1.Adresse[i].Equals(' ')==false)
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
        /// Methode de comparaison en fonction de l'ancienneté du client
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public int CompareToFirstOrder(Client c1, Client c2)
        {
            return c1.Premiere_commande.CompareTo(c2.Premiere_commande);
        }
    }
}
