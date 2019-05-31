using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ugrozene_Vrste.Model;

namespace Ugrozene_Vrste
{
    /// <summary>
    /// Interaction logic for DodavanjeTipaVrste.xaml
    /// </summary>
    public partial class RadSaTipomVrste : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private string ime;
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        private string opis;
        public string Opis
        {
            get
            {
                return opis;
            }
            set
            {
                if (value != opis)
                {
                    opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }
        private ImageSource ikonica;
        public ImageSource IkonicaP
        {
            get
            {
                return ikonica;
            }
            set
            {
                if (value != ikonica)
                {
                    ikonica = value;
                    ikonica = value;
                    OnPropertyChanged("Ikonica");
                }
            }
        }

        public bool Editing { get; set; }
        public TipVrste Selektovan { get; set; }
        public Window ParentWindow { get; set; }

        public RadSaTipomVrste(Window parent, bool edit, TipVrste sel)
        {
            InitializeComponent();
            this.DataContext = this;

            ParentWindow = parent;
            this.Owner = parent;
            Editing = edit;
            Selektovan = sel;

            if (Editing)
            {
                lblNaslov.Text = "Izmena tipa vrste";
                popuniPolja();
                txtID.IsEnabled = false;
            }
        }

        public void popuniPolja()
        {
            this.ID = Selektovan.ID;
            this.Opis = Selektovan.Opis;
            this.Ime = Selektovan.Ime;
            this.IkonicaP = Selektovan.Ikonica;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Begin dragging the window
            this.DragMove();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
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
            }
        }

        private void Potvrdi_Click (object sender, RoutedEventArgs e)
        {
            if (ID == null || ID.Equals(""))
            {
                MessageBox.Show("Pogrešan unos! Pokušajte ponovo.", "Greška");
                return;
            }
            else if (SpisakTipovaVrste.TipoviVrste.ContainsKey(ID) && Editing == false)
            {
                MessageBox.Show("ID već postoji!", "Pogrešan ID");
                return;
            }


            if(Editing == true)
            {
                //SpisakVrsta.Vrste[stariID].Ime = Ime;
                SpisakTipovaVrste.TipoviVrste[ID].Ime = Ime;
                SpisakTipovaVrste.TipoviVrste[ID].Opis = Opis;
                SpisakTipovaVrste.TipoviVrste[ID].Ikonica = IkonicaP;

            }
            else
            {
                SpisakTipovaVrste.TipoviVrste.Add(ID, new TipVrste(ID, Ime, Opis, IkonicaP));
            }

            if(ParentWindow is Pregled)
            {
                Pregled parentWindow = (Pregled)Owner;
                parentWindow.dodajTipVrste(new TipVrste(ID, Ime, Opis, IkonicaP));
            }

            this.Close();
        }
    }
}
