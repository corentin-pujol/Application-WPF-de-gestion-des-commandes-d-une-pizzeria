using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Problème_POOA_PUJOL;

namespace WpfAppPUJOL
{
    /// <summary>
    /// Logique d'interaction pour Effectifs.xaml
    /// </summary>
    public partial class Effectifs : Window
    {
        Pizzeria pizzeria;
        public List<Commis> myCommis;
        public List<Livreur> myLivreurs;
        public List<Client> myClients;
        public Effectifs(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            List<Client> clients = new List<Client>();
            foreach(KeyValuePair<string, Client> c in this.pizzeria.Clientele)
            {
                clients.Add(c.Value);
            }
            myClients = clients;
            myCommis = this.pizzeria.Effectifs_pizzeria.Employes_commis;
            myLivreurs = this.pizzeria.Effectifs_pizzeria.Employes_livreurs;
            string result = "";
            foreach (Client c in this.myClients)
            {
                result += c.ToString() + "\n";
            }
            ClientBlock.Text = result; //Affichage des clients
            result = "";
            foreach(Commis c in myCommis)
            {
                result += c.ToString() + "\n";
            }
            CommisBlock.Text = result; //Affichage des commis
            result = "";
            foreach (Livreur l in myLivreurs)
            {
                result += l.ToString() + "\n";
            }
            LivreursBlock.Text = result; //Affichage des livreurs
        }
        /// <summary>
        /// Bouton pour fermer la page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitterFenetre(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///Affiche les clients dans l'ordre alphabétique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriNomClient(object sender, RoutedEventArgs e)
        {
            this.myClients.Sort();
            string result = "";
            foreach(Client c in this.myClients)
            {
                result += c.ToString() + "\n";
            }
            ClientBlock.Text = result;
        }
        /// <summary>
        /// Affiche les clients dans l'ordre alphabétique des villes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriVilleClient(object sender, RoutedEventArgs e)
        {
            this.myClients.Sort(myClients[0].CompareToVille);
            string result = "";
            foreach (Client c in this.myClients)
            {
                result += c.ToString() + "\n";
            }
            ClientBlock.Text = result;
        }
        /// <summary>
        /// Affiche les clients en fonction de leur ancienneté
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriMontantClient(object sender, RoutedEventArgs e)
        {
            this.myClients.Sort(myClients[0].CompareToFirstOrder);
            string result = "";
            foreach (Client c in this.myClients)
            {
                result += c.ToString() + "\n";
            }
            ClientBlock.Text = result;
        }
        /// <summary>
        /// Affiche les commis dans l'ordre alphabétique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriNomCommis(object sender, RoutedEventArgs e)
        {
            myCommis.Sort();
            string result = "";
            foreach (Commis c in myCommis)
            {
                result += c.ToString() + "\n";
            }
            CommisBlock.Text = result;
        }
        /// <summary>
        /// Affiche les commis dans l'ordre alphabétique des villes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriVilleCommis(object sender, RoutedEventArgs e)
        {
            myCommis.Sort(myCommis[0].CompareToVille);
            string result = "";
            foreach (Commis c in myCommis)
            {
                result += c.ToString() + "\n";
            }
            CommisBlock.Text = result;
        }
        /// <summary>
        /// Affiche les commis dans l'ordre du nombre de commandes effectuées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriNbCommandeCommis(object sender, RoutedEventArgs e)
        {
            myCommis.Sort(myCommis[0].CompareToNbCommande);
            string result = "";
            foreach (Commis c in myCommis)
            {
                result += c.ToString() + "\n";
            }
            CommisBlock.Text = result;
        }
        /// <summary>
        /// Affiche les livreurs dans l'ordre alphabétique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriNomLivreur(object sender, RoutedEventArgs e)
        {
            myLivreurs.Sort();
            string result = "";
            foreach (Livreur l in myLivreurs)
            {
                result += l.ToString() + "\n";
            }
            LivreursBlock.Text = result;
        }
        /// <summary>
        /// Affiche les livreurs dans l'ordre alphabétique des villes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriVilleLivreur(object sender, RoutedEventArgs e)
        {
            myLivreurs.Sort(myLivreurs[0].CompareToVille);
            string result = "";
            foreach (Livreur l in myLivreurs)
            {
                result += l.ToString() + "\n";
            }
            LivreursBlock.Text = result;
        }
        /// <summary>
        /// Affiche les livreurs dans l'ordre des livraisons effectuées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriNbLivraisonLivreur(object sender, RoutedEventArgs e)
        {
            myLivreurs.Sort(myLivreurs[0].CompareToNbLivraison);
            string result = "";
            foreach (Livreur l in myLivreurs)
            {
                result += l.ToString() + "\n";
            }
            LivreursBlock.Text = result;
        }
    }
}
