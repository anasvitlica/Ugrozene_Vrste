using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Ugrozene_Vrste.Model;

namespace Ugrozene_Vrste
{
    /// <summary>
    /// Interaction logic for Pregled.xaml
    /// </summary>
    public partial class Pregled : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<Vrsta> Vrste
        {
            get;
            set;
        }
        public ObservableCollection<TipVrste> TipoviVrsta
        {
            get;
            set;
        }
        public ObservableCollection<Etiketa> Etikete
        {
            get;
            set;
        }

        public StackPanel trenutniPanel;

        public MainWindow ParentWindow;

        public Pregled(MainWindow par)
        {
            InitializeComponent();
            this.DataContext = this;

            ParentWindow = par;

            Vrste = new ObservableCollection<Vrsta>(SpisakVrsta.Vrste.Values);
            TipoviVrsta = new ObservableCollection<TipVrste>(SpisakTipovaVrste.TipoviVrste.Values);
            Etikete = new ObservableCollection<Etiketa>(SpisakEtiketa.Etikete.Values);
            trenutniPanel = pnlVrste;
        }

        public void popuniVrste()
        {
            this.Vrste = null;
            ObservableCollection<Vrsta> vrste = new ObservableCollection<Vrsta>(SpisakVrsta.Vrste.Values);
            this.Vrste = vrste;
            tabelaVrste.ItemsSource = Vrste;
        }

        public void popuniEtikete()
        {
            this.Etikete = null;
            ObservableCollection<Etiketa> etikete = new ObservableCollection<Etiketa>(SpisakEtiketa.Etikete.Values);
            this.Etikete = etikete;
            tabelaEtikete.ItemsSource = Etikete;
        }

        public void popuniTipoveVrsta()
        {
            this.TipoviVrsta = null;
            ObservableCollection<TipVrste> tipoviVrsta = new ObservableCollection<TipVrste>(SpisakTipovaVrste.TipoviVrste.Values);
            this.TipoviVrsta = tipoviVrsta;
            tabelaTipoviVrsta.ItemsSource = TipoviVrsta;
        }

        public void dodajVrstu(Vrsta v) {
            Vrste.Add(v);
        }

        public void dodajEtiketu(Etiketa e)
        {
            Etikete.Add(e);
        }

        public void dodajTipVrste(TipVrste tv)
        {
            TipoviVrsta.Add(tv);
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.SetVrsteItems();
            this.Close();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Begin dragging the window
            this.DragMove();
        }

        private void Vrste_Click(object sender, RoutedEventArgs e)
        {
            trenutniPanel.Visibility = Visibility.Hidden;
            trenutniPanel = pnlVrste;
            pnlVrste.Visibility = Visibility.Visible;
           
        }

        private void TipoviVrsta_Click(object sender, RoutedEventArgs e)
        {
            trenutniPanel.Visibility = Visibility.Hidden;
            trenutniPanel = pnlTipoviVrsta;
            pnlTipoviVrsta.Visibility = Visibility.Visible;
        }

        private void Etikete_Click(object sender, RoutedEventArgs e)
        {
            trenutniPanel.Visibility = Visibility.Hidden;
            trenutniPanel = pnlEtikete;
            pnlEtikete.Visibility = Visibility.Visible;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if(trenutniPanel == pnlVrste)
            {
                RadSaVrstom rsv = new RadSaVrstom(this, false, null);
                rsv.ShowDialog();
            }
            else if(trenutniPanel == pnlEtikete)
            {
                RadSaEtiketom rse = new RadSaEtiketom(this, false, null);
                rse.ShowDialog();
            }
            else if(trenutniPanel == pnlTipoviVrsta)
            {
                RadSaTipomVrste rstv = new RadSaTipomVrste(this, false, null);
                rstv.ShowDialog();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {

            if(trenutniPanel == pnlVrste)
            {
                String selektovanID = ((Vrsta)tabelaVrste.SelectedItem).ID ;
                SpisakVrsta.Vrste.Remove(selektovanID);
                popuniVrste();
            }
            else if(trenutniPanel == pnlEtikete)
            {
                String selektovanaID = ((Etiketa)tabelaEtikete.SelectedItem).ID;

                String message = "Da li ste sigurni da želite da obrišete etiketu čiji je ID " + selektovanaID + "?\n\n"
                    + "Njenim brisanjem ukloniće se i iz liste etiketa vrste kojoj pripada.";
                MessageBoxResult mbr = MessageBox.Show(message,"Brisanje", MessageBoxButton.YesNo);

                if (mbr == MessageBoxResult.Yes)
                {
                    SpisakEtiketa.Etikete.Remove(selektovanaID);


                    //kad se obrise iz tabele, brise se i iz liste etiketa kod vrste
                    foreach (KeyValuePair<String, Vrsta> pair in SpisakVrsta.Vrste)
                    {
                        foreach (Etiketa etiketa in pair.Value.Etikete)
                        {
                            if (etiketa.ID == selektovanaID)
                            {
                                pair.Value.Etikete.Remove(etiketa);
                                break;
                            }
                        }
                    }

                    popuniEtikete();
                }
                else
                {
                    return;
                }
                
            }
            else if(trenutniPanel == pnlTipoviVrsta)
            {
                String selektovanID = ((TipVrste)tabelaTipoviVrsta.SelectedItem).ID;
                SpisakTipovaVrste.TipoviVrste.Remove(selektovanID);
                popuniTipoveVrsta();
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (trenutniPanel == pnlVrste)
            {
                RadSaVrstom rsv = new RadSaVrstom(this, true, (Vrsta)tabelaVrste.SelectedItem);
                rsv.ShowDialog();
            }
            else if( trenutniPanel == pnlEtikete)
            {
                RadSaEtiketom rse = new RadSaEtiketom(this, true, (Etiketa)tabelaEtikete.SelectedItem);
                rse.ShowDialog();
            }
            else if(trenutniPanel == pnlTipoviVrsta)
            {
                RadSaTipomVrste rstv = new RadSaTipomVrste(this, true, (TipVrste)tabelaTipoviVrsta.SelectedItem);
                rstv.ShowDialog();
            }
        }

        //za sakrivanje kolona koje ne zelim da se vide u tabeli
        private void TabelaVrste_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Etikete")
            {
                e.Column = null;
            }
        }

        private void TabelaEtikete_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "BojaBrush")
            {
                e.Column = null;
            }
            else if(e.PropertyName == "Boja")
            {
                Style cellStyle = new Style(typeof(DataGridCell));
                cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty,new Binding("BojaBrush")));
                cellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, new SolidColorBrush(Colors.Transparent)));
                e.Column.CellStyle = cellStyle;
            }

        }
    }
}
