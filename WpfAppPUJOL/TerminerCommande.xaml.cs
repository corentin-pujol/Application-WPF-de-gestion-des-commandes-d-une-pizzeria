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
    /// Logique d'interaction pour TerminerCommande.xaml
    /// </summary>
    public partial class TerminerCommande : Window
    {
        Pizzeria pizzeria;
        Facture facture;
        bool facture_recup;
        bool argent_encaisse;
        bool fermer;

        public TerminerCommande(Pizzeria pizzeria, Facture facture)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
            this.facture = facture;
            facture_recup = false;
            argent_encaisse = false;
            fermer = false;
            NumCommandeBox.Text = facture.Commande.No_commande;
            foreach(Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
            {
                if(l.Nom.Equals(facture.Commande.Nom_livreur))
                {
                    NumLivreurBox.Text = l.Telephone;
                }
            }
        }
        /// <summary>
        /// Bouton de confirmation que le commis a bien recuperé le double de la facture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FactureRecup(object sender, RoutedEventArgs e)
        {
            if (NumCommandeBox.Text.Equals("") == false && NumLivreurBox.Text.Equals("") == false)
            {
                this.pizzeria.Liste_factures.Add(facture.Commande.No_commande, facture);
                facture_recup = true;
                MessageBox.Show("Facture enregistrée");
            }
            else
            {
                MessageBox.Show("Veuillez remplir les champs ci-dessus avant de continuer.");
            }
        }
        /// <summary>
        /// Bouton de confirmation que le commis a bien encaissé l'argent de la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncaisserArgent(object sender, RoutedEventArgs e)
        {
            if (NumCommandeBox.Text.Equals("") == false && NumLivreurBox.Text.Equals("") == false)
            {
                facture.Commande.Montant = facture.Montant;
                this.pizzeria.Tresorerie.Add(facture.Commande.No_commande, facture.Montant);
                argent_encaisse = true;
                MessageBox.Show("Montant encaissé");
            }
            else
            {
                MessageBox.Show("Veuillez remplir les champs ci-dessus avant de continuer.");
            }
        }
        /// <summary>
        /// Bouton permettant au commis de fermer la commande si la facture a bien été recupérée et si l'argent a bien été encaissé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FermerLaCommande(object sender, RoutedEventArgs e)
        {
            if (this.argent_encaisse == true)
            {
                if (this.facture_recup == true)
                {
                    if (NumCommandeBox.Text.Equals("") == false && NumLivreurBox.Text.Equals("") == false && this.facture.Payee == true)
                    {
                        this.facture.Commande.Etat = "fermee";
                        this.facture.Commande.MiseAjourDuSolde("ok");
                        if (NumLivreurBox.Text.Count() == 14)
                        {
                            this.pizzeria.Effectifs_pizzeria.FindDeleveryManByPhoneNumber(NumLivreurBox.Text, this.facture.Commande.Nom_livreur).LivraisonTerminee();
                            MessageBox.Show("Commande terminée");
                            fermer = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Le numéro du livreur n'est pas au bon format");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veuillez remplir l'ensemble des champs demandés");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez récupérer la facture avant de fermer la commande");
                }
            }
            else
            {
                MessageBox.Show("Veuillez encaisser l'argent avant de fermer la commande");
            }
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
                MessageBox.Show("Veuillez terminer la fermeture de la commande pour fermer la fenêtre.");
            }
            else
            {
                e.Cancel = false;
            }
        }
    }
}
