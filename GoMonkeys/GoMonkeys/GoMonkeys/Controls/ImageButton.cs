using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoMonkeys
{
    public class ImageButton : Image
    {
        readonly TapGestureRecognizer tap;

        public ImageButton()
        {
            tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            GestureRecognizers.Add(tap);
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            //var test = await CrossMedia.Current.PickPhotoAsync();
        }
    }
}
