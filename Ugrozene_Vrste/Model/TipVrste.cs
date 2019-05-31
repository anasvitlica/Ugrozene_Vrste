using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ugrozene_Vrste.Model
{
    [Serializable]
    public class TipVrste : INotifyPropertyChanged
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
        private ImageSource _ikonica;
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

        public TipVrste(string _id, string _ime, string _opis, ImageSource _ikonica)
        {
            ID = _id;
            Ime = _ime;
            Opis = _opis;
            Ikonica = _ikonica;
        }

        public override string ToString()
        {
            //(Ikonica as BitmapImage).UriSource;
            return this.Ime + ";" +
                   this.Opis + ";" +
                   this.Ikonica;
        }

    }
}
