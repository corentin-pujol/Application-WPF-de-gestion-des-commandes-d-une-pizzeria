using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème_POOA_PUJOL
{
    public class Effectif
    {
        List<Commis> employes_commis;
        List<Livreur> employes_livreurs;

        public Effectif(List<Commis> commis, List<Livreur> livreurs)
        {
            this.employes_commis = commis;
            this.employes_livreurs = livreurs;
        }

        public List<Commis> Employes_commis
        { get { return this.employes_commis; } }
        public List<Livreur> Employes_livreurs
        { get { return this.employes_livreurs; } }
        /// <summary>
        /// Permet d'avoir le détail des effectifs de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            string result = "";
            foreach (Commis c in this.employes_commis)
            {
                result += c.ToString() + "\n";
            }
            result += "\n";
            foreach (Livreur l in employes_livreurs)
            {
                result += l.ToString() + "\n";
            }
            return result;
        }
        /// <summary>
        /// Permet d'obtenir une liste des commis disponibles à la pizzeria pour eventuellement prendre une commande
        /// </summary>
        /// <returns>Liste des commis definis comme "sur place"</returns>
        public SortedList<string, Commis> Commis_disponible()
        {
            SortedList<string, Commis> commis_dispo = new SortedList<string, Commis>();
            foreach(Commis n in this.employes_commis)
            {
                if(n.Etat.Equals("sur place"))
                {
                    commis_dispo.Add(n.Nom, n);
                }
            }
            return commis_dispo;
        }
        /// <summary>
        /// Permet d'obtenir une liste des livreurs disponibles a la pizzeria
        /// </summary>
        /// <returns>Liste de livreurs "sur place"</returns>
        public SortedList<string, Livreur> Livreurs_disponible()
        {
            SortedList<string, Livreur> livreurs_dispo = new SortedList<string, Livreur>();
            foreach (Livreur n in this.employes_livreurs)
            {
                if (n.Etat.Equals("sur place"))
                {
                    livreurs_dispo.Add(n.Nom,n);
                }
            }
            return livreurs_dispo;
        }
        /// <summary>
        /// Méthode pour trouver dans la liste des livreurs un livreur par son numero de téléphone ou son nom
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="name"></param>
        /// <returns>Le livreur recherché ou null</returns>
        public Livreur FindDeleveryManByPhoneNumber(string telephone,string name)
        {
            foreach(Livreur l in this.employes_livreurs)
            {
                if(l.Telephone.Equals(telephone)||l.Nom.Equals(name))
                {
                    return l;
                }
            }
            return null;
        }
    }
}
