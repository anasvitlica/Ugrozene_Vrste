﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ugrozene_Vrste.Model;

namespace Ugrozene_Vrste
{
    /// <summary>
    /// Interaction logic for DodavanjeEtikete.xaml
    /// </summary>
    public partial class RadSaEtiketom : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        
        public ObservableCollection<String> Boje { get; set; }
        
        private string IDVrste { get; set; }

        private string id;
        private Color boja;
        private string opis;
        
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
        public Color Boja
        {
            get
            {
                return boja;
            }
            set
            {
                if (value != boja)
                {
                    boja = value;
                    OnPropertyChanged("Boja");
                }
            }
        }
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

        public bool Editing { get; set; }
        public Etiketa Selektovana { get; set; }
        public Window ParentWindow { get; set; }
        

        public RadSaEtiketom(Window parent, bool edit, Etiketa sel)
        {
            InitializeComponent();
            this.DataContext = this;

            this.ParentWindow = parent;
            this.Owner = parent;
            this.Editing = edit;
            this.Selektovana = sel;

            
           
            if (Editing)
            {
                lblNaslov.Text = "Izmena etikete";
                txtID.IsEnabled = false;
                popuniPolja();
            }

        }

        public void popuniPolja()
        {
            this.ID = Selektovana.ID;
            Brush bojaBrush = Selektovana.BojaBrush;
            Color bojaColor = (Color)ColorConverter.ConvertFromString(bojaBrush.ToString());
           this.Boja = bojaColor;
            this.Opis = Selektovana.Opis;
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
        

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

            if (Editing)
            {
                SpisakEtiketa.Etikete[Selektovana.ID].Boja = Boja;
                SpisakEtiketa.Etikete[Selektovana.ID].Opis = Opis;
            }
            else
            {
                if (SpisakEtiketa.Etikete.ContainsKey(ID))
                {
                    MessageBox.Show("ID već postoji!", "Pogrešan ID");
                    return;
                }
                if (ParentWindow is Pregled)
                {
                    Pregled p = (Pregled)Owner;
                    SolidColorBrush b = new SolidColorBrush((Color)cp.SelectedColor);
                    p.dodajEtiketu(new Etiketa(ID, (Color)cp.SelectedColor, Opis,b));

                }
                else if (ParentWindow is RadSaVrstom)
                {
                    RadSaVrstom v = (RadSaVrstom)Owner;
                    SolidColorBrush b = new SolidColorBrush((Color)cp.SelectedColor);
                    v.dodajEtiketu(new Etiketa(ID, (Color)cp.SelectedColor, Opis,b));
                    //v.Selektovan.Etikete.Add(new Etiketa(ID, (Color)cp.SelectedColor, Opis, b));
                }
                Color boja = (Color)cp.SelectedColor;
                SolidColorBrush cb = new SolidColorBrush((Color)cp.SelectedColor);
                SpisakEtiketa.Etikete.Add(ID, new Etiketa(ID, boja, Opis,cb));
                
            }
            
            Close();
        }

        private void Cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Boja = (Color)cp.SelectedColor;
        }
    }

    
}
