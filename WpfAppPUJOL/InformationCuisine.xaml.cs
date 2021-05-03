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
    /// Logique d'interaction pour InformationCuisine.xaml
    /// </summary>
    public partial class InformationCuisine : Window
    {
        Pizzeria pizzeria;
        public InformationCuisine(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
        }
        /// <summary>
        /// Fonction permettant d'afficher les commandes en préparation de la pizzeria et de paraméter la comboBox de selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandePrete(object sender, RoutedEventArgs e)
        {
            List<Commande> CommandePrete = new List<Commande>();
            foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.A_preparer)
            {
                CommandePrete.Add(c.Value);
            }
            CommandePrete.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = CommandePrete;
            string result = "";
            foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.A_preparer)
            {
                result += c.Value.ToString() + "\n";
            }
            RecapApreparerBlock.Text = result;
            sv_CommandeAPreparer.Content = RecapApreparerBlock.Text;

        }
        /// <summary>
        /// Paramètrage du bouton permettant de valider si une commande est prete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prete(object sender, RoutedEventArgs e)
        {
            if(ComboBox_CommandePretes.SelectedItem==null)
            {
                MessageBox.Show("Veuillez sélectionner un item prêt.");
            }
            else
            {
                Commande prete = (Commande)(ComboBox_CommandePretes.SelectedItem);
                this.pizzeria.Cuisine.CommandePretes(prete.No_commande);
                string result = "";
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                {
                    result += c.Value.ToString() + "\n";
                }
                RecapCommandePreteBlock.Text = result;
                sv_CommandePrete.Content = RecapCommandePreteBlock.Text;
                result = "";
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.A_preparer)
                {
                    result += c.Value.ToString() + "\n";
                }
                RecapApreparerBlock.Text = result;
                sv_CommandeAPreparer.Content = RecapApreparerBlock.Text;
                ComboBox_CommandePretes.SelectedItem = null;
                List<Commande> CommandeALivrer = new List<Commande>();
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.A_preparer)
                {
                    CommandeALivrer.Add(c.Value);
                }
                CommandeALivrer.Add(null);
                ComboBox_CommandePretes.ItemsSource = CommandeALivrer;
                List<Commande> CommandeLivraison = new List<Commande>();
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                {
                    CommandeLivraison.Add(c.Value);
                }
                CommandeLivraison.Add(null);
                ComboBox_CommandeALivrer.ItemsSource = CommandeLivraison;
            }
        }
        /// <summary>
        /// Fonction permettant d'afficher les commandes prêtes à être livrées de la pizzeria et de paraméter la comboBox de selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandeALivrer(object sender, RoutedEventArgs e)
        {
            List<Commande> CommandeALivrer = new List<Commande>();
            foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
            {
                CommandeALivrer.Add(c.Value);
            }
            CommandeALivrer.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = CommandeALivrer;
        }
        /// <summary>
        /// Bouton pour envoyer une commande sélectionnée en livraison
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ALivrer(object sender, RoutedEventArgs e)
        {
            if(ComboBox_CommandeALivrer.SelectedItem==null)
            {
                MessageBox.Show("Veuillez sélectionner un item prêt, à livrer.");
            }
            else
            {
                Commande a_livrer = (Commande)(ComboBox_CommandeALivrer.SelectedItem);
                this.pizzeria.Commandes[a_livrer.No_commande].Etat = "en livraison";
                this.pizzeria.Cuisine.Envoyer_en_livraison(a_livrer.No_commande);
                string result = "";
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                {
                    result += c.Value.ToString() + "\n";
                }
                RecapCommandePreteBlock.Text = result;
                sv_CommandePrete.Content = RecapCommandePreteBlock.Text;
                List<Commande> CommandeALivrer = new List<Commande>();
                foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                {
                    CommandeALivrer.Add(c.Value);
                }
                CommandeALivrer.Add(null);
                ComboBox_CommandeALivrer.ItemsSource = CommandeALivrer;
                if(this.pizzeria.Effectifs_pizzeria.Livreurs_disponible().Count!=0)
                {
                    Livreur dispo = this.pizzeria.Effectifs_pizzeria.Livreurs_disponible().First().Value;
                    this.pizzeria.Commandes[a_livrer.No_commande].Nom_livreur = dispo.Nom;
                    Facture commande_a_facturer = new Facture(this.pizzeria.Commandes[a_livrer.No_commande], false);
                    this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Find(X => X.Nom.Equals(dispo.Nom)).FactureLivraison(this.pizzeria.Commandes[a_livrer.No_commande]);
                    this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Find(X => X.Nom.Equals(dispo.Nom)).Etat = "en livraison";
                    MessageBox.Show("La commande numéro " + a_livrer.No_commande + " est partie en livraison !");
                    Livraison l = new Livraison(this.pizzeria, commande_a_facturer);
                    string commande = "";
                    foreach(Pizza p in this.pizzeria.Commandes[a_livrer.No_commande].Pizzas)
                    {
                        commande += p.ToString() + "\n";
                    }
                    foreach (Boisson b in this.pizzeria.Commandes[a_livrer.No_commande].Boissons)
                    {
                        commande += b.ToString() + "\n";
                    }
                    l.InformationLivraisonBlock.Text = this.pizzeria.Commandes[a_livrer.No_commande].No_commande + "\n" + this.pizzeria.Commandes[a_livrer.No_commande].Date + "\n\n" + commande + "\n" + "Total Commande : " + this.pizzeria.Commandes[a_livrer.No_commande].PrixCommande() + "€";
                    string num_client = this.pizzeria.Commandes[a_livrer.No_commande].Telephone_client;
                    l.ClientBlock.Text = this.pizzeria.FindCustomerbyPhone(num_client).Nom + " " + this.pizzeria.FindCustomerbyPhone(num_client).Prenom + "\n" + this.pizzeria.FindCustomerbyPhone(num_client).Adresse + "\n" + this.pizzeria.FindCustomerbyPhone(num_client).Telephone;
                    l.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Il n'y a pas de livreurs disponibles en pizzeria.");
                    this.pizzeria.AnnulerLivraison(this.pizzeria.Commandes[a_livrer.No_commande], this.pizzeria.Commandes[a_livrer.No_commande].Nom_livreur);
                    result = "";
                    foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                    {
                        result += c.Value.ToString() + "\n";
                    }
                    RecapCommandePreteBlock.Text = result;
                    sv_CommandePrete.Content = RecapCommandePreteBlock.Text;
                    List<Commande> CommandeALivrer2 = new List<Commande>();
                    foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                    {
                        CommandeALivrer2.Add(c.Value);
                    }
                    CommandeALivrer2.Add(null);
                    ComboBox_CommandeALivrer.ItemsSource = CommandeALivrer2;
                }
            }
        }
    }
}
