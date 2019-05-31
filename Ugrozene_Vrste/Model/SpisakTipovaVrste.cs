using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ugrozene_Vrste.Model
{
    [Serializable]
    class SpisakTipovaVrste
    {
        
        private static Dictionary<string, TipVrste> tipoviVrste = null;
        public static Dictionary<string, TipVrste> TipoviVrste
        {
            get
            {
                if (tipoviVrste == null)
                {
                    tipoviVrste = new Dictionary<string, TipVrste>();
                    tipoviVrste.Add("123", new TipVrste("123", "Ptica", "Leti",
                        new BitmapImage(new Uri("images/bird.png", UriKind.Relative))));
                    tipoviVrste.Add("456", new TipVrste("456", "Sisar", "...",
                         new BitmapImage(new Uri("images/cat.png", UriKind.Relative))));
                    tipoviVrste.Add("789", new TipVrste("789", "Riba", "Pliva",
                         new BitmapImage(new Uri("images/fish.png", UriKind.Relative))));
                    tipoviVrste.Add("159", new TipVrste("159", "Vodozemac", "...",
                         new BitmapImage(new Uri("images/frog.png", UriKind.Relative))));
                    tipoviVrste.Add("753", new TipVrste("753", "Gmizavac", "...",
                         new BitmapImage(new Uri("images/snake.png", UriKind.Relative))));
                }
                return tipoviVrste;
            }
            set
            {
                if (value != tipoviVrste)
                    tipoviVrste = value;
            }
        }

    }
}
