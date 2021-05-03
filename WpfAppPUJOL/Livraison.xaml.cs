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
    /// Logique d'interaction pour Livraison.xaml
    /// </summary>
    public partial class Livraison : Window
    {
        Pizzeria pizzeria;
        Facture commande;
        bool verif_adresse;
        bool fermer;
        public Livraison(Pizzeria pizzeria, Facture commande)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            this.commande = commande;
            this.verif_adresse = false;
            DateEtHeure.Text = DateTime.Now.ToString();
            fermer = false;
        }
        /// <summary>
        /// Bouton permettant la confirmation de l'adresse si le livreur la trouve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdresseCorrecte(object sender, RoutedEventArgs e)
        {
            if(this.commande.Commande.VerifDateTimeCommande(DateTime.Now)==true)
            {
                this.verif_adresse = true;
                MessageBox.Show("Adresse trouvée.");
                if (this.commande.Payee == true)
                {
                    MessageBox.Show("Livraison terminée");
                    foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                    {
                        if (l.Nom.Equals(this.commande.Commande.Nom_livreur))
                        {
                            l.Etat = "sur place";
                        }
                    }
                    TerminerCommande t = new TerminerCommande(this.pizzeria, this.commande);
                    t.Show();
                    fermer = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Commande perdue, veuillez fermer la commande");
            }
        }
        /// <summary>
        /// Bouton de confirmation si le livreur a bien facturé la commande livrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandeFacturee(object sender, RoutedEventArgs e)
        {
            if(this.commande.Commande.VerifDateTimeCommande(DateTime.Now)==true)
            {
                this.commande.Payee = true;
                MessageBox.Show("La commande a été payée.");
                if (this.verif_adresse == true)
                {
                    MessageBox.Show("Livraison terminée");
                    foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                    {
                        if (l.Nom.Equals(this.commande.Commande.Nom_livreur))
                        {
                            l.Etat = "sur place";
                        }
                    }
                    TerminerCommande t = new TerminerCommande(this.pizzeria, this.commande);
                    t.Show();
                    fermer = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Commande perdue, veuillez fermer la commande");
            }
        }
        /// <summary>
        /// Permet l'annulation de la livraison et la commande revient à la pizzeria, si par exemple le livreur a un soucis. Permet à un autre livreur de prendre la commande en charge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnulerLivraison(object sender, RoutedEventArgs e)
        {
            this.pizzeria.AnnulerLivraison(this.commande.Commande,this.commande.Commande.Nom_livreur);
            MessageBox.Show("Livraison annulée");
            InformationCuisine cuisine = new InformationCuisine(this.pizzeria);
            string result = "";
            foreach (KeyValuePair<string, Commande> c in this.pizzeria.Cuisine.Pretes)
            {
                result += c.Value.ToString() + "\n";
            }
            cuisine.RecapCommandePreteBlock.Text = result;
            cuisine.sv_CommandePrete.Content = cuisine.RecapCommandePreteBlock.Text;
            cuisine.Show();
            fermer = true;
            this.Close();
        }
        /// <summary>
        /// Si la commande est perdue, ce bouton permet de le confirmer et de fermer la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandePerdue(object sender, RoutedEventArgs e)
        {
            this.commande.Commande.MiseAjourDuSolde("perdue");
            this.commande.Commande.Etat = "fermee";
            this.commande.Payee = false;
            foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
            {
                if (l.Nom.Equals(this.commande.Commande.Nom_livreur))
                {
                    l.Etat = "sur place";
                }
            }
            MessageBox.Show("Commande fermée");
            fermer = true;
            this.Close();
        }
        /// <summary>
        /// Fonction de paramétrage de la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(fermer==false)
            {
                e.Cancel = true;
                MessageBox.Show("Vous ne pouvez pas fermer la fenêtre tant que la livraison n'est pas terminée...");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
