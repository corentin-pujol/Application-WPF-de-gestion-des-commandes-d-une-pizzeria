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
    /// Logique d'interaction pour Etat_des_commandes.xaml
    /// </summary>
    public partial class Etat_des_commandes : Window
    {
        Pizzeria pizzeria;
        public Etat_des_commandes(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
        }
        /// <summary>
        /// Bouton de fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitterFenetre(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
