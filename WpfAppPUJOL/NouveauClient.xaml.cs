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
    /// Logique d'interaction pour NouveauClient.xaml
    /// </summary>
    public partial class NouveauClient : Window
    {
        Pizzeria pizzeria;
        bool fermer;
        public NouveauClient(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            fermer = false;
        }
        /// <summary>
        /// Bouton de validation des informations choisies pour le nouveau client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreationClient(object sender, RoutedEventArgs e)
        {
            if (NomClientBox.Text.Equals("") || PrenomClientBox.Text.Equals("") || AdresseClientBox.Text.Equals("") || TelephoneClientBox.Text.Equals("")||TelephoneClientBox.Text.Count()!=14)
            {
                MessageBox.Show("Veuillez compléter l'ensemble des informations et au format :\nNom\nPrenom\nN°XX, Libellé de la voie, Code Postale et Ville\nTéléphone XX XX XX XX XX ");
            }
            else
            {
                string nom = NomClientBox.Text;
                string prenom = PrenomClientBox.Text;
                string adresse = AdresseClientBox.Text;
                string telephone = TelephoneClientBox.Text;
                Client NewCustomer = new Client(nom, prenom, adresse, telephone, DateTime.Now);
                this.pizzeria.Clientele.Add(NewCustomer.Telephone, NewCustomer);
                this.pizzeria.Commandes.Last().Value.Telephone_client = telephone;
                MessageBox.Show("Client enregistré !");
                SaisirCommande commande = new SaisirCommande(this.pizzeria);
                commande.NumeroClientBox.Text = telephone;
                commande.Show();
                fermer = true;
                this.Close();
            }
        }
        /// <summary>
        /// Paramétrage de la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(fermer==false)
            {
                e.Cancel = true;
                MessageBox.Show("Veuillez finir de compléter les informations.");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
