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
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques : Window
    {
        Pizzeria pizzeria;
        public List<Commis> myCommis { get; set; }
        public List<Livreur> myLivreurs { get; set; }
        public Statistiques(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            myCommis = pizzeria.Effectifs_pizzeria.Employes_commis;
            myLivreurs = pizzeria.Effectifs_pizzeria.Employes_livreurs;
            double moyenneMontantCommande = this.pizzeria.MoyenneCommandes();
            ResultBlock.Text = Convert.ToString(moyenneMontantCommande) + " €";
            double CA = this.pizzeria.ChiffreAffaireTotal();
            ResultBlockCA.Text = Convert.ToString(CA) + " €";
            DataContext = this;
        }
        /// <summary>
        /// Bouton quitter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitterFenetre(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
