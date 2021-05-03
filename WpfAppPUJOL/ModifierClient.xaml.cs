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
    /// Logique d'interaction pour ModifierClient.xaml
    /// </summary>
    public partial class ModifierClient : Window
    {
        Pizzeria pizzeria;
        public ModifierClient(Pizzeria pizzeria)
        {
            InitializeComponent();
            this.pizzeria = pizzeria;
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant la selection d'un client à modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifClient(object sender, RoutedEventArgs e)
        {
            List<Client> client = new List<Client>();
            foreach(KeyValuePair<string, Client> c in this.pizzeria.Clientele)
            {
                client.Add(c.Value);
            }
            client.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = client;
        }
        /// <summary>
        /// ComboBox permettant de selectionner un client à supprimer de la base de données de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuppClient(object sender, RoutedEventArgs e)
        {
            List<Client> client = new List<Client>();
            foreach (KeyValuePair<string, Client> c in this.pizzeria.Clientele)
            {
                client.Add(c.Value);
            }
            client.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = client;
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant la selection d'un commis à modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifCommis(object sender, RoutedEventArgs e)
        {
            List<Commis> commis = new List<Commis>();
            foreach(Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
            {
                commis.Add(c);
            }
            commis.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = commis;
        }
        /// <summary>
        /// ComboBox permettant de selectionner un commis à supprimer de la base de données de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuppCommis(object sender, RoutedEventArgs e)
        {
            List<Commis> commis = new List<Commis>();
            foreach (Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
            {
                commis.Add(c);
            }
            commis.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = commis;
        }
        /// <summary>
        /// Paramétrage de la comboBox permettant la selection d'un livreur à modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifLivreur(object sender, RoutedEventArgs e)
        {
            List<Livreur> livreurs = new List<Livreur>();
            foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
            {
                livreurs.Add(l);
            }
            livreurs.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = livreurs;
        }
        /// <summary>
        /// ComboBox permettant de selectionner un livreur à supprimer de la base de données de la pizzeria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SuppLivreur(object sender, RoutedEventArgs e)
        {
            List<Livreur> livreurs = new List<Livreur>();
            foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
            {
                livreurs.Add(l);
            }
            livreurs.Add(null);
            var combo = sender as ComboBox;
            combo.ItemsSource = livreurs;
        }
        /// <summary>
        /// Bouton de validation des modifications faites sur un client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderClient(object sender, RoutedEventArgs e)
        {
            if(ComboBox_ClientModif.SelectedItem!=null)
            {
                if(this.pizzeria.Clientele.ContainsValue((Client)ComboBox_ClientModif.SelectedItem))
                {
                    Client modif = (Client)ComboBox_ClientModif.SelectedItem;
                    if(NomBox.Text.Equals("")==false && NomBox.Text.Equals(modif.Nom) == false)
                    {
                        this.pizzeria.Clientele[modif.Telephone].Nom = NomBox.Text;
                    }
                    if(PrenomBox.Text.Equals("") == false && PrenomBox.Text.Equals(modif.Prenom) == false)
                    {
                        this.pizzeria.Clientele[modif.Telephone].Prenom = PrenomBox.Text;
                    }
                    if(AdresseBox.Text.Equals("") == false && AdresseBox.Text.Equals(modif.Adresse) == false)
                    {
                        this.pizzeria.Clientele[modif.Telephone].Adresse = AdresseBox.Text;
                    }
                    if(PhoneBox.Text.Equals("") == false  && PhoneBox.Text.Count()==14 &&PhoneBox.Text.Equals(modif.Telephone)==false)
                    {
                        Client nouveau = new Client(this.pizzeria.Clientele[modif.Telephone].Nom, this.pizzeria.Clientele[modif.Telephone].Prenom, this.pizzeria.Clientele[modif.Telephone].Adresse, PhoneBox.Text, this.pizzeria.Clientele[modif.Telephone].Premiere_commande);
                        this.pizzeria.Clientele.Add(nouveau.Telephone, nouveau);
                        foreach (KeyValuePair<string, Commande> c in this.pizzeria.Commandes)
                        {
                            if (c.Value.Telephone_client.Equals(modif.Telephone))
                            {
                                c.Value.Telephone_client = PhoneBox.Text;
                            }
                        }
                        this.pizzeria.Clientele.Remove(modif.Telephone);
                    }
                }
            }
            if (ComboBox_ClientSupp.SelectedItem != null)
            {
                if (this.pizzeria.Clientele.ContainsValue((Client)ComboBox_ClientSupp.SelectedItem))
                {
                    Client modif = (Client)ComboBox_ClientSupp.SelectedItem;
                    this.pizzeria.Clientele.Remove(modif.Telephone);
                }
            }
            MessageBox.Show("Mise à jour effectuée.");
            this.Close();
        }
        /// <summary>
        /// Bouton de validation des modifications faites sur un commis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderCommis(object sender, RoutedEventArgs e)
        {
            if (ComboBox_CommisModif.SelectedItem != null)
            {
                if (this.pizzeria.Effectifs_pizzeria.Employes_commis.Contains((Commis)ComboBox_CommisModif.SelectedItem))
                {
                    Commis modif = (Commis)ComboBox_CommisModif.SelectedItem;
                    if(ComboBox_CommisEtat.SelectedItem != null)
                    {
                        foreach(Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
                        {
                            if(c.Nom.Equals(modif.Nom))
                            {
                                c.Etat = (string)ComboBox_CommisEtat.SelectedItem;
                            }
                        }
                    }
                    if (NomBoxC.Text.Equals("") == false && NomBoxC.Text.Equals(modif.Nom) == false)
                    {
                        foreach (Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
                        {
                            if (c.Nom.Equals(modif.Nom))
                            {
                                c.Nom = NomBoxC.Text;
                                modif.Nom = c.Nom;
                            }
                        }
                    }
                    if (PrenomBoxC.Text.Equals("") == false && PrenomBoxC.Text.Equals(modif.Prenom) == false)
                    {
                        foreach (Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
                        {
                            if (c.Nom.Equals(modif.Nom))
                            {
                                c.Prenom = PrenomBoxC.Text;
                            }
                        }
                    }
                    if (AdresseBoxC.Text.Equals("") == false && AdresseBoxC.Text.Equals(modif.Adresse) == false)
                    {
                        foreach (Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
                        {
                            if (c.Nom.Equals(modif.Nom))
                            {
                                c.Adresse = AdresseBoxC.Text;
                            }
                        }
                    }
                    if (PhoneBoxC.Text.Equals("") == false && PhoneBoxC.Text.Count() == 14 && PhoneBoxC.Text.Equals(modif.Telephone) == false)
                    {
                        foreach (Commis c in this.pizzeria.Effectifs_pizzeria.Employes_commis)
                        {
                            if (c.Nom.Equals(modif.Nom))
                            {
                                c.Telephone = PhoneBoxC.Text;
                            }
                        }
                    }
                }
            }
            if (ComboBox_CommisSupp.SelectedItem != null)
            {
                if (this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Count > 1)
                {
                    if (this.pizzeria.Effectifs_pizzeria.Employes_commis.Contains((Commis)ComboBox_CommisSupp.SelectedItem))
                    {
                        this.pizzeria.Effectifs_pizzeria.Employes_commis.Remove((Commis)ComboBox_CommisSupp.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Il ne reste plus qu'un commis, impossible de le supprimer.");
                }
                
            }
            MessageBox.Show("Mise à jour effectuée.");
            this.Close();
        }
        /// <summary>
        /// Bouton de validation des modifications faites sur un livreur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValiderLivreur(object sender, RoutedEventArgs e)
        {
            if (ComboBox_LivreurModif.SelectedItem != null)
            {
                if (this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Contains((Livreur)ComboBox_LivreurModif.SelectedItem))
                {
                    Livreur modif = (Livreur)ComboBox_LivreurModif.SelectedItem;
                    if (ComboBox_LivreurEtat.SelectedItem != null)
                    {
                        foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                        {
                            if (l.Nom.Equals(modif.Nom))
                            {
                                l.Etat = (string)ComboBox_LivreurEtat.SelectedItem;
                            }
                        }
                    }
                    if (NomBoxL.Text.Equals("") == false && NomBoxL.Text.Equals(modif.Nom) == false)
                    {
                        foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                        {
                            if (l.Nom.Equals(modif.Nom))
                            {
                                l.Nom = NomBoxL.Text;
                                modif.Nom = l.Nom;
                            }
                        }
                    }
                    if (PrenomBoxL.Text.Equals("") == false && PrenomBoxL.Text.Equals(modif.Prenom) == false)
                    {
                        foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                        {
                            if (l.Nom.Equals(modif.Nom))
                            {
                                l.Prenom = PrenomBoxL.Text;
                            }
                        }
                    }
                    if (AdresseBoxL.Text.Equals("") == false && AdresseBoxL.Text.Equals(modif.Adresse) == false)
                    {
                        foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                        {
                            if (l.Nom.Equals(modif.Nom))
                            {
                                l.Adresse = AdresseBoxL.Text;
                            }
                        }
                    }
                    if (PhoneBoxL.Text.Equals("") == false && PhoneBoxL.Text.Count() == 14 && PhoneBoxL.Text.Equals(modif.Telephone) == false)
                    {
                        foreach (Livreur l in this.pizzeria.Effectifs_pizzeria.Employes_livreurs)
                        {
                            if (l.Nom.Equals(modif.Nom))
                            {
                                l.Telephone = PhoneBoxL.Text;
                            }
                        }
                    }
                }
                
            }
            if (ComboBox_LivreurSupp.SelectedItem != null)
            {
                if(this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Count>1)
                {
                    if (this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Contains((Livreur)ComboBox_LivreurSupp.SelectedItem))
                    {
                        this.pizzeria.Effectifs_pizzeria.Employes_livreurs.Remove((Livreur)ComboBox_LivreurSupp.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Il ne reste plus qu'un livreur, impossible de le supprimer.");
                }
            }
            MessageBox.Show("Mise à jour effectuée.");
            this.Close();
        }
        /// <summary>
        /// ComboBox permettant de choisir l'etat que l'on souhaite assigner à un commis 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EtatCommis(object sender, RoutedEventArgs e)
        {
            List<string> commis = new List<string> { "sur place", "en conges" };
            var combo = sender as ComboBox;
            combo.ItemsSource = commis;
        }
        /// <summary>
        /// ComboBox permettant de choisir l'etat que l'on souhaite assigner à un livreur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EtatLivreur(object sender, RoutedEventArgs e)
        {
            List<string> livreur = new List<string> { "sur place", "en conges", "en livraison" };
            var combo = sender as ComboBox;
            combo.ItemsSource = livreur;
        }
    }
}
