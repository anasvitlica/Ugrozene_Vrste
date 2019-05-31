using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ugrozene_Vrste.Model
{
    [Serializable]
    class SpisakEtiketa
    {
        private static Dictionary<string, Etiketa> etikete = null;
        public static Dictionary<string, Etiketa> Etikete
        {
            get
            {
                if (etikete == null)
                {
                    //string _id, string _boja, string _opis
                    etikete = new Dictionary<string, Etiketa>();
                    Etikete.Add("123", new Etiketa("123",Colors.Red,"...",new SolidColorBrush(Colors.Red)));
                }
                return etikete;
            }
            set
            {
                if (value != etikete)
                    etikete = value;
            }
        }
    }
}
