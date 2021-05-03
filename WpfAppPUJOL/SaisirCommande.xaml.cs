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
    /// Logique d'interaction pour SaisirCommande.xaml
    /// </summary>
    public partial class SaisirCommande : Window
    {
        Pizzeria pizzeria;
        string numero_commande;
        bool fermer;
        bool rechercher;
        public SaisirCommande(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            numero_commande = this.pizzeria.Commandes.Last().Key;
            fermer = false;
            rechercher = false;
        }
        /// <summary>
        /// Bouton de recherche du client en fonction de son numéro de téléphone, permet de confirmer que le client est bien dans la base de données de la pizzeria et permet donc de débuter la saisie de la commande. Sans la confirmation il est impossible de prendre une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RechercherClient(object sender, RoutedEventArgs e)
        {
            string numero = NumeroClientBox.Text;
            if(this.pizzeria.Clientele.ContainsKey(numero))
            {
                MessageBox.Show("Client trouvé : \n\n" + this.pizzeria.Clientele[numero].ToString());
                this.pizzeria.Commandes[numero_commande].Telephone_client = NumeroClientBox.Text;
                rechercher = true;
            }
            else
            {
                MessageBox.Show("Le client n'existe pas (réessayer au format : XX XX XX XX XX ?)");
            }
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant la selection des pizzas à ajouter dans la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_ChoixPizza(object sender, RoutedEventArgs e)
        {
            List<string> Choix = new List<string>();
            foreach(KeyValuePair<string,Pizza> p in this.pizzeria.Carte_pizzeria.Pizzas)
            {
                Choix.Add(p.Key);
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = Choix;
        }
        /// <summary>
        /// Même fonction mais pour les boissons à mettre dans la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_ChoixBoisson(object sender, RoutedEventArgs e)
        {
            List<string> Choix = new List<string>();
            foreach (KeyValuePair<string, Boisson> b in this.pizzeria.Carte_pizzeria.Boissons)
            {
                Choix.Add(b.Key);
            }
            var combo = sender as ComboBox;
            combo.ItemsSource = Choix;
        }
        /// <summary>
        /// Bouton de validation de la selection d'une pizza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterPizzaPanier(object sender, RoutedEventArgs e)
        {
            if(rechercher==true)
            {
                if (ComboBox_Pizza.Text.Equals(""))
                {

                }
                else
                {
                    this.pizzeria.Commandes[numero_commande].Pizzas.Add(this.pizzeria.Carte_pizzeria.Pizzas[ComboBox_Pizza.Text]);
                    string result = "";
                    double prix_total = this.pizzeria.Commandes[numero_commande].PrixCommande();
                    foreach (Pizza p in this.pizzeria.Commandes[numero_commande].Pizzas)
                    {
                        result += p.ToString() + "\n";

                    }
                    RecapPizzaBlock.Text = result;
                    sv_Pizza.Content = RecapPizzaBlock.Text;
                    AffichePrixBlock.Text = Convert.ToString(prix_total) + " €";
                }
            }
            else
            {
                MessageBox.Show("Appuyez sur rechercher pour confirmer le client");
            }
        }
        /// <summary>
        /// Bouton de validation de la selection d'une boisson
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterBoissonPanier(object sender, RoutedEventArgs e)
        {
            if(rechercher==true)
            {
                if (ComboBox_Boisson.Text.Equals(""))
                {

                }
                else
                {
                    this.pizzeria.Commandes[numero_commande].Boissons.Add(this.pizzeria.Carte_pizzeria.Boissons[ComboBox_Boisson.Text]);
                    string result = "";
                    double prix_total = this.pizzeria.Commandes[numero_commande].PrixCommande();
                    foreach (Boisson b in this.pizzeria.Commandes[numero_commande].Boissons)
                    {
                        result += b.ToString() + "\n";
                    }
                    RecapBoissonBlock.Text = result;
                    sv_Boisson.Content = RecapBoissonBlock.Text;
                    AffichePrixBlock.Text = Convert.ToString(prix_total) + " €";
                }
            }
            else
            {
                MessageBox.Show("Appuyez sur rechercher pour confirmer le client");
            }
        }
        /// <summary>
        /// Bouton d'envoie de commande en preparation, valide la commande saisie par le commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Validation_Commande(object sender, RoutedEventArgs e)
        {
            if(this.pizzeria.Commandes[numero_commande].Boissons.Count==0&&this.pizzeria.Commandes[numero_commande].Pizzas.Count==0)
            {
                MessageBox.Show("Veuillez faire un choix avant de valider.");
            }
            else
            {
                if(rechercher==false)
                {
                    MessageBox.Show("N'oubliez pas de cliquer sur Rechercher pour confirmer l'adresse et les informations du client.");
                }
                else
                {
                    this.pizzeria.Cuisine.CommandeAPreparer(this.pizzeria.Commandes[numero_commande]);
                    MessageBox.Show("La commande numéro " + this.numero_commande + " est partie en préparation");
                    InformationCuisine cuisine = new InformationCuisine(this.pizzeria);
                    string result = "";
                    foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.A_preparer)
                    {
                        result += c.Value.ToString() + "\n";
                    }
                    cuisine.RecapApreparerBlock.Text = result;
                    cuisine.sv_CommandeAPreparer.Content = cuisine.RecapApreparerBlock.Text;
                    result = "";
                    foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
                    {
                        result += c.Value.ToString() + "\n";
                    }
                    cuisine.RecapCommandePreteBlock.Text = result;
                    cuisine.sv_CommandePrete.Content = cuisine.RecapCommandePreteBlock.Text;
                    cuisine.Show();
                    this.pizzeria.Effectifs_pizzeria.Employes_commis.Find(X => X.Nom.Equals(this.pizzeria.Commandes[numero_commande].Nom_commis)).Etat = "sur place";
                    fermer = true;
                    this.Close();
                }
            }
        }
        /// <summary>
        /// Bouton d'accès à la fenetre de modification de la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modification_Commande(object sender, RoutedEventArgs e)
        {
            ModificationCommande modification = new ModificationCommande(this.pizzeria, this.numero_commande);
            modification.Show();
        }
        /// <summary>
        /// Bouton permetttant la mise à jour de l'ecran récapitulatif de la commande apres modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshRecap(object sender, RoutedEventArgs e)
        {
            string result = "";
            foreach (Boisson b in this.pizzeria.Commandes[numero_commande].Boissons)
            {
                result += b.ToString() + "\n";
            }
            RecapBoissonBlock.Text = result;
            sv_Boisson.Content = RecapBoissonBlock.Text;
            result = "";
            foreach (Pizza p in this.pizzeria.Commandes[numero_commande].Pizzas)
            {
                result += p.ToString() + "\n";
            }
            RecapPizzaBlock.Text = result;
            sv_Pizza.Content = RecapPizzaBlock.Text;
            double prix_total = this.pizzeria.Commandes[numero_commande].PrixCommande();
            AffichePrixBlock.Text = Convert.ToString(prix_total) + " €";

        }
        /// <summary>
        /// Paramétrage de la fermeture de la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(fermer==false)
            {
                e.Cancel = true;
                MessageBox.Show("Veuillez terminer la commande.");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
