
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problème_POOA_PUJOL
{
    public class Pizzeria
    {
        SortedList<string, Client> clientele;
        SortedList<string,Commande> commandes;
        Effectif effectifs_pizzeria;
        Carte carte_pizzeria;
        Cuisine cuisine;
        SortedList<string, Facture> liste_factures;
        SortedList<string, double> tresorerie;

        public Pizzeria(string filename1, string filename2, string filename3, string filename4, string filename5, string filename6)
        {
            SortedList<string, Facture> f = new SortedList<string, Facture>();
            this.liste_factures = f;

            List<Commis> commis_pizzeria = new List<Commis>();

            StreamReader fichLect1 = new StreamReader(filename1);
            char[] sep = new char[1] { ';' };
            string ligne = "";
            string[] datas = new string[7];
            while (fichLect1.Peek() > 0)
            {
                ligne = fichLect1.ReadLine(); //Lecture d'une ligne
                datas = ligne.Split(sep);
                string nom = datas[0];
                string prenom = datas[1];
                string adresse = datas[2];
                string telephone = datas[3];
                string etat = datas[4];
                DateTime embauche = Convert.ToDateTime(datas[5]);
                int nbcom = Convert.ToInt32(datas[6]);
                Commis commis = new Commis(nom, prenom, adresse, telephone, etat, embauche);
                commis.NombreCommande = nbcom;
                commis_pizzeria.Add(commis);
            }
            fichLect1.Close();

            List<Livreur> livreurs_pizzeria = new List<Livreur>();

            StreamReader fichLect2 = new StreamReader(filename2);
            while (fichLect2.Peek() > 0)
            {
                ligne = fichLect2.ReadLine(); //Lecture d'une ligne
                datas = ligne.Split(sep);
                string nom = datas[0];
                string prenom = datas[1];
                string adresse = datas[2];
                string telephone = datas[3];
                string etat = datas[4];
                string moyen_transport = datas[5];
                Livreur livreur = new Livreur(nom, prenom, adresse, telephone, etat, moyen_transport);
                livreur.Nombre_livraison = Convert.ToInt32(datas[6]);
                livreurs_pizzeria.Add(livreur);
            }
            fichLect2.Close();

            Effectif effectifs = new Effectif(commis_pizzeria, livreurs_pizzeria);
            this.effectifs_pizzeria = effectifs;

            StreamReader fichLect3 = new StreamReader(filename3);
            
            SortedList<string, Client> c = new SortedList<string, Client>();
            string[] datas_client = new string[6];
            while (fichLect3.Peek() > 0)
            {
                ligne = fichLect3.ReadLine(); //Lecture d'une ligne
                datas_client = ligne.Split(sep);
                string nom = datas_client[1];
                string prenom = datas_client[2];
                string adresse = datas_client[3];
                string telephone = datas_client[4];
                DateTime anciennete = Convert.ToDateTime(datas_client[5]);
                Client client_pizzeria = new Client(nom, prenom, adresse, telephone, anciennete); //La date de la premiere commande est mise au minimal du datetime et sera demandée par le commis ultérieurement dans le programme
                c.Add(telephone, client_pizzeria);
            }
            fichLect3.Close();
            this.clientele = c;

            SortedList<string,Commande> commandes_pizzeria = new SortedList<string,Commande>();

            StreamReader fichLect4 = new StreamReader(filename4);
            string[] datas_commande = new string[9];

            //Attention il faut sauter une ligne dans la lecture du fichier commandes.csv
            bool compteur = false;
            while (fichLect4.Peek() > 0)
            {
                ligne = fichLect4.ReadLine(); //Lecture d'une ligne
                if (compteur == true)
                {
                    datas_commande = ligne.Split(sep);
                    string no_commande = datas_commande[0];
                    string heure = datas_commande[1];

                    int hours = 0;
                    int minutes = 0;

                    string h = Convert.ToString(heure[0]) + Convert.ToString(heure[1]);
                    hours = Convert.ToInt32(h); //variable double hours pour ajouter l'heure a la date de la commande 

                    if (heure.Length == 5) //Si il y a une horaire avec des minutes de la forme 00H00 longueur = 5
                    {
                        string m = Convert.ToString(heure[3]) + Convert.ToString(heure[4]);
                        minutes = Convert.ToInt32(m); //variable double minutes pour ajouter les minutes a la date de la commande
                    }

                    DateTime j = Convert.ToDateTime(datas_commande[2]);
                    int year = j.Year;
                    int month = j.Month;
                    int day = j.Day;

                    DateTime jour = new DateTime(year, month, day, hours, minutes, 0);

                    string telephone_client = datas_commande[3];
                    string nom_client = this.clientele[telephone_client].Nom;
                    string nom_commis = datas_commande[4];
                    string nom_livreur = datas_commande[5];
                    string etat = datas_commande[6];
                    
                    //rajouter l'instance cumul des commandes dans la classe client et ensuite faire une méthode afin d'ajouter +1 ou pas en fonction de l'element 7 du tableau correspondant à l'honoration de la commande (solde ok ou perdue)
                    Commande x = new Commande(null, null, no_commande, telephone_client, nom_commis, nom_livreur, jour, etat); //Les pizzas et les boissons sont à l'etat null car c'est au commis de remplir les informations plus tard dans l'algorithme
                    x.Montant = Convert.ToDouble(datas_commande[8]);
                    commandes_pizzeria.Add(x.No_commande,x);
                }
                compteur=true;
            }
            fichLect4.Close();
            this.commandes = commandes_pizzeria;

            Carte carte = new Carte(filename5, filename6);
            this.carte_pizzeria = carte;
            SortedList<string, Commande> P1 = new SortedList<string, Commande>();
            SortedList<string, Commande> P2 = new SortedList<string, Commande>();
            Cuisine cuisine_pizzeria = new Cuisine(P1, P2);
            this.cuisine = cuisine_pizzeria;
            SortedList<string, double> t = new SortedList<string, double>();
            this.tresorerie = t;
            MiseAJourTresorerie();
        }

        public SortedList<string, Client> Clientele
        { get { return this.clientele; } }
        public SortedList<string,Commande> Commandes
        { get { return this.commandes; } }
        public Effectif Effectifs_pizzeria
        { get { return this.effectifs_pizzeria; } }
        public Carte Carte_pizzeria
        { get { return this.carte_pizzeria; } } 
        public Cuisine Cuisine
        { get { return this.cuisine; } }
        public SortedList<string, Facture> Liste_factures
        { get { return this.liste_factures; } }
        public SortedList<string, double> Tresorerie
        { get { return this.tresorerie; } }

        /// <summary>
        /// Permet d'avoir les détails principals de la pizzeria
        /// </summary>
        /// <returns>Les détails sous forme d'une chaine de caractères</returns>
        public override string ToString()
        {
            string result = "Clientèle de la pizzeria : \n\n";
            double montant_total = 0;
            foreach (KeyValuePair<string, Client> c in this.clientele)
            {
                result += c.ToString() + "\n";
            }
            result += "\n\n Commandes de la pizzeria : \n\n";
            foreach(KeyValuePair<string,Commande> c in this.commandes)
            {
                result += c.ToString() + "\n";
            }
            result += "\n\n Effectifs de la pizzeria : \n\n";
            if(this.tresorerie.Count!=0)
            {
                foreach(KeyValuePair<string, double> m in this.tresorerie)
                {
                    montant_total += m.Value;
                }
            }
            return result + this.effectifs_pizzeria.ToString();
        }
        /// <summary>
        /// Trouve un client par son numero de telephone
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns>Le client concerné</returns>
        public Client FindCustomerbyPhone(string telephone)
        {
            foreach(KeyValuePair<string, Client> c in this.clientele)
            {
                if(c.Value.Telephone.Equals(telephone))
                {
                    return c.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// Trouve une commande a partir du numero du client
        /// </summary>
        /// <param name="name"></param>
        /// <returns>La commande concernée</returns>
        public string FindOrderbyPhone(string name)
        {
            foreach (KeyValuePair<string, Commande> c in this.commandes)
            {
                if (c.Value.Telephone_client.Equals(name))
                {
                    return c.Key;
                }
            }
            return null;
        }
        /// <summary>
        /// Annule une livraison et met a jour les données
        /// </summary>
        /// <param name="c"></param>
        /// <param name="nom_livreur"></param>
        public void AnnulerLivraison(Commande c, string nom_livreur)
        {
            c.Etat = "prête à être livrer";
            c.Nom_livreur = "";
            this.cuisine.Pretes.Add(c.No_commande, c);
            foreach(Livreur l in this.effectifs_pizzeria.Employes_livreurs)
            {
                if(l.Nom.Equals(nom_livreur))
                {
                    l.Etat = "sur place";
                }
            }
        }
        /// <summary>
        /// Méthode de réécriture des données clientes dans un nouveau fichier excel
        /// </summary>
        public void SauvegardeClient()
        {
            string filename = "Clients_svg.csv";
            StreamWriter fichEcr = new StreamWriter(filename, false);
            foreach(KeyValuePair<string,Client> c in this.clientele)
            {
                string newligne = FindOrderbyPhone(c.Value.Telephone) + ";" + c.Value.Nom + ";" + c.Value.Prenom + ";" + c.Value.Adresse + ";" + c.Value.Telephone + ";" + c.Value.Premiere_commande.ToString("dd/MM/yyyy");
                fichEcr.WriteLine(newligne);
            }
            fichEcr.Close();
        }
        /// <summary>
        /// Méthode de réécriture des données sur les commis dans un nouveau fichier excel
        /// </summary>
        /// 
        public void SauvegardeCommis()
        {
            string filename = "Commis_svg.csv";
            StreamWriter fichEcr = new StreamWriter(filename, false);
            foreach (Commis c in this.effectifs_pizzeria.Employes_commis)
            {
                string newligne = c.Nom + ";" + c.Prenom + ";" + c.Adresse + ";" + c.Telephone + ";" + c.Etat + ";" + c.Embauche.ToString("dd/MM/yyyy") + ";" + c.NombreCommande;
                fichEcr.WriteLine(newligne);
            }
            fichEcr.Close();
        }
        /// <summary>
        /// Méthode de réécriture des données sur les livreurs dans un nouveau fichier excel
        /// </summary>
        public void SauvegardeLivreur()
        {
            string filename = "Livreur_svg.csv";
            StreamWriter fichEcr = new StreamWriter(filename, false);
            foreach (Livreur l in this.effectifs_pizzeria.Employes_livreurs)
            {
                string newligne = l.Nom + ";" + l.Prenom + ";" + l.Adresse + ";" + l.Telephone + ";" + l.Etat + ";" + l.Moyen_de_transport + ";" + l.Nombre_livraison;
                fichEcr.WriteLine(newligne);
            }
            fichEcr.Close();
        }
        /// <summary>
        /// Méthode de réécriture des données commandes dans un nouveau fichier excel
        /// </summary>
        public void SauvegardeCommande()
        {
            bool compteur = false;
            string filename = "Commandes_svg.csv";
            StreamWriter fichEcr = new StreamWriter(filename, false);
            while(compteur == false)
            {
                compteur = true;
                fichEcr.WriteLine("No de commande" + ";" + "heure" + ";" + "jour" + ";" + "client" + ";" + "commis" + ";" + "livreur" + ";" + "etat" + ";" + "solde" + ";" + "montant");
            }
            foreach (KeyValuePair<string, Commande> c in this.commandes)
            {
                string heure = Convert.ToString(c.Value.Date.Hour);
                string minute = Convert.ToString(c.Value.Date.Minute);

                if(heure.Length==1)
                {
                    heure = "0" + heure;
                }
                if(minute.Length==1)
                {
                    minute = "0" + minute;
                }

                string newligne = c.Key + ";" + heure + "H" + minute + ";" + c.Value.Date.ToString("dd/MM/yyyy") + ";" + c.Value.Telephone_client + ";" + c.Value.Nom_commis + ";" + c.Value.Nom_livreur + ";" + c.Value.Etat + ";" + c.Value.Solde + ";" + c.Value.Montant;
                fichEcr.WriteLine(newligne);
            }
            fichEcr.Close();
        }
        /// <summary>
        /// Calcule la moyenne du montant des commandes sur une session
        /// </summary>
        /// <returns></returns>
        public double MoyenneCommandes()
        {
            double moyenne = 0;
            foreach(KeyValuePair<string, double> montant in this.tresorerie)
            {
                moyenne += montant.Value;
            }
            return moyenne / this.tresorerie.Count();
        }
        /// <summary>
        /// Mise a jour tresorerie
        /// </summary>
        public void MiseAJourTresorerie()
        {
            foreach(KeyValuePair<string, Commande> c in this.Commandes)
            {
                this.tresorerie.Add(c.Key, c.Value.Montant);
            }
        }
        public double ChiffreAffaireTotal()
        {
            double CA = 0;
            foreach (KeyValuePair<string, double> montant in this.tresorerie)
            {
                CA += montant.Value;
            }
            return CA;
        }
    }
}
