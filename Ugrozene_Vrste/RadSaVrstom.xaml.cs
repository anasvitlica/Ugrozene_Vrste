using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ugrozene_Vrste.Model;

namespace Ugrozene_Vrste
{
    /// <summary>
    /// Interaction logic for DodavanjeVrste.xaml
    /// </summary>
    public partial class RadSaVrstom : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        

        public bool Editing { get; set; }
        public Vrsta Selektovan { get; set; }

        public ObservableCollection<String> StatusUgrozenosti { get; set; }
        public ObservableCollection<String> TuristickiStatus { get; set; }
        public ObservableCollection<string> TipoviVrsteString { get; set; }
        public ObservableCollection<Etiketa> Etikete { get; set; }
        public ObservableCollection<Etiketa> SveEtikete { get; set; }

        public Window ParWindow { get; set; }

        //polja
        private string _id;
        private string _ime;
        private string _opis;
        private string _tip;
        private string _status;
        private bool _opasna;
        private bool _lista;
        private bool _naseljena;
        private string _turisticki;
        private double _prihod;
        private string _datum;
        private ImageSource _ikonica;

        public double Prihod
        {
            get
            {
                return _prihod;
            }
            set
            {
                if(value != _prihod)
                {
                    _prihod = value;
                    OnPropertyChanged("Prihod");
                }
            }
        }
        public string Ime
        {
            get
            {
                return _ime;
            }
            set
            {
                if (value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string Opis
        {
            get
            {
                return _opis;
            }
            set
            {
                if (value != _opis)
                {
                    _opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }
        public string Tip
        {
            get
            {
                return _tip;
            }
            set
            {
                if (value != _tip)
                {
                    _tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        public bool Opasna
        {
            get
            {
                return _opasna;
            }
            set
            {
                if (value != _opasna)
                {
                    _opasna = value;
                    OnPropertyChanged("Opasna");
                }
            }
        }
        public bool Lista
        {
            get
            {
                return _lista;
            }
            set
            {
                if (value != _lista)
                {
                    _lista = value;
                    OnPropertyChanged("Lista");
                }
            }
        }
        public bool Naseljena
        {
            get
            {
                return _naseljena;
            }
            set
            {
                if (value != _naseljena)
                {
                    _naseljena = value;
                    OnPropertyChanged("Naseljena");
                }
            }
        }
        public string Turisticki
        {
            get
            {
                return _turisticki;
            }
            set
            {
                if (value != _turisticki)
                {
                    _turisticki = value;
                    OnPropertyChanged("Turisticki");
                }
            }
        }
        public string Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                if (value != _datum)
                {
                    _datum = value;
                    OnPropertyChanged("Datum");
                }
            }
        }
        public ImageSource IkonicaP
        {
            get
            {
                return _ikonica;
            }
            set
            {
                if (value != _ikonica)
                {
                    _ikonica = value;
                    OnPropertyChanged("Ikonica");
                }
            }
        }       


        public RadSaVrstom(Window parent, bool edit, Vrsta sel)
        {
            InitializeComponent();
            this.DataContext = this;

            this.Editing = edit;
            this.Selektovan = sel;

            ParWindow = parent;
            this.Owner = parent;
            
            
            TipoviVrsteString = new ObservableCollection<string>();
            foreach (TipVrste tip in SpisakTipovaVrste.TipoviVrste.Values)
            {
                TipoviVrsteString.Add(tip.Ime);
            }

            if(TipoviVrsteString.Count > 0)
            {
                Tip = TipoviVrsteString[0];
            }
            

            StatusUgrozenosti = new ObservableCollection<string>();
            StatusUgrozenosti.Add("Kritično ugrožena");
            StatusUgrozenosti.Add("Ugrožena");
            StatusUgrozenosti.Add("Ranjiva");
            StatusUgrozenosti.Add("Zavisna od očuvanja staništa");
            StatusUgrozenosti.Add("Blizu rizika");
            StatusUgrozenosti.Add("Najmanjeg rizika");
            Status = StatusUgrozenosti[0];

            TuristickiStatus = new ObservableCollection<string>();
            TuristickiStatus.Add("Izolovana");
            TuristickiStatus.Add("Delimično habituirana");
            TuristickiStatus.Add("Habituirana");
            Turisticki = TuristickiStatus[0];

            SveEtikete = new ObservableCollection<Etiketa>(SpisakEtiketa.Etikete.Values);
            Etikete = new ObservableCollection<Etiketa>();
            if (Editing)
            {
                lblNaslov.Text = "Izmena vrste";
                popuniPolja();
                txtID.IsEnabled = false;        //ID se ne moze menjati
                NapuniEtikete();
            }
            
            
        }

        
        private void NapuniEtikete()
        {
            Etikete = null;
            Etikete = new ObservableCollection<Etiketa>();
            foreach (Etiketa etiketa in Selektovan.Etikete)
            {
                Etikete.Add(etiketa);
            }
             listaEtiketa.ItemsSource = Etikete;

            SveEtikete = null;
            SveEtikete = new ObservableCollection<Etiketa>(SpisakEtiketa.Etikete.Values);
            foreach (Etiketa e in Etikete)
            {
                if (SveEtikete.Contains(e))     //?? Ne radi ??
                {
                    SveEtikete.Remove(e);
                }
                else
                    continue;
            }
            listaSvihEtiketa.ItemsSource = SveEtikete;
            
        }

        public void dodajEtiketu(Etiketa e)
        {
            Etikete.Add(e);
            listaEtiketa.ItemsSource = Etikete;
        }

        private void popuniPolja()
        {
            foreach (string s in StatusUgrozenosti)
            {
                if (s.Equals(Selektovan.StatusUgrozenosti))
                {
                    Status = s;
                }
            }

            foreach (string s in TuristickiStatus)
            {
                if (s.Equals(Selektovan.TuristickiStatus))
                {
                    Turisticki = s;
                }
            }

            foreach (string s in TipoviVrsteString)
            {
                if (s.Equals(Selektovan.Tip))
                {
                    Tip = s;
                }
            }

            ID = Selektovan.ID;
            Ime = Selektovan.Ime;
            Opis = Selektovan.Opis;
            Prihod = Selektovan.Prihod;
            Opasna = Selektovan.OpasnaZaLjude;
            Lista = Selektovan.IUCNLista;
            Naseljena = Selektovan.ZiviUNaseljenomRegionu;
            Opis = Selektovan.Opis;
            IkonicaP = Selektovan.Ikonica;
            Datum = Selektovan.Datum;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Begin dragging the window
            this.DragMove();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (ID == null || ID.Equals("") || Ime == null || Ime.Equals(""))
            {
                MessageBox.Show("Popunite sva obavezna polja!", "Greška");
                return;
            } 
            else if (SpisakVrsta.Vrste.ContainsKey(ID) && Editing==false)
            {
                MessageBox.Show("ID već postoji!", "Pogrešan ID");
                return;
            }

            if (Editing == true)
            {
                SpisakVrsta.Vrste[Selektovan.ID].Ime = Ime;
                SpisakVrsta.Vrste[Selektovan.ID].Datum = Datum;
                SpisakVrsta.Vrste[Selektovan.ID].IUCNLista = Lista;
                SpisakVrsta.Vrste[Selektovan.ID].OpasnaZaLjude = Opasna;
                SpisakVrsta.Vrste[Selektovan.ID].Opis = Opis;
                SpisakVrsta.Vrste[Selektovan.ID].Prihod = Prihod;
                SpisakVrsta.Vrste[Selektovan.ID].StatusUgrozenosti = Status;
                SpisakVrsta.Vrste[Selektovan.ID].Tip = Tip;
                SpisakVrsta.Vrste[Selektovan.ID].TuristickiStatus = Turisticki;
                SpisakVrsta.Vrste[Selektovan.ID].ZiviUNaseljenomRegionu = Naseljena;
                SpisakVrsta.Vrste[Selektovan.ID].Ikonica = IkonicaP;
                SpisakVrsta.Vrste[Selektovan.ID].Etikete = null;
                SpisakVrsta.Vrste[Selektovan.ID].Etikete = new List<Etiketa>();
                foreach (Etiketa etiketa in this.Etikete)
                {
                    SpisakVrsta.Vrste[Selektovan.ID].Etikete.Add(etiketa);
                }
            }
            else
            {
                //ako nije uneta posebna ikonica, uzima se ona od tipa vrste
                if (IkonicaP == null)
                {
                    string idTipa = "";
                    foreach (KeyValuePair<string, TipVrste> pair in SpisakTipovaVrste.TipoviVrste)
                    {
                        if (pair.Value.Ime.Equals(Tip))
                        {
                            idTipa = pair.Key;
                            break;
                        }
                    }
                    IkonicaP = SpisakTipovaVrste.TipoviVrste[idTipa].Ikonica;
                }
                    

                    //neobavezna polja
                    if (Opis == null)
                    {
                        Opis = "";
                    }

                Vrsta novaVrsta = new Vrsta(ID, Ime, Opis, Status, Opasna, Lista, Naseljena,
                Turisticki, Prihod, Datum, IkonicaP, Tip);
                novaVrsta.Etikete = new List<Etiketa>();
                foreach (Etiketa etiketa in this.Etikete)
                {
                    novaVrsta.Etikete.Add(etiketa);
                }
                    SpisakVrsta.Vrste.Add(ID,novaVrsta);
                }
                
            

            //Refresh liste u parent prozoru
            if (ParWindow is MainWindow)
            {
                MainWindow pw = (MainWindow)Owner;
                pw.setVrsteItems();
                //(ParWindow as MainWindow).setVrsteItems();
            } else if(ParWindow is Pregled)
            {
                Pregled parentWindow = (Pregled)Owner;
                parentWindow.dodajVrstu(new Vrsta(ID,Ime,Opis,Status,Opasna,Lista,Naseljena,Turisticki,Prihod,Datum,IkonicaP,Tip));
            }

            Selektovan = null;
            Etikete = null;

            Close();
        }

        private void Ikonica_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = "File", 
                DefaultExt = ".png", 
                Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
                "|PNG Portable Network Graphics (*.png)|*.png" +
                "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
                "|BMP Windows Bitmap (*.bmp)|*.bmp" +
                "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
                "|GIF Graphics Interchange Format (*.gif)|*.gif"
            };
            
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                txtIKONICA.Text = dlg.FileName;
                string url = dlg.FileName;
                Ikonica.Source = new BitmapImage(new Uri(url, UriKind.Absolute));   // za prikaz
                IkonicaP = new BitmapImage(new Uri(url, UriKind.Absolute));
            }
        }

        

        private void TxtTIP_GotFocus(object sender, RoutedEventArgs e)
        {
            borderTIP.BorderBrush = Brushes.Blue;
        }
        private void TxtTIP_LostFocus(object sender, RoutedEventArgs e)
        {
            borderTIP.BorderBrush = null;
        }
        private void TxtSTATUS_GotFocus(object sender, RoutedEventArgs e)
        {
            borderSTATUS.BorderBrush = Brushes.Blue;
        }
        private void TxtSTATUS_LostFocus(object sender, RoutedEventArgs e)
        {
            borderSTATUS.BorderBrush = null;
        }
        private void TxtTURISTICKI_GotFocus(object sender, RoutedEventArgs e)
        {
            borderTURISTICKI.BorderBrush = Brushes.Blue;
        }
        private void TxtTURISTICKI_LostFocus(object sender, RoutedEventArgs e)
        {
            borderTURISTICKI.BorderBrush = null;
        }
        private void ChcOPASNA_GotFocus(object sender, RoutedEventArgs e)
        {
            borderOPASNA.BorderBrush = Brushes.Blue;
        }
        private void ChcOPASNA_LostFocus(object sender, RoutedEventArgs e)
        {
            borderOPASNA.BorderBrush = null;
        }
        private void ChcLISTA_GotFocus(object sender, RoutedEventArgs e)
        {
            borderLISTA.BorderBrush = Brushes.Blue;
        }
        private void ChcLISTA_LostFocus(object sender, RoutedEventArgs e)
        {
            borderLISTA.BorderBrush = null;
        }
        private void ChcNASELJE_GotFocus(object sender, RoutedEventArgs e)
        {
            borderNASELJE.BorderBrush = Brushes.Blue;
        }
        private void ChcNASELJE_LostFocus(object sender, RoutedEventArgs e)
        {
            borderNASELJE.BorderBrush = null;
        }

        // Za datepicker nece
        private void DDATUM_GotFocus(object sender, RoutedEventArgs e)
        {
            borderDATUM.BorderBrush = Brushes.Blue;
        }
        private void DDATUM_LostFocus(object sender, RoutedEventArgs e)
        {
            borderDATUM.BorderBrush = null;
        }



        private void DodajEtiketu_Click(object sender, RoutedEventArgs e)
        {
            RadSaEtiketom re = new RadSaEtiketom(this,false,null);
            re.ShowDialog();
        }

        private void ObrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            Etiketa selektovana = (Etiketa)listaEtiketa.SelectedItem;

            if(selektovana == null)
            {
                MessageBox.Show("Označite iz liste etiketu koju želite da izbrišete!");
                return;
            }

            Etikete.Remove(selektovana);

            foreach (KeyValuePair<string,Etiketa> pair in SpisakEtiketa.Etikete)    //brisanje iz spiska etiketa
            {
                if (pair.Key.Equals(selektovana.ID))
                {
                    SpisakEtiketa.Etikete.Remove(pair.Key);
                    break;
                }
            }

            Selektovan.Etikete.Remove(selektovana);
            
            NapuniEtikete();
        }

        private void PrebaciEtiketu_Click(object sender, RoutedEventArgs e)
        {
            Etiketa selektovana = (Etiketa)listaSvihEtiketa.SelectedItem;
            SveEtikete.Remove(selektovana);
            Etikete.Add(selektovana);
        }
    }
}
