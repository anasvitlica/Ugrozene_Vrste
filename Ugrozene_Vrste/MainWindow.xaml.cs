using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ugrozene_Vrste.Model;

namespace Ugrozene_Vrste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static MainWindow instance = null;
        public static MainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }

        public string FileName { get; set; }

        private ObservableCollection<Vrsta> naMapi;
        public ObservableCollection<Vrsta> NaMapi
        {
            get
            {
                return naMapi;
            }
            set
            {
                if (value != naMapi)
                {
                    naMapi = value;
                    OnPropertyChanged("NaMapi");
                }
            }
        }

        private ObservableCollection<Vrsta> Vrste { get; set; }
        Point startPoint = new Point();
        bool isDragging = false;
        private DragAndDropHandler DDHandler { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            NaMapi = new ObservableCollection<Vrsta>();
            DDHandler = new DragAndDropHandler(this);

            //punjenje liste
            SetVrsteItems();
        }

        #region Clicks
        private void SveVrste_Click(object sender, RoutedEventArgs e)
        {
            Pregled p = new Pregled(this);
            p.ShowDialog();
        }

        private void DodajVrstu_Click(object sender, RoutedEventArgs e)
        {
            RadSaVrstom dodavanjeVrste = new RadSaVrstom(this, false, null);
            dodavanjeVrste.ShowDialog();
        }

        private void DodajEtiketu_Click(object sender, RoutedEventArgs e)
        {
            RadSaEtiketom dodavanjeEtikete = new RadSaEtiketom(this, false, null);
            dodavanjeEtikete.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtPRETRAGA_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPRETRAGA.Text = string.Empty;
            txtPRETRAGA.GotFocus -= TxtPRETRAGA_GotFocus;
        }

        private void DodajTipVrste_Click(object sender, RoutedEventArgs e)
        {
            RadSaTipomVrste dtv = new RadSaTipomVrste(this, false, null);
            dtv.ShowDialog();
        }

        //Izmena vrsta
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Vrsta selektovanaVrsta = (Vrsta)lista.SelectedItem;

            if (selektovanaVrsta == null)
            {
                MessageBox.Show("Označite iz liste vrstu koju želite da izmenite!");
                return;
            }

            RadSaVrstom dv = new RadSaVrstom(this, true, selektovanaVrsta);     //true jer se edituje
            dv.ShowDialog();
        }


        //Brisanje vrsta
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Vrsta selektovanaVrsta = (Vrsta)lista.SelectedItem;

            if (selektovanaVrsta == null)
            {
                MessageBox.Show("Označite iz liste vrstu koju želite da izbrišete!");
                return;
            }

            SpisakVrsta.Vrste.Remove(selektovanaVrsta.ID);
            foreach (Etiketa etiketa in selektovanaVrsta.Etikete)
            {
                SpisakEtiketa.Etikete.Remove(etiketa.ID);
            }
            this.SetVrsteItems();
        }

        private void Lista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Vrsta selektovanaVrsta = (Vrsta)lista.SelectedItem;
            if (SpisakVrsta.Vrste.ContainsKey(selektovanaVrsta.ID))
            {
                RadSaVrstom rsv = new RadSaVrstom(this, true, selektovanaVrsta);
                rsv.ShowDialog();
            }

        }

        private void BrisanjeSvihVrsta_Click(object sender, RoutedEventArgs e)
        {
            String message = "Da li ste sigurni da želite da obrišete sve unete vrste?\n\n";
            MessageBoxResult mbr = MessageBox.Show(message, "Brisanje svih vrsta", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                SpisakVrsta.Vrste = null;
                SpisakVrsta.Vrste = new Dictionary<string, Vrsta>();
                SetVrsteItems();
            }
        }

        private void BrisanjeSvihTipovaVrste_Click(object sender, RoutedEventArgs e)
        {
            String message = "Da li ste sigurni da želite da obrišete sve unete tipove vrsta?\n\n";
            MessageBoxResult mbr = MessageBox.Show(message, "Brisanje svih tipova vrsta", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                SpisakTipovaVrste.TipoviVrste = null;
                SpisakTipovaVrste.TipoviVrste = new Dictionary<string, TipVrste>();
                SetVrsteItems();
            }
        }

        private void BrisanjeSvihEtiketa_Click(object sender, RoutedEventArgs e)
        {
            String message = "Da li ste sigurni da želite da obrišete sve unete etikete?\n\n";
            MessageBoxResult mbr = MessageBox.Show(message, "Brisanje svih etiketa", MessageBoxButton.YesNo);

            if (mbr == MessageBoxResult.Yes)
            {
                SpisakEtiketa.Etikete = null;
                SpisakEtiketa.Etikete = new Dictionary<string, Etiketa>();

                foreach (KeyValuePair<string, Vrsta> pair in SpisakVrsta.Vrste)
                {
                    pair.Value.Etikete = null;
                    pair.Value.Etikete = new List<Etiketa>();
                }
            }
        }

        #endregion

        //Metoda koja puni listu stringovima sa imenima vrsta
        public void SetVrsteItems()
        {
            this.lista.ItemsSource = null;
            Vrste = new ObservableCollection<Vrsta>(SpisakVrsta.Vrste.Values);
            this.lista.ItemsSource = Vrste;
        }

        #region Import i Export
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "unknown.txt";
            savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";



            if (savefile.ShowDialog() == true)
            {
                FileName = savefile.FileName;
                FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
                using (fs)
                {
                    using (TextWriter tw = new StreamWriter(fs))
                    {
                        Export_Vrste(tw, FileName);
                        Export_TipoviVrste(tw, FileName);
                        Export_Etikete(tw, FileName);
                    }
                }
            }

        }

        private void Export_Etikete(TextWriter tw, string file)
        {
            foreach (KeyValuePair<String, Etiketa> kvp in SpisakEtiketa.Etikete)
            {
                tw.WriteLine(string.Format("{0};{1}", "ETIKETA" + kvp.Key, kvp.Value));
            }
        }

        private void Export_TipoviVrste(TextWriter tw, string file)
        {
            foreach (KeyValuePair<string, TipVrste> kvp in SpisakTipovaVrste.TipoviVrste)
            {
                tw.WriteLine(string.Format("{0};{1}", "TIPVRSTE" + kvp.Key, kvp.Value));      // na ID za tipove vrste konkateniram "TIPVRSTE"
            }
        }

        private void Export_Vrste(TextWriter tw, string file)
        {
            foreach (KeyValuePair<string, Vrsta> kvp in SpisakVrsta.Vrste)
            {
                tw.WriteLine(string.Format("{0};{1}", "VRSTA" + kvp.Key, kvp.Value)); //na ID za vrste konkateniram "VRSTA"
                foreach (Etiketa etiketa in kvp.Value.Etikete)
                {
                    tw.WriteLine(string.Format("{0};{1}", kvp.Key + ",ETIKETA" + etiketa.ID, etiketa));
                    SpisakEtiketa.Etikete.Remove(etiketa.ID);
                }
            }
        }

        private void DodajNaMapu(Vrsta v)
        {
            if(v.Point.X != 0 && v.Point.Y != 0)
            {
                NaMapi.Add(v);
            }
        }

        private void Import(System.IO.StreamReader sr, string file)
        {

            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    string splitMe = sr.ReadLine();
                    string[] splitted = splitMe.Split(new char[] { ';' });

                    if (splitted.Length < 2)
                    {
                        continue;
                    }
                    else if (splitted.Length == 2)
                    {
                        SpisakVrsta.Vrste.Add(splitted[0].Trim(), new Vrsta(splitted[0].Trim(), splitted[1].Trim()));
                    }
                    else if (splitted.Length > 2)
                    {

                        string id = splitted[0].Trim();
                        if (id.StartsWith("VRSTA"))
                        {
                            id = id.Substring(5);
                            if (SpisakVrsta.Vrste.ContainsKey(id))
                            {
                                continue;
                            }

                            //?????????
                            string urlIkonice = splitted[11].Trim();
                            if (urlIkonice.StartsWith("component/"))
                            {
                                urlIkonice = urlIkonice.Substring(10);
                            }
                            ImageSource ikonica = new BitmapImage(new Uri(urlIkonice, UriKind.RelativeOrAbsolute));

                            Point p = new Point();
                            string pointStr = splitted[13];
                            string[] coordsStr = pointStr.Split(',');
                            p.X = double.Parse(coordsStr[0]);
                            p.Y = double.Parse(coordsStr[1]);

                            Vrsta ucitanaVrsta = new Vrsta(id,                          //id
                                                            splitted[1].Trim(),              //ime
                                                            splitted[2].Trim(),              //opis
                                                            splitted[3].Trim(),              //status ugrozenosti
                                                            Convert.ToBoolean(splitted[4].Trim()),    //opasna
                                                            Convert.ToBoolean(splitted[5].Trim()),    //lista
                                                            Convert.ToBoolean(splitted[6].Trim()),    //naseljen
                                                            splitted[7].Trim(),              //turisticki status
                                                            Double.Parse(splitted[8].Trim()),//prihod
                                                            splitted[9].Trim(),              //datum
                                                            ikonica,                        //ikonica
                                                            splitted[11].Trim(),            //tip
                                                            p);                            //koordinate na mapi       

                            SpisakVrsta.Vrste.Add(id, ucitanaVrsta);

                            DodajNaMapu(ucitanaVrsta);

                        }
                        else if (id.StartsWith("TIPVRSTE"))
                        {
                            id = id.Substring(8);
                            if (SpisakTipovaVrste.TipoviVrste.ContainsKey(id))
                            {
                                continue;
                            }
                            string urlIkonice = splitted[3].Trim();
                            if (urlIkonice.StartsWith("component/"))
                            {
                                urlIkonice = urlIkonice.Substring(10);
                            }
                            else if (urlIkonice.StartsWith("/Ugrozene_Vrste;component/"))
                            {
                                urlIkonice = urlIkonice.Substring(26);
                            }
                            else if (urlIkonice.StartsWith("pack://application:,,,/Ugrozene_Vrste;component/"))
                            {
                                urlIkonice = urlIkonice.Substring(48);
                            }
                            ImageSource ikonica = new BitmapImage(new Uri(urlIkonice, UriKind.Relative));

                            SpisakTipovaVrste.TipoviVrste.Add(id, new TipVrste(id,
                                                                               splitted[1].Trim(),  //ime
                                                                               splitted[2].Trim(),  //opis
                                                                               ikonica));
                        }
                        else if (id.StartsWith("ETIKETA"))
                        {
                            id = id.Substring(7);
                            if (SpisakEtiketa.Etikete.ContainsKey(id))
                            {
                                continue;
                            }
                            Color boja = (Color)ColorConverter.ConvertFromString(splitted[2]);
                            SolidColorBrush b = new SolidColorBrush(boja);
                            SpisakEtiketa.Etikete.Add(id, new Etiketa(id,
                                                                    boja,
                                                                    splitted[1].Trim(), //opis
                                                                    b));    //bojaBrush
                        }
                        else if (id.Contains("ETIKETA") && id.Length > 7)
                        {
                            string[] splitID = id.Split(',');
                            string idVrste = splitID[0];
                            string idEtikete = splitID[1].Substring(7);

                            Color boja = (Color)ColorConverter.ConvertFromString(splitted[2]);
                            SolidColorBrush b = new SolidColorBrush(boja);
                            Etiketa e = new Etiketa(idEtikete, boja, splitted[1].Trim(), b);
                            SpisakEtiketa.Etikete.Add(id, e);
                            SpisakVrsta.Vrste[idVrste].Etikete.Add(e);
                        }
                    }

                }
            }

            SetVrsteItems();

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = "File",
                DefaultExt = ".txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                System.IO.StreamReader sr;
                sr = new System.IO.StreamReader(dlg.FileName);
                SpisakVrsta.Vrste = null;
                SpisakVrsta.Vrste = new Dictionary<string, Vrsta>();

                SpisakTipovaVrste.TipoviVrste = null;
                SpisakTipovaVrste.TipoviVrste = new Dictionary<string, TipVrste>();

                SpisakEtiketa.Etikete = null;
                SpisakEtiketa.Etikete = new Dictionary<string, Etiketa>();

                Import(sr, dlg.FileName);

            }

        }

        #endregion

        #region Drag and Drop
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Lista_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Lista_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !isDragging)
            {
                Point position = e.GetPosition(null);
                if (Math.Abs(position.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(position.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(e);
                }
            }
        }

        private void StartDrag(MouseEventArgs e)
        {
            isDragging = true;
            Vrsta selectedItem = (Vrsta)lista.SelectedItem;
            if(selectedItem != null)
            {
                DataObject dragData = new DataObject("ugrozeniDrag", selectedItem);
                if (isDragging == true)
                {
                    DragDrop.DoDragDrop(lista, dragData, DragDropEffects.Move);
                }
            }
            
            isDragging = false;
        }

        private void Mapa_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("ugrozeniDrag"))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Mapa_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ugrozeniDrag"))
            {

                Vrsta vrsta = e.Data.GetData("ugrozeniDrag") as Vrsta;
                //Console.WriteLine("Dropovan " + vrsta.Ime);

                //Provera
                //lok.Point = e.GetPosition(Mapa);

                double newLeft = e.GetPosition(Mapa).X;
                // newLeft inside canvas right-border?
                if (newLeft > Mapa.Margin.Left + Mapa.ActualWidth - 30)
                {
                    newLeft = Mapa.Margin.Left + Mapa.ActualWidth - 30;  //30 je sirina slike
                }
                // newLeft inside canvas left-border?
                else if (newLeft < Mapa.Margin.Left)
                {
                    newLeft = Mapa.Margin.Left;
                }

                double newTop = e.GetPosition(Mapa).Y;

                // newTop inside canvas bottom-border?
                if (newTop > Mapa.Margin.Top + Mapa.ActualHeight - 30)
                {
                    newTop = Mapa.Margin.Top + Mapa.ActualHeight - 30; //30 je visina slike 
                }
                // newTop inside canvas top-border?
                else if (newTop < Mapa.Margin.Top)
                {
                    newTop = Mapa.Margin.Top;
                }

                vrsta.Point = new Point(newLeft, newTop);

                //provera za presek sa ostalim ikonicama na mapi
                foreach (Vrsta v in NaMapi)
                {
                    //da li se novi sece po x osi
                    if (Math.Abs(vrsta.Point.X - v.Point.X) < 30) //ako su u preseku
                    {
                        if (Mapa.ActualWidth - vrsta.Point.X < Mapa.ActualWidth - v.Point.X) //ako je prevlaceni blizi levo
                        {
                            vrsta.Point = new Point(newLeft - 40, newTop);
                        }
                        else if (Mapa.ActualWidth - vrsta.Point.X > Mapa.ActualWidth - v.Point.X) //ako je prevlaceni blizi desno
                        {
                            vrsta.Point = new Point(newLeft + 40, newTop);
                        }
                    }

                    //da li se novi sece po y osi
                    if (Math.Abs(vrsta.Point.Y - v.Point.Y) < 30) //ako su u preseku
                    {
                        if (Mapa.ActualHeight - vrsta.Point.Y < Mapa.ActualHeight - v.Point.Y) //ako je prevlaceni blizi gore 
                        {
                            vrsta.Point = new Point(vrsta.Point.X, newTop - 40);
                        }
                        else if (Mapa.ActualHeight - vrsta.Point.Y > Mapa.ActualHeight - v.Point.Y) //ako je prevlaceni blizi dole
                        {
                            vrsta.Point = new Point(vrsta.Point.X, newTop + 40);
                        }
                    }
                }

                if (NaMapi.Contains(vrsta) == false)
                {
                    NaMapi.Add(vrsta);
                }

                Console.WriteLine(NaMapi.Count);
                isDragging = false;
            }
        }


        private void Mapa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Mapa.SelectedItem is Vrsta)
            {
                Vrsta v = Mapa.SelectedItem as Vrsta;
                var w = new RadSaVrstom(this, true, v);
                w.ShowDialog();
            }
        }

        private void NaMapi_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !isDragging)
            {
                Point position = e.GetPosition(null);
                if (Math.Abs(position.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(position.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDragMap(e);
                }
            }
        }

        //funkcija koja zapravo pokrene drag sa mape
        private void StartDragMap(MouseEventArgs e)
        {

            if (Mapa.SelectedItem is Vrsta) //zbog null, ako je neko krenuo da vuce po mapi bezveze
            {
                isDragging = true;
                Vrsta selectedItem = (Vrsta)Mapa.SelectedItem;
                ListBoxItem lwi = (ListBoxItem)Mapa.ItemContainerGenerator.ContainerFromItem(selectedItem);
                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("ugrozeniDrag", selectedItem);
                if (isDragging == true)
                {
                    DragDrop.DoDragDrop(lwi, dragData, DragDropEffects.Move);
                }

                isDragging = false;
            }
        }

        private void ButtonBrisanje_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ugrozeniDrag"))
            {
                Vrsta vrsta = e.Data.GetData("ugrozeniDrag") as Vrsta;
                NaMapi.Remove(vrsta);
            }
        }

        #endregion


    }
}
