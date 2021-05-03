using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Commande
    {
        List<Pizza> pizzas;
        List<Boisson> boissons;
        string no_commande;
        string telephone_client;
        string nom_commis;
        string nom_livreur;
        DateTime date;
        string etat;
        string solde;
        double montant;

        public Commande(List<Pizza> pizzas, List<Boisson> boissons, string no_commande, string nom_client, string nom_commis, string nom_livreur, DateTime date, string etat)
        {
            this.pizzas = pizzas;
            this.boissons = boissons;
            this.no_commande = no_commande;
            this.telephone_client = nom_client;
            this.nom_commis = nom_commis;
            this.nom_livreur = nom_livreur;
            this.date = date;
            this.etat = etat;
            this.solde = "ok"; //Par défaut
            montant = 0;
        }

        public List<Pizza> Pizzas
        { get { return this.pizzas; } }
        public List<Boisson> Boissons
        { get { return this.boissons; } }
        public string No_commande
        { get { return this.no_commande; } }
        public string Telephone_client
        { get { return this.telephone_client; } set { this.telephone_client = value; } }
        public string Nom_commis
        { get { return this.nom_commis; } }
        public string Nom_livreur
        { get { return this.nom_livreur; } set { this.nom_livreur = value; } }
        public DateTime Date
        { get { return this.date; } }
        public string Etat
        { get { return this.etat; } set { this.etat = value; } }

        public string Solde
        { get { return this.solde; } set { this.solde = value; } }
        public double Montant
        { get { return this.montant; } set { this.montant = value; } }

        /// <summary>
        /// Permet d'avoir le détail d'une commande de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            string result = "";
            if (pizzas != null)
            {
                foreach (Pizza p in pizzas)
                {
                    result += p.ToString() + "\n";
                }
            }
            if (boissons != null)
            {
                foreach (Boisson b in boissons)
                {
                    result += b.ToString() + "\n";
                }
            }
            return this.no_commande + " " + this.telephone_client + " " + this.nom_commis + " " + this.nom_livreur + " " + this.date.ToString() + " " + this.etat + "\n" + result;
        }
        /// <summary>
        /// Permet d'obtenir le prix total d'une commande
        /// </summary>
        /// <returns>le double prix total</returns>
        public double PrixCommande()
        {
            double prix_total = 0;
            if(this.pizzas!=null&&this.boissons!=null)
            {
                foreach (Pizza p in this.pizzas)
                {
                    prix_total += p.Prix;
                }
                foreach (Boisson b in this.boissons)
                {
                    prix_total += b.Prix;
                }
            }
            return prix_total;
        }
        /// <summary>
        /// Méthode permettant de vérifier si le temps d'obtention d'une commande est supérieur ou inférieur a 45 min, si le délai depasse 45 alors on considerera la commande perdue
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>vrai ou faux en fonction du delai</returns>
        public bool VerifDateTimeCommande(DateTime dt)//La pizzeria est supposé fermer vers 23h ce qui empeche les problemes de changement de jour pour la calcul du temps de preparation
        {
            int h = dt.Hour - this.date.Hour;
            
            if(h==0)
            {
                if(dt.Minute - this.date.Minute>45) //La commande est annulée si le client n'a pas été livré au bout de 45 minutes
                {
                    return false;
                }
                else
                { 
                    return true;
                }
            }
            else
            {
                if(h==1)
                {
                    if(dt.Minute - this.date.Minute > 0)
                    {
                        return false;
                    }
                    else
                    {
                        int minutes = 60 - this.date.Minute;
                        if(minutes+dt.Minute>45)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }
         /// <summary>
         /// Methode pour mettre à jour le solde de la commande, 
         /// </summary>
         /// <param name="solde"></param>
        public void MiseAjourDuSolde(string solde)
        {
            if(solde.Equals("ok"))
            {
                this.solde = solde;
            }
            else
            {
                if(solde.Equals("perdue"))
                {
                    this.solde = solde;
                }
                else
                {
                    this.solde = "Erreur dans la définition du solde";
                }
            }
        }

    }
}
