using Problème_POOA_PUJOL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppPUJOL
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Pizzeria ChezBrice = new Pizzeria("Commis_svg.csv", "Livreur_svg.csv", "Clients_svg.csv", "Commandes_svg.csv", "Pizzas.csv", "Boissons.csv");
        /// <summary>
        /// Bouton du menu Commande. Permet la saisie d'une commande par le commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCommander_click(object sender, RoutedEventArgs e)
        {
            Commander commander = new Commander(ChezBrice);
            commander.Show();
        }
        /// <summary>
        /// Permet un accès à la carte de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemCarte_Click(object sender, RoutedEventArgs e)
        {
            Carte carte = new Carte(ChezBrice);
            carte.CartePizzeria.Text = ChezBrice.Carte_pizzeria.ToString();
            carte.Show();
        }
        /// <summary>
        /// Permet l'accès aux différentes informations importantes de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemInformationPizzeria_Click(object sender, RoutedEventArgs e)
        {
            Info_Pizzeria information = new Info_Pizzeria(ChezBrice);
            information.TextBoxInformationPizzeria.Text = ChezBrice.ToString();
            information.Show();
        }
        /// <summary>
        /// Accès aux commandes en cuisine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoirCuisine(object sender, RoutedEventArgs e)
        {
            InformationCuisine cuisine = new InformationCuisine(ChezBrice);
            string result = "";
            foreach (KeyValuePair<string, Commande> c in ChezBrice.Cuisine.A_preparer)
            {
                result += c.Value.ToString() + "\n";
            }
            cuisine.RecapApreparerBlock.Text = result;
            cuisine.sv_CommandeAPreparer.Content = cuisine.RecapApreparerBlock.Text;
            result = "";
            foreach (KeyValuePair<string, Commande> c in ChezBrice.Cuisine.Pretes)
            {
                result += c.Value.ToString() + "\n";
            }
            cuisine.RecapCommandePreteBlock.Text = result;
            cuisine.sv_CommandePrete.Content = cuisine.RecapCommandePreteBlock.Text;
            cuisine.Show();
        }
        /// <summary>
        /// Accès à l'état des commandes de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EtatCommandes(object sender, RoutedEventArgs e)
        {
            Etat_des_commandes etat_Des_Commandes = new Etat_des_commandes(ChezBrice);
            string result = "";
            foreach(KeyValuePair<string, Commande> c in ChezBrice.Commandes)
            {
                result += c.Value.ToString() + "\n";
            }
            etat_Des_Commandes.EtatCommandesBlock.Text = result;
            etat_Des_Commandes.sv_EtatCommandes.Content = etat_Des_Commandes.EtatCommandesBlock.Text;
            etat_Des_Commandes.Show();
        }
        /// <summary>
        /// Bouton de sauvegarde des nouvelles données de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sauvegarder(object sender, RoutedEventArgs e)
        {
            ChezBrice.SauvegardeClient();
            ChezBrice.SauvegardeCommis();
            ChezBrice.SauvegardeLivreur();
            ChezBrice.SauvegardeCommande();
            MessageBox.Show("Enregistré !");
        }
        /// <summary>
        /// Accès aux différentes statistiques de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VoirStatistiques(object sender, RoutedEventArgs e)
        {
            Statistiques stat = new Statistiques(ChezBrice);
            stat.Show();
        }
        /// <summary>
        /// Accès aux états des effectifs de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemEtatEffectif_Click(object sender, RoutedEventArgs e)
        {
            Effectifs effectifPizzeria = new Effectifs(ChezBrice);
            effectifPizzeria.Show();
        }
        /// <summary>
        /// Paramétrage du bouton permettant d'accéder a la fenetre de modifications des clients et des effectifs de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifierClient(object sender, RoutedEventArgs e)
        {
            ModifierClient m = new ModifierClient(ChezBrice);
            m.Show();
        }
    }
}
