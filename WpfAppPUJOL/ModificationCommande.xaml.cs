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
    /// Logique d'interaction pour ModificationCommande.xaml
    /// </summary>
    public partial class ModificationCommande : Window
    {
        Pizzeria pizzeria;
        string no_commande;
        bool fermer;
        public ModificationCommande(Pizzeria pizzeria, string no_commande)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            this.no_commande = no_commande;
            fermer = false;
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant de selectionner une boisson à supprimer de la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuppressionBoisson(object sender, RoutedEventArgs e)
        {
            List<Boisson> boisson_available = new List<Boisson>();
            foreach(Boisson b in this.pizzeria.Commandes[this.no_commande].Boissons)
            {
                boisson_available.Add(b);
            }
            boisson_available.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = boisson_available;
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant la selection d'une pizza à supprimer dans la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuppressionPizza(object sender, RoutedEventArgs e)
        {
            List<Pizza> pizza_available = new List<Pizza>();
            foreach (Pizza p in this.pizzeria.Commandes[this.no_commande].Pizzas)
            {
                pizza_available.Add(p);
            }
            pizza_available.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = pizza_available;
        }
        /// <summary>
        /// Bouton de validation de la suppression
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerElementsComboBox(object sender, RoutedEventArgs e)
        {
            Pizza pizza_supp = (Pizza)ComboBox_PizzasCommande.SelectedItem;
            Boisson boisson_supp = (Boisson)ComboBox_BoissonsCommande.SelectedItem;
            if(pizza_supp != null && this.pizzeria.Commandes[no_commande].Pizzas.Contains(pizza_supp))
            {
                this.pizzeria.Commandes[no_commande].Pizzas.Remove(pizza_supp);
            }
            if (boisson_supp != null && this.pizzeria.Commandes[no_commande].Boissons.Contains(boisson_supp))
            {
                this.pizzeria.Commandes[no_commande].Boissons.Remove(boisson_supp);
            }
            MessageBox.Show("Appuyer sur le bouton Rafraichir pour actualiser le panier.");
            fermer = true;
            this.Close();
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
                MessageBox.Show("Veuillez terminer la modification.");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
