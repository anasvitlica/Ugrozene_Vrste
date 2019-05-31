using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Ugrozene_Vrste.Model
{
    [Serializable]
    public class Vrsta : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Point _point;
        private string _id;
        private string _ime;
        private string _opis;
        private string _statusUgrozenosti;
        private ImageSource _ikonica;
        private bool _opasnaZaLjude;
        private bool _IUCNLista;
        private bool _ziviUNaseljenomRegionu;
        private string _turistickiStatus;
        private double _prihod;
        private String _tip { get; set; }
        private string _datum { get; set; }
        private List<Etiketa> _etikete { get; set; }


        public List<Etiketa> Etikete
        {
            get
            {
                return _etikete;
            }
            set
            {
                _etikete = value;
            }
        }
        
        public Point Point
        {
            get
            {
                return _point;
            }
            set
            {
                if (value != _point)
                {
                    _point = value;
                    OnPropertyChanged("Point");
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
        public String Tip
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
        public bool ZiviUNaseljenomRegionu
        {
            get
            {
                return _ziviUNaseljenomRegionu;
            }
            set
            {
                if (value != _ziviUNaseljenomRegionu)
                {
                    _ziviUNaseljenomRegionu = value;
                    OnPropertyChanged("Zivi u naseljenom regionu");
                }
            }
        }
        public bool IUCNLista
        {
            get
            {
                return _IUCNLista;
            }
            set
            {
                if (value != _IUCNLista)
                {
                    _IUCNLista = value;
                    OnPropertyChanged("IUCN Lista");
                }
            }
        }
        public bool OpasnaZaLjude
        {
            get
            {
                return _opasnaZaLjude;
            }
            set
            {
                if (value != _opasnaZaLjude)
                {
                    _opasnaZaLjude = value;
                    OnPropertyChanged("Opasna za ljude");
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
        public double Prihod
        {
            get
            {
                return _prihod;
            }
            set
            {
                if (value != _prihod)
                {
                    _prihod = value;
                    OnPropertyChanged("Prihod");
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
        public string StatusUgrozenosti
        {
            get
            {
                return _statusUgrozenosti;
            }
            set
            {
                if (value != _statusUgrozenosti)
                {
                    _statusUgrozenosti = value;
                    OnPropertyChanged("Status ugroženosti");
                }
            }
        }
        public string TuristickiStatus
        {
            get
            {
                return _turistickiStatus;
            }
            set
            {
                if (value != _turistickiStatus)
                {
                    _turistickiStatus = value;
                    OnPropertyChanged("Turistički status");
                }
            }
        }
        public ImageSource Ikonica
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



        public Vrsta(string id,
                     string ime,
                     string opis,
                     string statusUgrozenosti,
                     bool opasna,
                     bool lista,
                     bool naseljen,
                     string turistickiStatus,
                     double prihod,
                     string datum,
                     ImageSource ikonica,
                     string tip)
        {
            ID = id;
            Ime = ime;
            Opis = opis;
            StatusUgrozenosti = statusUgrozenosti;
            OpasnaZaLjude = opasna;
            IUCNLista = lista;
            ZiviUNaseljenomRegionu = naseljen;
            TuristickiStatus = turistickiStatus;
            Prihod = prihod;
            Datum = datum;
            Ikonica = ikonica;
            Tip = tip;
            Etikete = new List<Etiketa>();
        }

        public Vrsta(string id, string ime)
        {
            Etikete = new List<Etiketa>();
            ID = id;
            Ime = ime;
        }

        public override string ToString()
        {
            //(Ikonica as BitmapImage).UriSource
            return this.Ime + ";" +
                   this.Opis + ";" +
                   this.StatusUgrozenosti + ";" +
                   this.OpasnaZaLjude + ";" +
                   this.IUCNLista + ";" +
                   this.ZiviUNaseljenomRegionu + ";" +
                   this.TuristickiStatus + ";" +
                   this.Prihod + ";" +
                   this.Datum + ";" +
                   this.Ikonica + ";" +
                   this.Tip;
        }


    }


}

