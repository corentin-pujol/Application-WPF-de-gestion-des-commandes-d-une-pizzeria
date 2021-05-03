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
using Problème_POOA_PUJOL;

namespace WpfAppPUJOL
{
    /// <summary>
    /// Logique d'interaction pour Commander.xaml
    /// </summary>
    public partial class Commander : Window
    {
        Pizzeria pizzeria;
        int nombre_de_clics;
        bool fermer;
        public Commander(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            nombre_de_clics = 0;
            fermer = false;
            MessageBox.Show("Lorsque vous appuyer sur valider dans la nouvelle fenêtre, il n'est plus possible de revenir en arrière et il faut continuer la commande.");
            
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant d'afficher les commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> commis_available = new List<string>();
            foreach(KeyValuePair<string,Commis> n in this.pizzeria.Effectifs_pizzeria.Commis_disponible())
            {
                commis_available.Add(n.Key);
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = commis_available;
        }
        /// <summary>
        /// Bouton de validation du choix du commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationCommis(object sender, RoutedEventArgs e)
        {
            nombre_de_clics++;
            
            string nom_commis = ComboBox_Commis.Text;
            if(nom_commis==null||nom_commis.Equals(""))
            {
                MessageBox.Show("Choisissez un commis.");
            }
            else
            {
                if(nombre_de_clics>1)
                {
                    MessageBox.Show("Vous avez déjà sélectionné un commis.");
                }
                else
                {
                    List<Pizza> listep = new List<Pizza>();
                    List<Boisson> listeb = new List<Boisson>();
                    Commande nouvelle_commande = new Commande(listep, listeb, Convert.ToString(Convert.ToInt32(this.pizzeria.Commandes.Last().Key) + 1), "", nom_commis, "", DateTime.Now, "en creation");
                    this.pizzeria.Effectifs_pizzeria.Commis_disponible()[nom_commis].Etat = "en charge";
                    this.pizzeria.Effectifs_pizzeria.Employes_commis.Find(X => X.Nom.Equals(nom_commis)).NombreCommande++;
                    this.pizzeria.Commandes.Add(nouvelle_commande.No_commande, nouvelle_commande);
                    this.pizzeria.Effectifs_pizzeria.Commis_disponible().Remove(nom_commis);
                    MessageBox.Show("Commis : " + nom_commis + " choisi(e)\nNombre de commandes effectuées : " + this.pizzeria.Effectifs_pizzeria.Employes_commis.Find(X => X.Nom.Equals(nom_commis)).NombreCommande);
                }
            }
        }
        /// <summary>
        /// Permet d'afficher la fenêtre de création d'un nouveau client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationNouveauClient(object sender, RoutedEventArgs e)
        {
            if(ComboBox_Commis.Text.Equals("")==false)
            {
                NouveauClient newcustomer = new NouveauClient(this.pizzeria);
                newcustomer.Show();
                fermer = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez choisir un commis ou appuyer sur Valider.");
            }
        }
        /// <summary>
        /// Code permettant de commencer la saisie pour un client déjà enregistré dans la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RechercherClientExistant(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Commis.Text.Equals("") == false)
            {
                SaisirCommande neworder = new SaisirCommande(this.pizzeria);
                neworder.Show();
                fermer = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez choisir un commis ou appuyer sur Valider.");
            }
        }
        /// <summary>
        /// Gestion de l'accès à la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(fermer==false)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
