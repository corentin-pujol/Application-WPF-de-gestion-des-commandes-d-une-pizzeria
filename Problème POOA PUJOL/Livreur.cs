using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Livreur : Personne, IComparable<Livreur>
    {
        string etat;
        string moyen_de_transport;
        Facture facture_commande;
        int nombre_livraison;

        public Livreur(string nom, string prenom, string adresse, string telephone, string etat, string moyen_de_transport) : base(nom, prenom, adresse, telephone)
        {
            this.etat = etat;
            this.moyen_de_transport = moyen_de_transport;
            this.nombre_livraison = 0;
        }

        public string Etat
        { get { return this.etat; } set { this.etat = value; } }
        public string Moyen_de_transport
        { get { return this.moyen_de_transport; } }
        public Facture Facture_commande
        { get { return this.facture_commande; } set { this.facture_commande = value; } }
        public int Nombre_livraison
        { get { return this.nombre_livraison; } set { this.nombre_livraison = value; } }
        /// <summary>
        /// Permet d'avoir le détail d'un livreur de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return base.ToString() + " " + this.etat + " " + this.moyen_de_transport;
        }
        /// <summary>
        /// Permet d'associer une facture a un livreur
        /// </summary>
        /// <param name="c"></param>
        public void FactureLivraison(Commande c)
        {
            Facture f = new Facture(c, false);
            this.facture_commande = f;
        }
        /// <summary>
        /// Permet de remettre un commis disponible apres une livraison
        /// </summary>
        public void LivraisonTerminee()
        {
            this.nombre_livraison++;
            this.etat = "sur place";
        }
        /// <summary>
        /// Implémente l'interface IComparable pour le tri de livreurs
        /// </summary>
        /// <param name="l"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareTo(Livreur l)
        {
            return this.nom.CompareTo(l.Nom);
        }
        /// <summary>
        /// Méthode de comparaison par ville pour utiliser un Sort avec invocation pour le tri d'une liste de livreurs par ville
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>-1, 0 ou 1</returns>
        public int CompareToVille(Livreur c1, Livreur c2)
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
        /// Même méthode que pour les tris précédents mais en fonction du nombre de livraison
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public int CompareToNbLivraison(Livreur c1, Livreur c2)
        {
            return c1.Nombre_livraison.CompareTo(c2.Nombre_livraison);
        }
    }
}
