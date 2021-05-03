using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Facture
    {
        Commande commande;
        double montant;
        bool payee;

        public Facture(Commande commande, bool payer)
        {
            this.commande = commande;
            double prix_total = 0;
            foreach(Pizza p in commande.Pizzas)
            {
                prix_total += p.Prix;
            }
            foreach(Boisson b in commande.Boissons)
            {
                prix_total += b.Prix;
            }
            this.montant = prix_total;
            this.payee = payer;
        }

        public Commande Commande
        { get { return this.commande; } }
        public double Montant
        { get { return this.montant; } }
        public bool Payee
        { get { return this.payee; } set { this.payee = value; } }
        /// <summary>
        /// Permet d'avoir le détail d'une facture de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            return commande.ToString() + "\n" + this.montant + "€ " + this.payee;
        }
    }
}
