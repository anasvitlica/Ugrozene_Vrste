using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Ugrozene_Vrste.Model
{
    [Serializable]
    class SpisakVrsta
    {
        //Singleton
        private static Dictionary<string, Vrsta> vrste = null;
        public static Dictionary<string, Vrsta> Vrste
        {
            get
            {
                if (vrste == null)
                {
                    vrste = new Dictionary<string, Vrsta>();
                    vrste.Add("123", new Vrsta("123", "Polarni medved", "Beli", "", true, true, false, "", 50000, "",
                        new BitmapImage(new Uri("images/polarnimedved.png", UriKind.Relative)), "Sisar"));
                    vrste.Add("456", new Vrsta("456", "Afrički slon", "Veliki", "", true, true, false, "", 70000, "",
                        new BitmapImage(new Uri("images/AfricanElephant1.png", UriKind.Relative)), "Sisar"));
                    vrste.Add("789", new Vrsta("789", "Tigar", "Straftasti", "", true, true, false, "", 100000, "",
                        new BitmapImage(new Uri("images/tiger1.png", UriKind.Relative)), "Sisar"));

                }
                return vrste;
            }
            set
            {
                if (value != vrste)
                {
                    vrste = value;
                }
            }
        }

        //Konstruktor
        public SpisakVrsta() { }

    }
}
