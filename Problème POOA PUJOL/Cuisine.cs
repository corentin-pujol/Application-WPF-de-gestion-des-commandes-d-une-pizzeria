using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Cuisine
    {
        SortedList<string,Commande> a_preparer;
        SortedList<string,Commande> pretes;

        public Cuisine(SortedList<string,Commande> a_preparer, SortedList<string,Commande> pretes)
        {
            this.a_preparer = a_preparer;
            this.pretes = pretes;
        }

        public SortedList<string,Commande> A_preparer
        { get { return this.a_preparer; } }
        public SortedList<string,Commande> Pretes
        { get { return this.pretes; } }
        /// <summary>
        /// Permet d'avoir le détail de la Cuisine de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            string result = "A préparer : \n";
            foreach(KeyValuePair<string,Commande> c in a_preparer)
            {
                result += c.ToString() + "\n"; 
            }
            result += "\n Prêtes : \n";
            foreach(KeyValuePair<string,Commande> c in pretes)
            {
                result += c.ToString() + "\n";
            }
            return result;
        }
        /// <summary>
        /// Change l'état d'une commande prete à etre livree apres avoir ete preparé en Cuisine
        /// </summary>
        /// <param name="no_commandep"></param>
        public void CommandePretes(string no_commandep)
        {
            foreach (KeyValuePair<string,Commande> c in a_preparer)
            {
                if(c.Value.No_commande==no_commandep)
                {
                    c.Value.Etat = "prête à être livrer";
                    pretes.Add(c.Key,c.Value);
                }
            }
            a_preparer.Remove(no_commandep);
        }
        /// <summary>
        /// Lorsqu'une commande est saisie, permet d' changer son etat et de la definir comme commande a preparer en cuisine
        /// </summary>
        /// <param name="c"></param>
        public void CommandeAPreparer(Commande c)
        {
            c.Etat = "à préparer";
            a_preparer.Add(c.No_commande,c);
        }
        /// <summary>
        /// Lorsqu'une commande prete a etre livree est prise en chagre par un livreur alors son etat passe a "en livraison"
        /// </summary>
        /// <param name="no_commande"></param>
        public void Envoyer_en_livraison(string no_commande)
        {
            foreach (KeyValuePair<string, Commande> c in pretes)
            {
                if (c.Value.No_commande == no_commande)
                {
                    c.Value.Etat = "en livraison";
                }
            }
            pretes.Remove(no_commande);
        }
    }
}
